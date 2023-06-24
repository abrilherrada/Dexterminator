using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] AudioSource doorOpening;


    private IEnumerator animationSFXCoroutine()
    {
        yield return new WaitForSeconds(1f);
        doorOpening.Play();
    }

    private void Awake()
    {
        animator.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.enabled = true;
            animator.SetBool("UsedDoorknob", true);
            StartCoroutine(animationSFXCoroutine());
            
        }
    }
}
