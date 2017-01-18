using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MagicWordCounter.Core;

namespace MagicWordCounter.Tests
{
    [TestClass]
    public class CountingTests
    {

        private WordCountingViewModel viewModel { get; set; }
        [TestInitialize]
        public void TestInit()
        {
            viewModel = new WordCountingViewModel();
            viewModel.TextToCount = Properties.Resources.TestString;
        }

        [TestMethod]
        public void BasicCounting()
        {
            viewModel.ExcludeArticles = false;
            viewModel.CountQuotesAsOneWord = false;
            viewModel.UpdateWordCount.Execute(null);
            Assert.AreEqual("34 word(s)", viewModel.WordCountDisplay);
        }

        [TestMethod]
        public void ExcludeArticles()
        {
            viewModel.ExcludeArticles = true;
            viewModel.CountQuotesAsOneWord = false;
            viewModel.UpdateWordCount.Execute(null);
            Assert.AreEqual("32 word(s)", viewModel.WordCountDisplay);
        }

        [TestMethod]
        public void QuotesAreOneWord()
        {
            viewModel.ExcludeArticles = false;
            viewModel.CountQuotesAsOneWord = true;
            viewModel.UpdateWordCount.Execute(null);
            Assert.AreEqual("29 word(s)", viewModel.WordCountDisplay);
        }

        [TestMethod]
        public void ExcludeArticlesAndCountQuotesAsOneWord()
        {
            viewModel.ExcludeArticles = true;
            viewModel.CountQuotesAsOneWord = true;
            viewModel.UpdateWordCount.Execute(null);
            Assert.AreEqual("28 word(s)", viewModel.WordCountDisplay);
        }
    }
}
