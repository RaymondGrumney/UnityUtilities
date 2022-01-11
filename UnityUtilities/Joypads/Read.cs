// based on: https://www.studica.com/blog/custom-input-manager-unity-tutorial

using UnityUtilities.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Joypad
{
    [Serializable]
    public class Boolple : MonoBehaviour
    {
        [SerializeField] public string Input;
        [SerializeField] public bool State;

        public Boolple() { }

        public Boolple(string key)
        {
            Input = key;
            State = false;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return ((Boolple)obj).Input == this.Input;
        }

        public void Update()
        {
            State = Joypad.Read.Buttons.Held(Input);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            throw new NotImplementedException();
            return base.GetHashCode();
        }
    }

    /// <summary>
    /// Provides access to buttons as configured in Player Prefs
    /// </summary>
    public class Read : MonoBehaviour
    {
        public static Read Buttons;
        public Dictionary<string,bool> LastFrameState = new Dictionary<string, bool>();

        // TODO: Make button states visible
        // [SerializeField] public List<Boolple> ButtonStates;

        public Read()
        {
        }

        void Awake()
        {
            //Singleton pattern
            if (Buttons == null)
            {
                DontDestroyOnLoad(gameObject);
                Buttons = this;
            }
            else if (Buttons != this)
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// If the button / axis is currently being held
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool Held(string action)
            => AxesInput(action.ToLower()) ?? UnityEngine.Input.GetKey(Easily.Parse<KeyCode>(Input.Buttons.Map[action.ToLower()]));
             

        /// <summary>
        /// If the button was pressed this frame.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool Pressed(string action)
        {
            bool? b = AxesInput(action.ToLower());
            if (b is null)
            {
                return UnityEngine.Input.GetKeyDown(Easily.Parse<KeyCode>(Input.Buttons.Map[action.ToLower()]));
            }
            else
            {
                if(!LastFrameState.ContainsKey(action))
                {
                    LastFrameState.Add(action, true);
                    return true;
                }
                else
                {
                    if(!LastFrameState[action] && (bool)b)
                    {
                        LastFrameState[action] = true;
                        return true;
                    }
                    else return false;
                }
            }
        }

        /// <summary>
        /// if the button was released this frame.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool Released(string action)
        {
            bool? b = AxesInput(action.ToLower());
            if(b is null)
            {
                return UnityEngine.Input.GetKeyUp(Easily.Parse<KeyCode>(Input.Buttons.Map[action.ToLower()]));
            }
            else
            {
                if(LastFrameState[action] && !(bool)b)
                {
                    LastFrameState[action] = false;
                    return true;
                }
                else return false;
            }
        }

        /// <summary>
        /// Returns true or false if getting input from an Axis, or null if it isn't one of those
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool? AxesInput(string action)
        {
            string a = Input.Buttons.Map[action];
            if (!String.IsNullOrEmpty(a))
            {
                string i = Input.Buttons.Map[action].ToString().ToLower();
                switch (i)
                {
                    case "up": return UnityEngine.Input.GetAxis("vertical") > Joypad.Input.Buttons.Deadzone;
                    case "down": return UnityEngine.Input.GetAxis("vertical") < -Joypad.Input.Buttons.Deadzone;
                    case "vertical": return UnityEngine.Input.GetAxis("vertical") > Joypad.Input.Buttons.Deadzone;
                    case "right": return UnityEngine.Input.GetAxis("horizontal") > Joypad.Input.Buttons.Deadzone;
                    case "left": return UnityEngine.Input.GetAxis("horizontal") < -Joypad.Input.Buttons.Deadzone;
                    case "horizontal": return UnityEngine.Input.GetAxis("horizontal") > Joypad.Input.Buttons.Deadzone;
                    case "righttrigger":
                    case "lefttrigger":
                        float f = UnityEngine.Input.GetAxis(i);
                        Debug.Log($"{i}: {f}");
                        return UnityEngine.Input.GetAxis(i) > Joypad.Input.Buttons.Deadzone;
                    default:
                        // TODO: Handle Axises better. Store which is negative
                        if (i.Contains("axis"))
                        {
                            switch (action)
                            {
                                case "left": return UnityEngine.Input.GetAxis(Input.Buttons.Map[action]) < -Joypad.Input.Buttons.Deadzone;
                                case "down": return UnityEngine.Input.GetAxis(Input.Buttons.Map[action]) < -Joypad.Input.Buttons.Deadzone;
                                case "up": return UnityEngine.Input.GetAxis(Input.Buttons.Map[action]) > Joypad.Input.Buttons.Deadzone;
                                case "right": return UnityEngine.Input.GetAxis(Input.Buttons.Map[action]) > Joypad.Input.Buttons.Deadzone;
                                default: break;
                            }
                        }
                        break;
                }
            }
            return null;
        }

        public float horizontal => Held("right") ? 1f : Held("left") ? -1 : 0;
        public float vertical => Held("up") ? 1f : Held("down") ? -1 : 0;
    }

}