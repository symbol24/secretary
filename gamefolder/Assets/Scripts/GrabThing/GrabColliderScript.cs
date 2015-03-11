using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class GrabColliderScript : MonoBehaviour
{
    public List<GrabbableScript> GrabbablesAvailable { get; set; }

    void Start()
    {
        GrabbablesAvailable = new List<GrabbableScript>();
    }

    public GrabbableScript GrabRandomObject(Transform ownerTransform)
    {
        GrabbableScript ret = null;
        if (IsThereAnything)
        {
            ret = GrabbablesAvailable.First(c => !c.IsGrabbed);
        }
        return ret;
    }

    public bool IsThereAnything { get { return GrabbablesAvailable.Any(c => !c.IsGrabbed); } }

    private void OnTriggerEnter(Collider coll)
    {
        var test = coll.gameObject.GetComponent<GrabbableScript>();
        if (test != null)
        {
            Debug.Log("Detected grabbableObject");
            if (!GrabbablesAvailable.Any(c => c.GetInstanceID() == test.GetInstanceID()))
            {
                GrabbablesAvailable.Add(test);
            }
        }
    }

    void OnTriggerExit(Collider coll)
    {
        var test = coll.gameObject.GetComponent<GrabbableScript>();
        if (test != null)
        {
            Debug.Log("grabbableObject leaving");
            if (GrabbablesAvailable.Any(c => c.GetInstanceID() == test.GetInstanceID()))
            {
                GrabbablesAvailable.Remove(test);
            }
        }
    }

}
