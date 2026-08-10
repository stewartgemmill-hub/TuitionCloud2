using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutionCloudWeb.Models.Database;

namespace TutionCloudWeb.Models.DataClass
{
    public class WebService
    {
        private TutionCloudEntities _context = new TutionCloudEntities();

        public List<SP_FETCH_USER_WORDLIST> getUserWordList(long userId, int index, int listCount, string searchText, string sortBy, string sortOrder)
        {
            var list = new List<SP_FETCH_USER_WORDLIST>();
            try
            {
                list = _context.SP_FETCH_USER_WORDLIST(userId, index, listCount, searchText ?? string.Empty, sortBy, sortOrder).ToList().
                    Select(z => new SP_FETCH_USER_WORDLIST(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<SP_FETCH_USER_WORDLIST>();
            }
        }

        public List<SP_FETCH_USER_NOTELIST> getUserNoteList(long userId, int index, string searchText, string sortBy, string sortOrder)
        {
            var list = new List<SP_FETCH_USER_NOTELIST>();
            try
            {
                list = _context.SP_FETCH_USER_NOTELIST(userId, index, searchText ?? string.Empty, sortBy, sortOrder).ToList().
                    Select(z => new SP_FETCH_USER_NOTELIST(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<SP_FETCH_USER_NOTELIST>();
            }
        }

        public SP_FETCH_WORD_ROWNUMBER getUserWordByRowNumber(long userId, int index)
        {
            try
            {
                var row = _context.SP_FETCH_WORD_ROWNUMBER(userId, index).ToList().
                    Select(z => new SP_FETCH_WORD_ROWNUMBER(z)).FirstOrDefault();
                return row;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SP_FETCH_USER_FAVOURITE_WORDLIST> getUserFavouriteWordList(long userId, int index, string searchText, string sortBy, string sortOrder)
        {
            var list = new List<SP_FETCH_USER_FAVOURITE_WORDLIST>();
            try
            {
                list = _context.SP_FETCH_USER_FAVOURITE_WORDLIST(userId, index, searchText ?? string.Empty, sortBy, sortOrder).ToList().
                    Select(z => new SP_FETCH_USER_FAVOURITE_WORDLIST(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<SP_FETCH_USER_FAVOURITE_WORDLIST>();
            }
        }

        public List<SP_FETCH_USER_SKIPPED_WORDLIST> getUserSkiplistWordList(long userId, int index, string searchText, string sortBy, string sortOrder)
        {
            var list = new List<SP_FETCH_USER_SKIPPED_WORDLIST>();
            try
            {
                list = _context.SP_FETCH_USER_SKIPPED_WORDLIST(userId, index, searchText ?? string.Empty, sortBy, sortOrder).ToList().
                    Select(z => new SP_FETCH_USER_SKIPPED_WORDLIST(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<SP_FETCH_USER_SKIPPED_WORDLIST>();
            }
        }


        public List<SP_FETCH_TOTAL_WORDLIST> getUserExamWordList(long userId, int index, int listCount, string searchText, string sortBy, string sortOrder)
        {
            var list = new List<SP_FETCH_TOTAL_WORDLIST>();
            try
            {
                list = _context.SP_FETCH_TOTAL_WORDLIST(userId, index, listCount, searchText ?? string.Empty, sortBy, sortOrder).ToList().
                    Select(z => new SP_FETCH_TOTAL_WORDLIST(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<SP_FETCH_TOTAL_WORDLIST>();
            }
        }


        public List<SP_FETCH_EXAM_WORDS> getExamWords(long userId, long examId)
        {
            var list = new List<SP_FETCH_EXAM_WORDS>();
            try
            {
                list = _context.SP_FETCH_EXAM_WORDS(examId, userId).ToList().
                    Select(z => new SP_FETCH_EXAM_WORDS(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<SP_FETCH_EXAM_WORDS>();
            }
        }

        //SP_FETCH_EXAM_WORDS_Share
        public List<SP_FETCH_EXAM_WORDS_Share> getExamWords_share(long userId, long examId)
        {
            var list = new List<SP_FETCH_EXAM_WORDS_Share>();
            try
            {
                list = _context.SP_FETCH_EXAM_WORDS_Share(examId, userId).ToList().
                    Select(z => new SP_FETCH_EXAM_WORDS_Share(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<SP_FETCH_EXAM_WORDS_Share>();
            }
        }
        public List<SP_FETCH_RESULTS_BY_EXAMID> getTestSummaryWordList(long userId, int index, int listCount, string searchText, string sortBy, string sortOrder, long examId)
        {
            var list = new List<SP_FETCH_RESULTS_BY_EXAMID>();
            try
            {
                list = _context.SP_FETCH_RESULTS_BY_EXAMID(userId, examId, index, searchText ?? string.Empty, sortBy, sortOrder).ToList().
                    Select(z => new SP_FETCH_RESULTS_BY_EXAMID(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<SP_FETCH_RESULTS_BY_EXAMID>();
            }
        }

        public List<SP_FETCH_RESULTS_BY_EXAMID_Share> getTestSummaryWordList_Share(long userId, int index, int listCount, string searchText, string sortBy, string sortOrder, long examId)
        {
            var list = new List<SP_FETCH_RESULTS_BY_EXAMID_Share>();
            try
            {
                list = _context.SP_FETCH_RESULTS_BY_EXAMID_Share(userId, examId, index, searchText ?? string.Empty, sortBy, sortOrder).ToList().
                    Select(z => new SP_FETCH_RESULTS_BY_EXAMID_Share(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<SP_FETCH_RESULTS_BY_EXAMID_Share>();
            }
        }


        public List<SP_FECTH_ONLINE_COMMUNITY_WORDS> getOnlineCommunityWordList(long userId, int index, string searchText, string sortBy, string sortOrder)
        {
            var list = new List<SP_FECTH_ONLINE_COMMUNITY_WORDS>();
            try
            {
                list = _context.SP_FECTH_ONLINE_COMMUNITY_WORDS(userId, index, searchText ?? string.Empty, sortBy, sortOrder).ToList().
                    Select(z => new SP_FECTH_ONLINE_COMMUNITY_WORDS(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<SP_FECTH_ONLINE_COMMUNITY_WORDS>();
            }
        }

        public List<SP_SHOW_ALL_WORDS> showAllWords(long userId)
        {
            var list = new List<SP_SHOW_ALL_WORDS>();
            try
            {
                list = _context.SP_SHOW_ALL_WORDS(userId).ToList().
                    Select(z => new SP_SHOW_ALL_WORDS(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<SP_SHOW_ALL_WORDS>();
            }
        }

        public List<SP_FETCH_USER_CROSSWORD_LIST> getCrosswordList(long userId, int index)
        {
            var list = new List<SP_FETCH_USER_CROSSWORD_LIST>();
            try
            {
                list = _context.SP_FETCH_USER_CROSSWORD_LIST(userId, index).ToList().
                    Select(z => new SP_FETCH_USER_CROSSWORD_LIST(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<SP_FETCH_USER_CROSSWORD_LIST>();
            }
        }

        public List<SP_FETCH_USER_CROSSWORD_WORDLIST> getCrosswordWordList(long userId, long crosswordId)
        {
            var list = new List<SP_FETCH_USER_CROSSWORD_WORDLIST>();
            try
            {
                list = _context.SP_FETCH_USER_CROSSWORD_WORDLIST(userId, crosswordId).ToList().
                    Select(z => new SP_FETCH_USER_CROSSWORD_WORDLIST(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<SP_FETCH_USER_CROSSWORD_WORDLIST>();
            }
        }


        public List<SP_FETCH_USER_TEST> getTestList(long userId, int index)
        {
            var list = new List<SP_FETCH_USER_TEST>();
            try
            {
                list = _context.SP_FETCH_USER_TEST(userId, index).ToList().
                    Select(z => new SP_FETCH_USER_TEST(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<SP_FETCH_USER_TEST>();
            }
        }

        public List<SP_GET_NOTEDETAILS> getNotedetails(int noteid)
        {
            var list = new List<SP_GET_NOTEDETAILS>();
            try
            {
                list = _context.SP_GET_NOTEDETAILS(noteid).ToList().
                    Select(z => new SP_GET_NOTEDETAILS(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<SP_GET_NOTEDETAILS>();
            }
        }

        public List<SP_SHOW_REVIEWED_WORDS> showREVIEWEDWords(long userId,string reviewedWordsId)
        {
            var list = new List<SP_SHOW_REVIEWED_WORDS>();
            try
            {
                list = _context.SP_SHOW_REVIEWED_WORDS(userId, reviewedWordsId).ToList().
                    Select(z => new SP_SHOW_REVIEWED_WORDS(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<SP_SHOW_REVIEWED_WORDS>();
            }
        }

        //public List<SP_PV_SHARING_SharedwithMeWords_List> GETSharedwithMeWords(long userId, int index, int listCount, string searchText, string sortBy, string sortOrder,string CreatedOn)
      public List<SP_PV_SHARING_SharedwithMeWords_List> GETSharedwithMeWords(TutionCloudWeb.Models.PostModel.Word.WordList Model)
           
    {
            List<SP_PV_SHARING_SharedwithMeWords_List> list = new List<SP_PV_SHARING_SharedwithMeWords_List>();
            try
            {               
               
                var results = _context.SP_PV_SHARING_SharedwithMeWords_List(Model.userId, Model.index, Model.ListCount, Model.searchText ?? string.Empty, Model.sortBy, Model.sortOrder, Model.CreatedOn).ToList();
               
                foreach (var a1 in results){
                    SP_PV_SHARING_SharedwithMeWords_List model = new SP_PV_SHARING_SharedwithMeWords_List();
                    model.CREATEDDATE = a1.CREATEDDATE;
                    model.DEFINITION = a1.DEFINITION;
                    model.HINT = a1.HINT;
                    model.OVERALLSCORE = a1.OVERALLSCORE;
                    model.PHRASE = a1.PHRASE;
                    model.PRONOUNCIATION = a1.PRONOUNCIATION;
                    model.UPDATEDDATE = a1.UPDATEDDATE;
                    model.WORD = a1.WORD;
                    model.WORDGUID = a1.WORDGUID;
                    model.WORDID = a1.WORDID;
                    list.Add(model);
                }
                return list;
            }
            catch (Exception ex)
            {
                return new List<SP_PV_SHARING_SharedwithMeWords_List>();
            }
        }

      public List<SP_PV_SHARING_SharedwithMeWords_List> GETSharedwithMeWords_Test(TutionCloudWeb.Models.PostModel.Word.WordList Model)
      {
          List<SP_PV_SHARING_SharedwithMeWords_List> list = new List<SP_PV_SHARING_SharedwithMeWords_List>();
          try
          {

              var results = _context.SP_PV_SHARING_SharedwithMeWords_List_Test_Test(Model.userId, Model.index, Model.ListCount, Model.searchText ?? string.Empty, Model.sortBy, Model.sortOrder, Model.CreatedOn).ToList();

              foreach (var a1 in results)
              {
                  SP_PV_SHARING_SharedwithMeWords_List model = new SP_PV_SHARING_SharedwithMeWords_List();
                  model.CREATEDDATE = a1.CREATEDDATE;
                  model.DEFINITION = a1.DEFINITION;
                  model.HINT = a1.HINT;
                  model.OVERALLSCORE = a1.OVERALLSCORE;
                  model.PHRASE = a1.PHRASE;
                  model.PRONOUNCIATION = a1.PRONOUNCIATION;
                  model.UPDATEDDATE = a1.UPDATEDDATE;
                  model.WORD = a1.WORD;
                  model.WORDGUID = a1.WORDGUID;
                  model.WORDID = a1.WORDID;
                  list.Add(model);
              }
              return list;
          }
          catch (Exception ex)
          {
              return new List<SP_PV_SHARING_SharedwithMeWords_List>();
          }
      }


      public List<SP_PV_SHARING_SharedwithMeWords_List> GETSWordListSharedwithbyWords(TutionCloudWeb.Models.PostModel.Word.WordList Model)
      {
          List<SP_PV_SHARING_SharedwithMeWords_List> list = new List<SP_PV_SHARING_SharedwithMeWords_List>();
          try
          {

              var results = _context.SP_PV_SHARING_WordListSharedwithbyWords_List(Model.index, Model.ListCount, Model.searchText ?? string.Empty, Model.sortBy, Model.sortOrder, Model.CreatedOn).ToList();

              foreach (var a1 in results)
              {
                  var check = list.Find(z => z.WORD == a1.WORD);
                  if (check == null)
                  {
                      SP_PV_SHARING_SharedwithMeWords_List model = new SP_PV_SHARING_SharedwithMeWords_List();
                      model.CREATEDDATE = a1.CREATEDDATE;
                      model.DEFINITION = a1.DEFINITION;
                      model.HINT = a1.HINT;
                      model.OVERALLSCORE = a1.OVERALLSCORE;
                      model.PHRASE = a1.PHRASE;
                      model.PRONOUNCIATION = a1.PRONOUNCIATION;
                      model.UPDATEDDATE = a1.UPDATEDDATE;
                      model.WORD = a1.WORD;
                      model.WORDGUID = a1.WORDGUID;
                      model.WORDID = a1.WORDID;
                      list.Add(model);
                  }
              }
              return list.Distinct().ToList();
          }
          catch (Exception ex)
          {
              return new List<SP_PV_SHARING_SharedwithMeWords_List>();
          }
      }

      public List<SP_PV_SHARING_SharedwithMeWords_List> GETSharedwithMeWordsNew(TutionCloudWeb.Models.PostModel.Word.WordList Model)
      {
          List<SP_PV_SHARING_SharedwithMeWords_List> list = new List<SP_PV_SHARING_SharedwithMeWords_List>();
          try
          {

              //var results = _context.SP_PV_WordListSharedbyMe_List(Model.userId, Model.index, Model.ListCount, Model.searchText ?? string.Empty, Model.sortBy, Model.sortOrder, Model.CreatedOn).ToList();
              var results = _context.SP_PV_WordListSharedbyMe_List(Model.userId, null, null, Model.searchText ?? string.Empty, Model.sortBy, Model.sortOrder, Model.CreatedOn).ToList();

              foreach (var a1 in results)
              {
                  SP_PV_SHARING_SharedwithMeWords_List model = new SP_PV_SHARING_SharedwithMeWords_List();
                  model.CREATEDDATE = a1.CREATEDDATE;
                  model.DEFINITION = a1.DEFINITION;
                  model.HINT = a1.HINT;
                  model.OVERALLSCORE = a1.OVERALLSCORE;
                  model.PHRASE = a1.PHRASE;
                  model.PRONOUNCIATION = a1.PRONOUNCIATION;
                  model.UPDATEDDATE = a1.UPDATEDDATE;
                  model.WORD = a1.WORD;
                  model.WORDGUID = a1.WORDGUID;
                  model.WORDID = a1.WORDID;
                  list.Add(model);
              }
              return list;
          }
          catch (Exception ex)
          {
              return new List<SP_PV_SHARING_SharedwithMeWords_List>();
          }
      }

      public List<SP_PV_SHARING_SharedwithMeWords_List> GETSWordListShareAnArchivedTest(TutionCloudWeb.Models.PostModel.Word.WordList Model)
      {
          List<SP_PV_SHARING_SharedwithMeWords_List> list = new List<SP_PV_SHARING_SharedwithMeWords_List>();
          try
          {

              var results = _context.SP_PV_SHARING_WordListShareAnArchivedTest_List(Model.index, Model.ListCount, Model.searchText ?? string.Empty, Model.sortBy, Model.sortOrder, Model.CreatedOn).ToList();

              foreach (var a1 in results)
              {
                  var check = list.Find(z => z.WORD == a1.WORD);
                  if (check == null)
                  {
                      SP_PV_SHARING_SharedwithMeWords_List model = new SP_PV_SHARING_SharedwithMeWords_List();
                      model.CREATEDDATE = Convert.ToDateTime(a1.CREATEDDATE);
                      model.DEFINITION = a1.DEFINITION;
                      model.HINT = a1.HINT;
                      model.OVERALLSCORE = a1.OVERALLSCORE;
                      model.PHRASE = a1.PHRASE;
                      model.PRONOUNCIATION = a1.PRONOUNCIATION;
                      model.UPDATEDDATE = Convert.ToDateTime(a1.UPDATEDDATE);
                      model.WORD = a1.WORD;
                      model.WORDGUID = a1.WORDGUID;
                      model.WORDID = a1.WORDID.ToString();
                      list.Add(model);
                  }
              }
              return list.Distinct().ToList();
          }
          catch (Exception ex)
          {
              return new List<SP_PV_SHARING_SharedwithMeWords_List>();
          }
      }

      public List<SP_PV_SHARING_SharedwithMeWords_List> GETSWordListSharedTestSharedbyMeWords(TutionCloudWeb.Models.PostModel.Word.WordList Model)
      {
          List<SP_PV_SHARING_SharedwithMeWords_List> list = new List<SP_PV_SHARING_SharedwithMeWords_List>();
          try
          {

              var results = _context.SP_PV_SHARING_WordListTestSharedbyMeWords_List(Model.index, Model.ListCount, Model.searchText ?? string.Empty, Model.sortBy, Model.sortOrder, Model.CreatedOn).ToList();

              foreach (var a1 in results)
              {
                  var check = list.Find(z => z.WORD == a1.WORD);
                  if (check == null)
                  {
                      SP_PV_SHARING_SharedwithMeWords_List model = new SP_PV_SHARING_SharedwithMeWords_List();
                      model.CREATEDDATE = a1.CREATEDDATE;
                      model.DEFINITION = a1.DEFINITION;
                      model.HINT = a1.HINT;
                      model.OVERALLSCORE = a1.OVERALLSCORE;
                      model.PHRASE = a1.PHRASE;
                      model.PRONOUNCIATION = a1.PRONOUNCIATION;
                      model.UPDATEDDATE = a1.UPDATEDDATE;
                      model.WORD = a1.WORD;
                      model.WORDGUID = a1.WORDGUID;
                      model.WORDID = a1.WORDID;
                      list.Add(model);
                  }
              }
              return list.Distinct().ToList();
          }
          catch (Exception ex)
          {
              return new List<SP_PV_SHARING_SharedwithMeWords_List>();
          }
      }


        //..........................Quester.......................


        public List<SP_PV_SHARING_SharedwithMeWords_List> Quester_GETSharedwithMeWords(TutionCloudWeb.Models.PostModel.Word.WordList Model)

        {
            List<SP_PV_SHARING_SharedwithMeWords_List> list = new List<SP_PV_SHARING_SharedwithMeWords_List>();
            try
            {

                var results = _context.Quester_SP_PV_SHARING_SharedwithMeWords_List(Model.userId, Model.index, Model.ListCount, Model.searchText ?? string.Empty, Model.sortBy, Model.sortOrder, Model.CreatedOn).ToList();

                foreach (var a1 in results)
                {
                    SP_PV_SHARING_SharedwithMeWords_List model = new SP_PV_SHARING_SharedwithMeWords_List();
                    model.CREATEDDATE = a1.CREATEDDATE;
                    model.DEFINITION = a1.DEFINITION;
                    model.HINT = a1.HINT;
                    model.OVERALLSCORE = a1.OVERALLSCORE;
                    model.PHRASE = a1.PHRASE;
                    model.PRONOUNCIATION = a1.PRONOUNCIATION;
                    model.UPDATEDDATE = a1.UPDATEDDATE;
                    model.WORD = a1.WORD;
                    model.WORDGUID = a1.WORDGUID;
                    model.WORDID = a1.WORDID;
                    list.Add(model);
                }
                return list;
            }
            catch (Exception ex)
            {
                return new List<SP_PV_SHARING_SharedwithMeWords_List>();
            }
        }


        public List<SP_PV_SHARING_SharedwithMeWords_List> Quester_GETSWordListSharedwithbyWords(TutionCloudWeb.Models.PostModel.Word.WordList Model)
        {
            List<SP_PV_SHARING_SharedwithMeWords_List> list = new List<SP_PV_SHARING_SharedwithMeWords_List>();
            try
            {

                var results = _context.Quester_SP_PV_SHARING_WordListSharedwithbyWords_List(Model.index, Model.ListCount, Model.searchText ?? string.Empty, Model.sortBy, Model.sortOrder, Model.CreatedOn).ToList();

                foreach (var a1 in results)
                {
                    var check = list.Find(z => z.WORD == a1.WORD);
                    if (check == null)
                    {
                        SP_PV_SHARING_SharedwithMeWords_List model = new SP_PV_SHARING_SharedwithMeWords_List();
                        model.CREATEDDATE = a1.CREATEDDATE;
                        model.DEFINITION = a1.DEFINITION;
                        model.HINT = a1.HINT;
                        model.OVERALLSCORE = a1.OVERALLSCORE;
                        model.PHRASE = a1.PHRASE;
                        model.PRONOUNCIATION = a1.PRONOUNCIATION;
                        model.UPDATEDDATE = a1.UPDATEDDATE;
                        model.WORD = a1.WORD;
                        model.WORDGUID = a1.WORDGUID;
                        model.WORDID = a1.WORDID;
                        list.Add(model);
                    }
                }
                return list.Distinct().ToList();
            }
            catch (Exception ex)
            {
                return new List<SP_PV_SHARING_SharedwithMeWords_List>();
            }
        }



        public List<SP_PV_SHARING_SharedwithMeWords_List> Quester_GETSWordListShareAnArchivedTest(TutionCloudWeb.Models.PostModel.Word.WordList Model)
        {
            List<SP_PV_SHARING_SharedwithMeWords_List> list = new List<SP_PV_SHARING_SharedwithMeWords_List>();
            try
            {
                       
               var results = _context.Quester_SP_PV_SHARING_WordListShareAnArchivedTest_List(Model.index, Model.ListCount, Model.searchText ?? string.Empty, Model.sortBy, Model.sortOrder, Model.CreatedOn).ToList();

                foreach (var a1 in results)
                {
                    var check = list.Find(z => z.WORD == a1.WORD);
                    if (check == null)
                    {
                        SP_PV_SHARING_SharedwithMeWords_List model = new SP_PV_SHARING_SharedwithMeWords_List();
                        model.CREATEDDATE = Convert.ToDateTime(a1.CREATEDDATE);
                        model.DEFINITION = a1.DEFINITION;
                        model.HINT = a1.HINT;
                        model.OVERALLSCORE = a1.OVERALLSCORE;
                        model.PHRASE = a1.PHRASE;
                        model.PRONOUNCIATION = a1.PRONOUNCIATION;
                        model.UPDATEDDATE = Convert.ToDateTime(a1.UPDATEDDATE);
                        model.WORD = a1.WORD;
                        model.WORDGUID = a1.WORDGUID;
                        model.WORDID = a1.WORDID.ToString();
                        list.Add(model);
                    }
                }
                return list.Distinct().ToList();
            }
            catch (Exception ex)
            {
                return new List<SP_PV_SHARING_SharedwithMeWords_List>();
            }
        }


        public List<SP_PV_SHARING_SharedwithMeWords_List> Quester_GETSharedwithMeWords_Test(TutionCloudWeb.Models.PostModel.Word.WordList Model)
        {
            List<SP_PV_SHARING_SharedwithMeWords_List> list = new List<SP_PV_SHARING_SharedwithMeWords_List>();
            try
            {

                var results = _context.Quester_SP_PV_SHARING_SharedwithMeWords_List_Test_Test(Model.userId, Model.index, Model.ListCount, Model.searchText ?? string.Empty, Model.sortBy, Model.sortOrder, Model.CreatedOn).ToList();

                foreach (var a1 in results)
                {
                    SP_PV_SHARING_SharedwithMeWords_List model = new SP_PV_SHARING_SharedwithMeWords_List();
                    model.CREATEDDATE = a1.CREATEDDATE;
                    model.DEFINITION = a1.DEFINITION;
                    model.HINT = a1.HINT;
                    model.OVERALLSCORE = a1.OVERALLSCORE;
                    model.PHRASE = a1.PHRASE;
                    model.PRONOUNCIATION = a1.PRONOUNCIATION;
                    model.UPDATEDDATE = a1.UPDATEDDATE;
                    model.WORD = a1.WORD;
                    model.WORDGUID = a1.WORDGUID;
                    model.WORDID = a1.WORDID;
                    list.Add(model);
                }
                return list;
            }
            catch (Exception ex)
            {
                return new List<SP_PV_SHARING_SharedwithMeWords_List>();
            }
        }

        public List<Quester_SP_FETCH_EXAM_WORDS_Share> Quester_getExamWords_share(long userId, long examId)
        {
            var list = new List<Quester_SP_FETCH_EXAM_WORDS_Share>();
            try
            {
                list = _context.Quester_SP_FETCH_EXAM_WORDS_Share(examId, userId).ToList().
                    Select(z => new Quester_SP_FETCH_EXAM_WORDS_Share(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<Quester_SP_FETCH_EXAM_WORDS_Share>();
            }
        }


        public List<SP_PV_SHARING_SharedwithMeWords_List> Quester_GETSWordListSharedTestSharedbyMeWords(TutionCloudWeb.Models.PostModel.Word.WordList Model)
        {
            List<SP_PV_SHARING_SharedwithMeWords_List> list = new List<SP_PV_SHARING_SharedwithMeWords_List>();
            try
            {

                var results = _context.Quester_SP_PV_SHARING_WordListTestSharedbyMeWords_List(Model.index, Model.ListCount, Model.searchText ?? string.Empty, Model.sortBy, Model.sortOrder, Model.CreatedOn).ToList();

                foreach (var a1 in results)
                {
                    var check = list.Find(z => z.WORD == a1.WORD);
                    if (check == null)
                    {
                        SP_PV_SHARING_SharedwithMeWords_List model = new SP_PV_SHARING_SharedwithMeWords_List();
                        model.CREATEDDATE = a1.CREATEDDATE;
                        model.DEFINITION = a1.DEFINITION;
                        model.HINT = a1.HINT;
                        model.OVERALLSCORE = a1.OVERALLSCORE;
                        model.PHRASE = a1.PHRASE;
                        model.PRONOUNCIATION = a1.PRONOUNCIATION;
                        model.UPDATEDDATE = a1.UPDATEDDATE;
                        model.WORD = a1.WORD;
                        model.WORDGUID = a1.WORDGUID;
                        model.WORDID = a1.WORDID;
                        list.Add(model);
                    }
                }
                return list.Distinct().ToList();
            }
            catch (Exception ex)
            {
                return new List<SP_PV_SHARING_SharedwithMeWords_List>();
            }
        }




    }
}