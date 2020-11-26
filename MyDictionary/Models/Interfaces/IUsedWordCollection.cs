using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDictionary.Models.Interfaces
{
    public interface IUsedWordCollection
    {
        public List<Word> UsedWordList { get; set; }
    }
}
