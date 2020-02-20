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
    public class History : MonoBehaviour
    {
        public static List<string> _history;

        public static List<string> Buttons => _history;
        public static History Of;
        private const int HISTORY_LIMIT = 10;

        public History()
        {
            _history = new List<string>();
        }

        void Awake()
        {
            //Singleton pattern
            if (Buttons == null)
            {
                DontDestroyOnLoad(gameObject);
                Of = this;
            }
            else if (Of != this)
            {
                Destroy(gameObject);
            }
        }

        private void FixedUpdate()
        {
            foreach(Twople input in Joypad.Input.Buttons.Map)
            {
                if(Joypad.Read.Buttons.Pressed(input.Key) 
                    && ( input.Key.ToLower() != "horizontal" || input.Key.ToLower() != "vertical" ))
                {
                    _history.Add( input.Key );

                    while ( _history.Count >= HISTORY_LIMIT )
                    {
                        _history.RemoveAt(0);
                    }
                }
            }
        }
    }
}