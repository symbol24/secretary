using System;
using UnityEngine;
using System.Collections;

public class GrabbableScript : MonoBehaviour
{

    public float speed = 10f;
    public bool IsGrabbed { get; private set; }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AttachToGameObject(GameObject toAttach)
    {
        if (!IsGrabbed)
        {
            transform.parent = toAttach.transform;
            GetComponent<Rigidbody>().isKinematic = true;
            _coroutineToken = MoveToPosition();
            StartCoroutine(_coroutineToken);
            IsGrabbed = true;
        }
    }

    public void Unattach()
    {
        if (IsGrabbed)
        {
            transform.parent = null;
            GetComponent<Rigidbody>().isKinematic = false;
            IsGrabbed = false;
        }
    }

    private IEnumerator _coroutineToken;
    private IEnumerator MoveToPosition()
    {
        if (transform.parent != null)
        {
            while (transform.position != transform.parent.position && IsGrabbed)
            {
                var newPosition = Vector3.MoveTowards(transform.position, transform.parent.position, speed*Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(newPosition);
                yield return new WaitForEndOfFrame();
            }

        }
    }
}
