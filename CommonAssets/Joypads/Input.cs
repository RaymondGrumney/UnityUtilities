// based on: https://www.studica.com/blog/custom-input-manager-unity-tutorial
 
using UnityEngine;
using System.Collections;
using CommonAssets.Joypads.Presets;
using CommonAssets.Utilities;

namespace Joypad {

    /// <summary>
    /// For defining button maps. Access via Input.Buttons
    /// </summary>
    public class Input : MonoBehaviour {
        /// <summary>
        /// Access the butons
        /// </summary>
        public static Input Button;

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


        void Awake()
        {
            //Singleton pattern
            if (Button == null)
            {
                DontDestroyOnLoad(gameObject);
                Button = this;
            }
            else if (Button != this)
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
            jump = Easily.Get<KeyCode>().From.PlayerPrefs("jumpKey").OrDefault(KeyCode.Space);
            attack = Easily.Get<KeyCode>().From.PlayerPrefs("attackKey").OrDefault(KeyCode.KeypadEnter);
            magic = Easily.Get<KeyCode>().From.PlayerPrefs("magicKey").OrDefault(KeyCode.M);
            item = Easily.Get<KeyCode>().From.PlayerPrefs("itemKey").OrDefault(KeyCode.RightShift);
            menu = Easily.Get<KeyCode>().From.PlayerPrefs("menuKey").OrDefault(KeyCode.Escape);
            up = Easily.Get<KeyCode>().From.PlayerPrefs("upKey").OrDefault(KeyCode.W);
            down = Easily.Get<KeyCode>().From.PlayerPrefs("downKey").OrDefault(KeyCode.S);
            left = Easily.Get<KeyCode>().From.PlayerPrefs("leftKey").OrDefault(KeyCode.A);
            right = Easily.Get<KeyCode>().From.PlayerPrefs("rightKey").OrDefault(KeyCode.D);
        }

        /// <summary>
        /// Sets each key to player prefs or preset default
        /// </summary>
        /// <param name="preset"></param>
        public void Set(JoypadPreset preset)
        {
            jump = Easily.Get<KeyCode>().From.PlayerPrefs("jumpKey").OrDefault(preset.jumpKey);
            attack = Easily.Get<KeyCode>().From.PlayerPrefs("attackKey").OrDefault(preset.attackKey);
            magic = Easily.Get<KeyCode>().From.PlayerPrefs("magicKey").OrDefault(preset.magicKey);
            item = Easily.Get<KeyCode>().From.PlayerPrefs("itemKey").OrDefault(preset.itemKey);
            menu = Easily.Get<KeyCode>().From.PlayerPrefs("menuKey").OrDefault(preset.menuKey);
            up = Easily.Get<KeyCode>().From.PlayerPrefs("upKey").OrDefault(preset.upKey);
            down = Easily.Get<KeyCode>().From.PlayerPrefs("downKey").OrDefault(preset.downKey);
            left = Easily.Get<KeyCode>().From.PlayerPrefs("leftKey").OrDefault(preset.leftKey);
            right = Easily.Get<KeyCode>().From.PlayerPrefs("rightKey").OrDefault(preset.rightKey);
        }
    }

}