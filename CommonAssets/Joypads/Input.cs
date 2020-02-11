// based on: https://www.studica.com/blog/custom-input-manager-unity-tutorial
 
using UnityEngine;
using System.Collections;
using CommonAssets.Joypads.Presets;
using CommonAssets.Utilities;
using System.Collections.Generic;
using System;

namespace Joypad {

    /// <summary>
    /// For defining button maps. Access via Input.Buttons
    /// </summary>
    public class Input : MonoBehaviour {
        /// <summary>
        /// Access the butons
        /// </summary>
        public static Input Buttons;

        /// <summary>
        /// Maps action names to joypad/keyboard inputs
        /// </summary>
        public ButtonMap Map { get; } = new ButtonMap();

        #region
        virtual public KeyCode jump { get; set; }
        virtual public KeyCode attack { get; set; }
        virtual public KeyCode magic { get; set; }
        virtual public KeyCode item { get; set; }
        virtual public KeyCode menu { get; set; }
        virtual public KeyCode up { get; set; }
        virtual public KeyCode down { get; set; }
        virtual public string horizontal { get; set; }
        virtual public KeyCode left { get; set; }
        virtual public KeyCode right { get; set; }
        virtual public string  vertical { get; set; }
        virtual public KeyCode menuConfirm { get; set; }
        virtual public KeyCode menuCancel { get; set; }
        virtual public KeyCode menuExit { get; set; }
        #endregion

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

            this.Set();
        }

        /// <summary>
        /// Sets each key to the player prefs or default
        /// </summary>
        public void Set() 
        {
            Map.Add("jump", "Space");
            jump = Easily.Get<KeyCode>().From.PlayerPrefs("jumpKey").OrDefault(KeyCode.Space);
            Map.Add("attack", "KeypadEnter");
            attack = Easily.Get<KeyCode>().From.PlayerPrefs("attackKey").OrDefault(KeyCode.KeypadEnter);
            Map.Add("magic", "M");
            magic = Easily.Get<KeyCode>().From.PlayerPrefs("magicKey").OrDefault(KeyCode.M);
            Map.Add("item", "RightShift");
            item = Easily.Get<KeyCode>().From.PlayerPrefs("itemKey").OrDefault(KeyCode.RightShift);
            Map.Add("menu", "Escape");
            menu = Easily.Get<KeyCode>().From.PlayerPrefs("menuKey").OrDefault(KeyCode.Escape);
            Map.Add("menuConfirm", "Space");
            menuConfirm = Easily.Get<KeyCode>().From.PlayerPrefs("menuConfirmKey").OrDefault(KeyCode.Space);
            Map.Add("menuCancel", "Backspace");
            menuCancel = Easily.Get<KeyCode>().From.PlayerPrefs("menuCancelKey").OrDefault(KeyCode.Backspace);
            Map.Add("menuExit", "Escape");
            menuExit = Easily.Get<KeyCode>().From.PlayerPrefs("menuExitKey").OrDefault(KeyCode.Escape);
            Map.Add("up", "W");
            up = Easily.Get<KeyCode>().From.PlayerPrefs("upKey").OrDefault(KeyCode.W);
            Map.Add("down", "S");
            down = Easily.Get<KeyCode>().From.PlayerPrefs("downKey").OrDefault(KeyCode.S);
            Map.Add("left", "A");
            left = Easily.Get<KeyCode>().From.PlayerPrefs("leftKey").OrDefault(KeyCode.A);
            Map.Add("right", "D");
            right = Easily.Get<KeyCode>().From.PlayerPrefs("rightKey").OrDefault(KeyCode.D);
        }

        /// <summary>
        /// Sets each key to player prefs or preset default
        /// </summary>
        /// <param name="preset"></param>
        public void Set(JoypadPreset preset)
        {
            Map.Add("jump", preset.jumpKey.ToString());
            Map.Add("attack", preset.attackKey.ToString());
            Map.Add("magic", preset.magicKey.ToString());
            Map.Add("item", preset.itemKey.ToString());
            Map.Add("menu", preset.menuKey.ToString());
            Map.Add("menuConfirm", preset.menuConfirmKey.ToString());
            Map.Add("menuCancel", preset.menuCancelKey.ToString());
            Map.Add("menuExit", preset.menuExitKey.ToString());
            Map.Add("up", preset.upKey.ToString());
            Map.Add("down", preset.downKey.ToString());
            Map.Add("left", preset.leftKey.ToString());
            Map.Add("right", preset.rightKey.ToString());

            jump = Easily.Get<KeyCode>().From.PlayerPrefs("jumpKey").OrDefault(preset.jumpKey);
            attack = Easily.Get<KeyCode>().From.PlayerPrefs("attackKey").OrDefault(preset.attackKey);
            magic = Easily.Get<KeyCode>().From.PlayerPrefs("magicKey").OrDefault(preset.magicKey);
            item = Easily.Get<KeyCode>().From.PlayerPrefs("itemKey").OrDefault(preset.itemKey);
            menu = Easily.Get<KeyCode>().From.PlayerPrefs("menuKey").OrDefault(preset.menuKey);
            menuConfirm = Easily.Get<KeyCode>().From.PlayerPrefs("menuKey").OrDefault(preset.menuConfirmKey);
            menuCancel = Easily.Get<KeyCode>().From.PlayerPrefs("menuKey").OrDefault(preset.menuCancelKey);
            menuExit = Easily.Get<KeyCode>().From.PlayerPrefs("menuKey").OrDefault(preset.menuExitKey);
            up = Easily.Get<KeyCode>().From.PlayerPrefs("upKey").OrDefault(preset.upKey);
            down = Easily.Get<KeyCode>().From.PlayerPrefs("downKey").OrDefault(preset.downKey);
            left = Easily.Get<KeyCode>().From.PlayerPrefs("leftKey").OrDefault(preset.leftKey);
            right = Easily.Get<KeyCode>().From.PlayerPrefs("rightKey").OrDefault(preset.rightKey);
        }
    }

    public class ButtonMap : IDictionary<string, string>
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

        public string this[string key]
        {
            get => Map[key];
            set
            {
                if (CaresAboutOpposites.Contains(value))
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