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
using MyDictionary.Models;
using MyDictionary.Models.ViewModels;
using MyDictionary.Utilitiy;

namespace MyDictionary.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly ApplicationDbContext _db;
        private UsedWord _usedWord;

        Random rnd = new Random();

        [BindProperty]
        public ExerciseViewModel ExerciseVM { get; set; }


        public ExerciseController(ApplicationDbContext db, UsedWord usedWord)
        {
            _db = db;
            _usedWord = usedWord;

            ExerciseVM = new ExerciseViewModel()
            {
                Word = new Word(),
                LessonList = _db.Words.OrderBy(u => u.Lesson).Select(w => w.Lesson).Distinct().ToList(),
                RepetitionList = new List<string>() { "No repetitions", "With Repetitions", "Only wrongs" }
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
        public async Task<IActionResult> Exercise(string lesson, string repetition, string correctFrenchWord, string inputFrenchWord, bool isWordsSame)
        {
            if (!string.IsNullOrEmpty(lesson))
            {
                ExerciseVM.Lesson = lesson;
            }
            if (!string.IsNullOrEmpty(repetition))
            {
                ExerciseVM.Repetition = repetition;
            }

            var listOfWords = await _db.Words.Where(w => w.Lesson == ExerciseVM.Lesson).ToListAsync();

            if (_usedWord.UsedWordList == null)
            {
                _usedWord.UsedWordList = new List<Word>();
            }

            if (ExerciseVM.Repetition.Equals("No repetitions"))
            {
                listOfWords.RemoveAll(w => _usedWord.UsedWordList.Exists(x => w.FrenchWord.Equals(x.FrenchWord)));
                if (listOfWords.Count == 0)
                {
                    return RedirectToAction("Index");
                }
                int number = rnd.Next(0, listOfWords.Count);
                Word tmpWord = listOfWords[number];
                ExerciseVM.Word = tmpWord;
                _usedWord.UsedWordList.Add(tmpWord);
            }

            if (ExerciseVM.Repetition.Equals("With Repetitions"))
            {
                int number = rnd.Next(0, listOfWords.Count);
                Word tmpWord = listOfWords[number];
                ExerciseVM.Word = tmpWord;
            }

            if (ExerciseVM.Repetition.Equals("Only wrongs"))
            {
                if (correctFrenchWord != null && correctFrenchWord.Equals(inputFrenchWord))
                {
                    var word = listOfWords.Where(w => w.FrenchWord.Equals(correctFrenchWord)).FirstOrDefault();
                    _usedWord.UsedWordList.Add(word);

                }
                listOfWords.RemoveAll(w => _usedWord.UsedWordList.Exists(x => w.FrenchWord.Equals(x.FrenchWord)));

                if (listOfWords.Count == 0)
                {
                    return RedirectToAction("Index");
                }

                int number = rnd.Next(0, listOfWords.Count);
                Word tmpWord = listOfWords[number];
                ExerciseVM.Word = tmpWord;
                ExerciseVM.InputFrenchWord = tmpWord.FrenchWord;
                ExerciseVM.CorrectFrenchWord = tmpWord.FrenchWord;

            }

            return View(ExerciseVM);
        }
    }
}
