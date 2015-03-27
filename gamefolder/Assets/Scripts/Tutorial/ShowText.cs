using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ShowText : MonoBehaviour
{
    public float distanceFromLeftAndRightBorder = 10f;
    public float distanceFromBottom = 10f;
    public float heightOfBox = 200;
    public GUIStyle StyleToUse;
    public string Title { get; set; }
    public string TextToShow { get; set; }
    void OnGUI()
    {
        GUI.Label(
            new Rect(distanceFromLeftAndRightBorder, Screen.height - heightOfBox - distanceFromBottom,
                Screen.width - distanceFromLeftAndRightBorder*2, heightOfBox),
            string.Format("{0}{1}{2}", Title, Environment.NewLine, TextToShow), StyleToUse);
    }

	// Use this for initialization
	void Start ()
	{
	    Title = string.Empty;
	    TextToShow = string.Empty;
	    enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
