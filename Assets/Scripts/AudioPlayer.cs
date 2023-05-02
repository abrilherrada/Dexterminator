using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    [SerializeField] AudioSource doorOpening;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            doorOpening.Play();
        }
    }
}
