using UnityEngine;
using System.Collections;

public class BeakAnimation_Script : MonoBehaviour, IGrabDecorator
{
    public GrabHandScript grabbingScript;
    private IGrabDecorator internalGrabbing;
    public GameObject BeakToMove;
    public GrabColliderScript CollidertoGoTo;
    public float speed = 400f;
    public float speedBackwards = 5f;
    public float distanceCorrection = -0.5f;
    public float distanceToKeepAfterGrab = 1f;

    public void Start()
    {
        internalGrabbing = grabbingScript;
    }

    public bool HasObjectGrabbed { get { return grabbingScript.HasObjectGrabbed || _isGrabbingInProgress; } }
    public void DetachObject()
    {
        if (HasObjectGrabbed)
        {
            StartCoroutine(DetachingCoroutine());
        }
    }

    public bool AttemptToGrab()
    {
        if (!_isGrabbingInProgress && !HasObjectGrabbed)
        {
            Debug.Log("Attmepting to Grab in GrabAnimation");
            StartCoroutine(GrabbingCoroutine());
        }
        return true;
    }

    private bool _isGrabbingInProgress = false;
    private Vector3 originalPosition;

    IEnumerator GrabbingCoroutine()
    {
        _isGrabbingInProgress = true;
        var distanceBetweenBeakAndCollider = Vector3.Distance(BeakToMove.transform.position,
            CollidertoGoTo.transform.position) + distanceCorrection;
        var vectorToTest = CollidertoGoTo.gameObject.transform.position;
        vectorToTest = BeakToMove.transform.InverseTransformPoint(vectorToTest);
        var positionTogoTo = vectorToTest.normalized * distanceBetweenBeakAndCollider;
        
        originalPosition = BeakToMove.transform.localPosition.Clone();
        while (positionTogoTo != BeakToMove.transform.localPosition)
        {
            BeakToMove.transform.localPosition = Vector3.MoveTowards(BeakToMove.transform.localPosition, positionTogoTo,
                speed*Time.deltaTime);
            yield return null;
        }
        if (!internalGrabbing.AttemptToGrab())
        {
            _isDetachingInProgress = true;
            while (_isDetachingInProgress)
            {
                StartCoroutine(DetachingCoroutine());
                yield return null;
            }
        }
        else
        {

            while (Vector3.Distance(BeakToMove.transform.localPosition, originalPosition) >= distanceToKeepAfterGrab)
            {
                BeakToMove.transform.localPosition = Vector3.MoveTowards(BeakToMove.transform.localPosition, originalPosition,
                    speedBackwards * Time.deltaTime);
                grabbingScript.ObjectGrabbed.transform.localPosition = Vector3.MoveTowards(grabbingScript.ObjectGrabbed.transform.localPosition, originalPosition,
                    speedBackwards * Time.deltaTime);
                yield return null;
            }

            _isGrabbingInProgress = false;
        }
        
        
    }

    private bool _isDetachingInProgress = false;
    IEnumerator DetachingCoroutine()
    {
        _isDetachingInProgress = true;
        grabbingScript.DetachObject();

        while (BeakToMove.transform.localPosition != originalPosition)
        {
            BeakToMove.transform.localPosition = Vector3.MoveTowards(BeakToMove.transform.localPosition, originalPosition,
                speedBackwards * Time.deltaTime);
            yield return null;
        }
        _isDetachingInProgress = false;
        _isGrabbingInProgress = false;
    }
}
