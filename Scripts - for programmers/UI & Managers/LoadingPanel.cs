using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
    #region Singleton
    public static LoadingPanel instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    string LevelName;
    [SerializeField] Image[] images;
    [SerializeField] TMPro.TMP_Text[] texts;

    public void DoInAnimation(string LevelName)
    {
        gameObject.SetActive(true);

        this.LevelName = LevelName;
        foreach (var item in images)
        {
            item.DOFade(1, 1);
        }

        foreach (var item in texts)
        {
            item.DOFade(1, 1);
        }

        Invoke("OnInAnimationEnd", 1.1f);
    }

    public void OnInAnimationEnd()
    {
        LevelManager.instance.LoadLevel(LevelName);
    }

    public void DoOutAnimation()
    {
        foreach (var item in images)
        {
            item.DOFade(0, 1);
        }

        foreach (var item in texts)
        {
            item.DOFade(0, 1);
        }

        Invoke("OnOutAnimationEnd", 1.1f);
    }

    public void OnOutAnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
