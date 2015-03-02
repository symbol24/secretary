using System;
using UnityEngine;
using System.Collections;

public class SquakingCollider : MonoBehaviour
{

    public EventHandler<SqueakableEventArgs> SqueakableFound;

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log("needSomechecking");
        if (SqueakableFound != null)
        {
            CallEvent(coll.gameObject);
        }
    }

    
    private void CallEvent(GameObject o)
    {
        if (SqueakableFound != null)
        {
            SqueakableFound(this, new SqueakableEventArgs() {SqueakableObject = o});
        }
    }
}
