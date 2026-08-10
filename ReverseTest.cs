using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutionCloudWeb.Models.PostModel
{
    public class ReverseTest
    {
        public class WordList
        {
            [Required(ErrorMessage = "required")]
            public long userId { get; set; }
            [Required(ErrorMessage = "required")]
            public int index { get; set; }
            public int prevIndex { get; set; }
            public string totalWordCount { get; set; }
            public bool isNewRandom { get; set; }
        }
    }
}