using UnityEngine;
using UnityEngine.UI;

public class ThemeManager : MonoBehaviour
{
    [Header("Theme Object")]
    public RawImage background;
    [Header("Working")]
    public Sprite[] wakakaStone;
    public Image iconCustom;
    public Image iconWorkingExplore;
    public Image iconWorkingTool;
    public Image iconWorkingFlag;
    public Text textWorkingExplore;
    public Text textWorkingTool;
    public Text textWorkingFlag;
    [Header("Tab")]
    public Image[] iconTab;
    public Image[] iconIndication;
    public Text[] textIndication;
    [Header("Page 1 Introduction")]
    public Text textSubtitleIntroduction;
    public Text textContentIntroduction;
    [Header("Page 2 Explore")]
    public Image iconTitleExplore;
    public Text textTitleExplore;
    public Text textContentExplore;
    public Text textFooterExplore;
    public Image iconGeneralExplore;
    public Image iconSpecialExplore;
    public Text textGeneralExplore;
    public Text textSpecialExplore;
    public Image[] iconSpecialListExplore;
    [Header("Page 3 Tool")]
    public Image iconTitleTool;
    public Text textTitleTool;
    public Text textContentTool;
    public Text textFooterTool;
    public Image iconUnknownTool;
    public Image iconTreasureTool;
    public Text textUnknownTool;
    public Text textTreasureTool;
    [Header("Page 3 Flag")]
    public Image iconTitleFlag;
    public Text textTitleFlag;
    public Text textContentFlag;
    [Header("Page 4 Trap")]
    public Image iconTitleTrap;
    public Text textTitleTrap;
    public Text textContentTrap;
    public Text textFooterTrap;
    public Image[] iconExampleTrap1;
    public Image[] iconExampleTrap2;
    [Header("Page 5")]
    public Image iconTitleTreasure;
    public Text textTitleTreasure;
    public Text textContentTreasure;
    public Text textFooterTreasure;
    public Image[] iconExampleTreasure1;
    public Image[] iconExampleTreasure2;
    [Header("Page 5")]
    public Text textExampleNoTreasure1;
    public Text textExampleNoTreasure2;
    public Image[] iconExampleNoTreasure1;
    public Image[] iconExampleNoTreasure2;
    public Image[] iconExampleSpecial;

    [Header("Page 6")]
    public Text textTitlePage6;
    public Text textContentPage6; // 說明
    public Image[] iconExamplePage6;
    [Header("Page 7")]
    public Image iconArrowPage7; // 箭頭
    public Image[] iconExamplePage7; // 範例
    public Text textContentPage7; // 說明
    [Header("Page 8")]
    public Image[] iconExamplePage8; // 範例
    public Text textContentPage8A; // 說明
    public Text textContentPage8B; // 說明
    public Text textContentPage8C; // 說明
    [Header("Result")]
    public Text textSuccess;
    public Text textFail;
    [Header("Theme Image")]
    public Texture2D[] backgroundTheme;
    public Sprite[] spriteInstruction;
    public Sprite[] spriteKocmoca;
    public Sprite[] spriteGobi;
    public Sprite[] spriteInca;


    private void Start()
    {
        //  MayaTheme();
    }

    string LimeText(string text)
    {
        return "<color=lime>" + text + "</color>";
    }
    string CyanText(string text)
    {
        return "<color=cyan>" + text + "</color>";
    }
    string OrangeText(string text)
    {
        return "<color=orange>" + text + "</color>";
    }
    string YellowText(string text)
    {
        return "<color=yellow>" + text + "</color>";
    }

    public struct ThemeData
    {
        public Color color;

        public Sprite spriteTrap;
        public Sprite spriteTreasure;
        public Sprite spriteExplore;
        public Sprite spriteTool;
        public Sprite spriteFlag;
        public Sprite spriteUnknown;
        public Sprite spriteGeneral;
        public SpecialGroup specialGroup;

        public string nameArea;
        public string nameTrap;
        public string nameTreasure;
        public string nameExplore;
        public string nameTool;
        public string nameFlag;
        public string nameGeneral;
        public string nameSpecial;
        public string doTool;
        public string resultTool;
        public string page0;
        public string title;
        public string intro;
    }

    public struct SpecialGroup
    {
        public Sprite[] sprite;
    }

    public ThemeData[] theme = new ThemeData[3];

    public void BasicIcon()
    {
        theme[(int)Theme.KocmocA-1].color = new Color(207.0f / 255.0f, 173.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
        theme[(int)Theme.Gobi-1].color = new Color(233.0f / 255.0f, 188.0f / 255.0f, 0.0f / 255.0f, 255.0f / 255.0f);
        theme[(int)Theme.Inca-1].color = new Color(137.0f / 255.0f, 255.0f / 255.0f, 0.0f / 255.0f, 255.0f / 255.0f);

        theme[(int)Theme.KocmocA-1].specialGroup.sprite = new Sprite[13];
        theme[(int)Theme.Gobi-1].specialGroup.sprite = new Sprite[13];
        theme[(int)Theme.Inca-1].specialGroup.sprite = new Sprite[13];

        theme[(int)Theme.KocmocA-1].spriteTrap = spriteKocmoca[16];
        theme[(int)Theme.KocmocA-1].spriteTreasure = spriteKocmoca[17];
        theme[(int)Theme.KocmocA-1].spriteExplore = spriteKocmoca[18];
        theme[(int)Theme.KocmocA-1].spriteTool = spriteKocmoca[19];
        theme[(int)Theme.KocmocA-1].spriteFlag = spriteKocmoca[20];
        theme[(int)Theme.KocmocA-1].spriteUnknown = spriteKocmoca[8];
        theme[(int)Theme.KocmocA-1].spriteGeneral = spriteKocmoca[7];
        for (int i = 0; i < theme[(int)Theme.KocmocA-1].specialGroup.sprite.Length; i++)
        {
            if (i < 7)
                theme[(int)Theme.KocmocA-1].specialGroup.sprite[i] = spriteKocmoca[i];
            else
                theme[(int)Theme.KocmocA-1].specialGroup.sprite[i] = spriteKocmoca[i + 2];
        }
        theme[(int)Theme.Gobi-1].spriteTrap = spriteGobi[16];
        theme[(int)Theme.Gobi-1].spriteTreasure = spriteGobi[17];
        theme[(int)Theme.Gobi-1].spriteExplore = spriteGobi[18];
        theme[(int)Theme.Gobi-1].spriteTool = spriteGobi[19];
        theme[(int)Theme.Gobi-1].spriteFlag = spriteGobi[20];
        theme[(int)Theme.Gobi-1].spriteUnknown = spriteGobi[8];
        theme[(int)Theme.Gobi-1].spriteGeneral = spriteGobi[7];
        for (int i = 0; i < theme[(int)Theme.Gobi-1].specialGroup.sprite.Length; i++)
        {
            if (i < 7)
                theme[(int)Theme.Gobi-1].specialGroup.sprite[i] = spriteGobi[i];
            else
                theme[(int)Theme.Gobi-1].specialGroup.sprite[i] = spriteGobi[i + 2];
        }
        theme[(int)Theme.Inca-1].spriteTrap = spriteInca[16];
        theme[(int)Theme.Inca-1].spriteTreasure = spriteInca[17];
        theme[(int)Theme.Inca-1].spriteExplore = spriteInca[18];
        theme[(int)Theme.Inca-1].spriteTool = spriteInca[19];
        theme[(int)Theme.Inca-1].spriteFlag = spriteInca[20];
        theme[(int)Theme.Inca-1].spriteUnknown = spriteInca[8];
        theme[(int)Theme.Inca-1].spriteGeneral = spriteInca[7];
        for (int i = 0; i < theme[(int)Theme.Inca-1].specialGroup.sprite.Length; i++)
        {
            if (i < 7)
                theme[(int)Theme.Inca-1].specialGroup.sprite[i] = spriteInca[i];
            else
                theme[(int)Theme.Inca-1].specialGroup.sprite[i] = spriteInca[i + 2];
        }
    }

    public void BasicText()
    {
        ThemeNoun();

        theme[(int)Theme.KocmocA-1].title = "卡斯摩沙";
        theme[(int)Theme.KocmocA-1].intro =
            "玩過" + CyanText("踩地雷") + "嗎？\n這次冒險將遭遇比" + CyanText("地雷") + "更刺激的" + LimeText(theme[(int)Theme.KocmocA-1].nameTrap) +
            "\n而我們的目標不只是用浮標進行" + YellowText("標記") + "\n還要" + theme[(int)Theme.KocmocA-1].doTool +
            "下面隱藏的" + OrangeText(theme[(int)Theme.KocmocA-1].nameTreasure) + "\n協助<color=#c0b2ff>大冒險家哇咔咔</color>完成任務吧！";

        theme[(int)Theme.Gobi-1].title = "戈壁秘境";
        theme[(int)Theme.Gobi-1].intro =
            "玩過" + CyanText("踩地雷") + "嗎？\n這次冒險將遭遇比" + CyanText("地雷") + "更刺激的" + LimeText(theme[(int)Theme.Gobi-1].nameTrap) +
            "\n而我們的目標不只是用浮標進行" + YellowText("標記") + "\n還要" + theme[(int)Theme.Gobi-1].doTool +
            "下面埋藏的" + OrangeText(theme[(int)Theme.Gobi-1].nameTreasure) + "\n協助<color=#c0b2ff>大冒險家哇咔咔</color>完成任務吧！";

        theme[(int)Theme.Inca-1].title = "印加秘境";
        theme[(int)Theme.Inca-1].intro =
            "玩過" + CyanText("踩地雷") + "嗎？\n這次冒險將遭遇比" + CyanText("地雷") + "更刺激的" + LimeText(theme[(int)Theme.Inca-1].nameTrap) +
            "\n而我們的目標不只是用浮標進行" + YellowText("標記") + "\n還要" + theme[(int)Theme.Inca-1].doTool +
            "下面埋藏的" + OrangeText(theme[(int)Theme.Inca-1].nameTreasure) + "\n協助<color=#c0b2ff>大冒險家哇咔咔</color>完成任務吧！";

        if (HexGridManager.isEscapeVer)
        {
            theme[(int)Theme.Inca-1].title = "印加秘境";
            theme[(int)Theme.Inca-1].intro =
                "玩過" + CyanText("踩地雷") + "嗎？\n這次冒險將遭遇比" + CyanText("地雷") + "更刺激的" + LimeText(theme[(int)Theme.Inca-1].nameTrap) +
                "\n而我們的目標不只要避開它\n還要" + theme[(int)Theme.Inca-1].doTool +
                "下面埋藏的" + OrangeText(theme[(int)Theme.Inca-1].nameTreasure) + "\n協助<color=#c0b2ff>大冒險家哇咔咔</color>完成任務吧！";
        }

        //"這次冒險家哇咔咔來到印加叢林，目標找出藏於" + LimeText(theme[(int)Theme.Inca-1].nameTrap) + "中的" + CyanText(target + "塊") + OrangeText(theme[(int)Theme.Inca-1].nameTreasure) +
        //"，根據" + YellowText(theme[(int)Theme.Inca-1].nameExplore) +
        //"的情報，隱匿於叢林中的" + LimeText(theme[(int)Theme.Inca-1].nameTrap) + "周圍都有古印加留下的" + LimeText(theme[(int)Theme.Inca-1].nameSpecial) +
        //"，某些沼澤就埋藏著印加文明留下的" + OrangeText(theme[(int)Theme.Inca-1].nameTreasure) + "。";

        theme[(int)Theme.KocmocA-1].resultTool = "定位完成";
        theme[(int)Theme.Gobi-1].resultTool = "成功出土";
        theme[(int)Theme.Inca-1].resultTool = "拍照完成";
    }

    void ThemeNoun()
    {
        // 宇宙
        theme[(int)Theme.KocmocA-1].nameArea = "星辰";
        theme[(int)Theme.KocmocA-1].nameTrap = "黑洞";
        theme[(int)Theme.KocmocA-1].nameTreasure = "時光隧道";
        theme[(int)Theme.KocmocA-1].nameExplore = "火箭隊";
        theme[(int)Theme.KocmocA-1].nameTool = "時光機";
        theme[(int)Theme.KocmocA-1].nameFlag = "太空浮標";
        theme[(int)Theme.KocmocA-1].nameGeneral = "星辰大海";
        theme[(int)Theme.KocmocA-1].nameSpecial = "時空裂痕";
        theme[(int)Theme.KocmocA-1].doTool = "定位";
        // 戈壁
        theme[(int)Theme.Gobi-1].nameArea = "大漠";
        theme[(int)Theme.Gobi-1].nameTrap = "流沙";
        theme[(int)Theme.Gobi-1].nameTreasure = "上古石碑";
        theme[(int)Theme.Gobi-1].nameExplore = "探險隊";
        theme[(int)Theme.Gobi-1].nameTool = "考古隊";
        theme[(int)Theme.Gobi-1].nameFlag = "沙漠浮標";
        theme[(int)Theme.Gobi-1].nameGeneral = "無際荒漠";
        theme[(int)Theme.Gobi-1].nameSpecial = "奇特沙丘";
        theme[(int)Theme.Gobi-1].doTool = "挖掘";
        // 印加
        theme[(int)Theme.Inca-1].nameArea = "叢林";
        theme[(int)Theme.Inca-1].nameTrap = "沼澤";
        theme[(int)Theme.Inca-1].nameTreasure = "神秘巨像";
        theme[(int)Theme.Inca-1].nameExplore = "探險隊";
        theme[(int)Theme.Inca-1].nameTool = "無人機";
        theme[(int)Theme.Inca-1].nameFlag = "沼澤浮標";
        theme[(int)Theme.Inca-1].nameGeneral = "迷霧叢林";
        theme[(int)Theme.Inca-1].nameSpecial = "圖騰密林";
        theme[(int)Theme.Inca-1].doTool = "拍攝";
    }

    public void SetTheme(int index)
    {
        background.texture = backgroundTheme[index];
        iconCustom.sprite = wakakaStone[index];
        iconWorkingExplore.sprite = theme[index].spriteExplore;
        iconWorkingTool.sprite = theme[index].spriteTool;
        iconWorkingFlag.sprite = theme[index].spriteFlag;
        textWorkingExplore.text = theme[index].nameExplore;
        textWorkingTool.text = theme[index].nameTool;
        textWorkingFlag.text = theme[index].nameFlag;
        // Tab
        for (int i = 0; i < iconTab.Length; i++)
        {
            iconTab[i].sprite = spriteInstruction[i];
        }
        if (HexGridManager.isEscapeVer)
        {
            iconTab[0].sprite = spriteInstruction[0];
            iconTab[1].sprite = spriteInstruction[1];
            iconTab[2].transform.parent.gameObject.SetActive(false);
            iconTab[3].sprite = spriteInstruction[2];
            iconTab[4].sprite = spriteInstruction[3];
            iconTab[5].sprite = spriteInstruction[4];
        }
        // Indication
        iconIndication[0].sprite = theme[index].spriteExplore;
        iconIndication[1].sprite = theme[index].spriteTool;
        iconIndication[2].sprite = theme[index].spriteFlag;
        iconIndication[3].sprite = theme[index].spriteTrap;
        iconIndication[4].sprite = theme[index].spriteTreasure;
        iconIndication[5].sprite = theme[index].specialGroup.sprite[0];
        iconIndication[6].sprite = theme[index].specialGroup.sprite[1];
        iconIndication[7].sprite = theme[index].specialGroup.sprite[2];
        iconIndication[8].sprite = theme[index].specialGroup.sprite[3];
        iconIndication[9].sprite = theme[index].specialGroup.sprite[4];
        iconIndication[10].sprite = theme[index].specialGroup.sprite[5];
        iconIndication[11].sprite = theme[index].specialGroup.sprite[6];
        textIndication[0].text = theme[index].nameExplore;
        textIndication[1].text = theme[index].nameTool;
        textIndication[2].text = theme[index].nameFlag;
        textIndication[3].text = theme[index].nameTrap;
        textIndication[4].text = theme[index].nameTreasure;
        textIndication[5].text = theme[index].nameSpecial;

        // 密室逃脫版本不會使用Tool與Flag
        if (HexGridManager.isEscapeVer)
        {
            iconIndication[1].gameObject.SetActive(false);
            iconIndication[2].gameObject.SetActive(false);
            textIndication[1].gameObject.SetActive(false);
            textIndication[2].gameObject.SetActive(false);
        }

        // Page 1: Introduction
        textSubtitleIntroduction.text = theme[index].title;
        textSubtitleIntroduction.color = theme[index].color;
        textContentIntroduction.text = theme[index].intro;

        // Page 2: Explore
        iconTitleExplore.sprite = theme[index].spriteExplore;
        textTitleExplore.text = theme[index].nameExplore;
        textContentExplore.text = "探索" + theme[index].nameArea + "的各個區域取得線索";
        textFooterExplore.text = LimeText(theme[index].nameSpecial) + "只會被" + YellowText(theme[index].nameExplore) + "所發現\n不同的特徵對應著" + CyanText("1~6") +
            "六個等級";
        iconGeneralExplore.sprite = theme[index].spriteGeneral;
        iconSpecialExplore.sprite = theme[index].specialGroup.sprite[0];
        textGeneralExplore.text = theme[index].nameGeneral + " - 周圍不會出現" + LimeText(theme[index].nameTrap);
        textSpecialExplore.text = LimeText(theme[index].nameSpecial) + " - 根據經驗共有" + CyanText("6種");
        for (int i = 0; i < iconSpecialListExplore.Length; i++)
        {
            if (i < 6)
                iconSpecialListExplore[i].sprite = theme[index].specialGroup.sprite[i + 1];
            else if (i < 12)
                iconSpecialListExplore[i].color = theme[index].color;
            else
                iconSpecialListExplore[i].sprite = theme[index].specialGroup.sprite[i - 5];
        }

        // Page 3 Tool
        iconTitleTool.sprite = theme[index].spriteTool;
        textTitleTool.text = theme[index].nameTool;
        textContentTool.text = "進入" + LimeText(theme[index].nameTrap) + theme[index].doTool + OrangeText(theme[index].nameTreasure);
        textFooterTool.text = "若" + LimeText(theme[index].nameTrap) + "中沒有" + OrangeText(theme[index].nameTreasure) + "，" + YellowText(theme[index].nameTool) +
            "將無法返回";
        iconUnknownTool.sprite = theme[index].spriteUnknown;
        iconTreasureTool.sprite = theme[index].spriteTreasure;
        textUnknownTool.text = "未知區域 - " + YellowText(theme[index].nameTool) + "無法進行探索";
        textTreasureTool.text = OrangeText(theme[index].nameTreasure) + " - 藏匿於部分" + LimeText(theme[index].nameTrap) + "之下";
        // Page 3  Flag
        iconTitleFlag.sprite = theme[index].spriteFlag;
        textTitleFlag.text = theme[index].nameFlag;
        textContentFlag.text = "輔助標記疑似" + LimeText(theme[index].nameTrap) + "的區域\n標記過的區域可以移除\n也可指派" + YellowText(theme[index].nameExplore) +
            "或" + YellowText(theme[index].nameTool) + "前往";

        // Page 4 Trap
        iconTitleTrap.sprite = theme[index].spriteTrap;
        textTitleTrap.text = theme[index].nameTrap;
        textContentTrap.text = YellowText(theme[index].nameExplore) + "與" + YellowText(theme[index].nameTool) + "誤觸" + LimeText(theme[index].nameTrap) +
            "將無法返回\n同時會使周圍的探索功虧一簣\n建議先將可疑的" + LimeText(theme[index].nameTrap) + "使用" + YellowText(theme[index].nameFlag) + "標記";
        if (HexGridManager.isEscapeVer)
            textContentTrap.text = YellowText(theme[index].nameExplore) + "誤觸" + LimeText(theme[index].nameTrap) +
                "將無法返回\n同時會使周圍的探索功虧一簣";
        textFooterTrap.text = "每個區域的" + LimeText(theme[index].nameTrap) + "都會對周圍增加" + CyanText("1級") + "的特徵";
        for (int i = 0; i < iconExampleTrap1.Length; i++)
        {
            iconExampleTrap1[i].sprite = theme[index].specialGroup.sprite[7];
        }
        iconExampleTrap1[0].sprite = theme[index].spriteTrap;

        for (int i = 0; i < iconExampleTrap2.Length; i++)
        {
            if (i < 2)
                iconExampleTrap2[i].sprite = theme[index].spriteTrap;
            else if (i < 8)
                iconExampleTrap2[i].sprite = theme[index].specialGroup.sprite[7];
            else
                iconExampleTrap2[i].sprite = theme[index].specialGroup.sprite[8];
        }

        // Page 5 Treasure
        iconTitleTreasure.sprite = theme[index].spriteTreasure;
        textTitleTreasure.text = theme[index].nameTreasure;
        textContentTreasure.text = "藏於部分" + LimeText(theme[index].nameTrap) + "之下\n只有" + YellowText(theme[index].nameTool) + "能進行" + theme[index].doTool;
        if (HexGridManager.isEscapeVer)
            textContentTreasure.text = "藏於部分" + LimeText(theme[index].nameTrap) + "之下";
        textFooterTreasure.text = "每個區域的" + OrangeText(theme[index].nameTreasure) + "都會對周圍增加" + CyanText("2級") + "的特徵";
        for (int i = 0; i < iconExampleTreasure1.Length; i++)
        {
            if (i < 1)
                iconExampleTreasure1[i].sprite = theme[index].spriteTrap;
            else if (i < 2)
                iconExampleTreasure1[i].sprite = theme[index].spriteTreasure;
            else if (i < 5)
                iconExampleTreasure1[i].sprite = theme[index].specialGroup.sprite[7];
            else if (i < 8)
                iconExampleTreasure1[i].sprite = theme[index].specialGroup.sprite[8];
            else
                iconExampleTreasure1[i].sprite = theme[index].specialGroup.sprite[9];
        }
        for (int i = 0; i < iconExampleTreasure2.Length; i++)
        {
            if (i < 2)
                iconExampleTreasure2[i].sprite = theme[index].spriteTrap;
            else if (i < 3)
                iconExampleTreasure2[i].sprite = theme[index].spriteTreasure;
            else if (i < 7)
                iconExampleTreasure2[i].sprite = theme[index].specialGroup.sprite[7];
            else if (i < 10)
                iconExampleTreasure2[i].sprite = theme[index].specialGroup.sprite[8];
            else
                iconExampleTreasure2[i].sprite = theme[index].specialGroup.sprite[9];
        }

        // Page 6 Wakaka
        textExampleNoTreasure1.text = "獨立" + LimeText(theme[index].nameTrap) + "不會藏有" + OrangeText(theme[index].nameTreasure);
        textExampleNoTreasure2.text = "多個" + OrangeText(theme[index].nameTreasure) + "不會相鄰";
        for (int i = 0; i < iconExampleNoTreasure1.Length; i++)
        {
            iconExampleNoTreasure1[i].sprite = theme[index].specialGroup.sprite[7];
        }
        iconExampleNoTreasure1[0].sprite = theme[index].spriteTreasure;
        for (int i = 0; i < iconExampleNoTreasure2.Length; i++)
        {
            if (i < 1)
                iconExampleNoTreasure2[i].sprite = theme[index].spriteTrap;
            else if (i < 3)
                iconExampleNoTreasure2[i].sprite = theme[index].spriteTreasure;
            else if (i < 5)
                iconExampleNoTreasure2[i].sprite = theme[index].specialGroup.sprite[7];
            else if (i < 9)
                iconExampleNoTreasure2[i].sprite = theme[index].specialGroup.sprite[8];
            else if (i < 11)
                iconExampleNoTreasure2[i].sprite = theme[index].specialGroup.sprite[9];
            else
                iconExampleNoTreasure2[i].sprite = theme[index].specialGroup.sprite[10];
        }
        for (int i = 0; i < iconExampleSpecial.Length; i++)
        {
            if (i < 3)
                iconExampleSpecial[i].sprite = theme[index].spriteTrap;
            else if (i < 5)
                iconExampleSpecial[i].sprite = theme[index].spriteTreasure;
            else if (i < 8)
                iconExampleSpecial[i].sprite = theme[index].specialGroup.sprite[7];
            else if (i < 14)
                iconExampleSpecial[i].sprite = theme[index].specialGroup.sprite[8];
            else if (i < 16)
                iconExampleSpecial[i].sprite = theme[index].specialGroup.sprite[8];
            else if (i < 17)
                iconExampleSpecial[i].sprite = theme[index].specialGroup.sprite[10];
            else
                iconExampleSpecial[i].sprite = theme[index].specialGroup.sprite[12];
        }
        
        // 以下為舊版範例說明
        // Page 7
        textTitlePage6.color = theme[index].color;
        textContentPage6.text =
            LimeText(theme[index].nameSpecial) + "會提示周圍的" + LimeText(theme[index].nameTrap) +
            "數量，然而這數量不一定是正確的，若周圍某一區域的" + theme[index].nameTrap + "藏有" + OrangeText(theme[index].nameTreasure) +
            "則" + theme[index].nameTrap + "的提示數量" + YellowText("不會增加") + "，但" + YellowText("也不會消失") + "。";

        for (int i = 0; i < iconExamplePage6.Length; i++)
        {
            if (i < 12)
                iconExamplePage6[i].sprite = theme[index].spriteGeneral;
            else if (i < 32)
                iconExamplePage6[i].sprite = theme[index].specialGroup.sprite[1];
            else if (i < 34)
                iconExamplePage6[i].sprite = theme[index].spriteTreasure;
            else
                iconExamplePage6[i].sprite = theme[index].spriteTrap;
        }

        // Page 8
        textContentPage7.text =
            "觀察中央的格子，能發現" + LimeText(theme[index].nameSpecial) + "提供的線索表示周圍有" + CyanText("2處") + LimeText(theme[index].nameTrap) +
            "，但我們的" + YellowText(theme[index].nameExplore) + "實際卻發現有" + CyanText("3處") + LimeText(theme[index].nameTrap) +
            "，這種衝突告訴我們在這三處" + theme[index].nameTrap + "中必有一處藏有" + OrangeText(theme[index].nameTreasure) +
            "，透過更多線索找出最可疑的" + theme[index].nameTrap + "，並派出" + YellowText(theme[index].nameTool) + "吧！";
        for (int i = 0; i < iconExamplePage7.Length; i++)
        {
            if (i < 4)
                iconExamplePage7[i].sprite = theme[index].spriteGeneral;
            else if (i < 17)
                iconExamplePage7[i].sprite = theme[index].specialGroup.sprite[1];
            else if (i < 20)
                iconExamplePage7[i].sprite = theme[index].specialGroup.sprite[2];
            else
                iconExamplePage7[i].sprite = theme[index].spriteTrap;
        }
        iconArrowPage7.color = theme[index].color;

        // Page 9
        textContentPage8A.text =
            "下圖中，觀察ABC三處" + LimeText(theme[index].nameSpecial) + "，A與B顯示周圍必有" + CyanText("1處") + LimeText(theme[index].nameTrap) +
            "，C周圍則至少" + CyanText("2處") + LimeText(theme[index].nameTrap) + "。";
        textContentPage8B.text = "由於C周圍只剩下" + CyanText("2處") + "未探索區域，故E與F兩區必然不會是藏匿" + OrangeText(theme[index].nameTreasure) +
            "的" + LimeText(theme[index].nameTrap) + "。\n\n試著派出" + YellowText(theme[index].nameTool) +
            "前往D處找到" + OrangeText(theme[index].nameTreasure) + "。";
        textContentPage8C.text = "若C區的" + LimeText(theme[index].nameSpecial) + "與AB相同，則唯一的可能就是DEF都是" + OrangeText(theme[index].nameTreasure) + "。";
        for (int i = 0; i < iconExamplePage8.Length; i++)
        {
            if (i < 15)
                iconExamplePage8[i].sprite = theme[index].spriteGeneral;
            else if (i < 23)
                iconExamplePage8[i].sprite = theme[index].specialGroup.sprite[1];
            else
                iconExamplePage8[i].sprite = theme[index].specialGroup.sprite[2];
        }

        // Result
        textSuccess.text = theme[index].nameTreasure + "\n全部" + theme[index].resultTool;
        textFail.text = "你的探險隊\n皆已遭" + theme[index].nameTrap + "埋沒";
    }
}