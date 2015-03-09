using UnityEngine;
using System.Collections;

public class GrabPlayerScript : MonoBehaviour
{

    //public GrabHandScript grabbingHand;
    public GrabArmScript grabbingArm;
    private IController Controller;

    // Use this for initialization
    private void Start()
    {
        if(grabbingArm == null) Debug.LogError("NoGrabbingArm");
        Controller = GetComponent(typeof (IController)) as IController;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Controller.GrabAction())
        {
            if (grabbingArm.HasObjectGrabbed)
            {
                grabbingArm.DetachObject();
            }
            else
            {
                grabbingArm.AttemptToGrab();
            }
        }

    }
}
