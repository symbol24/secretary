using System;
using UnityEngine;
using System.Collections;

public class ScreechingCollider : MonoBehaviour
{

    public EventHandler<ScreechableEventArgs> SqueakableFound;

    void OnTriggerEnter(Collider coll)
    {
        //Debug.Log("needSomechecking");
        if (SqueakableFound != null)
        {
            CallEvent(coll.gameObject);
        }
    }

    
    private void CallEvent(GameObject o)
    {
        if (SqueakableFound != null)
        {
            SqueakableFound(this, new ScreechableEventArgs() {ScreechableObject = o});
        }
    }
}
