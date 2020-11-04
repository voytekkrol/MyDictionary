﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        Random rnd = new Random();
        WordLessonComparer lessonComparer = new WordLessonComparer();

        public ExerciseController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            ExerciseViewModel model = new ExerciseViewModel()
            {
                Word = new Word(),
                LessonList = await _db.Words.OrderBy(u => u.Lesson).Select(w => w.Lesson).Distinct().ToListAsync()
                
            };

            return View(model);
        }

        public async Task<IActionResult> Exercise()
        {
            var listOfWords = await _db.Words.ToListAsync();
            int number = rnd.Next(0, listOfWords.Count);

            return View(listOfWords[number]);
        }
    }
}
