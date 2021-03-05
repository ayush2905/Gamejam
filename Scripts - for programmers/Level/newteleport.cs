using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newteleport : MonoBehaviour
{
    // Start is called before the first frame update

public Transform teleportTarget;

void OnTriggerEnter(Collider other)
{
    if(other.gameObject.tag == "Player")
    {
    other.transform.position = teleportTarget.transform.position;
    }
}

}
