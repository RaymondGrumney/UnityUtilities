using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DestroyOnOtherScriptTrigger : MonoBehaviour
{
    public string DestructiveScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent(DestructiveScript))
        {
            Destroy(this.gameObject);
        }
    }
}
