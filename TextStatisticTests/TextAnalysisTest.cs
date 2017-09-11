using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextStatistics;
using System.Collections.Generic;
using System.Linq;

namespace TextStatisticTests
{
    [TestClass]
    public class TextAnalysisTest
    {
        [TestMethod]
        public void GetLettersUsageFrequencyJSONListTest()
        {
            TextAnalysis textAnalysis = new TextAnalysis();
            List<string> actual = textAnalysis.GetLettersUsageFrequencyJSONList("ub1111ccdx   ,,,,,@@@@", 22);
            Assert.AreEqual(3, actual.Count);
        }

        [TestMethod]
        public void GetLettersUsageFrequencyTest() {
            TextAnalysis textAnalysis = new TextAnalysis();
            Dictionary<char,float> actual = textAnalysis.GetLettersUsageFrequency("aaaa,kkkk,HHH0830239482");
            var keys = actual.Keys.ToArray(); 
            Assert.AreEqual(3, actual.Count);
            Assert.AreEqual(0.364, Math.Round(actual[keys[0]],3));
            Assert.AreEqual(0.364, Math.Round(actual[keys[1]],3));
            Assert.AreEqual(0.273, Math.Round(actual[keys[2]],3));

        }

    }
}
//[a, 0,364]}
//k, 0,364]}
//h, 0,273]