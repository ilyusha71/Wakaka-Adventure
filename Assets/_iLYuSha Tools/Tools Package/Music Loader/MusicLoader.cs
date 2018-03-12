/**************************************************************************************** 
 * Wakaka Studio 2017
 * Author: iLYuSha Dawa-mumu Wakaka Kocmocovich Kocmocki KocmocA
 * Project: 0escape Medieval - Music Loader
 * Version: Tool Package
 * Tools: Unity 5/C#
 * Last Updated: 2017/11/15
 * Add mp3 files load
 ****************************************************************************************/
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicLoader : MonoBehaviour
{
    public GameObject bgmCanvas;
    public Text textLoader, textPlaying;
    public static AudioSource bgAudio;
    public static AudioClip[] clips;
    public static Text textTrackPlaying;
    public static int defaultTrack;
    private static int orderPlay;
    private string OrderColor(int rank)
    {
        return TextCustom.TextColor("#FFD700", rank + ". ");
    }

    void Awake()
    {
        textLoader.text = "";
        textPlaying.text = "";
        bgAudio = GetComponent<AudioSource>();
        textTrackPlaying = textPlaying;
        defaultTrack = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "Track");
        StartCoroutine(MusicLoading());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F7))
            bgmCanvas.SetActive(!bgmCanvas.activeSelf);
        if (Input.GetKeyDown(KeyCode.F6))
            textPlaying.text = NextTrack();
    }
#if UNITY_2017_2
    IEnumerator MusicLoading()
    {
        // 資源路徑
        string dPath = Application.streamingAssetsPath + "/Music";
        if (!Directory.Exists(dPath))
            Directory.CreateDirectory(dPath);
        // 取得該路徑下資源數，篩選wav檔，因為Editor會含有meta的檔案
        string[] wav = Directory.GetFileSystemEntries(dPath, "*.wav");
        string[] mp3 = Directory.GetFileSystemEntries(dPath, "*.mp3");
        string[] files = mp3.Union(wav).ToArray();
        int count = files.Length;
        if (count > 0)
        {
            clips = new AudioClip[count];

            for (int i = 0; i < count; i++)
            {
                string sPath = "file://" + files[i];
                WWW www = new WWW(sPath);
                yield return www;
                clips[i] = WWWAudioExtensions.GetAudioClip(www, true, true);
                clips[i].name = files[i].Replace(dPath + "\\", "");
                textLoader.text += OrderColor(i + 1) + clips[i].name + "\n";
            }
            bgAudio.clip = clips[defaultTrack];
            textPlaying.text = clips[defaultTrack].name;
        }
        else
        {
            textLoader.text += "No Track\n";
        }
    }
#endif
    static void SetDefalut()
    {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "Track", orderPlay);
    }
    public static string PlayTrack()
    {
        if (!bgAudio.isPlaying)
        {
            orderPlay++;
            if (orderPlay == clips.Length)
                orderPlay = 0;
            bgAudio.clip = clips[orderPlay];
            bgAudio.Play();
            textTrackPlaying.text = clips[orderPlay].name;
        }
        return clips[orderPlay].name;
    }
    public static string NextTrack()
    {
        orderPlay++;
        if (orderPlay == clips.Length)
            orderPlay = 0;
        bgAudio.clip = clips[orderPlay];
        bgAudio.Play();
        textTrackPlaying.text = clips[orderPlay].name;
        SetDefalut();
        return clips[orderPlay].name;
    }
    public static string DefaultTrack()
    {
        bgAudio.clip = clips[defaultTrack];
        bgAudio.Play();
        textTrackPlaying.text = clips[defaultTrack].name;
        return clips[orderPlay].name;
    }
    public static string RandomTrack()
    {
        orderPlay = Random.Range(0, clips.Length);
        bgAudio.clip = clips[orderPlay];
        bgAudio.Play();
        textTrackPlaying.text = clips[orderPlay].name;
        return clips[orderPlay].name;
    }
    public static string CustomTrack(AudioClip clip)
    {
        bgAudio.clip = clip;
        bgAudio.Play();
        textTrackPlaying.text = clip.name;
        return clip.name;
    }
}
