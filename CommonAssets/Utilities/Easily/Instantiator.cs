using System;
using UnityEngine;

internal class Instantiator
{
    internal static void Make(GameObject spawnOnImpact, Vector3 center)
    {
        if (spawnOnImpact != null)
        {
            GameObject.Instantiate(spawnOnImpact, center, Quaternion.identity);
        }
    }
}