using CommonAssets.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UnityEngine;

namespace UnitTest.Easily
{
    [TestClass]
    public class Get
    {
        [TestMethod]
        public void GivesDefaultString()
        {
            string Example = "test";
            string Test = CommonAssets.Utilities.Easily.Get<string>().OrDefault("test");

            Assert.AreEqual(Example, Test);
        }

        [TestMethod]
        public void GivesDefaultEnum()
        {
            KeyCode Example = KeyCode.T;
            KeyCode Test = CommonAssets.Utilities.Easily.Get<KeyCode>().OrDefault(KeyCode.T);

            Assert.AreEqual(Example, Test);
        }

        [TestMethod]
        public void SetsToThingOrDefault()
        {

        }

        [TestMethod]
        public void FromPlayerPrefs()
        {
            Assert.Inconclusive("This test may not be possible.");

            UnityEngine.PlayerPrefs.SetString("test", "moogle");

            string Example = UnityEngine.PlayerPrefs.GetString("test");
            string Test = CommonAssets.Utilities.Easily.Get<string>().From.PlayerPrefs("test").Please;

            Assert.AreEqual(Example, Test);
        }
    }

    public static class UnityEngine
    {
        public static FakePrefs PlayerPrefs => new FakePrefs(); 
    }

    public class FakePrefs
    {
        Dictionary<string, string> _prefs = new Dictionary<string, string>();
        public string GetString(string key)
            => _prefs[key];
        public void SetString(string key, string value)
            => _prefs.Add(key, value);
    }
}

