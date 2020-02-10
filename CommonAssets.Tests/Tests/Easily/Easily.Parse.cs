using CommonAssets.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

namespace UnitTest.Easily
{       
    [TestClass]
    public class Parse
    {
        [TestMethod]
        public void ParsesKeyCodes()
        {
            KeyCode Example = KeyCode.A;
            KeyCode Test = CommonAssets.Utilities.Easily.Parse<KeyCode>("A");
            Assert.AreEqual(Example, Test);
        }

        [TestMethod]
        public void ParsesArbitraryEnum()
        {
            ArbitraryEnum Example = ArbitraryEnum.SpanishInquisition;
            var Test = CommonAssets.Utilities.Easily.Parse<ArbitraryEnum>("SpanishInquisition");

            Assert.AreEqual(Example, Test);
        }
    }

    enum ArbitraryEnum { SpanishInquisition, DeadParrot }
}
