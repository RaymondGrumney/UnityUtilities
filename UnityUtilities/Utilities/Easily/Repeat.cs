using System;

namespace UnityUtilities.Utilities
{
    public class Repeat
    {
        private Action _action;
        private float _delay = 0f;

        public Repeat(Action action)
        {
            _action = action;
        }

        public Repeat Delaying(float delay) 
        {
            _delay=delay;
            return this;
        }

        public Repeat ThisManyTimes(int loops)
        {
            float d = _delay;
            for (int i = 0; i < loops; i++)
            {
                Easily.StartCoroutine( _action ).After(d).Seconds();
                d += _delay;
            }
            return this;
        }
    }
}
