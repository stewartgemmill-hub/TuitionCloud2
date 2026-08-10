using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutionCloudWeb.Models.PostModel
{
    public class Favourites
    {
        public class WordList
        {
            public int index { get; set; }
            public long userId { get; set; }
            public string totalWordCount { get; set; }
            public string searchText { get; set; }
            public string sortBy { get; set; }
            public string sortOrder { get; set; }
        }
        public class WordClass
        {
            public long userId { get; set; }
            public string totalWordCount { get; set; }
        }
        public class WordListPagination
        {
            public int index { get; set; }
            public long userId { get; set; }
            public string totalWordCount { get; set; }
            public string searchText { get; set; }
            public string sortBy { get; set; }
            public string sortOrder { get; set; }
        }
    }
}