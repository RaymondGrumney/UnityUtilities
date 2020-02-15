using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CommonAssets.Utilities
{
    public static class Easily
    {
        /// <summary>
        /// Retrieves a value from the player prefs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Get<T> Get<T>() => new Get<T>();
        
        /// <summary>
        /// Parses an Enum of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Parse<T>(string value) => (T)System.Enum.Parse(typeof(T), value);
        

        /// <summary>
        /// Instantiates an object
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="here">Where in the game to instantiate it.</param>
        public static void Instantiate(GameObject gameObject, Vector3 here) => Instantiator.Make(gameObject, here);
        


        /// <summary>
        /// Plays a sound at the camera's position
        /// </summary>
        /// <param name="audioClip"></param>
        public static void PlaySound(AudioClip audioClip) => SoundHelper.Play(audioClip);

        /// <summary>
        /// Plays a sound at a position
        /// </summary>
        /// <param name="audioClip"></param>
        /// <param name="position"></param>
        public static void PlaySound(AudioClip audioClip, Vector2 position) => SoundHelper.Play(audioClip, position);


        /// <summary>
        /// Knocks an object back
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static KnockBack Knock(GameObject @this) => new KnockBack(@this);
        /// <summary>
        /// Knocks an object back
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static KnockBack Knock(Collision2D @this) => new KnockBack(@this);
        /// <summary>
        /// Knocks an object back
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static KnockBack Knock(Collider2D @this) => new KnockBack(@this);
        /// <summary>
        /// Knocks an object back
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static KnockBack Knock(Rigidbody2D @this) => new KnockBack(@this);
    }
}
