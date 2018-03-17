using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;


public enum AdventureMode
{
    Doctor = -1,
    Explore = 0,
    Tool = 1,
    Flag = 2,
}
public class HexGridManager : MonoBehaviour
{
    [Header("Testing")]
    public bool showAll;
    [Header("主題與難度")]
    public int indexTheme;
    public Difficulty difficulty;
    public GameObject instruction; // 教學指示
    private AudioSource bgm;
    public AudioClip[] playBGM;
    [Header("六角網格建置")]
    public Transform gridPrefab;
    private Transform gridGruop;
    private bool rotateMap = false;
    public static bool firstTry = false;
    private int maxLayer = 7;
    private List<int> gridIndex; // 儲存尚未佈置的網格
    [Header("關卡設定")]
    public Sprite[] mark;
    public ThemeManager themeManager;
    private int layer = 5; // 網格階層
    private int countHexagonGrids = 1; // 網格數
    private int maxTrapGroup = 2;
    private int trap = 7; // 陷阱數
    private int target = 3; // 目標數
    private int opportunity = 10; // 容錯次數
    public float rpsMap = 0.0237f; // 地圖旋轉係數
    public int firstIndex;
    [Header("音效")]
    public AudioSource audioPlayer;
    public AudioClip[] clips;
    public AudioClip audioMission;
    [Header("即時資訊")]
    public Text textLevel;
    public Text textDifficultyEng;
    public Text textDifficultyCht;
    public Text textTimer;
    private int countTreasure;
    private bool clock;
    private float timer;
    [Header("冒險模式")]
    public AdventureMode adventureMode;
    public CanvasGroup adventureWarning;
    private int countOpportunity;
    public Text textOpportunity;
    public Toggle toggleDoctor;
    public Toggle toggleExolore;
    public Toggle toggleTool;
    public Image nightVision;
    [Header("結果顯示")]
    public GameObject result;
    public Toggle resultSuccess;
    public Toggle resultFail;
    public Image btnRestart;
    public Sprite[] iconRestart;
    public Transform restartArrow;
    [Header("外掛")]
    public GameObject[] tipsButton;
    public Toggle numeralization;
    public Toggle dontClear;
    public Toggle checkSuspected;
    public bool activePossible;
    private int countFail;

    void Awake()
    {
        bgm = transform.root.GetComponent<AudioSource>();
        gridGruop = this.transform;
        CreateHexagonalGrids();
        ChooseTheme(0);
        ChooseDifficulty(1);
    }
    // 建置六角網格
    void CreateHexagonalGrids()
    {
        HexagonMesh.grids = new HexGrid[HexagonMesh.CalculateHexGrids(maxLayer)];
        HexagonMesh.gridsIndex = new List<int>();
        for (int i = 0; i < HexagonMesh.grids.Length; i++)
        {
            HexagonMesh.gridsIndex.Add(i);
            HexagonMesh.grids[i] = Instantiate(gridPrefab).GetComponent<HexGrid>();
            HexagonMesh.grids[i].indexGrid = i;
            HexagonMesh.grids[i].name = "Hex Grid Button " + (i);
            HexagonMesh.grids[i].transform.SetParent(gridGruop);
            HexagonMesh.grids[i].transform.localScale = new Vector3(1, 1, 1);
            HexagonMesh.grids[i].gridCoordinate = new Vector3(1, 1, 1);
            HexagonMesh.grids[i].transform.localRotation *= Quaternion.Euler(0, 0, Random.Range(0, 6) * 60);
            HexagonMesh.grids[i].manager = this;
            HexagonMesh.grids[i].themeManager = this.themeManager;
            HexagonMesh.grids[i].FirstTryEvent += OnFirstTry;
        }
    }
    public void ChooseTheme(int indexTheme)
    {
        this.indexTheme = indexTheme;
        themeManager.BasicIcon();
        themeManager.BasicText();
        themeManager.SetTheme(indexTheme);
        for (int i = 0; i < HexagonMesh.grids.Length; i++)
        {
            HexagonMesh.grids[i].indexTheme = this.indexTheme;
            HexagonMesh.grids[i].Reset(); // 重置網格
            HexagonMesh.grids[i].MissClickEvent(); // 遊戲開始前禁止觸發點擊
        }
    }
    public void ChooseDifficulty(int level)
    {
        difficulty = (Difficulty)level;
        switch (difficulty)
        {
            case Difficulty.Doctor: layer = 4; trap = 5; target = 2; opportunity = 10;
                tipsButton[0].SetActive(true);
                tipsButton[1].SetActive(true);
                tipsButton[2].SetActive(true);
                toggleDoctor.gameObject.SetActive(true);
                toggleDoctor.isOn = true;
                adventureMode = AdventureMode.Doctor;
                break;
            case Difficulty.Newbie: layer = 4; trap = 5; target = 2; opportunity = 2; gridGruop.localScale = Vector3.one; break;
            case Difficulty.Trainee: layer = 5; trap = 8; target = 4; opportunity = 3; gridGruop.localScale = Vector3.one; break;
            case Difficulty.Elite: layer = 5; trap = 8; target = 5; opportunity = 4; gridGruop.localScale = Vector3.one; break;
            case Difficulty.Expert: layer = 5; trap = 8; target = 6; opportunity = 5; gridGruop.localScale = Vector3.one; break;
            case Difficulty.Master: layer = 6; trap = 12; target = 9; opportunity= 7; gridGruop.localScale = new Vector3(0.84f, 0.84f, 0.84f); break;
            case Difficulty.Crazy: layer = 6; trap = 13; target = 11; opportunity = 9; gridGruop.localScale = new Vector3(0.84f, 0.84f, 0.84f); break;
            case Difficulty.Wakaka: layer = 7; trap = 26; target = 11; opportunity = 11; gridGruop.localScale = new Vector3(0.7f, 0.7f, 0.7f); break;
        }
        textLevel.text = "Lv. " + level;
        textDifficultyEng.text = difficulty.ToString();
        textDifficultyCht.text = ((DifficultyCht)level).ToString();

        // 設置難度之後要重新排列網格
        countHexagonGrids = HexagonMesh.CalculateHexGrids(layer);
        rotateMap = false;
        gridGruop.localRotation = Quaternion.identity; // 排列過程中不要旋轉
        for (int i = 0; i < HexagonMesh.grids.Length; i++)
        {
            if (i < countHexagonGrids)
                ArrangeGrid(i);
            else
            {
                Vector3 gridCoordinate = new Vector3(999, 999, 999);
                HexagonMesh.grids[i].gridCoordinate = gridCoordinate;
                HexagonMesh.grids[i].transform.localPosition = new Vector3(7000, 7000, 7000);
            }
        }
        rotateMap = true;
    }
    void ArrangeGrid(int index)
    {
        int order = 0;
        for (int x = -(layer - 1); x <= (layer - 1); x++)
        {
            for (int y = -(layer - 1); y <= (layer - 1); y++)
            {
                for (int z = -(layer - 1); z <= (layer - 1); z++)
                {
                    if (x + y + z == 0)
                    {
                        // 滿足等式者為六角網格上的座標
                        Vector3 cood = new Vector3(x, y, z);
                        // 依序尋找下一個不重複的六角網格座標
                        if (cood != HexagonMesh.grids[order].gridCoordinate)
                        {
                            HexagonMesh.grids[index].gridCoordinate = cood;
                            HexagonMesh.grids[index].transform.localPosition = HexagonMesh.HexToCartesian(cood);
                            return;
                        }
                        order++;
                    }
                }
            }
        }
    }

    void Update()
    {
        if (timer > 180)
            tipsButton[2].SetActive(true);
        else if (timer > 120)
            tipsButton[1].SetActive(true);

        if (rotateMap)
            gridGruop.localRotation *= Quaternion.Euler(0, 0, rpsMap * 360 * Time.deltaTime);
        if(restartArrow.gameObject.activeSelf)
            restartArrow.localRotation *= Quaternion.Euler(0, 0, 0.7f * 360 * Time.deltaTime);

        if (clock && !instruction.activeSelf)
        {
            timer += Time.deltaTime;
            int min = (int)Mathf.Floor(timer / 60.0f);
            int sec = (int)Mathf.Floor(timer % 60.0f);
            textTimer.text = string.Format("{0:00}", min) + " : " + string.Format("{0:00}", sec) ;
        }
    }

    // 網格佈署
    void OnFirstTry(object sender, FirstTryEventArgs e)
    {
        adventureWarning.DOKill();
        adventureWarning.DOFade(0, 0.73f);
        timer = 0;
        clock = true;
        checkSuspected.isOn = false;
        activePossible = false;

        // 先將第一次觸發區偽裝成目標區，再進行部署，最後將其回歸
        firstIndex = e.firstIndex;
        Minelayer(); // 進行佈置
        Precalculate(); // 預計算
        firstTry = true;
        e.firstTry.Working();
    }
    void Minelayer()
    {
        gridIndex = new List<int>();
        for (int i = 0; i < countHexagonGrids; i++)
        {
            gridIndex.Add(HexagonMesh.gridsIndex[i]); // 複製網格索引，用於本張地圖佈置使用;
        }
        //HexagonMesh.gridsIndex.ForEach(index => gridIndex.Add(index)); 
        SafeArea();
        PlaceTreasure();
        PlaceTrap();
    }
    private void SafeArea()
    {
        gridIndex.Remove(firstIndex);
        HexagonMesh.grids[firstIndex].gridInfo = -999;
        List<int> safeIndex = HexagonMesh.FindNeighborIndex(firstIndex);
        for (int i = 0; i < safeIndex.Count; i++)
        {
            gridIndex.Remove(safeIndex[i]);
            HexagonMesh.grids[safeIndex[i]].gridInfo = -777;
        }
    }
    private void PlaceTreasure()
    {
        for (int i = 0; i < target; i++)
        {
            // Step.1 隨機設置寶藏位置，但是寶藏彼此不得相鄰
            List<int> placeTreasureIndex = new List<int>(); // 寶藏可佈置網格索引
            List<int> placeTrapIndex; // 寶藏鄰近陷阱可佈置網格索引
            List<int> treasureNeighborIndex; // 寶藏鄰近網格索引
            bool placeAgain; // 不滿足條件
            int placeRandom; // 隨機索引值
            int placeNeighborTrap; // 鄰近陷阱數
            gridIndex.ForEach(index => placeTreasureIndex.Add(index)); // 複製一份當前剩餘的網格列表，僅用於下方迴圈;
            do
            {
                // 重置參數
                placeTrapIndex = new List<int>();
                placeAgain = false;
                placeRandom = placeTreasureIndex[Random.Range(0, placeTreasureIndex.Count)];
                placeNeighborTrap = 0;

                // 檢查鄰近網格
                HexagonMesh.grids[placeRandom].Neighbor = HexagonMesh.GetNeighbor(placeRandom);
                HexagonMesh.grids[placeRandom].NeighborIndex = HexagonMesh.FindNeighborIndex(placeRandom);
                treasureNeighborIndex = HexagonMesh.grids[placeRandom].NeighborIndex;                
                treasureNeighborIndex.ForEach(index => placeTrapIndex.Add(index)); // 複製一份寶藏鄰近網格列表，新列表將用於佈置陷阱;

                lock (treasureNeighborIndex)
                {
                    for (int j = 0; j < treasureNeighborIndex.Count; j++)
                    {
                        int indexNeighbor = treasureNeighborIndex[j];
                        // 鄰近網格是否為寶藏或安全區
                        if (HexagonMesh.grids[indexNeighbor].gridInfo == -99 || HexagonMesh.grids[indexNeighbor].gridInfo == -999)
                        {
                            placeAgain = true;
                            placeTreasureIndex.Remove(placeRandom); // 移除不符合條件之隨機索引值
                        }
                        // 鄰近網格是否為陷阱
                        else if (HexagonMesh.grids[indexNeighbor].gridInfo == -1)
                        {
                            placeNeighborTrap++;
                            placeTrapIndex.Remove(indexNeighbor);
                        }

                        // 鄰近陷阱數量超過難度限制，重新生成索引值
                        if (placeNeighborTrap > maxTrapGroup)
                        {
                            placeAgain = true;
                            placeTreasureIndex.Remove(placeRandom); // 移除不符合條件之隨機索引值
                        }
                    }
                }
            }
            while (placeAgain);

            // 檢查完成
            gridIndex.Remove(placeRandom);
            HexagonMesh.grids[placeRandom].gridInfo = -99;
            HexagonMesh.grids[placeRandom].isSuspected = true;

            if(showAll)
                HexagonMesh.grids[placeRandom].gridImage.sprite = themeManager.iconTitleTreasure.sprite;

            // Step.2 寶藏周圍隨機設置一處陷阱
            if (placeTrapIndex.Count > 0)
            {
                do
                {
                    if (placeTrapIndex.Count == 0)
                        break;
                    // 重置參數
                    placeRandom = placeTrapIndex[Random.Range(0, placeTrapIndex.Count)];
                    placeNeighborTrap = 0;
                    placeAgain = false;

                    // 檢查鄰近網格
                    List<int> trapNeighborIndex; // 陷阱鄰近網格索引
                    lock (trapNeighborIndex = HexagonMesh.FindNeighborIndex(placeRandom))
                    {
                        for (int j = 0; j < trapNeighborIndex.Count; j++)
                        {
                            int indexNeighbor = trapNeighborIndex[j];

                            // 鄰近網格是否為安全區
                            if (HexagonMesh.grids[indexNeighbor].gridInfo == -999)
                            {
                                placeAgain = true;
                                placeTrapIndex.Remove(placeRandom); // 移除不符合條件之隨機索引值
                            }
                            // 鄰近網格是否為陷阱或寶藏
                            else if (HexagonMesh.grids[indexNeighbor].gridInfo == -1 || HexagonMesh.grids[indexNeighbor].gridInfo == -99)
                                placeNeighborTrap++;

                            // 鄰近陷阱與寶藏數量超過難度限制，重新生成索引值
                            if (placeNeighborTrap > maxTrapGroup)
                            {
                                placeAgain = true;
                                placeTrapIndex.Remove(placeRandom); // 移除不符合條件之隨機索引值
                            }
                        }
                    }

                }
                while (placeAgain);

                // 檢查完成
                gridIndex.Remove(placeRandom);
                HexagonMesh.grids[placeRandom].gridInfo = -1;
                HexagonMesh.grids[placeRandom].isSuspected = true;

                if (showAll)
                    HexagonMesh.grids[placeRandom].gridImage.sprite = themeManager.iconTitleTrap.sprite;
            }
            else
                Debug.LogWarning("陷阱危機");
        }
    }
    private void PlaceTrap()
    {
        for (int i = 0; i < trap - target; i++)
        {
            // Step.1 隨機設置陷阱位置
            List<int> placeIndex = new List<int>(); // 可使用網格索引
            List<int> trapNeighborIndex; // 陷阱鄰近網格索引
            int placeRandom; // 隨機索引值
            int placeNeighborTrap; // 鄰近陷阱數
            bool placeAgain; // 不滿足條件
            gridIndex.ForEach(index => placeIndex.Add(index)); // 複製一份當前剩餘的網格列表，僅用於下方迴圈;
            do
            {
                // 重置參數
                placeRandom = placeIndex[Random.Range(0, placeIndex.Count)];
                placeNeighborTrap = 0;
                placeAgain = false;

                // 檢查鄰近網格
                lock (trapNeighborIndex = HexagonMesh.FindNeighborIndex(placeRandom))
                {
                    for (int j = 0; j < trapNeighborIndex.Count; j++)
                    {
                        int indexNeighbor = trapNeighborIndex[j];
                        // 鄰近網格是否為安全區
                        if (HexagonMesh.grids[indexNeighbor].gridInfo == -999)
                        {
                            placeAgain = true;
                            placeIndex.Remove(placeRandom); // 移除不符合條件之隨機索引值
                        }
                        // 鄰近網格是否為陷阱或寶藏
                        else if (HexagonMesh.grids[indexNeighbor].gridInfo == -1 || HexagonMesh.grids[indexNeighbor].gridInfo == -99)
                            placeNeighborTrap++;

                        // 鄰近陷阱與寶藏數量超過難度限制，重新生成索引值
                        if (placeNeighborTrap > maxTrapGroup)
                        {
                            placeAgain = true;
                            placeIndex.Remove(placeRandom); // 移除不符合條件之隨機索引值
                        }
                    }
                }
            }
            while (placeAgain);

            // 檢查完成
            gridIndex.Remove(placeRandom);
            HexagonMesh.grids[placeRandom].gridInfo = -1;
            if (showAll)
                HexagonMesh.grids[placeRandom].gridImage.sprite = themeManager.iconTitleTrap.sprite;
        }
    }
    void Precalculate()
    {
        for (int i = 0; i < HexagonMesh.grids.Length; i++)
        {
            if (HexagonMesh.grids[i].gridInfo != -1 && HexagonMesh.grids[i].gridInfo != -99)
            {
                HexagonMesh.grids[i].gridInfo = DetectMine(HexagonMesh.grids[i].gridCoordinate);
            }
        }
    }
    int DetectMine(Vector3 coordinate)
    {
        int numMine = 0;
        //int numTarget = 0;
        HexGrid[] grids = HexagonMesh.FindNeighbor(coordinate);
        for (int i = 0; i < grids.Length; i++)
        {
            if (grids[i])
            {
                if (grids[i].gridInfo == -1)
                    numMine++;
                if (grids[i].gridInfo == -99)
                    numMine+=2;
            }
        }
        //if (numMine == 0)
        //    numMine += numTarget;
        //else
        //    numMine += numTarget*2;
        if (numMine > 6)
            numMine = 6;
        return numMine;
    }
    // 網格控制
    public void IntoTrap()
    {
        countOpportunity--;
        textOpportunity.text = "" + countOpportunity;

        if (countOpportunity == 0 )
        {
            result.SetActive(true);
            resultFail.isOn = true;
            countFail++;

            if (countFail >= 7)
                tipsButton[2].SetActive(true);
            if (countFail >= 5)
                tipsButton[1].SetActive(true);
            if (countFail >= 3)
                tipsButton[0].SetActive(true);

            Ending();
        }
    }
    public void ClearNeigbor(Vector3 coordinate)
    {
        HexGrid[] grids = HexagonMesh.FindNeighbor(coordinate);
        for (int i = 0; i < grids.Length; i++)
        {
            grids[i].Disappear();
        }
    }
    public void ExploreNeigbor(Vector3 coordinate)
    {
        HexGrid[] grids = HexagonMesh.FindNeighbor(coordinate);
        for (int i = 0; i < grids.Length; i++)
        {
            if(grids[i])
                grids[i].Explore();
        }
    }
    public void GetTarget()
    {
        countTreasure++;

        if (countTreasure == target)
        {
            result.SetActive(true);
            resultSuccess.isOn = true;
            Ending();
            if (difficulty == Difficulty.Doctor)
            {
                FindObjectOfType<HighRateTerminal>().Pass();
                FindObjectOfType<HighRateTerminal>().Pass();
                FindObjectOfType<HighRateTerminal>().Pass();
                bgm.loop = false;
                bgm.clip = audioMission;
                bgm.Play();
                btnRestart.gameObject.SetActive(false);
            }               
        }
    }
    public void Ending()
    {
        btnRestart.sprite = iconRestart[1];
        restartArrow.gameObject.SetActive(true);
        if (difficulty == Difficulty.Doctor)
            toggleDoctor.isOn = true;
        else
            toggleExolore.isOn = true;
        clock = false;
        checkSuspected.isOn = false;
        activePossible = false;
        for (int i = 0; i < HexagonMesh.grids.Length; i++)
        {
            HexagonMesh.grids[i].MissClickEvent();
        }
    }
    public void AudioPlay(int audio)
    {
        audioPlayer.PlayOneShot(clips[audio]);
    }
    // Adventure Mode 冒險模式
    public void SwitchAdventureMode(int mode)
    {
        if (firstTry)
            adventureMode = (AdventureMode)mode;
        else
        {
            adventureWarning.DOFade(1,1.37f);
            if (difficulty == Difficulty.Doctor)
                toggleDoctor.isOn = true;
            else
                toggleExolore.isOn = true;
        }
        nightVision.enabled = toggleTool.isOn ? true : false;
    }
    public void Restart()
    {
        if (result.activeSelf == true && resultSuccess.isOn  ==true)
        {
            //tipsButton[0].SetActive(false);
            //tipsButton[1].SetActive(false);
            //tipsButton[2].SetActive(false);
            numeralization.isOn = false;
            dontClear.isOn = false;
            if((int)difficulty<7)
                ChooseDifficulty((int)difficulty + 1);
        }
        clock = false;
        checkSuspected.isOn = false;
        activePossible = false;
        btnRestart.sprite = iconRestart[0];
        restartArrow.gameObject.SetActive(false);
        countOpportunity = opportunity;
        countTreasure = 0;
        result.SetActive(false);
        textTimer.text = "---";
        textOpportunity.text = "" + countOpportunity;
        for (int i = 0; i < HexagonMesh.grids.Length; i++)
        {
            HexagonMesh.grids[i].Reset();
        }
        if (difficulty == Difficulty.Doctor)
            toggleDoctor.isOn = true;
        else
            toggleExolore.isOn = true;
        firstTry = false;
        bgm.clip = playBGM[Random.Range(0, playBGM.Length)];
        bgm.Play();
    }
    // 外掛
    // 失敗五次之後出現
    public void Numeralization()
    {
        for (int i = 0; i < countHexagonGrids; i++)
        {
            HexagonMesh.grids[i].Numerical();
        }
    }
    // 失敗七次之後出現，單局花費超過3分鐘
    public void DontClear()
    {
        for (int i = 0; i < countHexagonGrids; i++)
        {
            HexagonMesh.grids[i].isStable = !HexagonMesh.grids[i].isStable;
        }
    }
    // 失敗七次之後出現，單局花費超過3分鐘
    public void ShowSuspected()
    {
        if (clock)
        {
            activePossible = !activePossible;
            for (int i = 0; i < countHexagonGrids; i++)
            {
                if (HexagonMesh.grids[i].isSuspected)
                    HexagonMesh.grids[i].suspected.enabled = activePossible;
            }
        }
    }
}