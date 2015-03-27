using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuTextShowing : MonoBehaviour
{
    public float speedOfText;
    public Text FirstText;
    public Text SecondText;
    public Text PressAnyKeyText;
    public Image imageToShow;
    public float fadeFactor = 1f;
    private bool _readyToSelect = false;
    public float delayFirstText = 0f;
    public float delaySecondText = 1f;
    public float delayImage = 2f;
    public float secondsToWaitForInput = 5f;
    public string nextScene = "GYM";
    public float intervalBetweenPressAnyKeyText;

	// Use this for initialization
	void Start ()
	{   
	    imageToShow.color = new Color(imageToShow.color.r, imageToShow.color.g, imageToShow.color.b, 0);
        FirstText.color = new Color(FirstText.color.r, FirstText.color.g, FirstText.color.b, 0);
        SecondText.color = new Color(SecondText.color.r, SecondText.color.g, SecondText.color.b, 0);
        PressAnyKeyText.color = new Color(PressAnyKeyText.color.r, PressAnyKeyText.color.g, PressAnyKeyText.color.b, 0);
	    StartCoroutine(FadeInTexture(FirstText, delayFirstText));
	    StartCoroutine(FadeInTexture(SecondText, delaySecondText));
	    StartCoroutine(FadeInTexture(imageToShow, delayImage));
	    StartCoroutine(ShowPressAnyKey());
	}

    private bool _isReadyToReactToInput = false;
    private void Update()
    {
        if (Input.anyKeyDown && _isReadyToReactToInput)
        {
            Application.LoadLevel(nextScene);
        }
    }

    private IEnumerator currentRoutine;

    private IEnumerator FadeInTexture(Graphic toPlayWith, float delayToStart)
    {
        yield return new WaitForSeconds(delayToStart);
        while (toPlayWith.color.a != 1)
        {
            var nextAlpha = toPlayWith.color.a + Time.deltaTime * fadeFactor > 1
                ? 1
                : toPlayWith.color.a + Time.deltaTime * fadeFactor;
            toPlayWith.color = new Color(toPlayWith.color.r, toPlayWith.color.g, toPlayWith.color.b,
                nextAlpha);
            yield return null;
        }
    }

    IEnumerator FadeOutTexture(Graphic toPlayWith)
    {
        while (toPlayWith.color.a != 1)
        {
            var nextAlpha = toPlayWith.color.a + Time.deltaTime * fadeFactor > 1
                ? 1
                : toPlayWith.color.a + Time.deltaTime * fadeFactor;
            toPlayWith.color = new Color(toPlayWith.color.r, toPlayWith.color.g, toPlayWith.color.b,
                nextAlpha);
            yield return null;
        }
    }

    IEnumerator ShowPressAnyKey()
    {
        yield return new WaitForSeconds(secondsToWaitForInput);
        _isReadyToReactToInput = true;
        while (true)
        {
            var nextAlpha = PressAnyKeyText.color.a > 0 ? 0 : 1;
            PressAnyKeyText.color = new Color(PressAnyKeyText.color.r, PressAnyKeyText.color.g, PressAnyKeyText.color.b,
                nextAlpha);
            yield return new WaitForSeconds(intervalBetweenPressAnyKeyText);
        }

    }
}
