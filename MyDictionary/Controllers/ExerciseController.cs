﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Models;

namespace MyDictionary.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly ApplicationDbContext _db;
        Random rnd = new Random();

        public ExerciseController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            
            var listOfWords = await _db.Words.ToListAsync();
            int number = rnd.Next(0, listOfWords.Count);

            return View(listOfWords[number]);
        }
    }
}