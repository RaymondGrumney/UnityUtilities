using UnityUtilities.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UnityUtilities.Joypads.Presets
{
    class XInputJoypad : JoypadPreset
    {
        string JoypadPreset.jumpKey { get => "JoystickButton0"; }
        string JoypadPreset.attackKey { get => "JoystickButton2"; }
        string JoypadPreset.shieldKey { get => "righttrigger"; }
        string JoypadPreset.interactKey { get => "JoystickButton2"; }
        string JoypadPreset.magicKey { get => "JoystickButton3"; }
        string JoypadPreset.cyclemagicleft { get => "JoystickButton4"; }
        string JoypadPreset.cyclemagicright { get => "JoystickButton5"; }
        string JoypadPreset.itemKey { get => "JoystickButton1"; }
        string JoypadPreset.upKey { get => "up"; }
        string JoypadPreset.downKey { get => "down"; }
        string JoypadPreset.leftKey { get => "left"; }
        string JoypadPreset.rightKey { get => "right"; }
        string JoypadPreset.horizontalAxis { get => "horizontal"; }
        string JoypadPreset.verticalAxis { get => "vertical"; }
        string JoypadPreset.menuKey { get => "JoystickButton9"; }
        string JoypadPreset.menuConfirmKey { get => "JoystickButton0"; }
        string JoypadPreset.menuCancelKey { get => "JoystickButton1"; }
        string JoypadPreset.menuExitKey { get => "JoystickButton9"; }
    }
}
