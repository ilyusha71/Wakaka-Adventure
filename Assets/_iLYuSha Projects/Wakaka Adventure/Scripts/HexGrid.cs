using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public delegate void FirstTryEventHandler<TEventArgs>(object sender, TEventArgs e) where TEventArgs : System.EventArgs;
public class FirstTryEventArgs : System.EventArgs
{
    public readonly HexGrid firstTry;
    public readonly int firstIndex;

    public FirstTryEventArgs(HexGrid firstTry, int firstIndex)
    {
        this.firstTry = firstTry;
        this.firstIndex = firstIndex;
    }
}

public class HexGrid : MonoBehaviour
{
    public HexGrid[] Neighbor;
    public int[] NeighborR;
    public List<int> NeighborIndex;
    [HideInInspector]
    public int indexGrid;
    public int gridInfo; // -99 = 寶藏，-1 = 陷阱
    [HideInInspector]
    public Image suspected;
    [HideInInspector]
    public Vector3 gridCoordinate;
    [HideInInspector]
    public Image gridImage;
    [HideInInspector]
    public PolygonCollider2D gridCollider;
    [HideInInspector]
    public CanvasGroup gridCanvas;
    [HideInInspector]
    public HexGridManager manager;
    public int indexTheme;
    public ThemeManager themeManager;
    public bool isExplored; // 判斷此區域是否已經探索
    public bool isDisappearing; // 判斷此區域是否正在消失
    public bool isNumerical; // 特殊圖樣數值化
    public bool isStable; // 陷阱不會清除周圍區域
    public bool isSuspected; // 疑似寶藏

    public event FirstTryEventHandler<FirstTryEventArgs> FirstTryEvent;
    bool buttonDown;

    private float alphaUnknown = 0.73f;
    private float alphaExplore = 0.84f;
    private float alphaExcavation = 0.93f;

    void Awake()
    {
        suspected= transform.GetChild(0).GetComponent<Image>();
        gridImage = GetComponent<Image>();
        gridCollider = GetComponent<PolygonCollider2D>();
        gridCollider.SetPath(0, HexagonMesh.vertex);
        gridCanvas = GetComponent<CanvasGroup>();
    }
    public void Reset()
    {
        suspected.enabled = false;
        gridImage.sprite = themeManager.iconUnknownTool.sprite;
        gridCollider.enabled = true;
        gridCanvas.alpha = alphaUnknown;
        gridInfo = 0;
        isExplored = false;
        isDisappearing = false;
        isSuspected = false;
    }
    void Update()
    {
        if (isDisappearing)
        {
            gridCanvas.alpha -= 0.47f * Time.deltaTime;
            if (gridCanvas.alpha < 0.0173f)
                Clear();
        }
    }
    void OnMouseDown()
    {
        buttonDown = true;
        if (!HexGridManager.firstTry)
            FirstTryEvent(this, new FirstTryEventArgs(this, indexGrid));
        else
            Working();
        buttonDown = false;
    }
    public void MissClickEvent() // 禁止觸發網格事件
    {
        gridCollider.enabled = false; // 關閉網格Collider
    }
    public void Working()
    {
        if (isExplored)
            return;
        switch (manager.adventureMode)
        {
            case AdventureMode.Explore: Explore(); break;
            case AdventureMode.Tool: UseTool(); break;
            case AdventureMode.Flag: PlaceFlag(); break;
            case AdventureMode.Doctor: Research(); break;
        }
    }
    public void Research()
    {
        if (!isExplored)
        {
            isExplored = true;
            isDisappearing = false;
            gridCanvas.alpha = alphaExplore;

            if (gridInfo == -99)
                IsTreasure();
            else if (gridInfo == -1)
                IsTrap();
            else if (gridInfo == 0)
                IsGeneral();
            else
                IsSpecial();
        }
    }
    public void Explore() // 探索（探險隊）
    {
        if (!isExplored)
        {
            isExplored = true;
            isDisappearing = false;
            gridCanvas.alpha = alphaExplore;

            if (gridInfo == -99 || gridInfo == -1)
                IsTrap();
            else if (gridInfo == 0)
                IsGeneral();
            else
                IsSpecial();
        }
    }
    void UseTool() // 使用工具（考古隊 or 無人機）
    {
        if (gridInfo == -99)
            IsTreasure();
        else if (gridInfo == -1)
            IsTrap();
        else
            IsUnknown();
        gridCanvas.alpha = alphaExcavation;
    }
    void PlaceFlag() // 放置旗標
    {
            gridImage.sprite= gridImage.sprite != themeManager.spriteGobi[20]? themeManager.spriteGobi[20]: themeManager.iconUnknownTool.sprite;
    }
    
    public void Disappear()
    {
        if (gridImage.sprite != themeManager.iconTitleTreasure.sprite)
        {
            isExplored = false;
            isDisappearing = true;
        }
    }
    void Clear()
    {
        if (gridImage.sprite != themeManager.spriteGobi[20])
            gridImage.sprite = themeManager.iconUnknownTool.sprite;
        gridCollider.enabled = true;
        gridCanvas.alpha = alphaUnknown;
        isExplored = false;
        isDisappearing = false;
    }

    /* 網格揭示 */
    void IsGeneral()
    {
        gridImage.sprite = themeManager.iconGeneralExplore.sprite;
        manager.ExploreNeigbor(gridCoordinate);
        if (buttonDown)
            manager.AudioPlay(2);
    }
    void IsSpecial()
    {
        if (!isNumerical) // 圖像化提示
            gridImage.sprite = themeManager.theme[indexTheme].specialGroup.sprite[gridInfo];
        else // 數字提示
            gridImage.sprite = themeManager.theme[indexTheme].specialGroup.sprite[gridInfo + 6];

        if (buttonDown)
            manager.AudioPlay(3);
    }
    void IsTrap()
    {
        gridImage.sprite = themeManager.iconTitleTrap.sprite;
        if (!isStable) // 清除周圍
        {
            Disappear();
            manager.ClearNeigbor(gridCoordinate);
        }
        manager.IntoTrap();
        if (buttonDown)
            manager.AudioPlay(1);
    }
    void IsUnknown()
    {
        gridImage.sprite = themeManager.iconUnknownTool.sprite;
    }
    void IsTreasure()
    {
        isExplored = true;
        isDisappearing = false;
        gridImage.sprite = themeManager.iconTitleTreasure.sprite;
        manager.GetTarget();
        if (buttonDown)
            manager.AudioPlay(0);
    }

    // Help Method
    public void Numerical()
    {
        isNumerical = !isNumerical;
        if (gridInfo >= 1 && gridInfo <= 6)
        {
            if (isExplored)
            {
                if (isNumerical)
                {
                    transform.localRotation = Quaternion.identity;
                    gridImage.sprite = themeManager.theme[indexTheme].specialGroup.sprite[gridInfo + 6];
                }
                else
                {
                    transform.localRotation *= Quaternion.Euler(0, 0, Random.Range(0, 6) * 60);
                    gridImage.sprite = themeManager.theme[indexTheme].specialGroup.sprite[gridInfo];
                }
            }
        }
    }
}
