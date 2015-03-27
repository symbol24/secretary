using UnityEngine;
using System.Collections;

public class GrabHandScript : MonoBehaviour, IGrabDecorator
{
    public GrabColliderScript colliderToGetStuffFrom;
    public Transform transformToKeepObject; //Where the object grab is gonna stay
    
    private GrabbableScript _objectGrabbed;
    public GrabbableScript ObjectGrabbed
    {
        get { return _objectGrabbed; }
        private set
        {
            _objectGrabbed = value;
            colliderToGetStuffFrom.ForceNotShowCollider = _objectGrabbed != null;
        }
    }
    public bool HasObjectGrabbed { get { return ObjectGrabbed != null; } }

	// Use this for initialization
	void Start ()
	{
	    if (colliderToGetStuffFrom == null) colliderToGetStuffFrom = GetComponent<GrabColliderScript>();
	    if (transformToKeepObject == null) transformToKeepObject = transform;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool AttemptToGrab()
    {
        if (colliderToGetStuffFrom.IsThereAnything)
        {
            var grabbingObject = colliderToGetStuffFrom.GrabRandomObject(transform);
            grabbingObject.AttachToGameObject(gameObject);
            ObjectGrabbed = grabbingObject;
            return true;
        }
        else
        {
            Debug.Log("NothingToGrab");
            return false;
        }
    }


    public void DetachObject()
    {
        if (HasObjectGrabbed)
        {
            ObjectGrabbed.Unattach();
            ObjectGrabbed = null;
        }
    }
}
