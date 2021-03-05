using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PickupCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI starText;
    [SerializeField] RawImage cameraFadeOutImage;
    [SerializeField] int maxStars; //for tuning purposes
    public int starsCollected = 0;

    private void Start() {
        cameraFadeOutImage.canvasRenderer.SetAlpha(0);
    }

    public void IncreaseStarsCollected()
    {
        starsCollected++;
        starText.text = "Stars Collected: " + starsCollected.ToString() + "/" + maxStars;

        if (starsCollected >= maxStars)
        {
            cameraFadeOutImage.CrossFadeAlpha(1, 4, false);
            StartCoroutine(VoiceLength());
        }
    }

    IEnumerator VoiceLength()
    {
        yield return new WaitForSeconds(22);
        LevelManager.instance.StartCutscene();
    }
}
