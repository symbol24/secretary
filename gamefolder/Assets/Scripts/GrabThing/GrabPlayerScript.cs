using UnityEngine;
using System.Collections;

public class GrabPlayerScript : MonoBehaviour
{

    public GrabHandScript grabbingHand;

    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (grabbingHand.HasObjectGrabbed)
            {
                grabbingHand.DetachObject();
            }
            else
            {
                grabbingHand.AttemptToGrab();
            }
        }

    }
}
