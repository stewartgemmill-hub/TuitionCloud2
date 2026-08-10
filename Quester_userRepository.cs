using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using TutionCloudWeb.Model.Quester;
using TutionCloudWeb.Models.Database;

namespace TutionCloudWeb.Repository.Quester
{
    public class Quester_userRepository : Quester_BaseRepository
    {
        #region Quester Words
        internal Quester_ReturnMessage_Model Quester_addword(Quester_AddWord model)
        {
            var returnModel = new Quester_ReturnMessage_Model();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                if (model.CategoryName != null && model.CategoryName_TextBox != null)
                {
                    if ((model.CategoryName.Length > 0) && (model.CategoryName_TextBox.Length > 0))
                    {
                        returnModel.status = false;
                        returnModel.message = "Multiple category Not allowed";

                        return returnModel;
                    }
                }
                

                if(model.CategoryName != null)
                {
                    if (model.CategoryName.Length > 0)
                    {
                        var user = _context.Quester_User.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                        if (user != null)
                        {

                            TutionCloudEntities Category_context = new TutionCloudEntities();

                            long catID = Convert.ToInt32(model.CategoryName);

                            var catogeryChek = Category_context.Quester_Category.Where(x => x.CategoryId == catID && x.IsActive == true).FirstOrDefault();

                            if (catogeryChek != null)
                            {

                                string questions = model.word.Trim();

                                var questionsChek = Category_context.Quester_Word.Where(x => x.UserId == model.userId && x.Word == questions && x.IsActive == true).FirstOrDefault();

                                if (questionsChek == null)
                                {
                                    System.Guid Guid = Guid.NewGuid();

                                    var currentTime = System.DateTime.UtcNow;
                                    var newWord = _context.Quester_Word.Create();
                                    newWord.CreatedDate = currentTime;
                                    newWord.Definition = "Definition";//model.definition.Trim() ?? string.Empty;
                                    newWord.Guid = Guid;

                                    if (model.hint == null)
                                    {
                                        newWord.Hint = "-";
                                    }
                                    else
                                    {
                                        newWord.Hint = model.hint.Trim() ?? string.Empty;
                                    }
                                    newWord.IsActive = true;
                                    if (!string.IsNullOrWhiteSpace(model.phrase))
                                    {
                                        newWord.Phrase = model.phrase.Trim() ?? string.Empty;
                                    }
                                    else
                                    {
                                        newWord.Phrase = "-";
                                    }
                                    newWord.Pronounciation = model.pronounciation.Trim() ?? string.Empty;
                                    newWord.ShareOnline = model.sharOnline;
                                    newWord.UpdatedDate = currentTime;
                                    newWord.UserId = model.userId;
                                    newWord.Word = model.word.Trim() ?? string.Empty;

                                    newWord.CategoryName = catogeryChek.CategoryName;
                                    newWord.CategoryId = catogeryChek.CategoryId;


                                    _context.Quester_Word.Add(newWord);

                                    returnModel.status = _context.SaveChanges() > 0 ? true : false;
                                    returnModel.message = returnModel.status == true ? Guid.ToString() : "failed";
                                }
                                else
                                {
                                    returnModel.status = false;
                                    returnModel.message = "Duplicate question detection";
                                }

                                
                            }
                            else
                            {
                                returnModel.status = false;
                                returnModel.message = "catogery";
                            }


                           
                        }
                        else
                        {
                            returnModel.status = false;
                            returnModel.message = "Invalid user";
                        }
                    }

                   
                }
                else if (model.CategoryName_TextBox != null)
                {
                    if (model.CategoryName_TextBox.Length > 0)
                    {
                        var user = _context.Quester_User.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                        if (user != null)
                        {
                            ///This to starting..........
                            ///
                            TutionCloudEntities Category_context = new TutionCloudEntities();
                            var catogeryChek = Category_context.Quester_Category.Where(x => x.UserId== model.userId && x.CategoryName == model.CategoryName_TextBox && x.IsActive == true).FirstOrDefault();
                            if (catogeryChek == null)
                            {

                                ///This to starting..........
                                ///

                                var currentTime = System.DateTime.UtcNow;

                                var catego = Category_context.Quester_Category.Create();
                                catego.UserId = model.userId;
                                catego.CategoryName = model.CategoryName_TextBox;
                                catego.IsActive = true;
                                catego.UpdateDate = currentTime;
                                catego.CreateDate = currentTime;
                                Category_context.Quester_Category.Add(catego);
                                Category_context.SaveChanges();

                                var getcata = Category_context.Quester_Category.Where(x => x.UserId == model.userId && x.CategoryName == model.CategoryName_TextBox && x.IsActive == true).FirstOrDefault();

                                if (getcata != null)
                                {
                                    System.Guid Guid = Guid.NewGuid();

                                    var newWord = _context.Quester_Word.Create();
                                    newWord.CreatedDate = currentTime;
                                    newWord.Definition = "Definition";//model.definition.Trim() ?? string.Empty;
                                    newWord.Guid = Guid;
                                    newWord.Hint = model.hint.Trim() ?? string.Empty;
                                    newWord.IsActive = true;
                                    if (!string.IsNullOrWhiteSpace(model.phrase))
                                    {
                                        newWord.Phrase = model.phrase.Trim() ?? string.Empty;
                                    }
                                    else
                                    {
                                        newWord.Phrase = "-";
                                    }
                                    //newWord.Phrase = model.phrase.Trim() ?? string.Empty;

                                    newWord.Pronounciation = model.pronounciation.Trim() ?? string.Empty;
                                    newWord.ShareOnline = model.sharOnline;
                                    newWord.UpdatedDate = currentTime;
                                    newWord.UserId = model.userId;
                                    newWord.Word = model.word.Trim() ?? string.Empty;

                                    newWord.CategoryName = getcata.CategoryName;
                                    newWord.CategoryId = getcata.CategoryId;

                                    _context.Quester_Word.Add(newWord);

                                    returnModel.status = _context.SaveChanges() > 0 ? true : false;
                                    returnModel.message = returnModel.status == true ? Guid.ToString() : "failed";
                                }
                                else
                                {
                                    returnModel.status = false;
                                    returnModel.message = "Question not saved please refresh page";
                                    return returnModel;
                                }

                                
                            }
                            else
                            {
                                returnModel.status = false;
                                returnModel.message = "Category already Exists";
                                return returnModel;
                            }

                            ////////////////


                            
                        }
                        else
                        {
                            returnModel.status = false;
                            returnModel.message = "Invalid user";
                            return returnModel;
                        }
                    }
                    
                }
                else
                {
                    returnModel.status = false;
                    returnModel.message = "Please select Category";
                    return returnModel;
                }

                




                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }

        internal Tuple<bool, string> Quester_CreateWord_Add_Tree(string jsondatas, long userId)
        {
            bool status = false;
            string message = "failed";

            try
            {
                var gettreedata = _context.Quester_TreeView.Where(x => x.UserId == userId && x.IsActive == true).FirstOrDefault();
                if (gettreedata == null)
                {
                    var createtreeview = _context.Quester_TreeView.Create();
                    createtreeview.UserId = userId;
                    createtreeview.TreeData = jsondatas;
                    createtreeview.Guid = Guid.NewGuid();
                    createtreeview.IsActive = true;
                    createtreeview.CreatedDate = DateTime.Now;
                    createtreeview.UpdatedDate = DateTime.Now;

                    _context.Quester_TreeView.Add(createtreeview);

                     status = _context.SaveChanges() > 0 ? true : false;
                     message =  status == true ? "Success" : "failed";

                    return new Tuple<bool, string>(status, message);
                }
                else
                {

                    gettreedata.TreeData = jsondatas;
                    gettreedata.UpdatedDate = DateTime.Now;

                    status = _context.SaveChanges() > 0 ? true : false;
                    message = status == true ? "Success" : "failed";

                    return new Tuple<bool, string>(status, message);

                }

                                
            }
            catch (Exception ex)
            {
                
                return new Tuple<bool, string>(false, ex.Message);
            }
        }

        internal Tuple<bool, string> Quester_GetAll_SelectId_Tree(long userId)
        {
            bool status = false;
            string message = "failed";

            try
            {
                var gettreedata = _context.Quester_TreeView.Where(x => x.UserId == userId && x.IsActive == true).FirstOrDefault();
                if (gettreedata != null)
                {
                    

                    status =  true ;
                    message = gettreedata.TreeData;

                    return new Tuple<bool, string>(status, message);
                }
                else
                {

                    status = false;
                    message = "failed";

                    return new Tuple<bool, string>(status, message);

                }


            }
            catch (Exception ex)
            {

                return new Tuple<bool, string>(false, ex.Message);
            }
        }



        internal Tuple<bool, string> Quester_deletWord(string id)
        {
            try
            {
                var wordId = Convert.ToInt64(id);
                var word = _context.Quester_Word.FirstOrDefault(z => z.WordId == wordId);
                if (word != null)
                {
                    word.IsActive = false;
                    word.UpdatedDate = System.DateTime.UtcNow;
                    if (_context.SaveChanges() > 0)
                    {
                        try
                        {
                            _context.Quester_SP_UPDATE_CROSSWORD_STATUS(id);
                        }
                        catch
                        {

                        }
                        return new Tuple<bool, string>(true, "Success");
                    }
                    else
                    {
                        return new Tuple<bool, string>(false, "Failed");
                    }
                }
                else
                {
                    return new Tuple<bool, string>(false, "Word not found");
                }
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }
        }


        internal Quester_ReturnMessage_Model Quester_deletSelectedWords(Quester_DeleteSelectedWord model)
        {
            var returnModel = new Quester_ReturnMessage_Model();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.Quester_User.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var currentTime = System.DateTime.UtcNow;

                    string wordIds = model.wordIds;
                    //string[] values = wordIds.Split(',');
                    //for (int i = 0; i < values.Length; i++)
                    //{
                    //    var wordId = Convert.ToInt64(values[i].ToString());
                    //    values[i] = values[i].Trim();


                    //}
                    var res = _context.Quester_SP_DELETE_SELECTED_WORDS(user.UserId, wordIds);
                    if (res > 0)
                    {
                        try
                        {
                            _context.Quester_SP_UPDATE_CROSSWORD_STATUS(wordIds);
                        }
                        catch
                        {

                        }
                        returnModel.status = true;
                        returnModel.message = "success";
                    }
                    else
                    {
                        returnModel.status = false;
                        returnModel.message = "failed";
                    }
                }
                else
                {
                    returnModel.status = false;
                    returnModel.message = "Invalid user";
                }
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }

        internal Quester_ReturnMessage_Model Quester_deleteAllWords(Quester_DeleteSelectedWord model)
        {
            var returnModel = new Quester_ReturnMessage_Model();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.Quester_User.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var res = _context.Quester_SP_DELETE_ALL_WORDS(user.UserId);
                    if (res > 0)
                    {
                        returnModel.status = true;
                        returnModel.message = "success";
                    }
                    else
                    {
                        returnModel.status = false;
                        returnModel.message = "failed";
                    }
                }
                else
                {
                    returnModel.status = false;
                    returnModel.message = "Invalid user";
                }
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }

        internal Quester_ReturnMessage_Model Quester_editword(Quester_EditWord_Model model)
        {
            var returnModel = new Quester_ReturnMessage_Model();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.Quester_User.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var word = _context.Quester_Word.FirstOrDefault(z => z.WordId == model.wordId && z.UserId == model.userId);
                    if (word != null)
                    {
                        var currentTime = System.DateTime.UtcNow;
                        word.Definition = "Definition";//model.definition.Trim() ?? string.Empty;
                        word.Hint = model.hint.Trim() ?? string.Empty;
                        word.Phrase = model.phrase.Trim() ?? string.Empty;
                        word.Pronounciation = model.pronounciation.Trim() ?? string.Empty;
                        word.ShareOnline = model.sharOnline;
                        word.UpdatedDate = currentTime;
                        word.Word = model.word.Trim() ?? string.Empty;

                        returnModel.Guid = word.Guid;
                        returnModel.wordId = word.WordId;
                        returnModel.status = _context.SaveChanges() > 0 ? true : false;
                        returnModel.message = returnModel.status == true ? "success" : "failed";
                    }
                    else
                    {
                        returnModel.wordId = 0;
                        returnModel.status = false;
                        returnModel.message = "Word not found";
                    }
                }
                else
                {
                    returnModel.wordId = 0;
                    returnModel.status = false;
                    returnModel.message = "Invalid user";
                }
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.wordId = 0;
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }

        #endregion


        #region Quester_Note

        internal Quester_ReturnMessage_Model Quester_addnote(Quester_AddNote model)
        {
            var returnModel = new Quester_ReturnMessage_Model();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.Quester_User.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var currentTime = System.DateTime.UtcNow;
                    var newNote = _context.Quester_Note.Create();
                    newNote.CreatedDate = currentTime;
                    newNote.NotGuid = Guid.NewGuid();
                    newNote.IsActive = true;
                    newNote.UpdatedDate = currentTime;
                    newNote.UserId = model.userId;
                    newNote.Title = model.title ?? string.Empty;
                    newNote.Description = model.description ?? string.Empty;
                    newNote.ExamId = model.ExamId;
                    _context.Quester_Note.Add(newNote);

                    returnModel.status = _context.SaveChanges() > 0 ? true : false;
                    returnModel.message = returnModel.status == true ? "success" : "failed";
                }
                else
                {
                    returnModel.status = false;
                    returnModel.message = "Invalid user";
                }
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }


        internal Quester_ReturnMessage_Model Quester_editnote(Quester_EditNote model)
        {
            var returnModel =new Quester_ReturnMessage_Model();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.Quester_User.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var note = _context.Quester_Note.FirstOrDefault(z => z.NoteId == model.noteId && z.UserId == model.userId);
                    if (note != null)
                    {
                        var currentTime = System.DateTime.UtcNow;
                        note.UpdatedDate = currentTime;
                        note.Title = model.title ?? string.Empty;
                        note.Description = model.description ?? string.Empty;
                        returnModel.status = _context.SaveChanges() > 0 ? true : false;
                        returnModel.message = returnModel.status == true ? "success" : "failed";
                    }
                    else
                    {
                        returnModel.status = false;
                        returnModel.message = "Note not found";
                    }
                }
                else
                {
                    returnModel.status = false;
                    returnModel.message = "Invalid user";
                }
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }

        internal Tuple<bool, string> Quester_deletNote(string id)
        {
            try
            {
                var noteId = Convert.ToInt64(id);
                var note = _context.Quester_Note.FirstOrDefault(z => z.NoteId == noteId);
                if (note != null)
                {
                    note.IsActive = false;
                    note.UpdatedDate = System.DateTime.UtcNow;
                    if (_context.SaveChanges() > 0)
                    {
                        return new Tuple<bool, string>(true, "Success");
                    }
                    else
                    {
                        return new Tuple<bool, string>(false, "Failed");
                    }
                }
                else
                {
                    return new Tuple<bool, string>(false, "Word not found");
                }
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }
        }



        #endregion

        #region Quester_Favourites
        internal Tuple<bool, string> Quester_deletWordFromFavourites(string id, long userId)
        {
            try
            {
                var wordId = Convert.ToInt64(id);
                var word = _context.Quester_Word.FirstOrDefault(z => z.WordId == wordId);
                if (word != null)
                {
                    var row = _context.Quester_UserFavouriteWord.Where(z => z.UserId == userId && z.WordId == wordId).FirstOrDefault();
                    if (row != null)
                    {
                        _context.Quester_UserFavouriteWord.Remove(row);
                    }
                    if (_context.SaveChanges() > 0)
                    {
                        return new Tuple<bool, string>(true, "Success");
                    }
                    else
                    {
                        return new Tuple<bool, string>(false, "Failed");
                    }
                }
                else
                {
                    return new Tuple<bool, string>(false, "Word not found");
                }
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }
        }

        internal Quester_ReturnMessage_Model Quester_deletSelectedFavouriteWords(Quester_Commen_Model model)
        {
            var returnModel = new Quester_ReturnMessage_Model();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.Quester_User.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var currentTime = System.DateTime.UtcNow;

                    string wordIds = model.wordIds;
                     

                    var res = _context.Quester_SP_DELETE_SELECTED_WORDS_FROM_FAVOURITES(user.UserId, wordIds);
                    if (res > 0)
                    {
                        returnModel.status = true;
                        returnModel.message = "success";
                    }
                    else
                    {
                        returnModel.status = false;
                        returnModel.message = "failed";
                    }
                }
                else
                {
                    returnModel.status = false;
                    returnModel.message = "Invalid user";
                }
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }

        internal Quester_ReturnMessage_Model Quester_deleteAllFavouriteWords(Quester_Commen_Model model)
        {
            var returnModel = new Quester_ReturnMessage_Model();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.Quester_User.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var res = _context.Quester_SP_DELETE_ALL_WORDS_FROM_FAVOURITES(user.UserId);
                    if (res > 0)
                    {
                        returnModel.status = true;
                        returnModel.message = "success";
                    }
                    else
                    {
                        returnModel.status = false;
                        returnModel.message = "failed";
                    }
                }
                else
                {
                    returnModel.status = false;
                    returnModel.message = "Invalid user";
                }
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }

        internal Quester_ReturnMessage_Model Quester_addfavouriteword(Quester_AddFavouriteWord model)
        {
            var returnModel = new Quester_ReturnMessage_Model();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.Quester_User.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var currentTime = System.DateTime.UtcNow;

                    string wordIds = model.wordIds;
                    string[] values = wordIds.Split(',');
                    int count = 0;
                    for (int i = 0; i < values.Length; i++)
                    {
                        var wordId = Convert.ToInt64(values[i].ToString());

                        values[i] = values[i].Trim();

                        if (_context.Quester_SP_CHECK_FAVOURITE_WORD_ALREADY_ADDED(wordId, model.userId).FirstOrDefault() <= 0)
                        {
                            var favouriteWord = _context.Quester_UserFavouriteWord.Create();
                            favouriteWord.CreatedDate = currentTime;
                            favouriteWord.UserId = model.userId;
                            favouriteWord.IsActive = true;
                            favouriteWord.WordId = wordId;
                            _context.Quester_UserFavouriteWord.Add(favouriteWord);

                            
                        }
                        else
                        {
                            count = count + 1;
                        }

                    }
                    if (count < values.Length)
                    {

                        returnModel.status = _context.SaveChanges() > 0 ? true : false;
                        returnModel.message = returnModel.status == true ? "success" : "failed";
                    }
                    else
                    {
                        returnModel.status = false;
                        returnModel.message = "Already Added to Favourites";
                    }
                }
                else
                {
                    returnModel.status = false;
                    returnModel.message = "Invalid user";
                }
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }


        #endregion

        #region skipedList

        internal Tuple<bool, string> Quester_deletWordFromSkipList(string id, long userId)
        {
            try
            {
                var wordId = Convert.ToInt64(id);
                var word = _context.Quester_Word.FirstOrDefault(z => z.WordId == wordId);
                if (word != null)
                {
                    var row = _context.Quester_UserSkippedWord.Where(z => z.UserId == userId && z.WordId == wordId).FirstOrDefault();
                    if (row != null)
                    {
                        _context.Quester_UserSkippedWord.Remove(row);
                    }
                    if (_context.SaveChanges() > 0)
                    {
                        return new Tuple<bool, string>(true, "Success");
                    }
                    else
                    {
                        return new Tuple<bool, string>(false, "Failed");
                    }
                }
                else
                {
                    return new Tuple<bool, string>(false, "Question not found");
                }
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }
        }


        internal Quester_ReturnMessage_Model Quester_deletSelectedSkiplistWords(Quester_Commen_Model model)
        {
            var returnModel = new Quester_ReturnMessage_Model();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.Quester_User.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var currentTime = System.DateTime.UtcNow;

                    string wordIds = model.wordIds;
                    var res = _context.Quester_SP_DELETE_SELECTED_WORDS_FROM_SKIPLIST(user.UserId, wordIds);
                    if (res > 0)
                    {
                        returnModel.status = true;
                        returnModel.message = "success";
                    }
                    else
                    {
                        returnModel.status = false;
                        returnModel.message = "failed";
                    }
                }
                else
                {
                    returnModel.status = false;
                    returnModel.message = "Invalid user";
                }
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }

        internal Quester_ReturnMessage_Model Quester_deleteAllSkiplistWords(Quester_Commen_Model model)
        {
            var returnModel = new Quester_ReturnMessage_Model();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.Quester_User.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var res = _context.Quester_SP_DELETE_ALL_WORDS_FROM_SKIPLIST(user.UserId);
                    if (res > 0)
                    {
                        returnModel.status = true;
                        returnModel.message = "success";
                    }
                    else
                    {
                        returnModel.status = false;
                        returnModel.message = "failed";
                    }
                }
                else
                {
                    returnModel.status = false;
                    returnModel.message = "Invalid user";
                }
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }

        internal Quester_ReturnMessage_Model Quester_addskippedword(Quester_Commen_Model model)
        {
            var returnModel = new Quester_ReturnMessage_Model();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.Quester_User.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var currentTime = System.DateTime.UtcNow;

                    string wordIds = model.wordIds;
                    string[] values = wordIds.Split(',');
                    int count = 0;
                    for (int i = 0; i < values.Length; i++)
                    {
                        var wordId = Convert.ToInt64(values[i].ToString());
                        values[i] = values[i].Trim();

                        if (_context.Quester_SP_CHECK_SKIPLIST_WORD_ALREADY_ADDED(wordId, model.userId).FirstOrDefault() <= 0)
                        {
                            var skippedWord = _context.Quester_UserSkippedWord.Create();
                            skippedWord.CreatedDate = currentTime;
                            skippedWord.UserId = model.userId;
                            skippedWord.IsActive = true;
                            skippedWord.WordId = wordId;
                            _context.Quester_UserSkippedWord.Add(skippedWord);

                           
                        }
                        else
                        {

                            count = count + 1;
                        }
                    }
                    if (count < values.Length)
                    {

                        returnModel.status = _context.SaveChanges() > 0 ? true : false;
                        returnModel.message = returnModel.status == true ? "success" : "failed";
                    }
                    else
                    {
                        returnModel.status = false;
                        returnModel.message = "Already Added to Skiplist";
                    }
                }
                else
                {
                    returnModel.status = false;
                    returnModel.message = "Invalid user";
                }
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }


        #endregion

        #region Quester_OnlineCommunity
        internal Quester_ReturnMessage_Model Quester_addWordToDatabase(Quester_Commen_Model model)
        {
            var returnModel = new Quester_ReturnMessage_Model();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.Quester_User.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var word = _context.Quester_Word.FirstOrDefault(z => z.WordId == model.wordId);
                    if (word != null)
                    {
                        using (var dbContextTransaction = _context.Database.BeginTransaction())
                        {
                            try
                            {
                                var currentTime = System.DateTime.UtcNow;
                                var newWord = _context.Quester_Word.Create();
                                newWord.Word = word.Word.Trim();
                                newWord.UserId = model.userId;
                                newWord.UpdatedDate = currentTime;
                                newWord.ShareOnline = false;
                                newWord.Pronounciation = word.Pronounciation;
                                newWord.Phrase = word.Phrase;
                                newWord.IsActive = true;
                                newWord.Hint = word.Hint;
                                newWord.Guid = Guid.NewGuid();
                                newWord.Definition = word.Definition;
                                newWord.CreatedDate = currentTime;
                                _context.Quester_Word.Add(newWord);

                                returnModel.status = _context.SaveChanges() > 0 ? true : false;
                                dbContextTransaction.Commit();
                                returnModel.message = returnModel.status == true ? "success" : "failed";
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                returnModel.status = false;
                                returnModel.message = "failed";
                            }
                        }

                    }
                    else
                    {
                        returnModel.status = false;
                        returnModel.message = "failed";
                    }
                }
                else
                {
                    returnModel.status = false;
                    returnModel.message = "Invalid user";
                }
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }



        internal Quester_ReturnMessage_Model Quester_addAllWordToDatabase(Quester_Commen_Model model)
        {
            var returnModel = new Quester_ReturnMessage_Model();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {

                string wordUserId = model.wordIds;
                string[] SendUserIdArry = wordUserId.Split(',');
                model.wordIds = null;
                for (int i = 0; i < SendUserIdArry.Length; i++)
                {
                    long wordID = Convert.ToInt64(SendUserIdArry[i]);
                    var r2 = _context.Quester_Word.Where(x => x.WordId == wordID).FirstOrDefault();
                    var r1 = _context.Quester_Word.Where(z => z.UserId == model.userId && z.IsActive == true && z.Word == r2.Word).FirstOrDefault();
                    if (r1 == null)
                    {
                        if (model.wordId == null)
                        {
                            model.wordIds = SendUserIdArry[i];
                        }
                        else
                        {
                            model.wordIds = model.wordIds + "," + SendUserIdArry[i];
                        }
                        //model.wordId = model.wordId + 
                    }
                }


                var user = _context.Quester_User.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    //next...Edit
                    var res = _context.Quester_SP_INSERT_ONLINECOMMUNITY_WORDS(model.userId, model.wordIds);
                    if (res > 0)
                    {
                        returnModel.status = true;
                        returnModel.message = "Words added";
                    }
                    else
                    {
                        returnModel.status = false;
                        returnModel.message = "failed";
                    }
                }
                else
                {
                    returnModel.status = false;
                    returnModel.message = "Invalid user";
                }
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }




        #endregion


        #region Exams

        internal Quester_ReturnMessage_Model Quester_createtest(Quester_CreateTest model)
        {
            var returnModel = new Quester_ReturnMessage_Model();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.Quester_User.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var currentTime = System.DateTime.UtcNow;
                    var newTest = _context.Quester_Exam.Create();
                    newTest.CreatedDate = currentTime;
                    newTest.ExamGuid = Guid.NewGuid();
                    newTest.IsActive = true;
                    if (model.testTime == null || model.testTime == string.Empty)
                    {
                        newTest.TestTime = "0";
                    }
                    else
                    {
                        newTest.TestTime = model.testTime;
                    }
                    if (model.timePerWord == null || model.timePerWord == string.Empty)
                    {
                        newTest.TimePerWord = "0";
                    }
                    else
                    {
                        newTest.TimePerWord = model.timePerWord;
                    }
                    newTest.Title = model.title;
                    newTest.UserId = Convert.ToInt64(model.userId);
                    _context.Quester_Exam.Add(newTest);
                    returnModel.status = _context.SaveChanges() > 0 ? true : false;
                    returnModel.message = returnModel.status == true ? "success" : "failed";
                    returnModel.examId = "";
                    if (returnModel.status)
                    {
                        returnModel.examId = newTest.ExamId.ToString();
                        List<string> result = model.wordIds.Split(',').ToList();
                        if (result.Count > 0)
                        {
                            foreach (var item in result)
                            {
                                var examWord = _context.Quester_ExamWord.Create();
                                examWord.ExamId = newTest.ExamId;
                                examWord.WordId = Convert.ToInt64(item.ToString());
                                _context.Quester_ExamWord.Add(examWord);
                            }
                        }
                        _context.SaveChanges();
                    }
                }
                else
                {
                    returnModel.status = false;
                    returnModel.message = "Invalid user";
                }
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }

        internal Quester_ReturnMessage_Model Quester_SaveResultToDb(Quester_SaveResult model)
        {
            var returnModel = new Quester_ReturnMessage_Model();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var userId = Convert.ToInt64(model.userId);
                var examId = Convert.ToInt64(model.examId);
                List<Quester_ResultData> data = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<Quester_ResultData>>(model.resultData);
                var currentTime = System.DateTime.UtcNow;

                foreach (var item in data)
                {
                    var examRes = _context.Quester_ExamResult.Create();
                    examRes.ExamId = examId;
                    examRes.Guid = Guid.NewGuid();
                    examRes.IsCorrect = item.isCorrect;
                    examRes.Timestamp = currentTime;
                    examRes.WordId = Convert.ToInt64(item.wordId);
                    _context.Quester_ExamResult.Add(examRes);
                }
                returnModel.status = _context.SaveChanges() > 0 ? true : false;
                returnModel.message = returnModel.status == true ? "success" : "failed";
                returnModel.examId = model.examId;
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }

        internal Tuple<bool, string, string> Quester_resittest(string id, long userId)
        {
            bool status = false;
            string message = "failed";
            string examIdRes = "";

            byte[] b = Convert.FromBase64String(id);

            var examIdString = System.Text.Encoding.UTF8.GetString(b);
            var examId = Convert.ToInt64(examIdString);

            try
            {
                var user = _context.Quester_User.FirstOrDefault(z => z.UserId == userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {

                    var exam = _context.Quester_Exam.FirstOrDefault(z => z.ExamId == examId);
                    if (exam != null)
                    {
                        var currentTime = System.DateTime.UtcNow;
                        var newTest = _context.Quester_Exam.Create();
                        newTest.CreatedDate = currentTime;
                        newTest.ExamGuid = Guid.NewGuid();
                        newTest.IsActive = true;
                        newTest.TestTime = exam.TestTime;
                        newTest.TimePerWord = exam.TimePerWord;
                        newTest.Title = exam.Title;
                        newTest.UserId = Convert.ToInt64(userId);
                        _context.Quester_Exam.Add(newTest);

                        status = _context.SaveChanges() > 0 ? true : false;
                        message = status == true ? "success" : "failed";
                        examIdRes = "";
                        if (status)
                        {
                            examIdRes = newTest.ExamId.ToString();
                            List<long> result = exam.Quester_ExamWord.ToList().Select(z => z.WordId).ToList();
                            if (result.Count > 0)
                            {
                                foreach (var item in result)
                                {
                                    var examWord = _context.Quester_ExamWord.Create();
                                    examWord.ExamId = newTest.ExamId;
                                    examWord.WordId = Convert.ToInt64(item.ToString());
                                    _context.Quester_ExamWord.Add(examWord);
                                }
                            }
                            _context.SaveChanges();
                        }
                    }
                    else
                    {
                        status = false;
                        message = "Invalid user";
                    }
                    return new Tuple<bool, string, string>(status, message, examIdRes);
                }
                else
                {
                    return new Tuple<bool, string, string>(false, "User Not found", "");

                }

            }
            catch (Exception ex)
            {
                return new Tuple<bool, string, string>(false, ex.Message, "");
            }
        }


        internal Tuple<bool, string> Quester_deletSelectedTest(string id, long userId)
        {
            try
            {
               

                byte[] b = Convert.FromBase64String(id);

                var strOriginal = System.Text.Encoding.UTF8.GetString(b);

                var user = _context.Quester_User.FirstOrDefault(z => z.UserId == userId);
                if (user != null)
                {
                    var res = _context.Quester_SP_DELETE_SELECTED_TEST(userId, strOriginal).ToString();
                    if (Convert.ToInt32(res) > 0)
                    {
                        return new Tuple<bool, string>(true, "Success");
                    }
                    else
                    {
                        return new Tuple<bool, string>(false, "Failed");
                    }
                }
                else
                {
                    return new Tuple<bool, string>(false, "User not found");
                }
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }
        }

        internal Tuple<bool, string> Quester_deleteAllTest(string id)
        {
            try
            {
                var userId = Convert.ToInt64(id);
                var user = _context.Quester_User.FirstOrDefault(z => z.UserId == userId);
                if (user != null)
                {
                    var res = _context.Quester_SP_DELETE_ALL_USER_TEST(userId).ToString();
                    if (Convert.ToInt32(res) > 0)
                    {
                        return new Tuple<bool, string>(true, "Success");
                    }
                    else
                    {
                        return new Tuple<bool, string>(false, "Failed");
                    }
                }
                else
                {
                    return new Tuple<bool, string>(false, "User not found");
                }
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }
        }


        internal Tuple<bool, string> Quester_deleteTest(string id)
        {
            try
            {
                var testId = Convert.ToInt64(id);
                var test = _context.Quester_Exam.FirstOrDefault(z => z.ExamId == testId);
                if (test != null)
                {
                    test.IsActive = false;
                    if (_context.SaveChanges() > 0)
                    {
                        return new Tuple<bool, string>(true, "Success");
                    }
                    else
                    {
                        return new Tuple<bool, string>(false, "Failed");
                    }
                }
                else
                {
                    return new Tuple<bool, string>(false, "Test not found");
                }
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }
        }



        //jibin 10/13/2020

        internal Tuple<bool, string> Quester_CreateWord_Add_TreeTest(string jsondatas, long userId)
        {
            bool status = false;
            string message = "failed";

            try
            {
                var gettreedata = _context.Quester_TreeView.Where(x => x.UserId == userId && x.IsActive == true).FirstOrDefault();
                if (gettreedata == null)
                {
                    var createtreeview = _context.Quester_TreeView.Create();
                    createtreeview.UserId = userId;
                    createtreeview.TreeData = jsondatas;
                    createtreeview.Guid = Guid.NewGuid();
                    createtreeview.IsActive = true;
                    createtreeview.CreatedDate = DateTime.Now;
                    createtreeview.UpdatedDate = DateTime.Now;

                    _context.Quester_TreeView.Add(createtreeview);

                    status = _context.SaveChanges() > 0 ? true : false;
                    message = status == true ? "Success" : "failed";

                    return new Tuple<bool, string>(status, message);
                }
                else
                {

                    gettreedata.TreeData = jsondatas;
                    gettreedata.UpdatedDate = DateTime.Now;

                    status = _context.SaveChanges() > 0 ? true : false;
                    message = status == true ? "Success" : "failed";

                    return new Tuple<bool, string>(status, message);

                }


            }
            catch (Exception ex)
            {

                return new Tuple<bool, string>(false, ex.Message);
            }
        }

        //jibin 10/13/2020
        #endregion

    }
}