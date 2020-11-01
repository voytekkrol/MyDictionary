using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDictionary.Models
{
    public class Word
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PolishWord { get; set; }

        [Required]
        public string FrenchWord { get; set; }

        public string Lesson { get; set; }
    }
}
