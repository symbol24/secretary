using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class TextQueue : MonoBehaviour
{
    private Queue<DialogueObject> _queue = new Queue<DialogueObject>();
    private Queue<DialogueObject> Queue
    {
        get { return _queue; }
        set { _queue = value; }
    }

    public float intervalToCheckQueue = 0.5f;
    public float intervalPerLetter = 0.05f;
    public float intervalToWaitWhenCompleted = 3f;
    public ShowText showText;


    // Use this for initialization
    private void Start()
    {
        currentRoutineCheckingQueue = CheckForQueue();
        StartCoroutine(currentRoutineCheckingQueue);
        if (showText == null) GetComponent<ShowText>();
        if (showText == null) Debug.LogError("NoShowText component in " + gameObject.name);
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void Enqueue(string text)
    {
        Queue.Enqueue(new DialogueObject{Text = text});
    }

    public void Enqueue(DialogueObject text)
    {
        Queue.Enqueue(text);
    }

    private IEnumerator currentRoutineCheckingQueue;
    private IEnumerator CheckForQueue()
    {
        while (true)
        {
            if (Queue.Any())
            {
                StartCoroutine(StartWritingText(Queue.Dequeue()));
                StopCoroutine(currentRoutineCheckingQueue);
            }
            yield return new WaitForSeconds(intervalToCheckQueue);
        }
    }

    private IEnumerator StartWritingText(DialogueObject dialogueObject)
    {
        showText.enabled = true;
        string currentActiveString = string.Empty;
        showText.Title = dialogueObject.Title;
        var toWrite = dialogueObject.Text;
        while (currentActiveString.Length < toWrite.Length)
        {
            currentActiveString = toWrite.Substring(0, currentActiveString.Length + 1);
            showText.TextToShow = currentActiveString;
            yield return new WaitForSeconds(intervalPerLetter);
        }
        yield return new WaitForSeconds(intervalToWaitWhenCompleted);
        showText.enabled = false;
        showText.TextToShow = string.Empty;
        currentRoutineCheckingQueue = CheckForQueue();
        StartCoroutine(currentRoutineCheckingQueue);
    }

}
