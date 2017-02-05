using MagicWordCounter.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicWordCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Magic Word Counter");
            Console.WriteLine("The Magic Console");
            Console.WriteLine("");
            Console.WriteLine("Enter your text to count it, or enter 'exit' to exit");
            while (true)
            {
                var line = Console.ReadLine();
                var viewModel = new WordCountingViewModel();
                viewModel.ExcludeArticles = args.Contains("-excludeArticles");
                viewModel.CountQuotesAsOneWord = args.Contains("-quotesAsOne");
                if (line == "exit")
                {
                    break;
                }
                else
                {
                    viewModel.TextToCount = line;
                    viewModel.UpdateWordCount.Execute(null);
                    Console.WriteLine("Word count: " + viewModel.WordCount);
                }
            }
        }
    }
}
