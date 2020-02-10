using CommonAssets.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CommonAssets.Joypads.Presets
{
    class XInputJoypad : JoypadPreset
    {
        KeyCode JoypadPreset.jumpKey { get => Easily.Parse<KeyCode>("Button0"); }
        KeyCode JoypadPreset.attackKey { get => Easily.Parse<KeyCode>("Button2"); }
        KeyCode JoypadPreset.magicKey { get => Easily.Parse<KeyCode>("Button3"); }
        KeyCode JoypadPreset.itemKey { get => Easily.Parse<KeyCode>("Button1"); }
        KeyCode JoypadPreset.menuKey { get => Easily.Parse<KeyCode>("Button7"); }
        KeyCode JoypadPreset.upKey { get => Easily.Parse<KeyCode>("6thAxis"); }
        KeyCode JoypadPreset.downKey { get => Easily.Parse<KeyCode>("6thAxis"); }
        KeyCode JoypadPreset.leftKey { get => Easily.Parse<KeyCode>("7thAxis"); }
        KeyCode JoypadPreset.rightKey { get => Easily.Parse<KeyCode>("7thAxis"); }
        string JoypadPreset.horizontalAxis { get => "6thAxis"; }
        string JoypadPreset.verticalAxis { get => "7thAxis"; }
    }
}
