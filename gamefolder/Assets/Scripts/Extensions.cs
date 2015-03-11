using UnityEngine;
using System.Collections;

public static class Extensions
{
    public static Vector3 Clone(this Vector3 vector)
    {
        return new Vector3(vector.x, vector.y, vector.z);
    }

}
