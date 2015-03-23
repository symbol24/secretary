using UnityEditor;
using UnityEngine;
using System.Collections;

public class GrabHandScript : MonoBehaviour, IGrabDecorator
{
    public GrabColliderScript colliderToGetStuffFrom;
    public Transform transformToKeepObject; //Where the object grab is gonna stay

    private GrabbableScript objectGrabbed;
    public bool HasObjectGrabbed { get { return objectGrabbed != null; } }

	// Use this for initialization
	void Start ()
	{
	    if (colliderToGetStuffFrom == null) colliderToGetStuffFrom = GetComponent<GrabColliderScript>();
	    if (colliderToGetStuffFrom == null && EditorApplication.isPlaying)
	    {
	        Debug.LogError("NoBoxCollider attached");
            EditorApplication.isPlaying = false;
	    }
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
            objectGrabbed = grabbingObject;
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
            objectGrabbed.Unattach();
            objectGrabbed = null;
        }
    }
}
