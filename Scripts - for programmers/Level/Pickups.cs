using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pickups : MonoBehaviour
{
    [SerializeField] AudioClip pickupSound;

    [SerializeField] GameObject gameManager;

    PickupCount pickupCount;

    private void Start() {
        pickupCount = gameManager.GetComponentInChildren<PickupCount>();
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            pickupCount.IncreaseStarsCollected();

            //Sound
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            Destroy(gameObject);
        }
    }
}
