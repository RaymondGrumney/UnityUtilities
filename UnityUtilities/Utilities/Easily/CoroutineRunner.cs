using System;
using System.Collections;
using UnityEngine;

namespace UnityUtilities.Utilities
{
    public class CoroutineRunner : MonoBehaviour
    {
        public static void RunCoroutine(IEnumerator coroutine) 
        { 
            GameObject gObject = GameObject.Find("CoroutineRunner");
            CoroutineRunner runner;
            if (gObject is null)
            {
                gObject = Easily.Instantiate(new GameObject(), new Vector3());
                gObject.name = "CoroutineRunner";
                runner = gObject.AddComponent<CoroutineRunner>();
            }
            else
            {
                runner = gObject.GetComponent<CoroutineRunner>();
                if (runner is null)
                {
                    runner = gObject.AddComponent<CoroutineRunner>();
                }
            }

            runner.StartCoroutine(coroutine);   
        }
    }
}