using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutionCloudWeb.Models.PostModel
{
    public class Crossword
    {
        public class MainCross
        {
            public string word { get; set; }
            public long wordId { get; set; }
            public int slno { get; set; }
            public int legth { get; set; }
            public List<WordList> list { get; set; }
            public bool sucessStatus { get; set; }
        }

        public class WordModel
        {
            public List<Word> Words { get; set; }
            public int MaxId { get; set; }
        }
        public class Word
        {
            public string SingleWord { get; set; }
        }

        public class WordList
        {
            public string letter { get; set; }
            public int xAxis { get; set; }
            public int yAxis { get; set; }
            public bool first { get; set; }
            public long wordId { get; set; }
            public bool isVertical { get; set; }
        }
        public class ListWordList
        {
            public List<WordList> list { get; set; }
            public List<WordDetails> wordDetailsList { get; set; }
            public int maxCount { get; set; }
            public bool isNew { get; set; }
            public long crosswordId { get; set; }
            public bool completeStatus { get; set; }
            public string html { get; set; }
            public long userId { get; set; }
        }

        public class WordDetails
        {
            public long WORDID { get; set; }
            public System.Guid WORDGUID { get; set; }
            public string WORD { get; set; }
            public string PRONOUNCIATION { get; set; }
            public string PHRASE { get; set; }
            public string HINT { get; set; }
            public string DEFINITION { get; set; }
            public System.DateTime CREATEDDATE { get; set; }
            public System.DateTime UPDATEDDATE { get; set; }
            public decimal OVERALLSCORE { get; set; }
            public bool ISFAVOURITE { get; set; }
            public bool ISSKIPPED { get; set; }
        }


        public class AddCrosswordToDB
        {
            public string wordIdListString { get; set; }
            public string isNewCrossword { get; set; }
            public string completeStatus { get; set; }
            public long userId { get; set; }
            public long crosswordId { get; set; }
            public string html { get; set; }
        }
        public class AddCrosswordToDBReturn
        {
            public bool status { get; set; }
            public string message { get; set; }
        }

        public class CrosswordArchives
        {
            public long userId { get; set; }
            public string totalCrosswordCount { get; set; }
        }
        public class CrosswordArchivesList
        {
            public int index { get; set; }
            public long userId { get; set; }
            public string totalCrosswordCount { get; set; }
        }

        public class CrosswordArchivesListPagination
        {
            public int index { get; set; }
            public long userId { get; set; }
            public string totalCrosswordCount { get; set; }
        }

    }
}