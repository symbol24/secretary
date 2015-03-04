using UnityEditor;
using UnityEngine;
using System.Collections;

public class GrabHandScript : MonoBehaviour
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

    public void AttemptToGrab()
    {
        if (colliderToGetStuffFrom.IsThereAnything)
        {
            var grabbingObject = colliderToGetStuffFrom.GrabRandomObject(transform);
            grabbingObject.AttachToGameObject(gameObject);
            objectGrabbed = grabbingObject;
            //DoSomeAnimation
        }
        else
        {
            Debug.Log("NothingToGrab");
        }
    }


    public void DetachObject()
    {
        objectGrabbed.Unattach();
        objectGrabbed = null;
    }
}
