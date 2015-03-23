using UnityEngine;
using System.Collections;

public class GrabPlayerScript : MonoBehaviour
{

    //public GrabHandScript grabbingHand;
    public BeakAnimation_Script grabbingArm;
    private IController Controller;
    private IGrabDecorator grabDecorator;

    // Use this for initialization
    private void Start()
    {
        if(grabbingArm == null) Debug.LogError("NoGrabbingArm");
        Controller = GetComponent(typeof (IController)) as IController;
        grabDecorator = grabbingArm.GetComponent<IGrabDecorator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Controller.GrabAction())
        {
            if (grabDecorator.HasObjectGrabbed)
            {
                grabDecorator.DetachObject();
            }
            else
            {
                grabDecorator.AttemptToGrab();
            }
        }

    }
}
