using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DestroyOnOtherScriptTrigger : MonoBehaviour
{
    public MonoScript DestructiveScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent(DestructiveScript.name))
        {
            Destroy(this.gameObject);
        }
    }
}
