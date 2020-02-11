// based on: https://www.studica.com/blog/custom-input-manager-unity-tutorial

using System;
using System.Collections;
using System.Collections.Generic;

namespace Joypad
{
    /// <summary>
    /// Provides access to buttons as configured in Player Prefs
    /// </summary>
    public class Read
    {
        // TODO: custom naming via an array. not all games will have jump, attack, magic, etc. as standard actions

        public static Read Buttons;

        public double deadzone { get; set; } = 0.1;


        /// <summary>
        /// If the button / axis is currently being held
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool Held(string action)
        {
            return AxesInput(action) ?? UnityEngine.Input.GetKey(Input.Buttons.Map[action]);
        }

        /// <summary>
        /// If the button was pressed this frame. For Axes, will return if the direction is currently being pressed.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool Pressed(string action)
        {
            return AxesInput(action) ?? UnityEngine.Input.GetKeyDown(Input.Buttons.Map[action]);
        }

        /// <summary>
        /// if the button was released this frame. For Axes, will return if the direction is currently being pressed.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool Released(string action)
        {
            return AxesInput(action) ?? UnityEngine.Input.GetKeyUp(Input.Buttons.Map[action]);
        }

        /// <summary>
        /// Returns true or false if getting input from an Axis, or null if it isn't one of those
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool? AxesInput(string action)
        {
            // TODO: Handle Axises better. Store which is negative
            if (Input.Buttons.Map[action] == ("vertical"))
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
            else if (Input.Buttons.Map[action] == "horizontal")
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
            else if (Input.Buttons.Map[action].ToLower().Contains("axis"))
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

        public bool jump => 
            UnityEngine.Input.GetKey(Joypad.Input.Buttons.jump);
        public bool attack => 
            UnityEngine.Input.GetKey(Joypad.Input.Buttons.attack);
        public bool magic => 
            UnityEngine.Input.GetKey(Joypad.Input.Buttons.magic);
        public bool item => 
            UnityEngine.Input.GetKey(Joypad.Input.Buttons.item);
        public bool menu =>
            UnityEngine.Input.GetKey(Joypad.Input.Buttons.menu);
        public bool menuConfirm =>
            UnityEngine.Input.GetKey(Joypad.Input.Buttons.menuConfirm);
        public bool menuCancel =>
            UnityEngine.Input.GetKey(Joypad.Input.Buttons.menuCancel);
        public bool menuExit =>
            UnityEngine.Input.GetKey(Joypad.Input.Buttons.menuExit);
        public bool up =>
            !down // down overrides up
            && ( UnityEngine.Input.GetKey(Joypad.Input.Buttons.up) 
            || horizontal > deadzone );
        public bool down => 
            UnityEngine.Input.GetKey(Joypad.Input.Buttons.down)
            || horizontal < -deadzone;
        public bool right =>
            UnityEngine.Input.GetKey(Joypad.Input.Buttons.right)
            || vertical > deadzone;
        public bool left => 
            !right // right overrides left
            && ( UnityEngine.Input.GetKey(Joypad.Input.Buttons.left)
            || vertical < -deadzone );

        public float horizontal => UnityEngine.Input.GetAxis(Joypad.Input.Buttons.horizontal);
        public float vertical => UnityEngine.Input.GetAxis(Joypad.Input.Buttons.vertical);
    }

}