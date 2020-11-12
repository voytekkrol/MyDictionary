using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDictionary.Models.ViewModels
{
    public class ExerciseViewModel
    {
        public string Lesson { get; set; }
        public Word Word { get; set; }
        public List<string> LessonList { get; set; }
        public string Repetition { get; set; }
        public List<string> RepetitionList { get; set; }
        public string InputFrenchWord { get; set; }
    }
}
