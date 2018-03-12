using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class LogoFX : MonoBehaviour
{
    CanvasGroup logo;
    public float delay;
    public float period;
    public float fadeMin;
	void Start ()
    {
        logo = GetComponent<CanvasGroup>();
        if (period == 0)
            period = Random.Range(1.37f, 1.63f);
        if (fadeMin == 0)
            fadeMin = 0.137f;
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        Darkness();
    }

    void Brightness()
    {
        logo.DOFade(1.0f, period).SetEase(Ease.InOutSine).OnComplete(Darkness);
    }
    void Darkness()
    {
        logo.DOFade(fadeMin, period).SetEase(Ease.InOutSine).OnComplete(Brightness);
    }

}
