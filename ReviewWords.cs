using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutionCloudWeb.Models.PostModel
{
    public class ReviewWords
    {
        public class WordList
        {
            public long userId { get; set; }
            public bool showAllWords { get; set; }
            public string totalWordCount { get; set; }
            public int mainPanelReviewedtotalWordCount { get; set; }
            public string mainPanelReviewedWordIDs { get; set; }
        }
    }
}