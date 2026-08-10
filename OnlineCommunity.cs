using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutionCloudWeb.Models.PostModel
{
    public class OnlineCommunity
    {
        public class WordList
        {
            public int index { get; set; }
            public long userId { get; set; }
            public string totalWordCount { get; set; }
            public string searchText { get; set; }
            public string sortBy { get; set; }
            public string sortOrder { get; set; }
            public string wordIds { get; set; }
            public string CreatedOn { get; set; } 

        }
        public class WordClass
        {
            public long userId { get; set; }
            public string totalWordCount { get; set; }
            public string TestId { get; set; }
            public string TestId_Second { get; set; }

            public string examId { get; set; }
            public string timePerWord { get; set; }
            public string testTime { get; set; }
            public string LastName { get; set; }
            public string Password { get; set; }
            public string SendUserId { get; set; }
            public string datestring { get; set; }
            public TutionCloudWeb.Models.PostModel.Exam.CreateTest CreateTest { get; set; }

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

        public class AddWord
        {
            public long userId { get; set; }
            public long wordId { get; set; }
        }
        public class AddWordReturn
        {
            public bool status { get; set; }
            public string message { get; set; }
        }

        public class AddAllWord
        {
            public long userId { get; set; }
            public string wordId { get; set; }
        }
        public class AddAllWordReturn
        {
            public bool status { get; set; }
            public string message { get; set; }
        }

        public class ShareAnArchivedTest_Get_Set
        {
            public long ShareAnArchivedTestId { get; set; }
            public Nullable<long> WordId { get; set; }
            public Nullable<long> ExamId { get; set; }
            public Nullable<System.Guid> Guid { get; set; }
            public Nullable<long> UserId { get; set; }
            public string Password { get; set; }
            public string TestID { get; set; }
            public string LastName { get; set; }
            public string SentUserID { get; set; }
            public string Word { get; set; }
            public string Pronounciation { get; set; }
            public string Phrase { get; set; }
            public string Hint { get; set; }
            public string Definition { get; set; }
            public Nullable<bool> IsActive { get; set; }
            public Nullable<bool> ShareOnline { get; set; }
            public string CreatedDate { get; set; }
            public string UpdatedDate { get; set; }
            public string ExaminationId { get; set; }            
            public List<ShareAnArchivedTest_Get_Set> ShareanArchivedTest_Lists { get; set; }
        }

    }
}