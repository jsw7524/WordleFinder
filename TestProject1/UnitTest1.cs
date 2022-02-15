using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyApp;
using System.Collections.Generic;
using System.Linq;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            WordleHelper wh = new WordleHelper(5);
            var result = wh.GetWords();
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void TestMethod2()
        {
            WordleHelper wh = new WordleHelper(5);
            var words = wh.GetWords();
            var dict = wh.GetDict(words);
            Assert.IsTrue(dict[0]['d'].Any(s => "daily" == s.Word));
            Assert.IsTrue(dict[1]['a'].Any(s => "daily" == s.Word));
            Assert.IsTrue(dict[2]['i'].Any(s => "daily" == s.Word));
            Assert.IsTrue(dict[3]['l'].Any(s => "daily" == s.Word));
            Assert.IsTrue(dict[4]['y'].Any(s => "daily" == s.Word));
        }

        [TestMethod]
        public void TestMethod3()
        {
            int n = 5;
            WordleHelper wh = new WordleHelper(n);
            WordleFinder wf = new WordleFinder(n);
            var words = wh.GetWords();
            var dict = wh.GetDict(words);
            wf.SetPossible(0, 'd', dict[0]);
            Assert.IsFalse(dict[0]['d'].Any(s => s.IsPossible == false));
        }
        [TestMethod]
        public void TestMethod4()
        {
            int n = 5;
            WordleHelper wh = new WordleHelper(n);
            WordleFinder wf = new WordleFinder(n);
            var words = wh.GetWords();
            var dict = wh.GetDict(words);
            wf.SetPossible(2, 'd', dict[2]);
            Assert.IsFalse(dict[2]['c'].Any(s => s.IsPossible == true));
        }

        [TestMethod]
        public void TestMethod5()
        {
            int n = 5;
            WordleHelper wh = new WordleHelper(n);
            WordleFinder wf = new WordleFinder(n);
            var words = wh.GetWords();
            var dict = wh.GetDict(words);
            wf.SetImpossible(1, 'r', dict);
            Assert.IsFalse(dict[0]['r'].Any(s => s.IsPossible == true));
            Assert.IsFalse(dict[1]['r'].Any(s => s.IsPossible == true));
            Assert.IsFalse(dict[2]['r'].Any(s => s.IsPossible == true));
            Assert.IsFalse(dict[3]['r'].Any(s => s.IsPossible == true));
            Assert.IsFalse(dict[4]['r'].Any(s => s.IsPossible == true));
        }


        [TestMethod]
        public void TestMethod6()
        {
            int n = 5;
            WordleFinder wf = new WordleFinder(n);
            List<Wordle> Wordles = new List<Wordle> { new Wordle() { Word = "abc", IsPossible = true }, new Wordle() { Word = "kab", IsPossible = true }, new Wordle() { Word = "iak", IsPossible = true } };
            wf.Filter(Wordles, new List<char> { 'a', 'b' });

            Assert.AreEqual(2, Wordles.Where(a=>a.IsPossible==true).Count());
        }

        //		Word	"abase"	string
        [TestMethod]
        public void TestMethod7()
        {
            int n = 5;
            WordleFinder wf = new WordleFinder(n);
            List<Wordle> Wordles = new List<Wordle> { new Wordle() { Word = "abase", IsPossible = true } };
            wf.Filter(Wordles, new List<char> { 'e', 's' });

            Assert.AreEqual(1, Wordles.Where(a => a.IsPossible == true).Count());
        }

        [TestMethod]
        public void TestMethod8()
        {
            int n = 5;
            WordleHelper wh = new WordleHelper(n);
            WordleFinder wf = new WordleFinder(n);
            var words = wh.GetWords();
            var dict = wh.GetDict(words);
            wf.SetNotHere(2, 'd', dict[2]);
            Assert.IsTrue(dict[2]['d'].All(s => s.IsPossible == false));
            Assert.IsTrue(dict[2]['c'].All(s => s.IsPossible == true));
        }
    }
}