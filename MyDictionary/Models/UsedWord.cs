using MyDictionary.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDictionary.Models
{
    public class UsedWord : IUsedWordCollection

    {
        List<Word> IUsedWordCollection.UsedWordList { get ; set ; }
    }
}
