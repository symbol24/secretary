using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class Gustable : MonoBehaviour
{
    private List<int> gustCollidersGoneThrough;
    private Rigidbody ownRigidBody;
    public float inmunitySeconds = 4f;
    // Use this for initialization
    private void Start()
    {
        ownRigidBody = GetComponent<Rigidbody>();
        gustCollidersGoneThrough = new List<int>();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void OnTriggerEnter(Collider coll)
    {
        var test = coll.gameObject.GetComponent<GustCollider>();
        if (test != null && !gustCollidersGoneThrough.Any(c => c == test.GetInstanceID()))
        {
            var source = transform.position - test.SourceOfGust.transform.position;
            var vectorToUse = (source + Vector3.up).normalized;
            ownRigidBody.AddForce(vectorToUse * test.ForceToUse/source.magnitude, ForceMode.Impulse);
            int instanceID = test.GetInstanceID();
            gustCollidersGoneThrough.Add(instanceID);
            StartCoroutine(RemoveInstanceAfter(inmunitySeconds, instanceID));
        }
    }

    private IEnumerator RemoveInstanceAfter(float seconds, int toRemove)
    {
        yield return new WaitForSeconds(seconds);
        if (gustCollidersGoneThrough.Any(c => c == toRemove))
        {
            gustCollidersGoneThrough.Remove(toRemove);
        }
    }
}
