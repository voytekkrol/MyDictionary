using MyDictionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDictionary.Utilitiy
{
    public static class RandomWordFromList
    {
        public static Word GetRandomWord(List<Word> listOfWords)
        {
            Random rnd = new Random();
            int number = rnd.Next(0, listOfWords.Count);
            Word tmpWord = listOfWords[number];

            return tmpWord;
        }
    }
}
