using System;
using System.Collections.Generic;
using System.Text;

namespace CommonAssets.Utilities
{
    public static class Easily
    {
        public static Get<T> Get<T>() => new Get<T>();
        public static T Parse<T>(string value) => (T)System.Enum.Parse(typeof(T), value);
    }
}
