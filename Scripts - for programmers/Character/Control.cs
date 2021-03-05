using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    //Serialized Variables
    [SerializeField] AudioClip howlSound;
    [SerializeField] float howlTime;

    //Cached References
    Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        Howl();
    }

    private void Howl()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            animator.SetBool("howl", true);
            StartCoroutine(StopHowlAnimation());
        }
    }

    private void HowlStart()
    {
        AudioSource.PlayClipAtPoint(howlSound, transform.position);
    }

    IEnumerator StopHowlAnimation()
    {
        yield return new WaitForSeconds(howlTime);
        animator.SetBool("howl", false);
    }
}
