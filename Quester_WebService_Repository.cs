using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutionCloudWeb.Model.Quester;
using TutionCloudWeb.Models.DataClass;


namespace TutionCloudWeb.Repository.Quester
{
    public class Quester_WebService_Repository :Quester_BaseRepository
    {
        public Quester_SP_FETCH_WORD_ROWNUMBER Quester_getUserWordByRowNumber(long userId, int index)
        {
            try
            {
                var row = _context.Quester_SP_FETCH_WORD_ROWNUMBER(userId, index).ToList().
                    Select(z => new Quester_SP_FETCH_WORD_ROWNUMBER(z)).FirstOrDefault();
                return row;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Quester_SP_FETCH_USER_WORDLIST> Quester_getUserWordList(long userId, int index, int listCount, string searchText, string sortBy, string sortOrder)
        {
            var list = new List<Quester_SP_FETCH_USER_WORDLIST>();
            try
            {
                list = _context.Quester_SP_FETCH_USER_WORDLIST(userId, index, listCount, searchText ?? string.Empty, sortBy, sortOrder).ToList().
                    Select(z => new Quester_SP_FETCH_USER_WORDLIST(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<Quester_SP_FETCH_USER_WORDLIST>();
            }
        }

        public List<Quester_SP_FETCH_USER_WORDLIST_Model> Quester_getUserWordList_Folder(long userId, int index, int listCount, string searchText, string sortBy, string sortOrder)
        {
           List<Quester_SP_FETCH_USER_WORDLIST_Model> list = new List<Quester_SP_FETCH_USER_WORDLIST_Model>();
            try
            {

                
                string[] identifireSlit = searchText.Split(',');
                //string[] identifireSlit2;

                List<String> temp = new List<string>();
                foreach (var s in identifireSlit)
                {
                    if (s!= "null")
                    {
                        temp.Add(s);
                    }
                        
                }
                identifireSlit = temp.ToArray();
                
                
                foreach (var a1 in identifireSlit)
                {
                   
                        Guid guid = new Guid(a1);
                  
                    
                    string wodId_searchText = _context.Quester_Word.Where(x => x.IsActive == true && x.Guid == guid).Select(x => x.Word).FirstOrDefault();

                    if(wodId_searchText != null)
                    {
                   //     var list_wodId_searchText = _context.Quester_SP_FETCH_USER_WORDLIST(userId, index, listCount, wodId_searchText ?? string.Empty, sortBy, sortOrder).ToList().
                   //Select(z => new Quester_SP_FETCH_USER_WORDLIST(z)).ToList();

                        var list_wodId_searchText = _context.Quester_SP_FETCH_USER_WORDLIST_Folder_Search(userId, index, listCount, wodId_searchText ?? string.Empty, sortBy, sortOrder).ToList().
                   Select(z => new Quester_SP_FETCH_USER_WORDLIST_Folder_Search(z)).ToList();



                        foreach (var a2 in list_wodId_searchText)
                        {
                            Quester_SP_FETCH_USER_WORDLIST_Model mo = new Quester_SP_FETCH_USER_WORDLIST_Model();
                           
                            var sorting = list.Where(x => x.WORDID == a2.WORDID).FirstOrDefault();

                            if (sorting == null)
                            {
                                mo.WORDID = a2.WORDID;
                                mo.WORDGUID = a2.WORDGUID;
                                mo.WORD = a2.WORD;
                                mo.PRONOUNCIATION = a2.PRONOUNCIATION;
                                mo.PHRASE = a2.PHRASE;
                                mo.HINT = a2.HINT;
                                mo.DEFINITION = a2.DEFINITION;
                                mo.CATEGORYNAME = a2.CATEGORYNAME;
                                mo.CREATEDDATE = a2.CREATEDDATE;
                                mo.UPDATEDDATE = a2.UPDATEDDATE;
                                mo.OVERALLSCORE = a2.OVERALLSCORE;

                                list.Add(mo);
                            }
                            

                        }
                    }

                  


                }

                return list;
            }
            catch (Exception ex)
            {
                return new List<Quester_SP_FETCH_USER_WORDLIST_Model>();
            }
        }



        public List<Quester_SP_FETCH_USER_NOTELIST> Quester_getUserNoteList(long userId, int index, string searchText, string sortBy, string sortOrder)
        {
            var list = new List<Quester_SP_FETCH_USER_NOTELIST>();
            try
            {
                list = _context.Quester_SP_FETCH_USER_NOTELIST(userId, index, searchText ?? string.Empty, sortBy, sortOrder).ToList().
                    Select(z => new Quester_SP_FETCH_USER_NOTELIST(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<Quester_SP_FETCH_USER_NOTELIST>();
            }
        }

        public List<Quester_SP_GET_NOTEDETAILS> Quester_getNotedetails(int noteid)
        {
            var list = new List<Quester_SP_GET_NOTEDETAILS>();
            try
            {
                list = _context.Quester_SP_GET_NOTEDETAILS(noteid).ToList().
                    Select(z => new Quester_SP_GET_NOTEDETAILS(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<Quester_SP_GET_NOTEDETAILS>();
            }
        }

        public List<Quester_SP_FETCH_USER_FAVOURITE_WORDLIST> Quester_getUserFavouriteWordList(long userId, int index, string searchText, string sortBy, string sortOrder)
        {
            var list = new List<Quester_SP_FETCH_USER_FAVOURITE_WORDLIST>();
            try
            {
                list = _context.Quester_SP_FETCH_USER_FAVOURITE_WORDLIST(userId, index, searchText ?? string.Empty, sortBy, sortOrder).ToList().
                    Select(z => new Quester_SP_FETCH_USER_FAVOURITE_WORDLIST(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<Quester_SP_FETCH_USER_FAVOURITE_WORDLIST>();
            }
        }
                    
        public List<Quester_SP_FETCH_USER_SKIPPED_WORDLIST> Quester_getUserSkiplistWordList(long userId, int index, string searchText, string sortBy, string sortOrder)
        {
            var list = new List<Quester_SP_FETCH_USER_SKIPPED_WORDLIST>();
            try
            {
                list = _context.Quester_SP_FETCH_USER_SKIPPED_WORDLIST(userId, index, searchText ?? string.Empty, sortBy, sortOrder).ToList().
                    Select(z => new Quester_SP_FETCH_USER_SKIPPED_WORDLIST(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<Quester_SP_FETCH_USER_SKIPPED_WORDLIST>();
            }
        }

        public List<Quester_SP_FETCH_USER_SKIPPED_WORDLIST_Model> Quester_getUserSkiplistWordList_Folder(long userId, int index, string searchText, string sortBy, string sortOrder)
        {

            //Quester_SP_FETCH_USER_SKIPPED_WORDLIST_Model

            // var list = new List<Quester_SP_FETCH_USER_SKIPPED_WORDLIST>();

            List<Quester_SP_FETCH_USER_SKIPPED_WORDLIST_Model> list = new List<Quester_SP_FETCH_USER_SKIPPED_WORDLIST_Model>();

            try
            {

                string jsondata = _context.Quester_TreeView.Where(x => x.UserId == userId && x.IsActive == true).Select(x => x.TreeData).FirstOrDefault(); ;
                List<Quester_TreeView_Model> DeserializeData = JsonConvert.DeserializeObject<List<Quester_TreeView_Model>>(jsondata);

                var Selectquestions = DeserializeData.Where(x => x.pId == searchText && x.isParent == false).ToList();

                foreach (var a1 in Selectquestions) {

                    Guid guid = new Guid(a1.tId);
                    string selecttext = _context.Quester_Word.Where(x => x.Guid == guid).Select(x => x.Word).FirstOrDefault();

                    var listItems = _context.Quester_SP_FETCH_USER_SKIPPED_WORDLIST(userId, index, selecttext ?? string.Empty, sortBy, sortOrder).ToList();

                    foreach (var a2 in listItems)
                    {
                        Quester_SP_FETCH_USER_SKIPPED_WORDLIST_Model mo = new Quester_SP_FETCH_USER_SKIPPED_WORDLIST_Model();
                        mo.WORDID = a2.WORDID;
                        mo.WORDGUID = a2.WORDGUID;
                        mo.WORD = a2.WORD;
                        mo.PRONOUNCIATION = a2.PRONOUNCIATION;
                        mo.PHRASE = a2.PHRASE;
                        mo.HINT = a2.HINT;
                        mo.DEFINITION = a2.DEFINITION;
                        mo.CATEGORYNAME = a2.CATEGORYNAME;
                        mo.CREATEDDATE = a2.CREATEDDATE;
                        mo.UPDATEDDATE = a2.UPDATEDDATE;
                       // mo.OVERALLSCORE = a2.OVERALLSCORE;

                        list.Add(mo);

                    }

                }


                //var list = _context.Quester_SP_FETCH_USER_SKIPPED_WORDLIST(userId, index, searchText ?? string.Empty, sortBy, sortOrder).ToList().
                //    Select(z => new Quester_SP_FETCH_USER_SKIPPED_WORDLIST(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<Quester_SP_FETCH_USER_SKIPPED_WORDLIST_Model>();
            }
        }



        public List<Quester_SP_FETCH_TOTAL_WORDLIST> Quester_getUserExamWordList(long userId, int index, int listCount, string searchText, string sortBy, string sortOrder)
        {
            var list = new List<Quester_SP_FETCH_TOTAL_WORDLIST>();
            try
            {
                list = _context.Quester_SP_FETCH_TOTAL_WORDLIST(userId, index, listCount, searchText ?? string.Empty, sortBy, sortOrder).ToList().
                    Select(z => new Quester_SP_FETCH_TOTAL_WORDLIST(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<Quester_SP_FETCH_TOTAL_WORDLIST>();
            }
        }


        public List<Quester_SP_FECTH_ONLINE_COMMUNITY_WORDS> Quester_getOnlineCommunityWordList(long userId, int index, string searchText, string sortBy, string sortOrder)
        {
            var list = new List<Quester_SP_FECTH_ONLINE_COMMUNITY_WORDS>();
            try
            {
                list = _context.Quester_SP_FECTH_ONLINE_COMMUNITY_WORDS(userId, index, searchText ?? string.Empty, sortBy, sortOrder).ToList().
                    Select(z => new Quester_SP_FECTH_ONLINE_COMMUNITY_WORDS(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<Quester_SP_FECTH_ONLINE_COMMUNITY_WORDS>();
            }
        }

        public List<Quester_SP_FETCH_EXAM_WORDS> Quester_getExamWords(long userId, long examId)
        {
            var list = new List<Quester_SP_FETCH_EXAM_WORDS>();
            try
            {
                list = _context.Quester_SP_FETCH_EXAM_WORDS(examId, userId).ToList().
                    Select(z => new Quester_SP_FETCH_EXAM_WORDS(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<Quester_SP_FETCH_EXAM_WORDS>();
            }
        }

        public List<Quester_SP_FETCH_USER_TEST> Quester_getTestList(long userId, int index)
        {
            var list = new List<Quester_SP_FETCH_USER_TEST>();
            try
            {
                list = _context.Quester_SP_FETCH_USER_TEST(userId, index).ToList().
                    Select(z => new Quester_SP_FETCH_USER_TEST(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<Quester_SP_FETCH_USER_TEST>();
            }
        }


        public List<Quester_SP_FETCH_RESULTS_BY_EXAMID> Quester_getTestSummaryWordList(long userId, int index, int listCount, string searchText, string sortBy, string sortOrder, long examId)
        {
            var list = new List<Quester_SP_FETCH_RESULTS_BY_EXAMID>();
            try
            {
                list = _context.Quester_SP_FETCH_RESULTS_BY_EXAMID(userId, examId, index, searchText ?? string.Empty, sortBy, sortOrder).ToList().
                    Select(z => new Quester_SP_FETCH_RESULTS_BY_EXAMID(z)).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<Quester_SP_FETCH_RESULTS_BY_EXAMID>();
            }
        }



    }
}