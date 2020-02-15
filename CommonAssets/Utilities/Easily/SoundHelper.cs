using System;
using UnityEngine;

/// <summary>
/// Helps plays sounds
/// </summary>
internal class SoundHelper
{
    /// <summary>
    /// Plays a passed sound at the specified position
    /// </summary>
    /// <param name="sound">The audio clip to play.</param>
    /// <param name="position">Where to play it.</param>
    internal static void Play(AudioClip sound, Vector2 position)
    {
        if (sound != null)
        {
            AudioSource.PlayClipAtPoint(sound, position);
        }
    }

    /// <summary>
    /// Plays a sound at the camera position
    /// </summary>
    /// <param name="sound">The audio clip to play.</param>
    internal static void Play(AudioClip sound)
    {
        if (sound != null)
        {
            AudioSource.PlayClipAtPoint(sound, GameObject.Find("Main Camera").transform.position);
        }
    }
}