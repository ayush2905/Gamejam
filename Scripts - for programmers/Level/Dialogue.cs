using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{

    AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            audioSource.Play();
            GetComponent<Collider>().enabled = false;
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
}
