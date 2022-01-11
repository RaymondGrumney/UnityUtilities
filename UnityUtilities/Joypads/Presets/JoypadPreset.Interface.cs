using UnityEngine;

namespace UnityUtilities.Joypads.Presets
{
    public interface JoypadPreset
    {
        string jumpKey { get; }
        string attackKey { get; }
        string shield { get; }
        string interactKey { get; }
        string magicKey { get; }
        string cyclemagicright { get; }
        string cyclemagicleft { get; }
        string itemKey { get; }
        string menuKey { get; }
        string menuConfirmKey { get; }
        string menuCancelKey { get; }
        string menuExitKey { get; }
        string upKey { get; }
        string downKey { get; }
        string leftKey { get; }
        string rightKey { get; }
        string horizontalAxis { get; }
        string verticalAxis { get; }
        string quit => KeyCode.F12.ToString();

    }
}