/* * * * * * * * * * * * * * * * * * * * *
 * 
 *    Title: "哇咔咔大冒險"
 * 
 *    Dsecription:
 *                  功能: 遊戲管理器
 *                   1. 遊戲模式管理（章節、自定義、密室逃脫）
 *                   2. 遊戲場景列表管理
 *                   3. 遊戲難度列表管理
 * 
 *     Author: iLYuSha
 *     
 *     Date: 2018.03.24
 *     
 *     Modify:
 *                  03.24 修改: 
 *                   1. 繼承自Singleton
 *     
 * * * * * * * * * * * * * * * * * * * * */
using UnityEngine;
using UnityEngine.UI;

namespace WakakaAdventureSpace
{
    public enum GameMode
    {
        Ready = 0, Campaign = 1, Custom = 2, Escape = 3,
    }
    public enum Theme
    {
        Inca = 1, Gobi = 2, KocmocA = 3,
    }
    public enum ThemeCht
    {
        印加秘境 = 1, 戈壁秘境 = 2, 卡斯摩沙 = 3,
    }
    public enum Difficulty
    {
        Doctor = 0, Newbie = 1, Trainee = 2, Elite = 3, Expert = 4, Master = 5, Crazy = 6, Wakaka = 7,
    }
    public enum DifficultyCht
    {
        博士 = 0, 萌新 = 1, 學徒 = 2, 菁英 = 3, 磚家 = 4, 大師 = 5, 瘋王 = 6, 哇咔咔 = 7,
    }

    public partial class GameManager : Singleton<GameManager>
    {
        public GameMode gameMode;
        [Header("UI")]
        public GameObject canvasQuit;
        [Header("機關城守衛暗語")]
        public GameObject panelOrder;
        public GameObject panelGuard;
        public InputField secret;

        void Update()
        {
            if (panelOrder.activeSelf)
            {
                if (secret.text == "epic" || secret.text == "EPIC")
                {
                    FindObjectOfType<HighRateTerminal>().UseBluetooth();
                    panelOrder.SetActive(false);
                    panelGuard.SetActive(true);
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
                AskingQuit();

            if (Input.GetKeyDown(KeyCode.Alpha1))
                PlayerPrefs.SetInt("Unlock Theme", 1);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                PlayerPrefs.SetInt("Unlock Theme", 2);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                PlayerPrefs.SetInt("Unlock Theme", 3);

            if (Input.GetKeyDown(KeyCode.Keypad1))
                PlayerPrefs.SetInt("Unlock Diffculty", 1);
            if (Input.GetKeyDown(KeyCode.Keypad2))
                PlayerPrefs.SetInt("Unlock Diffculty", 2);
            if (Input.GetKeyDown(KeyCode.Keypad3))
                PlayerPrefs.SetInt("Unlock Diffculty", 3);
            if (Input.GetKeyDown(KeyCode.Keypad4))
                PlayerPrefs.SetInt("Unlock Diffculty", 4);
            if (Input.GetKeyDown(KeyCode.Keypad5))
                PlayerPrefs.SetInt("Unlock Diffculty", 5);
            if (Input.GetKeyDown(KeyCode.Keypad6))
                PlayerPrefs.SetInt("Unlock Diffculty", 6);
            if (Input.GetKeyDown(KeyCode.Keypad7))
                PlayerPrefs.SetInt("Unlock Diffculty", 7);
        }

        public void VersionUpdate()
        {
            Application.OpenURL("https://drive.google.com/file/d/1QCqkUQb049LlGaGFXxqYTKSqqgNiL390/");
        }
        public void AskingQuit()
        {
            canvasQuit.SetActive(true);
        }
        public void Quit()
        {
            Application.Quit();
        }
        public void SetGameMode(int indexGameMode)
        {
            gameMode = (GameMode)indexGameMode;
        }
    }
}