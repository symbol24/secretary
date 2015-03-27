using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ColliderPlacer : MonoBehaviour
{
    public GameObject CollidertoMove;
    public GameObject ForwardReference;
    private GrabColliderScript colliderScript;
    private Rigidbody colliderRigidBody;
    private int colliderInstanceID;
    public float maxReach = 1f;
    public float timerToRetreat = 0.1f;
    private float currentTimerToRetreat = 0f;
    private Vector3 initialPosition;
    public bool enableOrderBy = false;

    // Use this for initialization
    private void Start()
    {
        colliderScript = CollidertoMove.GetComponent<GrabColliderScript>();
        if (colliderScript == null) colliderScript = CollidertoMove.GetComponentInChildren<GrabColliderScript>();
        if (colliderScript == null) Debug.LogError("NoCollider Attached to " + gameObject.name);
        colliderRigidBody = colliderScript.gameObject.GetComponent<Rigidbody>();
        colliderInstanceID = colliderScript.gameObject.GetInstanceID();
        initialPosition = CollidertoMove.transform.localPosition.Clone();
    }

    // Update is called once per frame
    private void Update()
    {
        var rayTocheckCollision = new Ray(ForwardReference.transform.position,
            ForwardReference.transform.TransformDirection(Vector3.forward));

        var rayCastResults =
            Physics.RaycastAll(rayTocheckCollision, maxReach)
                .Where(c => c.collider.gameObject.GetInstanceID() != colliderInstanceID);
        if (rayCastResults.Any())
        {
            Debug.DrawRay(ForwardReference.transform.position,
                ForwardReference.transform.TransformDirection(Vector3.forward), Color.red, Time.deltaTime);
            var chosenResult = ChooseObjectProperly(rayCastResults);

            CollidertoMove.transform.position = chosenResult.point;
            CollidertoMove.transform.rotation = Quaternion.Euler(chosenResult.normal);
            currentTimerToRetreat = 0f;
        }
        else
        {
            currentTimerToRetreat += Time.deltaTime;
            if (currentTimerToRetreat > timerToRetreat)
            {
                CollidertoMove.transform.localPosition = initialPosition ;
            }
        }
    }

    private RaycastHit ChooseObjectProperly(IEnumerable<RaycastHit> rayCastResults)
    {
        RaycastHit ret;
        if (enableOrderBy)
        {
            if (rayCastResults.Any(c => c.transform.gameObject.GetComponent<GrabbableScript>() != null))
            {
                ret =
                    rayCastResults.OrderBy(c => c.distance)
                        .First(c => c.transform.gameObject.GetComponent<GrabbableScript>() != null);
            }
            else ret = rayCastResults.OrderBy(c => c.distance).First();
        }
        else
        {
            if (rayCastResults.Any(c => c.transform.gameObject.GetComponent<GrabbableScript>() != null))
            {
                ret = rayCastResults.First(c => c.transform.gameObject.GetComponent<GrabbableScript>() != null);
            }
            else
            {
                ret = rayCastResults.First();
            }
        }
        return ret;
    }
}
