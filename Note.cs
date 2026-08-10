using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutionCloudWeb.Models.PostModel
{
    public class Note
    {
        public class AddNote
        {
            [Required(ErrorMessage = "required")]
            public string title { get; set; }
            [Required(ErrorMessage = "required")]
            public string description { get; set; }
            public long userId { get; set; }
            public string ExamId { get; set; }

        }
        public class AddNoteReturn
        {
            public bool status { get; set; }
            public string message { get; set; }
        }
        public class NoteList
        {
            public int index { get; set; }
            public long userId { get; set; }
            public string totalNoteCount { get; set; }
            public string searchText { get; set; }
            public string sortBy { get; set; }
            public string sortOrder { get; set; }
        }
        public class NoteClass
        {
            public long userId { get; set; }
            public string totalNoteCount { get; set; }
        }
        public class NoteListPagination
        {
            public int index { get; set; }
            public long userId { get; set; }
            public string totalNoteCount { get; set; }
            public string searchText { get; set; }
            public string sortBy { get; set; }
            public string sortOrder { get; set; }
        }
        public class EditNote
        {
            [Required(ErrorMessage = "required")]
            public string title { get; set; }
            [Required(ErrorMessage = "required")]
            public string description { get; set; }
            [Required(ErrorMessage = "required")]
            public long userId { get; set; }
            [Required(ErrorMessage = "required")]
            public long noteId { get; set; }
        }
        public class EditNoteReturn
        {
            public bool status { get; set; }
            public string message { get; set; }
        }
        //Basheer on 28/02/2019
        public class NoteDescription
        {
            public int Noteid { get; set; }
            public string Description { get; set; }

            public string Title { get; set; }
        }
    }
}