using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using MyDictionary.Models;
using MyDictionary.Models.Constans;
using MyDictionary.Models.Interfaces;
using MyDictionary.Models.ViewModels;
using MyDictionary.Utilitiy;

namespace MyDictionary.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly ILogger<ExerciseController> _logger;
        private readonly ApplicationDbContext _db;
        private IUsedWordCollection _usedWord;

        Random rnd = new Random();

        
        public ExerciseViewModel ExerciseVM { get; set; }


        public ExerciseController(ApplicationDbContext db, IUsedWordCollection usedWord, ILogger<ExerciseController> logger)

        {
            _db = db;
            _usedWord = usedWord;
            _logger = logger;

            ExerciseVM = new ExerciseViewModel()
            {
                Word = new Word(),
                LessonList = _db.Words.OrderBy(u => u.Lesson).Select(w => w.Lesson).Distinct().ToList(),
                RepetitionList = Constans.KindofRepetitionList,
            };
        }


        public IActionResult Index()
        {

            if (_usedWord.UsedWordList != null)
            {
                _usedWord.UsedWordList.Clear();
            }
            return View(ExerciseVM);
        }

        [HttpPost]
        public async Task<IActionResult> Exercise(string lesson, string repetition, string frenchWord, string inputFrenchWord)
        {
            if (!string.IsNullOrEmpty(lesson))
            {
                ExerciseVM.Lesson = lesson;
            }
            if (!string.IsNullOrEmpty(repetition))
            {
                ExerciseVM.Repetition = repetition;
            }

            if (_usedWord.UsedWordList == null)
            {
                _usedWord.UsedWordList = new List<Word>();
            }

            var listOfWords = await _db.Words.Where(w => w.Lesson == ExerciseVM.Lesson).ToListAsync();

            if (ExerciseVM.Repetition.Equals(ExerciseVM.RepetitionList[0]))
            {
                listOfWords.RemoveAll(w => _usedWord.UsedWordList.Exists(x => w.FrenchWord.Equals(x.FrenchWord)));
                if (listOfWords.Count == 0)
                {
                    return RedirectToAction("Index");
                }
                ExerciseVM.Word = RandomWordFromList.GetRandomWord(listOfWords);
                _usedWord.UsedWordList.Add(ExerciseVM.Word);
            }

            if (ExerciseVM.Repetition.Equals(ExerciseVM.RepetitionList[1]))
            {
                ExerciseVM.Word = RandomWordFromList.GetRandomWord(listOfWords);
            }

            if (ExerciseVM.Repetition.Equals(ExerciseVM.RepetitionList[2]))
            {
                if (frenchWord != null && frenchWord.ToLower().Equals(inputFrenchWord.ToLower()))
                {
                    var word = listOfWords.Where(w => w.FrenchWord.Equals(frenchWord)).FirstOrDefault();
                    _usedWord.UsedWordList.Add(word);

                }
                listOfWords.RemoveAll(w => _usedWord.UsedWordList.Exists(x => w.FrenchWord.Equals(x.FrenchWord)));

                if (listOfWords.Count == 0)
                {
                    return RedirectToAction("Index");
                }

                ExerciseVM.Word = RandomWordFromList.GetRandomWord(listOfWords);
                ExerciseVM.InputFrenchWord = ExerciseVM.Word.FrenchWord;
            }
            return View(ExerciseVM);
        }
    }
}
