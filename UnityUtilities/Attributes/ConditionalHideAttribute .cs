/*
  This script adapted from: http://www.brechtos.com/hiding-or-disabling-inspector-properties-using-propertydrawers-within-unity-5/
*/

using UnityEngine;
using System;
using System.Collections;
 
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
    AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class ConditionalHideAttribute : PropertyAttribute
{
    //The name of the bool field that will be in control
    public string[] ConditionalSourceFields = new string[] {""};
    //TRUE = Hide in inspector / FALSE = Disable in inspector 
    public bool HideInInspector = false;
 
    public ConditionalHideAttribute(params string[] conditionalSourceField)
    {
        this.ConditionalSourceFields = conditionalSourceField;
        this.HideInInspector = false;
    }
}