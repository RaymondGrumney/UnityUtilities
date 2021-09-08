using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UnityUtilities.Utilities
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
        public static GameObject Instantiate(GameObject gameObject, Vector3 here) => Instantiator.Make(gameObject, here);
        


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
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static KnockBack Knock(GameObject gameObject) => new KnockBack(gameObject);
        /// <summary>
        /// Knocks an object back
        /// </summary>
        /// <param name="collision"></param>
        /// <returns></returns>
        public static KnockBack Knock(Collision2D collision) => new KnockBack(collision);
        /// <summary>
        /// Knocks an object back
        /// </summary>
        /// <param name="collider"></param>
        /// <returns></returns>
        public static KnockBack Knock(Collider2D collider) => new KnockBack(collider);
        /// <summary>
        /// Knocks an object back
        /// </summary>
        /// <param name="rigidBody"></param>
        /// <returns></returns>
        public static KnockBack Knock(Rigidbody2D rigidBody) => new KnockBack(rigidBody);

        public static void StartCoroutine(IEnumerator coroutine)
            => CoroutineRunner.RunCoroutine(coroutine);

        public static Flip Flip(GameObject gameObject) => new Flip(gameObject);

        public static Vector3 Clone(Vector3 original) => new Vector3(original.x, original.y, original.z);
        public static Quaternion Clone(Quaternion original)
            => new Quaternion(original.x, original.y, original.z, original.w);

        public static Repeat Repeat(Action action)
            => new Repeat(action);

        public static Getter<T> GetChild<T>(string name)
            => new Getter<T>(GetterType.ChildByName).Query(name);

        public static Oscillator Oscillate
            => new Oscillator();
    }
}
