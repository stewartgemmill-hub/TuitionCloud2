using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutionCloudWeb.Models.PostModel
{
    public class Exam
    {
        public class CreateTest 
        {
            //[Required(ErrorMessage = "required")]
            public string title { get; set; }
            //[Required(ErrorMessage = "required")]
            public string testTime { get; set; }
            //[Required(ErrorMessage = "required")]
            public string timePerWord { get; set; }
            public long userId { get; set; }
            public string wordIds { get; set; }

            //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            //{
            //    if (testTime == null && timePerWord == "" && testTime == "" && timePerWord == null)
            //    {
            //        yield return new ValidationResult("ProposedCost must be provided.");
            //    }
            //}

        }
        public class CreateTestReturn
        {
            public bool status { get; set; }
            public string message { get; set; }
            public string examId { get; set; }
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
        public class WordList
        {
            public int index { get; set; }
            public long userId { get; set; }
            public string totalWordCount { get; set; }
            public string searchText { get; set; }
            public string sortBy { get; set; }
            public string sortOrder { get; set; }
            public string wordIds { get; set; }
        }

        public class StartTest
        {
            public long userId { get; set; }
            public string examId { get; set; }
            public string timePerWord { get; set; }
            public string testTime { get; set; }
        }

        public class ViewTestAnswer
        {
            public long userId { get; set; }
            public string examId { get; set; }
            public List<long> wordIds { get; set; }
            public string notviewedwordIds { get; set; }
        }

        public class SaveResult
        {
            public string userId { get; set; }
            public string examId { get; set; }
            public string resultData { get; set; }
        }

        public class ResultData
        {
            public string wordId { get; set; }
            public bool isCorrect { get; set; }
        }

        public class SaveResultResponse
        {
            public bool status { get; set; }
            public string message { get; set; }
            public string examId { get; set; }
        }

        public class TestSummary
        {
            public long userId { get; set; }
            public string examId { get; set; }
            public string totalWordCount { get; set; }
        }

        public class TestSummaryWordList
        {
            public int index { get; set; }
            public long userId { get; set; }
            public long examId { get; set; }
            public string totalWordCount { get; set; }
            public string searchText { get; set; }
            public string sortBy { get; set; }
            public string sortOrder { get; set; }
        }

        public class TestSummaryWordListPagination
        {
            public int index { get; set; }
            public long userId { get; set; }
            public long examId { get; set; }
            public string totalWordCount { get; set; }
            public string searchText { get; set; }
            public string sortBy { get; set; }
            public string sortOrder { get; set; }
        }



        public class TestArchives
        {
            public long userId { get; set; }
            public string totalTestCount { get; set; }
        }
        public class TestArchivesList
        {
            public int index { get; set; }
            public long userId { get; set; }
            public string totalTestCount { get; set; }
        }

        public class TestArchivesListPagination
        {
            public int index { get; set; }
            public long userId { get; set; }
            public string totalTestCount { get; set; }
        }


    }
}