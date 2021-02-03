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

            // ButtonStates = new List<Boolple>();

            //foreach (Twople button in Joypad.Input.Buttons.Map.Map)
            //{
            //    ButtonStates.Add(new Boolple(button.Key));
            //}
        }

        /// <summary>
        /// If the button / axis is currently being held
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool Held(string action)
        {
            return AxesInput(action) ?? UnityEngine.Input.GetKey(Easily.Parse<KeyCode>(Input.Buttons.Map[action]));
        }

        /// <summary>
        /// If the button was pressed this frame. For Axes, will return if the direction is currently being pressed.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool Pressed(string action)
        {
            return AxesInput(action) ?? UnityEngine.Input.GetKeyDown(Easily.Parse<KeyCode>(Input.Buttons.Map[action]));
        }

        /// <summary>
        /// if the button was released this frame. For Axes, will return if the direction is currently being pressed.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool Released(string action)
        {
            return AxesInput(action) ?? UnityEngine.Input.GetKeyUp(Easily.Parse<KeyCode>(Input.Buttons.Map[action]));
        }

        /// <summary>
        /// Returns true or false if getting input from an Axis, or null if it isn't one of those
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool? AxesInput(string action)
        {
            // TODO: Handle Axises better. Store which is negative
            if (Input.Buttons.Map[action].ToString().ToLower() == ("vertical"))
            {
                if (action == "up")
                {
                    return UnityEngine.Input.GetAxis("vertical") > Joypad.Input.Buttons.Deadzone;
                }
                else if (action == "down")
                {
                    return UnityEngine.Input.GetAxis("vertical") < -Joypad.Input.Buttons.Deadzone;
                }
                else // default to positive
                {
                    return UnityEngine.Input.GetAxis("vertical") > Joypad.Input.Buttons.Deadzone;
                }
            }
            else if (Input.Buttons.Map[action].ToString().ToLower() == "horizontal")
            {
                if (action == "right")
                {
                    return UnityEngine.Input.GetAxis("horizontal") > Joypad.Input.Buttons.Deadzone;
                }
                else if (action == "left")
                {
                    return UnityEngine.Input.GetAxis("horizontal") < -Joypad.Input.Buttons.Deadzone;
                }
                else
                {
                    return UnityEngine.Input.GetAxis("horizontal") > Joypad.Input.Buttons.Deadzone;
                }
            }
            else if (Input.Buttons.Map[action].ToString().ToLower().Contains("axis"))
            {
                if (action == "left" || action == "down")
                {
                    return UnityEngine.Input.GetAxis(Input.Buttons.Map[action]) < -Joypad.Input.Buttons.Deadzone;
                }
                else // default to positive
                {
                    return UnityEngine.Input.GetAxis(Input.Buttons.Map[action]) > Joypad.Input.Buttons.Deadzone;
                }
            }
            else
            {
                return null;
            }
        }

        public float horizontal => Held("right") ? 1f : Held("left") ? -1 : 0;
        //public float horizontal => UnityEngine.Input.GetAxis(Joypad.Input.Buttons.horizontal);
        public float vertical => Held("up") ? 1f : Held( "down" ) ? -1 : 0;
        //public float vertical => UnityEngine.Input.GetAxis(Joypad.Input.Buttons.vertical);
    }

}