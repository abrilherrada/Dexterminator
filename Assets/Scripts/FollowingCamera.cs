using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private GameObject camera1, camera2;
    
    private void AlternateCameras()
    {
        camera1.SetActive(!camera1.activeSelf);
        camera2.SetActive(!camera2.activeSelf);
    }

    private void Start()
    {
        camera1.SetActive(true);
        camera2.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AlternateCameras();
        }
    }
}
