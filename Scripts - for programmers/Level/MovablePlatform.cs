using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
    public GameObject blackWolf, whiteWolf;
    public List<GameObject> onPlatform { get; private set; }

    void Start()
    {
        onPlatform = null;
        onPlatform = new List<GameObject>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If either wolf stands on the platform
        if (collision.gameObject == blackWolf || collision.gameObject == whiteWolf)
        {
            //If the wolf isn't in the list add it to the list
            if (!onPlatform.Contains(collision.gameObject))
            {
                onPlatform.Add(collision.gameObject);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // If the collider was deactivated do nothing
        if (!collision.gameObject.activeInHierarchy)
            return;

        if (onPlatform.Contains(collision.gameObject))
        {
            onPlatform.Remove(collision.gameObject);
        }
    }
}
