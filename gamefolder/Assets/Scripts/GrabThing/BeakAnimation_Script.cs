using UnityEngine;
using System.Collections;

public class BeakAnimation_Script : MonoBehaviour, IGrabDecorator
{
    public GrabHandScript grabbingScript;
    private IGrabDecorator internalGrabbing;
    public GameObject BeakToMove;
    public GrabColliderScript CollidertoGoTo;
    public float speed = 400f;

    public void Start()
    {
        internalGrabbing = grabbingScript.GetComponent<IGrabDecorator>();
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
        var positionTogoTo = CollidertoGoTo.gameObject.transform.localPosition.Clone();
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
                speed * Time.deltaTime);
            yield return null;
        }
        _isDetachingInProgress = false;
        _isGrabbingInProgress = false;
    }
}
