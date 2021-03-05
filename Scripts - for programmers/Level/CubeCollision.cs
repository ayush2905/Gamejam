using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    private int timesHit = 0;
    private bool isPlaying = false;
    [SerializeField] AudioSource hitDialogue;
    [SerializeField] AudioSource goAway;

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player")
        {
            if(timesHit <= 5)
            {
                if (!isPlaying)
                {
                    timesHit++;
                    hitDialogue.Play();
                    isPlaying = true;
                    StartCoroutine(Wait());
                }
            }

            else if(timesHit > 5)
            {
                if (!isPlaying)
                {
                    goAway.Play();
                    isPlaying = true;
                    StartCoroutine(Wait());
                }
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(6);
        isPlaying = false;
    }
}
