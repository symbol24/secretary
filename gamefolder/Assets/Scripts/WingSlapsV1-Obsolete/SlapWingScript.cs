using System;
using UnityEngine;
using System.Collections;

public class SlapWingScript : MonoBehaviour
{
    public Transform target;
    public GameObject emptyGameObject;
    public GameObject wingToUseHidden;

    public float speedGoingIn = 5f;
    public float timeBeforeGoingOut = 0.5f;
    public float speedGoingOut = 5f;

    private Transform originalTransform;

    public void Start()
    {
        if (wingToUseHidden == null) wingToUseHidden = gameObject;
        if (emptyGameObject == null) Debug.LogError("Missing EmptyGameObject prefab");
        originalTransform =
            ((GameObject)Instantiate(emptyGameObject, wingToUseHidden.transform.position, wingToUseHidden.transform.rotation)).transform;
        originalTransform.parent = wingToUseHidden.transform.parent;
        originalTransform.gameObject.name = Guid.NewGuid().ToString();
    }


    public void Slap()
    {
        if (!_IsStompingAsync)
        {
            StartCoroutine(StompAsync());
        }
    }

    private bool _IsStompingAsync = false;
    public IEnumerator StompAsync()
    {
        if (!_IsStompingAsync)
        {
            _IsStompingAsync = true;
            Debug.Log("startingRotation");
            while (wingToUseHidden.transform.rotation != target.rotation)
            {
                RotateTowards(target, speedGoingIn);
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("StartingtoWait");
            yield return new WaitForSeconds(timeBeforeGoingOut);
            Debug.Log("StartingReturn");
            while (wingToUseHidden.transform.rotation != originalTransform.rotation)
            {
                RotateTowards(originalTransform, speedGoingOut);
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("FinishedReturn");
            _IsStompingAsync = false;
        }
    }



    public void RotateTowards(Transform target, float speed)
    {
        var q = target.rotation;
        wingToUseHidden.transform.rotation = Quaternion.RotateTowards(wingToUseHidden.transform.rotation, q, speed * Time.deltaTime);
    }

}
