using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConvertStringToNumber.Tests
{
    [TestClass]
    public class ConvertStringTests
    {
        [TestMethod]
        public void ToInt_String9_Int9()
        {
            var actual = ConvertString.ToInt("9");

            const int expected = 9;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToInt_checkingNegativeValue()
        {
            int actual = ConvertString.ToInt("-9");

            int expected = -9;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToInt_FormatException()
        {
            ConvertString.ToInt("edytytyj");
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ToInt_OverflowException()
        {
            ConvertString.ToInt("2147483648");
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ToInt_IndexOutOfRangeException()
        {
            ConvertString.ToInt("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToInt_ArgumentNullException()
        {
            ConvertString.ToInt(null);
        }
    }
}
