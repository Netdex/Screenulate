using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Screenulate.NLP;

namespace Screenulate.Tests
{
    [TestClass]
    public class TestDeinflector
    {
        [TestMethod]
        public void Deinflect_WithInflectedVerb_ShouldRemoveInflections()
        {
            var saremashita = Deinflector.Deinflect("されました");
            
            var deinflectedStrings = saremashita as Deinflector.DeinflectedString[]
                                     ?? saremashita.ToArray();
            Assert.AreEqual(deinflectedStrings.Length, 7);
            Assert.IsTrue(deinflectedStrings.Any(x => x.Text == "さる"));
            Assert.IsTrue(deinflectedStrings.Any(x => x.Text == "されましる"));
            Assert.IsTrue(deinflectedStrings.Any(x => x.Text == "されます"));
            Assert.IsTrue(deinflectedStrings.Any(x => x.Text == "されまする"));
            Assert.IsTrue(deinflectedStrings.Any(x => x.Text == "される"));
            Assert.IsTrue(deinflectedStrings.Any(x => x.Text == "す"));
            Assert.IsTrue(deinflectedStrings.Any(x => x.Text == "する"));
        }
    }
}