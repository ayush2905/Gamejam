using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] AudioClip goalSFX;
    // [SerializeField] GameObject goalParticles;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Ball")
        {
            AudioSource.PlayClipAtPoint(goalSFX, transform.position);
            // Instantiate(goalParticles, other.transform.position, Quaternion.identity);
        }
    }
}
