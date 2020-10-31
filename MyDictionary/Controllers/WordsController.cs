using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyDictionary.Controllers
{
    public class WordsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
