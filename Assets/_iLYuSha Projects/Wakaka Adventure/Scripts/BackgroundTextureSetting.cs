using UnityEngine;
using UnityEngine.UI;

public class BackgroundTextureSetting : MonoBehaviour
{
    private CanvasScaler scaler;
    void Start ()
    {
        scaler = transform.root.GetComponent<CanvasScaler>();
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        float developmentRatio = (float)scaler.referenceResolution.x / (float)scaler.referenceResolution.y;
        float screenRatio = (float)Screen.width / (float)Screen.height;

        float def = screenRatio / developmentRatio;

        if (def > 1)
        {
            scaler.matchWidthOrHeight = 1;
            RectTransform bg = GetComponent<RectTransform>();
            Vector2 size = bg.sizeDelta;
            bg.sizeDelta = new Vector2(size.x * def, size.y * def);
        }
        else
        {
            scaler.matchWidthOrHeight = 0;
            RectTransform bg = GetComponent<RectTransform>();
            Vector2 size = bg.sizeDelta;
            bg.sizeDelta = new Vector2(size.x / def, size.y / def);
        }
    }
}
