using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField] GameObject glassShatter;
    [SerializeField] AudioClip glassShatterAudio;
   private void OnCollisionEnter(Collision other) {
       if(other.gameObject.tag == "Player" && other.gameObject.GetComponent<Movement>().dashing)
       {
           AudioSource.PlayClipAtPoint(glassShatterAudio, Camera.main.transform.position, 10f);
           GameObject glassParticle = Instantiate(glassShatter, other.transform.position, Quaternion.identity);
           Destroy(gameObject);
       }
   }
}
