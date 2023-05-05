using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : MonoBehaviour
{
    private GameObject[] portions;
    private int currentIndex;
    public bool allConsumed;


    private void Start()
    {
        bool skipFirst = transform.childCount > 4;
        portions = new GameObject[skipFirst ? transform.childCount-1 : transform.childCount];
        for (int i = 0; i < portions.Length; i++)
        {
            portions[i] = transform.GetChild(skipFirst ? i + 1 : i).gameObject;
            if (portions[i].activeInHierarchy)
                currentIndex = i;
        }
        allConsumed = false;
    }

     public void Consume()
    {
        if (allConsumed)
        {
            return;
        }
        if (currentIndex == portions.Length - 1)
        {
            portions[currentIndex].SetActive(false);
            currentIndex++;
            allConsumed = true;
            return;
        }

        if (currentIndex < portions.Length - 1)
        {
            portions[currentIndex].SetActive(false);
            currentIndex++;
            portions[currentIndex].SetActive(true);
        }
    }
}
