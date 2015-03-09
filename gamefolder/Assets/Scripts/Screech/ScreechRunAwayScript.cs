using UnityEngine;
using System.Collections;

public class ScreechRunAwayScript : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }


    void OnTriggerEnter(Collider coll)
    {
        var test = coll.GetComponent<ScreechingCollider>();
        if (test != null)
        {
            //RunAway
            Debug.Log("RunAway");
        }
    }

}
