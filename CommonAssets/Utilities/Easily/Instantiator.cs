using System;
using UnityEngine;

internal class Instantiator
{
    internal static void Make(GameObject gameObject, Vector3 here)
    {
        if (gameObject != null)
        {
            GameObject.Instantiate(gameObject, here, Quaternion.identity);
        }
    }
}