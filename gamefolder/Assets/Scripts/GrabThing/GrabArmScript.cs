using UnityEngine;
using System.Collections;

public class GrabArmScript : MonoBehaviour, IGrabDecorator
{
    public GrabHandScript grabbingHand;

    public Transform rotationToReach;
    private Transform originalTransform;
    public GameObject emptyGameObject;
    public float inWardsSpeed = 10f;
    public float outWardsSpeed = 10f;

    public bool HasObjectGrabbed
    {
        get { return grabbingHand.HasObjectGrabbed; }
    }

    // Use this for initialization
    private void Start()
    {
        if (grabbingHand == null) grabbingHand = GetComponentInChildren<GrabHandScript>();
        if (grabbingHand == null) Debug.LogError("NoHandScript");
        originalTransform =
            ((GameObject)Instantiate(emptyGameObject, transform.position, transform.rotation)).transform;
        originalTransform.parent = transform.parent;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void DetachObject()
    {
        grabbingHand.DetachObject();
    }

    public bool AttemptToGrab()
    {
        if (!_grabbing)
        {
            _grabbing = true;
            StartCoroutine(AttemptToGrabCoroutine());
        }
        return true;
    }

    private bool _grabbing = false;
    public IEnumerator AttemptToGrabCoroutine()
    {
        _grabbing = true;

        var originalCameraParent = Camera.main.transform.parent;
        Camera.main.transform.parent = grabbingHand.transform;
        while (rotationToReach.localRotation != transform.localRotation)
        {
            RotateTowards(rotationToReach, inWardsSpeed);
            yield return null;
        }
        grabbingHand.AttemptToGrab();
        while (originalTransform.localRotation != transform.localRotation)
        {
            RotateTowards(originalTransform, outWardsSpeed);
            yield return null;
        }
        Camera.main.transform.parent = originalCameraParent;
        yield return null;
        _grabbing = false;
    }

    public void RotateTowards(Transform target, float speed)
    {
        var q = target.localRotation;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, q, speed * Time.deltaTime);
    }

}
