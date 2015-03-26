using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class GrabColliderScript : MonoBehaviour
{
    public List<GrabbableScript> GrabbablesAvailable { get; set; }
    public List<GameObject> ShowCollider;
    public bool enableShowCollider = false;
    public float inactiveTimeToDisableGameObjects = 0.05f;
    public bool ForceNotShowCollider = false;

    void Start()
    {
        GrabbablesAvailable = new List<GrabbableScript>();
        SetObjectsActive(enableShowCollider);
    }

    void Update()
    {
        
        if (enableShowCollider)
        {
            if (ForceNotShowCollider)
            {
                SetObjectsActive(false);
            }
            else if (GrabbablesAvailable.Any())
            {
                SetObjectsActive(true);
            }
            else
            {
                SetObjectsActive(false);
            }
        }
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

    private void SetObjectsActive(bool active)
    {
        if (!_isSetObjectsActiveRunning || active)
        {
            if(_isSetObjectsActiveRunning) StopCoroutine(currentSetObjectsActiveRunning);
            _isSetObjectsActiveRunning = true;
            currentSetObjectsActiveRunning = SetObjectsActiveCoroutine(active);
            StartCoroutine(currentSetObjectsActiveRunning);
        }
    }

    private bool _isSetObjectsActiveRunning = false;
    private IEnumerator currentSetObjectsActiveRunning;
    private IEnumerator SetObjectsActiveCoroutine(bool active)
    {
        _isSetObjectsActiveRunning = true;
        var currentTimer = 0f;
        if (!active)
        {
            while (currentTimer < inactiveTimeToDisableGameObjects)
            {
                currentTimer += Time.deltaTime;
                yield return null;
            }
        }

        for (int i = 0; i < ShowCollider.Count; i++)
        {
            var o = ShowCollider[i];
            o.SetActive(active);
        }

        _isSetObjectsActiveRunning = false;
    }
}
