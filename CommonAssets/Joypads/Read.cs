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
        /// Maps action names to joypad/keyboard inputs
        /// </summary>
        ButtonMap Map { get; } = new ButtonMap();

        public bool Input(string action)
        {
            // TODO: Handle Axises better. Store which is negative
            if (Map[action] == ("vertical"))
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
            else if (Map[action] == "horizontal")
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
            else if (Map[action].ToLower().Contains("axis"))
            {
                if ( action == "left"  || action == "down" )
                {
                    return UnityEngine.Input.GetAxis(Map[action]) < -deadzone;
                }
                else // default to positive
                {
                    return UnityEngine.Input.GetAxis(Map[action]) > deadzone;
                }
            }
            else
            {
                return UnityEngine.Input.GetKey(Map[action]);
            }
        }

        public bool jump => 
            UnityEngine.Input.GetKey(Joypad.Input.Button.jump);
        public bool attack => 
            UnityEngine.Input.GetKey(Joypad.Input.Button.attack);
        public bool magic => 
            UnityEngine.Input.GetKey(Joypad.Input.Button.magic);
        public bool item => 
            UnityEngine.Input.GetKey(Joypad.Input.Button.item);
        public bool menu => 
            UnityEngine.Input.GetKey(Joypad.Input.Button.menu);
        public bool up =>
            !down // down overrides up
            && ( UnityEngine.Input.GetKey(Joypad.Input.Button.up) 
            || horizontal > deadzone );
        public bool down => 
            UnityEngine.Input.GetKey(Joypad.Input.Button.down)
            || horizontal < -deadzone;
        public bool right =>
            UnityEngine.Input.GetKey(Joypad.Input.Button.right)
            || vertical > deadzone;
        public bool left => 
            !right // right overrides left
            && ( UnityEngine.Input.GetKey(Joypad.Input.Button.left)
            || vertical < -deadzone );

        public double horizontal => UnityEngine.Input.GetAxis(Joypad.Input.Button.horizontal);
        public double vertical => UnityEngine.Input.GetAxis(Joypad.Input.Button.vertical);
    }

    class ButtonMap : IDictionary<string, string>
    {
        private List<Tuple<string, string>> Opposites = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>("up", "down"),
            new Tuple<string, string>("left", "right")
        };

        private List<string> CaresAboutOpposites = new List<string>()
        {
            "horizontal",
            "vertical"
        };

        private Dictionary<string, string> Map;

        public string this[string key] { 
            get => Map[key];
            set
            {
                if( CaresAboutOpposites.Contains(value) )
                {
                    bool set = false;

                    foreach (Tuple<string, string> turtle in Opposites)
                    {
                        if (turtle.Item1 == key || turtle.Item2 == key)
                        {
                            Map[turtle.Item1] = value;
                            Map[turtle.Item2] = value;
                            set = true;
                        }
                    }

                    if (!set)
                    {
                        Map[key] = value;
                    }
                }
                else
                {
                    Map[key] = value;
                }
            }
        }

        public ICollection<string> Keys { get => Map.Keys; }
        public ICollection<string> Values { get => Map.Values; }
        public int Count { get => Map.Count; }
        public bool IsReadOnly { get => false; }

        public void Add(string key, string value)
            => Map.Add(key, value);

        public void Add(KeyValuePair<string, string> item)
            => Map.Add(item.Key, item.Value);

        public void Clear()
            => Map.Clear();

        public bool Contains(KeyValuePair<string, string> item)
            => Map.ContainsKey(item.Key) && Map[item.Key].Equals(item.Value);

        public bool ContainsKey(string key)
            => Map.ContainsKey(key);

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
            => Map.GetEnumerator();

        public bool Remove(string key)
            => Map.Remove(key);

        public bool Remove(KeyValuePair<string, string> item)
            => Map.ContainsKey(item.Key) && Map[item.Key].Equals(item.Value) && Map.Remove(item.Key);

        public bool TryGetValue(string key, out string value)
           => Map.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator()
            => Map.GetEnumerator();
    }
}