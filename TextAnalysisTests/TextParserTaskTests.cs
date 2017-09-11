using Microsoft.VisualStudio.TestTools.UnitTesting;
using TweetsStatistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetsStatistic.Tests
{
    [TestClass()]
    public class TextParserTaskTests
    {
        [TestMethod()]
        public void TextParseTest()
        {
            TextParserTask test = new TextParserTask();
           string testtext=  test.TextParse("https://govno.com, борода города");
            Assert.AreEqual(" борода города", testtext) ;
        }
    }
}