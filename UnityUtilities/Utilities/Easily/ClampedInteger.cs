using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtilities.Utilities;

namespace UnityUtilities.Utilities
{
    [Serializable]
    public class ClampedInteger
    {
        internal int _value = 0;
        public int Value
        {
            get => _value;
            set
            {
                _value = value;

                if (_value < _min)
                {
                    _value = _max;
                }
                else if (_value >= _max)
                {
                    _value = _min;
                }
            }
        }
        public int Max
        {
            get => _max;
            set => _max = value;
        }
        private int _max = 0;
        public int Min
        {
            get => _min;
            set => _min = value;
        }
        private int _min = 0;
        public static implicit operator int(ClampedInteger instance)
            => instance.Value;
        public static ClampedInteger operator +(ClampedInteger instance, int i)
        {
            instance.Value += i;
            return instance;
        }
        public static ClampedInteger operator ++(ClampedInteger instance)
        {
            instance.Value += 1;
            return instance;
        }
        public static ClampedInteger operator -(ClampedInteger instance, int i)
        {
            instance.Value -= i;
            return instance;
        }
        public static ClampedInteger operator --(ClampedInteger instance)
        {
            instance.Value -= 1;
            return instance;
        }
    }
}