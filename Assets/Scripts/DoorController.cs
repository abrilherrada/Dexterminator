using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            animator.SetTrigger("DoorknobUsed");
        }
    }
}
