using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] AudioClip doorOpenSFX;
    [SerializeField] float doorPositionChange;
    [SerializeField] float doorSpeed;

    private Vector3 startingPos;
    private bool triggered;

    private void Start() {
        startingPos = new Vector3(door.transform.position.x, door.transform.position.y, door.transform.position.z);
        triggered = false;
    }

    private void Update()
    {   if(doorSpeed>0f){
        if (triggered && door.transform.position.y < startingPos.y + doorPositionChange)
        {
            door.transform.Translate(Vector3.up * Time.deltaTime * doorSpeed);

            if (door.GetComponent<MovablePlatform>().onPlatform.Count != 0)
            {
                foreach (GameObject wolf in door.GetComponent<MovablePlatform>().onPlatform)
                {
                    wolf.transform.Translate(Vector3.up * Time.deltaTime * doorSpeed);
                }
            }
        }
       }
else {if (triggered && door.transform.position.y > startingPos.y - doorPositionChange)
        {
            door.transform.Translate(Vector3.up * Time.deltaTime * doorSpeed);

            if (door.GetComponent<MovablePlatform>().onPlatform.Count != 0)
            {
                foreach (GameObject wolf in door.GetComponent<MovablePlatform>().onPlatform)
                {
                    wolf.transform.Translate(Vector3.up * Time.deltaTime * doorSpeed);
                }
            }
        }
     }
    }

    private void OnTriggerEnter(Collider other) {
        triggered = true;
        AudioSource.PlayClipAtPoint(doorOpenSFX, transform.position);
        //door.transform.position += new Vector3(0f, doorPositionChange, 0f) * Time.deltaTime * doorSpeed;
        GetComponent<Animator>().SetBool("triggered", true);
    }

    private void OnTriggerExit(Collider other) {
        // If the collider was deactivated do nothing
        if (!other.gameObject.activeInHierarchy)
            return;

        triggered = false;
        door.transform.position = startingPos;
        GetComponent<Animator>().SetBool("triggered", false);
    }
}
