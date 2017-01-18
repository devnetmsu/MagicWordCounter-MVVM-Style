using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MagicWordCounter.Core
{
    public class WordCountingViewModel : INotifyPropertyChanged
    {
        public WordCountingViewModel()
        {
            WordCountDisplay = "0 word(s)";
            UpdateWordCount = new RelayCommand(new Action(UpdateWordCountExecute), UpdateWordCountCanExecute);
            PropertyChanged += OnPropertyChanged;
        }

        ~WordCountingViewModel()
        {
            PropertyChanged -= OnPropertyChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(WordCount):
                    WordCountDisplay = $"{WordCount} word(s)";
                    break;
            }
        }

        #region Properties

        /// <summary>
        /// The text to count
        /// </summary>
        public string TextToCount
        {
            get
            {
                return _textToCount;
            }
            set
            {
                if (_textToCount != value)
                {
                    _textToCount = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextToCount)));
                }
            }
        }
        string _textToCount;

        /// <summary>
        /// Whether or not to exclude article adjectives (`a`, `an`, and `the`) from the word count
        /// </summary>
        public bool ExcludeArticles
        {
            get
            {
                return _excludeArticles;
            }
            set
            {
                if (_excludeArticles != value)
                {
                    _excludeArticles = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ExcludeArticles)));
                }
            }
        }
        bool _excludeArticles;

        /// <summary>
        /// Whether or not to count text inside quotation marks as one word
        /// </summary>
        public bool CountQuotesAsOneWord
        {
            get
            {
                return _countQuotesAsOneWord;
            }
            set
            {
                if (_countQuotesAsOneWord != value)
                {
                    _countQuotesAsOneWord = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CountQuotesAsOneWord)));
                }
            }
        }
        bool _countQuotesAsOneWord;

        /// <summary>
        /// How many words there are in <see cref="TextToCount"/>.
        /// </summary>
        public int WordCount
        {
            get
            {
                return _wordCount;
            }
            protected set
            {
                if (_wordCount != value)
                {
                    _wordCount = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WordCount)));
                }
            }
        }
        int _wordCount;

        /// <summary>
        /// User-friendly representation of <see cref="WordCount"/>
        /// </summary>
        public string WordCountDisplay
        {
            get
            {
                return _wordCountDisplay;
            }
            protected set
            {
                if (_wordCountDisplay != value)
                {
                    _wordCountDisplay = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WordCountDisplay)));
                }
            }
        }
        string _wordCountDisplay;

        /// <summary>
        /// Updates <see cref="WordCount"/>
        /// </summary>
        public ICommand UpdateWordCount { get; protected set; }

        #endregion

        #region Counting Logic

        private bool isArticle(string word)
        {
            return word.ToLower() == "a" || word.ToLower() == "an" || word.ToLower() == "the";
        }

        /// <summary>
        /// Regular expression that matches text inside quotation marks (including the quotation marks), taking into account smart quotes.
        /// </summary>
        private static Regex QuoteRegex => new Regex($"(\\\"|\\{(char)0x201C}|\\{(char)0x201D})((.|\n)*?)(\\\"|\\{(char)0x201C}|\\{(char)0x201D})", RegexOptions.IgnoreCase | RegexOptions.Compiled);


        private void UpdateWordCountExecute()
        {
            var textToCount = this.TextToCount.Replace(Environment.NewLine, " ").Replace("…", " ");
            var wordCount = 0;

            // Count words, taking into account article adjective settings
            foreach (var word in textToCount.Split(' '))
            {
                if (!string.IsNullOrEmpty(word) && (!isArticle(word) || !ExcludeArticles))
                {
                    wordCount += 1;
                }
            }

            // Take into account quotations
            if (CountQuotesAsOneWord)
            {
                foreach (Match match in QuoteRegex.Matches(textToCount))
                {
                    // Count the words inside the quotation marks
                    var quoteCount = 0;
                    foreach (var word in match.Groups[2].Value.Split(' '))
                    {
                        if (!string.IsNullOrEmpty(word) && (!isArticle(word) || !ExcludeArticles))
                        {
                            quoteCount += 1;
                        }
                    }

                    // Because the words inside the quote have already been counted in wordCount, subtract the number of words in the quote from wordCount
                    wordCount -= (quoteCount - 1); //The "- 1" part makes it so that quotes count as 1 word, instead of 0
                }
            }

            this.WordCount = wordCount;
        }
        private bool UpdateWordCountCanExecute()
        {
            return true;
        }

        #endregion
    }
}