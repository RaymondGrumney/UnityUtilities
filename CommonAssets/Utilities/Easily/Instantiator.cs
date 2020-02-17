using System;
using UnityEngine;

internal class Instantiator
{
    internal static GameObject Make(GameObject gameObject, Vector3 here)
    {
        if (gameObject != null)
        {
            return UnityEngine.Object.Instantiate(gameObject, here, Quaternion.identity);
        }
        else
        {
            throw new ArgumentNullException("gameObject cannot be Null.");
        }
    }
}