using CommonAssets.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CommonAssets.Joypads.Presets
{
    class XInputJoypad : JoypadPreset
    {
        string JoypadPreset.jumpKey { get => "JoystickButton0"; }
        string JoypadPreset.attackKey { get => "JoystickButton2"; }
        string JoypadPreset.magicKey { get => "JoystickButton3"; }
        string JoypadPreset.itemKey { get => "JoystickButton1"; }
        string JoypadPreset.menuKey { get => "JoystickButton7"; }
        string JoypadPreset.upKey { get => "vertical"; }
        string JoypadPreset.downKey { get => "vertical"; }
        string JoypadPreset.leftKey { get => "horizontal"; }
        string JoypadPreset.rightKey { get => "horizontal"; }
        string JoypadPreset.horizontalAxis { get => "horizontal"; }
        string JoypadPreset.verticalAxis { get => "vertical"; }
        string JoypadPreset.menuConfirmKey { get => "JoystickButton0"; }
        string JoypadPreset.menuCancelKey { get => "JoystickButton7"; }
        string JoypadPreset.menuExitKey { get => "JoystickButton7"; }
    }
}
