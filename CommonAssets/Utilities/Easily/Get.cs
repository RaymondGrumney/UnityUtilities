using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CommonAssets.Utilities
{
    public class Get<T>
    {
        /// <summary>
        /// The Result 
        /// </summary>
        public T Please { get; private set; }

        /// <summary>
        /// The Key Being Used
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// The Default Value if the getter returns null
        /// </summary>
        public T DefaultValue { get; private set; }

        public Get<T> From => this;


        /// <summary>
        /// Sets the PlayerPrefs key to retreive
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Get<T> PlayerPrefs(string key)
        {
            Key = key;
            T value = default;

            if (value is string)
            {
                SetPlease(UnityEngine.PlayerPrefs.GetString(Key, DefaultValue.ToString()));
            }

             SetPlease(value);
            return this;
        }

        /// <summary>
        /// Defines the default and returns the result
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public T OrDefault(int value) // for use with Enums
        {
            // the generic version of this seems to cover enums
            if (typeof(T).IsEnum)
            {
                DefaultValue = (T)(System.Object)value; // force it

                if (!Please.Equals(default(T)))
                {
                    Please = DefaultValue;
                }
            }
            
            return Please;
        }

        public T OrDefault(string value)
        {
            if (typeof(T).IsEnum)
            {
                T parsed = Easily.Parse<T>(value.ToString());

                DefaultValue = parsed;

                if (Please == null)
                {
                    Please = parsed;
                }
                else if (!Please.Equals(default(T)))
                {
                    Please = parsed;
                }
            }
            else if (typeof(T) == typeof(string))
            {
                DefaultValue = (T)(System.Object)value;

                if (Please == null)
                {
                    Please = DefaultValue;
                }
            }

            return Please;
        }

        /// <summary>
        /// Defines the default and returns the result
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public T OrDefault(T @default)
        {
            DefaultValue = @default;

            if(Please.Equals( default(T) ))
            {
                Please = DefaultValue;
            }

            return Please;
        }

        private void SetPlease(T value)
        {
            Please = Please.Equals(default(T)) ? Please : value;
        }

        private void SetPlease(string value)
        {
            Please = Please.Equals(default(T)) ? Please : Easily.Parse<T>(value);
        }
    }
}