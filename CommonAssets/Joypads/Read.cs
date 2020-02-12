// based on: https://www.studica.com/blog/custom-input-manager-unity-tutorial

using CommonAssets.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Joypad
{
    /// <summary>
    /// Provides access to buttons as configured in Player Prefs
    /// </summary>
    public class Read : MonoBehaviour
    {
        // TODO: custom naming via an array. not all games will have jump, attack, magic, etc. as standard actions

        public static Read Buttons;

        public float deadzone { get; set; } = 0.1f;

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
                    return UnityEngine.Input.GetAxis("vertical") > deadzone;
                }
                else if (action == "down")
                {
                    return UnityEngine.Input.GetAxis("vertical") < -deadzone;
                }
                else // default to positive
                {
                    return UnityEngine.Input.GetAxis("vertical") > deadzone;
                }
            }
            else if (Input.Buttons.Map[action].ToString().ToLower() == "horizontal")
            {
                if (action == "right")
                {
                    return UnityEngine.Input.GetAxis("horizontal") > deadzone;
                }
                else if (action == "left")
                {
                    return UnityEngine.Input.GetAxis("horizontal") < -deadzone;
                }
                else
                {
                    return UnityEngine.Input.GetAxis("horizontal") > deadzone;
                }
            }
            else if (Input.Buttons.Map[action].ToString().ToLower().Contains("axis"))
            {
                if (action == "left" || action == "down")
                {
                    return UnityEngine.Input.GetAxis(Input.Buttons.Map[action]) < -deadzone;
                }
                else // default to positive
                {
                    return UnityEngine.Input.GetAxis(Input.Buttons.Map[action]) > deadzone;
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