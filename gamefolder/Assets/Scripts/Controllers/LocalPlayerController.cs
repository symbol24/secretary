using UnityEngine;
using System.Collections;

public class LocalPlayerController : MonoBehaviour, IController
{
    public bool GetLeftWingDown()
    {
        return Input.GetMouseButtonDown(1);
    }

    public bool GetRightWingDown()
    {
        return Input.GetMouseButtonDown(0);
    }

    public bool Squeak()
    {
        return Input.GetKeyDown(KeyCode.F);
    }

}
