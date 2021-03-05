using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWall : MonoBehaviour
{
    public GameObject blackWolf, whiteWolf;
    private Color color;

    void Start()
    {
        color = Color.white;
    }

    void Update()
    {
        if (blackWolf.activeInHierarchy && Mathf.Abs(blackWolf.transform.position.x - transform.position.x) <= 50)
        {
            color.a = 1 - (Mathf.Abs(blackWolf.transform.position.x - transform.position.x) / 50);
            GetComponent<MeshRenderer>().material.SetColor("_TintColor", color);
        }

        else if (whiteWolf.activeInHierarchy && Mathf.Abs(whiteWolf.transform.position.x - transform.position.x) <= 50)
        {
            color.a = 1 - (Mathf.Abs(whiteWolf.transform.position.x - transform.position.x) / 50);
            GetComponent<MeshRenderer>().material.SetColor("_TintColor", color);
        }

        else if (color.a != 0)
        {
            color.a = 0;
            GetComponent<MeshRenderer>().material.SetColor("_TintColor", color);
        }
    }
}
