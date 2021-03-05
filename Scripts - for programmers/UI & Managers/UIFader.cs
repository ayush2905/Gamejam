using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFader : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] float targetAlpha;
    [SerializeField] float beforeFadingInTime;
    [SerializeField] float fadeInDuration;
    [SerializeField] float timeBetween;
    [SerializeField] float fadeOutDuration;
    
    
    
    private void Start()
    {
        image.canvasRenderer.SetAlpha(0);
        StartCoroutine(FadingIn());
    }
    private void Update() {
        if(image.canvasRenderer.GetAlpha() == targetAlpha)
        {
            StartCoroutine(FadingOut());
        }
    }

    IEnumerator FadingIn()
    {
        yield return new WaitForSeconds(beforeFadingInTime);
        image.CrossFadeAlpha(targetAlpha, fadeInDuration, false);
    }

    IEnumerator FadingOut()
    {
        yield return new WaitForSeconds(timeBetween);
        image.CrossFadeAlpha(0, fadeOutDuration, false);
    }
    
}
