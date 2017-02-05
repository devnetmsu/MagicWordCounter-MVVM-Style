using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MagicWordCounter.Core;

namespace MagicWordCounterOnline.Controllers
{
    public class CountingController : Controller
    {
        // GET: Counting
        public ActionResult Index()
        {
            var viewModel = new WordCountingViewModel();
            return View(viewModel);
        }

        // POST: Counting
        [HttpPost]
        public ActionResult Index(WordCountingViewModel viewModel)
        {
            viewModel.UpdateWordCount.Execute(null);
            return View(viewModel);
        }
    }
}
