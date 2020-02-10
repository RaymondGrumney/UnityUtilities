using UnityEngine;

namespace CommonAssets.Joypads.Presets
{
    public interface JoypadPreset
    {
        KeyCode jumpKey { get; }
        KeyCode attackKey { get; }
        KeyCode magicKey { get; }
        KeyCode itemKey { get; }
        KeyCode menuKey { get; }
        KeyCode upKey { get; }
        KeyCode downKey { get; }
        KeyCode leftKey { get; }
        KeyCode rightKey { get; }
        string horizontalAxis { get; }
        string verticalAxis { get; }
    }
}