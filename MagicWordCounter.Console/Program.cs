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
            Console.WriteLine("Magic Word Counter's Magic Console");
            Console.WriteLine("Type 'exit' to exit");
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
