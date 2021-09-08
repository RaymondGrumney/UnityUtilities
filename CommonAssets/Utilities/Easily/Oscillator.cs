using System;
using UnityEngine;

namespace CommonAssets
{
    /// <summary>
    /// Oscillates between two value as time passes in game.
    /// </summary>
    public class Oscillator
    {
        private float? _amplitude;
        private float? _frequency;
        private float _phase = 0;

        private float? _firstValue;
        private float? _secondValue;
        private float _center = 0;


        /// <summary>
        /// Where within the cycle to start.
        /// </summary>
        /// <param name="nSeconds"></param>
        /// <returns></returns>
        public Oscillator OffsetBy(float nSeconds)
        {
            _phase = nSeconds;
            return this;
        }

        /// <summary>
        /// Defines the first value of the oscillation. Call And() to set second value
        /// </summary>
        /// <param name="firstValue"></param>
        /// <returns></returns>
        public Oscillator Between(float firstValue)
        {
            this._firstValue = firstValue;
            return this;
        }

        /// <summary>
        /// Defines the second value of the oscillation. 
        /// Call Between(firstValue) first
        /// </summary>
        /// <param name="secondValue"></param>
        /// <returns></returns>
        public Oscillator And(float secondValue)
        {
            if (_firstValue is null) throw new ArgumentNullException("Set a value with Between(firstValue) before calling And(secondValue).");

            this._secondValue = secondValue;
            _center = ((float)_firstValue + secondValue) / 2f; 
            _amplitude = ((float)_firstValue - secondValue) / 2f;

            return this;
        }

        /// <summary>
        /// How much to vary from 0, +/-
        /// </summary>
        /// <param name="value">How much to vary from 0, +/-</param>
        /// <returns></returns>
        public Oscillator By(float value)   
        {
            if (_firstValue != null || _secondValue != null) throw new ArgumentException("The By(value) cannot be combined with Between(firstValue) And(secondValue) methods. Use either By() or Between().And().");

            _amplitude = value;
            return this;
        }

        /// <summary>
        /// Length of time between cycles, in seconds.
        /// </summary>
        /// <param name="nSeconds">How many seconds between cycles.</param>
        /// <returns></returns>
        public Oscillator Every(float nSeconds)
        {
            _frequency = nSeconds;
            return this;
        }


        /// <summary>
        /// Yes it is a float
        /// </summary>
        /// <param name="oscillator"></param>
        public static implicit operator float(Oscillator oscillator)
            => oscillator.Value;

        /// <summary>
        /// The result at this Time.time
        /// </summary>
        public float Value
        {
            get
            {
                if (_firstValue != null && _secondValue is null) throw new ArgumentNullException("Set a second value to oscillate between with And(secondValue).");
                if (_amplitude is null) throw new ArgumentNullException("Amplitude must be set with By(value).");
                if (_frequency is null) throw new ArgumentNullException("Frequency must be set with Every(nSeconds).");

                return _center + (float)_amplitude * Mathf.Cos(AngularFrequency(1 / (float) _frequency) * Time.time + _phase);
            }
        }

        private static float AngularFrequency(float frequency)
        {
            return 2 * Mathf.PI * frequency;
        }
    }
}
