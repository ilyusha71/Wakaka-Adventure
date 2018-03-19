using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileSimulator : MonoBehaviour
{
    [Header("待機界面")]
    public GameObject ready;
    public Image[] atlasKey;
    public Image[] showKey;
    public Sprite[] spriteAmethyst;
    public Sprite[] spriteCitrine;
    public bool[] hasVary;
    [Header("來電")]
    public GameObject phoneCall;
    public GameObject call;
    public GameObject talking;
    public AudioSource audioBell;
    public AudioSource audioTalk;
    public bool callMobile = false; // true為來電
    public bool answerTheCall = false; // true為接通
    // 手機振動計時器
    private int counts;
    private float delay = 1.5f;
    private float timerVibration;
    private float timerMobile;
    [Header("接通")]
    public Text textCallerName;
    public Text textCallTimer;
    private float callStart;
    public float callTime;
    private int rank;

    private void Awake()
    {
        hasVary = new bool[7];
    }

    public void Vary(int index)
    {
        hasVary[index] = !hasVary[index];
        if (hasVary[index])
            atlasKey[index].sprite = spriteCitrine[index];
        else if (!hasVary[index])
            atlasKey[index].sprite = spriteAmethyst[index];

        if (hasVary[2] && hasVary[5] && hasVary[6] && !hasVary[0] && !hasVary[1] && !hasVary[3] && !hasVary[4])
        {
            callMobile = true;
            ready.SetActive(false);
            phoneCall.SetActive(true);
            timerVibration = Time.time + 1.0f;
            timerMobile = Time.time + 90.0f;
        }
    }

    void Update ()
    {
        if (callMobile)
        {
            if (Time.time > timerVibration && !answerTheCall)
            {
                if (!audioBell.isPlaying)
                    audioBell.Play();

                if (counts % 2 == 0)
                    timerVibration = Time.time + 0.3f;
                else
                    timerVibration = Time.time + delay;
                Handheld.Vibrate();
                counts++;
            }

            if (Time.time > timerMobile)
                AnswerTheCall();

            if (answerTheCall)
            {
                callTime = (int)(Time.time - callStart);
                textCallTimer.text = "00 : " + string.Format("{0:00}", callTime);
                if (callTime > 11)
                {
                    FindObjectOfType<HexGridManager>().Restart();
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public void AnswerTheCall()
    {
        if (!answerTheCall)
        {
            answerTheCall = true;
            audioBell.Pause();
            audioTalk.Play();

            call.SetActive(false);
            talking.SetActive(true);
            callStart = Time.time;
            textCallerName.color = Color.green;
        }
    }

    public void HangUp()
    {
        FindObjectOfType<HexGridManager>().Restart();
        gameObject.SetActive(false);
    }

    //public void Test(int score)
    //{
    //    rank += score;
    //    //Debug.Log(rank + " : " + score);
    //}
    //public void Final()
    //{
    //    rank = -100;
    //    if (rank >= 99)
    //        FindObjectOfType<HexGridManager>().ChooseDifficulty(Difficulty.Crazy);
    //    else if (rank >= 84)
    //        FindObjectOfType<HexGridManager>().ChooseDifficulty(Difficulty.Master);
    //    else if (rank >= 71)
    //        FindObjectOfType<HexGridManager>().ChooseDifficulty(Difficulty.Expert);
    //    else if (rank >= 49)
    //        FindObjectOfType<HexGridManager>().ChooseDifficulty(Difficulty.Elite);
    //    else if (rank >= 36)
    //        FindObjectOfType<HexGridManager>().ChooseDifficulty(Difficulty.Trainee);
    //    else if (rank >= 0)
    //        FindObjectOfType<HexGridManager>().ChooseDifficulty(Difficulty.Newbie);
    //    else
    //        FindObjectOfType<HexGridManager>().ChooseDifficulty(Difficulty.Doctor);
    //    gameObject.SetActive(false);
    //}
}
