using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameMode gameMode;
    [Header("UI")]
    public GameObject canvasQuit;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

     void Update()
    {
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

    public void AskingQuit()
    {
        canvasQuit.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }

}

public enum GameMode
{
    Ready = 0,
    Campaign = 1,
    Custom = 2,
    Escape = 3,
}
public enum Theme
{
    Inca = 1,
    Gobi = 2,
    KocmocA = 3,
}
public enum ThemeCht
{
    印加秘境 = 1,
    戈壁秘境 = 2,
    卡斯摩沙 = 3,
}
public enum Difficulty
{
    Doctor = 0,
    Newbie = 1,
    Trainee = 2,
    Elite = 3,
    Expert = 4,
    Master = 5,
    Crazy = 6,
    Wakaka = 7,
}
public enum DifficultyCht
{
    博士 = 0,
    萌新 = 1,
    學徒 = 2,
    菁英 = 3,
    磚家 = 4,
    大師 = 5,
    瘋王 = 6,
    哇咔咔 = 7,
}
