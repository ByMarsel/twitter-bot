using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterBot.Tests
{
    [TestClass()]
    public class TweetsParserTests
    {
        TweetsParser twParser = new TweetsParser();
        [TestMethod()]
        public void DeleteUserNameTest()
        {
            string actual = twParser.DeleteUserName(@"@boo_NNN,один@booga дон");
            string expected = ",один дон";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DeleteURLTest()
        {
            string actual = twParser.DeleteURL(@"даhttps://dzone.com/articles/good-url-regular-expression, удаляем ссылку");
            string expected = "да, удаляем ссылку";
            Assert.AreEqual(expected,actual);
        }
    }
}