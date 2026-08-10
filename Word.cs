using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutionCloudWeb.Models.PostModel
{
    public class Word
    {
        public class AddWord
        {
            [Required(ErrorMessage = "required")]
            public string word { get; set; }
            [Required(ErrorMessage = "required")]
            public string pronounciation { get; set; }
            [Required(ErrorMessage = "required")]
            public string phrase { get; set; }
            [Required(ErrorMessage = "required")]
            public string hint { get; set; }
            [Required(ErrorMessage = "required")]
            public string definition { get; set; }
            public bool sharOnline { get; set; }
            public long userId { get; set; }
        }

        public class AddWordReturn
        {
            public bool status { get; set; }
            public string message { get; set; }
        }

        public class WordList
        {
            public int index { get; set; }
            public long userId { get; set; }
            public string totalWordCount { get; set; }
            public string searchText { get; set; }
            public string sortBy { get; set; }
            public string sortOrder { get; set; }
            public string CreatedOn { get; set; }
            public string wordIds { get; set; }
            public int ListCount { get; set; }
        }
        public class WordClass
        {
            public long userId { get; set; }
            public string totalWordCount { get; set; }
            public string CreatedOn { get; set; }
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

        public class EditWord
        {
            [Required(ErrorMessage = "required")]
            public string word { get; set; }
            [Required(ErrorMessage = "required")]
            public string pronounciation { get; set; }
            [Required(ErrorMessage = "required")]
            public string phrase { get; set; }
            [Required(ErrorMessage = "required")]
            public string hint { get; set; }
            [Required(ErrorMessage = "required")]
            public string definition { get; set; }
            public bool sharOnline { get; set; }
            [Required(ErrorMessage = "required")]
            public long userId { get; set; }
            [Required(ErrorMessage = "required")]
            public long wordId { get; set; }
            public string redirectTo { get; set; }
            public int mainPanelIndex { get; set; }
        }

        public class EditWordReturn
        {
            public bool status { get; set; }
            public string message { get; set; }
        }

        public class AddFavouriteWord
        {
            [Required(ErrorMessage = "required")]
            public long userId { get; set; }
            [Required(ErrorMessage = "required")]
            public string wordIds { get; set; }

            //added sibi
            public string Password { get; set; }
            public string TestID { get; set; }
            public string ListName { get; set; }
            public string SendUserId { get; set; }

            public TutionCloudWeb.Models.PostModel.Exam.CreateTest CreateTest { get; set; }
        }
        public class AddFavouriteWordReturn
        {
            public bool status { get; set; }
            public string message { get; set; }
        }
        public class AddSkippedWord
        {
            [Required(ErrorMessage = "required")]
            public long userId { get; set; }
            [Required(ErrorMessage = "required")]
            public string wordIds { get; set; }
        }
        public class AddSkippedWordReturn
        {
            public bool status { get; set; }
            public string message { get; set; }
        }

        public class DeleteSelectedWord
        {
            [Required(ErrorMessage = "required")]
            public long userId { get; set; }
            [Required(ErrorMessage = "required")]
            public string wordIds { get; set; }
            public string CreatedOn { get; set; }
            public string searchText { get; set; }

        }
        public class DeleteSelectedWordReturn
        {
            public bool status { get; set; }
            public string message { get; set; }
        }
        public class DeleteAllWord
        {
            [Required(ErrorMessage = "required")]
            public long userId { get; set; }
        }
        public class DeleteAllWordReturn
        {
            public bool status { get; set; }
            public string message { get; set; }
        }


        public class DeleteSelectedFavoutiteWord
        {
            [Required(ErrorMessage = "required")]
            public long userId { get; set; }
            [Required(ErrorMessage = "required")]
            public string wordIds { get; set; }
        }
        public class DeleteSelectedFavoutiteWordReturn
        {
            public bool status { get; set; }
            public string message { get; set; }
        }
        public class DeleteAllFavoutiteWord
        {
            [Required(ErrorMessage = "required")]
            public long userId { get; set; }
        }
        public class DeleteAllFavoutiteWordReturn
        {
            public bool status { get; set; }
            public string message { get; set; }
        }


        public class DeleteSelectedSkiplistWord
        {
            [Required(ErrorMessage = "required")]
            public long userId { get; set; }
            [Required(ErrorMessage = "required")]
            public string wordIds { get; set; }
        }
        public class DeleteSelectedSkiplistWordReturn
        {
            public bool status { get; set; }
            public string message { get; set; }
        }
        public class DeleteAllSkiplistWord
        {
            [Required(ErrorMessage = "required")]
            public long userId { get; set; }
        }
        public class DeleteAllSkiplistWordReturn
        {
            public bool status { get; set; }
            public string message { get; set; }
        }


    }
}