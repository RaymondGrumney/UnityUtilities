using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CommonAssets.Utilities
{
    public static class Easily
    {
        public static Get<T> Get<T>() => new Get<T>();
        public static T Parse<T>(string value) => (T)System.Enum.Parse(typeof(T), value);
        public static void Instantiate(GameObject gameObject, Vector3 center) => Instantiator.Make(gameObject, center);
        public static void PlaySound(AudioClip audioClip) => SoundHelper.Play(audioClip);
        public static void PlaySound(AudioClip audioClip, Vector2 position) => SoundHelper.Play(audioClip, position);
    }
}
