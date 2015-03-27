using UnityEngine;
using System.Collections;

public class ShowTextCollider : MonoBehaviour
{
    private bool _hasBeenActivated = false;

    public bool isOneTriggeredOneTimeOnly = true;
    public string title;
    public string instruction;
    public TextQueue textQueue;

    // Use this for initialization
    private void Start()
    {
        if (textQueue == null) textQueue = FindObjectOfType<TextQueue>();
        if (textQueue == null) Debug.LogError("There is no TextQueue in the scene");
    }

    // Update is called once per frame
    private void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("detected " + other.name);
        if(!_hasBeenActivated)
        {
            _hasBeenActivated = isOneTriggeredOneTimeOnly;
            var test = other.GetComponent<CharacterController>();
            if (test != null)
            {
                textQueue.Enqueue(new DialogueObject() {Text = instruction, Title = title});
            }
        }
    }
}
