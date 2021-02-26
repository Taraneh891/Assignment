using Assignment;
using NUnit.Framework;
using System.Collections.Generic;

namespace AssignmentTest
{
    [TestFixture]
    public class WordLogicTest
    {
        private WordLogic wordLogic;
        [SetUp]
        public void Setup()
        {
            wordLogic = new WordLogic();
        }

        [Test]
        [TestCase("This is a test case. Here's a special character!")]
        public void GetWordListFromLineTest(string line)
        {
            var result = wordLogic.GetWordListFromLine(line);
            Assert.IsNotNull(result, "result == null");
            Assert.AreEqual(9, result.Count, "incorrect result count");
        }

        [Test]
        public void RemoveStopWordsTest()
        {
            var lineWords = new List<string> { "This", "is", "a", "Test" };
            var stopWords = new string[] { "is", "a" };
            var result = wordLogic.RemoveStopWords(lineWords, stopWords);
            Assert.IsNotNull(result, "result == null");
            Assert.AreEqual(2, result.Count, "incorrect result count");
            Assert.IsTrue(!result.Contains("a"), "'a' is not removed as a stop word");
        }

        [Test]
        public void RemoveNonAlphaCharsTest()
        {
            var lineWords = new List<string> { "There's.,", "!", "123For" };
            var result = wordLogic.RemoveNonAlphaChars(lineWords);
            
            Assert.IsNotNull(result, "result == null");
            Assert.AreEqual(3, result.Count, "incorrect result count");

            Assert.IsTrue(result.Contains("Theres"), "special chars not removed");
            Assert.AreEqual("", result[1], "! is not removed");
            Assert.AreEqual("For", result[2], "0-9 is not removed");
        }
    }
}