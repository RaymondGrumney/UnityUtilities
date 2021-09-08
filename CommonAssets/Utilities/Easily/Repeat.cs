using System;

namespace CommonAssets.Utilities
{
    public class Repeat
    {
        private Action _action;

        public Repeat(Action action) 
        {
            _action = action;
        }

        public void ThisManyTimes(int loops)
        {
            for (int i = 0; i < loops; i++)
            {
                _action();
            }
        }
    }
}
