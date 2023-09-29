// based on: https://www.studica.com/blog/custom-input-manager-unity-tutorial
 
using UnityEngine;
using System.Collections;
using Joypads = UnityUtilities.Joypads;
using UnityUtilities.Joypads.Presets;
using UnityUtilities.Utilities;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Joypad
{
    /// <summary>
    /// For defining button maps. Access via Input.Buttons
    /// </summary>
    public class Input : MonoBehaviour
    {
        /// <summary>
        /// Access the butons
        /// </summary>
        public static Input Buttons;

        public float Deadzone = 0.1f;


        /// <summary>
        /// Maps action names to joypad/keyboard inputs
        /// </summary>
        public ButtonMap Map = new ButtonMap();
        public string ConnectedJoypadName;

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

            Set();
        }

        private void Update() 
        {
            string[] js = UnityEngine.Input.GetJoystickNames();
            string j = "";
            foreach (string item in js)
            {
                if(!String.IsNullOrEmpty(item)) { j = item; break; }
            }

            if ( j != ConnectedJoypadName) 
            {
                Debug.Log(j);
                ConnectedJoypadName = j;
                switch (ConnectedJoypadName)
                {
                    case "Controller (XBOX 360 For Windows)":
                        Set(new XInputJoypad());
                        break;
                    default: break;
                }
            }
        }

        /// <summary>
        /// Sets each key to the player prefs or default
        /// </summary>
        public void Set()
        {
            Map = new ButtonMap
            {
                { "jump", KeyCode.Space.ToString() },
                { "attack", KeyCode.Return.ToString() },
                { "interact", KeyCode.Return.ToString() },
                { "magic", KeyCode.M.ToString() },
                { "item", KeyCode.RightShift.ToString() },
                { "shield", KeyCode.LeftShift.ToString() },
                { "menu", KeyCode.Escape.ToString() },
                { "menuconfirm", KeyCode.Return.ToString() },
                { "menucancel", KeyCode.Backspace.ToString() },
                { "menuexit", KeyCode.Escape.ToString() },
                { "up", KeyCode.W.ToString() },
                { "down", KeyCode.S.ToString() },
                { "left", KeyCode.A.ToString() },
                { "right", KeyCode.D.ToString() },
                { "cyclemagicleft", KeyCode.LeftArrow.ToString() },
                { "cyclemagicright", KeyCode.RightArrow.ToString() },
                { "item1", KeyCode.Alpha1.ToString() },
                { "item2", KeyCode.Alpha2.ToString() },
                { "item3", KeyCode.Alpha3.ToString() },
                { "item4", KeyCode.Alpha4.ToString() },
                { "item5", KeyCode.Alpha5.ToString() },
                { "item6", KeyCode.Alpha6.ToString() },
                { "item7", KeyCode.Alpha7.ToString() },
                { "item8", KeyCode.Alpha8.ToString() },
                { "item9", KeyCode.Alpha9.ToString() },
                { "item10", KeyCode.Alpha0.ToString() },
                { "quit", KeyCode.F12.ToString() }
            };
        }

        /// <summary>
        /// Sets each key to player prefs or preset default
        /// </summary>
        /// <param name="preset"></param>
        public void Set(JoypadPreset preset)
        {
            Map = new ButtonMap
            {
                { "jump", preset.jumpKey },
                { "attack", preset.attackKey },
                { "shield", preset.shieldKey },
                { "activate", preset.interactKey },
                { "magic", preset.magicKey },
                { "cyclemagicleft", preset.cyclemagicleft },
                { "cyclemagicright", preset.cyclemagicright },
                { "item", preset.itemKey },
                { "menu", preset.menuKey },
                { "menuconfirm", preset.menuConfirmKey },
                { "menucancel", preset.menuCancelKey },
                { "menuexit", preset.menuExitKey },
                { "up", preset.upKey },
                { "down", preset.downKey },
                { "left", preset.leftKey },
                { "right", preset.rightKey },
                { "quit", preset.quit }
            };
        }
    }

    [Serializable]
    public class Twople
    {
        public string Key;
        public string Value;

        public Twople() { }

        public Twople(string key, string value)
        {
            Key = key;
            Value = value;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return ((Twople)obj).Key == this.Key;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            throw new NotImplementedException();
            return base.GetHashCode();
        }
    }

    [Serializable]
    public class ButtonMap : IEnumerable
    {
        private readonly List<Tuple<string, string>> Opposites = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>("up", "down"),
            new Tuple<string, string>("left", "right")
        };

        private readonly List<string> CaresAboutOpposites = new List<string>()
        {
            "horizontal",
            "vertical",
            "6thAxis",
            "7thAxis",
            "YAxis",
            "XAxis"
        };
        

        [SerializeField] public List<Twople> Map;

        public ButtonMap()
        {
            Map = new List<Twople>();
        }

        public string this[string key]
        {
            get
            {
                foreach(Twople candidate in Map)
                {
                    if(candidate.Key == key)
                    {
                        return candidate.Value;
                    }
                }

                return null;
            }
            set
            {
                bool set = false;

                if (CaresAboutOpposites.Contains(key))
                {
                    foreach (Tuple<string, string> turtle in Opposites)
                    {
                        if (turtle.Item1 == key || turtle.Item2 == key)
                        {
                            if (Map.Contains(new Twople(turtle.Item1, value))) // Twople.equals checks key, not value
                            {
                                Map.Find(x => x.Key == turtle.Item1).Value = value;
                                set = true;
                            }

                            if (Map.Contains(new Twople(turtle.Item2, value))) // Twople.equals checks key, not value
                            {
                                Map.Find(x => x.Key == turtle.Item2).Value = value;
                                set = true;
                            }
                        }
                    }
                }
                else
                {
                    if (!set && Map.Contains(new Twople(key, value))) // Twople.equals checks key, not value
                    {
                        Map.Find(x => x.Key == key).Value = value;
                    }
                }
            }
        }


        public void Add(string key, string value)
        {
            Twople candidate = new Twople(key, value);

            if (!Map.Contains(candidate)) {
                Map.Add(candidate);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)Map).GetEnumerator();
        }
    }
}
