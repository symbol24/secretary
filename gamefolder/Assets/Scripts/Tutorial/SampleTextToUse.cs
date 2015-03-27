using System;
using UnityEngine;
using System.Collections;

public class SampleTextToUse : MonoBehaviour
{
    public TextQueue textQueue;
    // Use this for initialization
    private void Start()
    {
        string toPass = string.Empty;
        string toPass2 = string.Empty;
        for (int i = 0; i < 100; i++)
        {
            toPass += "A";
            toPass2 += "B";
        }
        textQueue.Enqueue(toPass);
        textQueue.Enqueue(toPass2);
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
