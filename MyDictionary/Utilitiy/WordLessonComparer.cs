using MyDictionary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MyDictionary.Utilitiy
{
    public class WordLessonComparer : IEqualityComparer<Word>
    {
        public bool Equals([AllowNull] Word x, [AllowNull] Word y)
        {
            return x.Lesson.Equals(y.Lesson);
        }

        public int GetHashCode([DisallowNull] Word obj)
        {
            return obj.GetHashCode();
        }
    }
}
