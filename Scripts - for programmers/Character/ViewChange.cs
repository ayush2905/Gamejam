using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewChange : MonoBehaviour
{

    //Char1 is black
    //char2 is white
    [SerializeField] Material nightSky;
    [SerializeField] Material daySky;
    [SerializeField] GameObject blackParticles;
    [SerializeField] GameObject whiteParticles;
    [SerializeField] GameObject blackWolf;
    [SerializeField] GameObject whiteWolf;

    Movement movement1, movement2;

    public GameObject char1, char2;

    void Start()
    {
        movement1 = blackWolf.GetComponent<Movement>();
        movement2 = whiteWolf.GetComponent<Movement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && movement1.isGrounded && movement2.isGrounded)
        {
            SwitchChar();
        }

        blackParticles.transform.position = blackWolf.transform.position;
        whiteParticles.transform.position = whiteWolf.transform.position;
    }

    void SwitchChar()
    {
        if (char1.activeInHierarchy == true)
        {
            char1.SetActive(false);
            //blackWolf.GetComponent<BoxCollider>().enabled = true;
            char2.SetActive(true);
            RenderSettings.skybox = daySky;
        }
        else if (char2.activeInHierarchy == true)
        {
            char2.SetActive(false);
            //whiteWolf.GetComponent<BoxCollider>().enabled = true;
            char1.SetActive(true);
            RenderSettings.skybox = nightSky;
        }
    }
}
