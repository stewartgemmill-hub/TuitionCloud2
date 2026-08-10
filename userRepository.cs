using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using TutionCloudWeb.Model.Wordutopia;
using TutionCloudWeb.Models.Database;

namespace TutionCloudWeb.Models.Repository
{
    public class userRepository : BaseRepository
    {
        //private TutionCloudEntities _context = new TutionCloudEntities();

        internal TutionCloudWeb.Models.PostModel.User.RegisterReturn register(PostModel.User.Register model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.User.RegisterReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {

                if (_context.tUsers.Any(z => z.Username == model.username && z.IsActive && z.Webname == model.webname))
                {
                    returnModel.status = false;
                    returnModel.message = "Username already taken";
                }
                else
                {
                    if (_context.tUsers.Any(z => z.email == model.Email))
                    {
                        returnModel.status = false;
                        returnModel.message = "Email id already exists";
                    }
                    else
                    {
                        var currentTime = System.DateTime.UtcNow;
                        var user = _context.tUsers.Create();
                        user.AccountCreatedDate = currentTime;
                        user.Forename = model.forename ?? string.Empty;
                        user.IsActive = true;
                        user.IsSuspended = false;
                        user.Password = StringCipher.Encrypt(model.password, salt);
                        user.ProfileImage = string.Empty;
                        user.ProfileLastEditedDate = currentTime;
                        user.PushStatus = true;
                        user.Surname = model.surname ?? string.Empty;
                        user.UserGuid = Guid.NewGuid();
                        user.Username = model.username;
                        user.Webname = model.webname;
                        user.email = model.Email.Trim(); //Basheer on 05/03/2019
                        _context.tUsers.Add(user);
                        returnModel.status = _context.SaveChanges() > 0 ? true : false;
                        returnModel.message = returnModel.status == true ? "success" : "failed";

                        FormsAuthentication.SetAuthCookie(user.UserId.ToString(), true);
                    }
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

        internal TutionCloudWeb.Models.PostModel.User.LoginReturn login(PostModel.User.Login model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.User.LoginReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var password = StringCipher.Encrypt(model.password, salt);
                // this Line is Temperory Commend.......
                //var user = _context.tUsers.FirstOrDefault(z => z.Username == model.username && z.Password == password && z.IsActive && z.IsSuspended == false && z.Webname == model.webname);
                var user = _context.tUsers.FirstOrDefault(z => z.Username == model.username && z.IsActive && z.IsSuspended == false && z.Webname == model.webname);
                
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.UserId.ToString(), true);
                    returnModel.status = true;
                    returnModel.message = "success";
                    returnModel.userid = user.UserId;
                    //sibi edit GetUserID
                    SessionModel.UserId = user.UserId;
                }
                else
                {
                    returnModel.status = false;
                    returnModel.message = "Invalid username /  password";
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

        internal TutionCloudWeb.Models.PostModel.Word.AddWordReturn addword(PostModel.Word.AddWord model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.Word.AddWordReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.tUsers.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var currentTime = System.DateTime.UtcNow;
                    var newWord = _context.tWords.Create();
                    newWord.CreatedDate = currentTime;
                    newWord.Definition = model.definition.Trim() ?? string.Empty;
                    newWord.Guid = Guid.NewGuid();
                    newWord.Hint = model.hint.Trim() ?? string.Empty;
                    newWord.IsActive = true;
                    newWord.Phrase = model.phrase.Trim() ?? string.Empty;
                    newWord.Pronounciation = model.pronounciation.Trim() ?? string.Empty;
                    newWord.ShareOnline = model.sharOnline;
                    newWord.UpdatedDate = currentTime;
                    newWord.UserId = model.userId;
                    newWord.Word = model.word.Trim() ?? string.Empty;
                    _context.tWords.Add(newWord);

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

        internal Tuple<bool, string> deletWord(string id)
        {
            try
            {
                var wordId = Convert.ToInt64(id);
                var word = _context.tWords.FirstOrDefault(z => z.WordId == wordId);
                if (word != null)
                {
                    word.IsActive = false;
                    word.UpdatedDate = System.DateTime.UtcNow;
                    if (_context.SaveChanges() > 0)
                    {
                        try
                        {
                            _context.SP_UPDATE_CROSSWORD_STATUS(id);
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

        internal Tuple<bool, string> deletNote(string id)
        {
            try
            {
                var noteId = Convert.ToInt64(id);
                var note = _context.tNotes.FirstOrDefault(z => z.NoteId == noteId);
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

        internal TutionCloudWeb.Models.PostModel.Note.AddNoteReturn addnote(PostModel.Note.AddNote model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.Note.AddNoteReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.tUsers.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var currentTime = System.DateTime.UtcNow;
                    var newNote = _context.tNotes.Create();
                    newNote.CreatedDate = currentTime;
                    newNote.NotGuid = Guid.NewGuid();
                    newNote.IsActive = true;
                    newNote.UpdatedDate = currentTime;
                    newNote.UserId = model.userId;
                    newNote.Title = model.title ?? string.Empty;
                    newNote.Description = model.description ?? string.Empty;
                    newNote.ExamId = model.ExamId;
                    _context.tNotes.Add(newNote);

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

        internal TutionCloudWeb.Models.PostModel.Word.EditWordReturn editword(PostModel.Word.EditWord model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.Word.EditWordReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.tUsers.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var word = _context.tWords.FirstOrDefault(z => z.WordId == model.wordId && z.UserId == model.userId);
                    if (word != null)
                    {
                        var currentTime = System.DateTime.UtcNow;
                        word.Definition = model.definition.Trim() ?? string.Empty;
                        word.Hint = model.hint.Trim() ?? string.Empty;
                        word.Phrase = model.phrase.Trim() ?? string.Empty;
                        word.Pronounciation = model.pronounciation.Trim() ?? string.Empty;
                        word.ShareOnline = model.sharOnline;
                        word.UpdatedDate = currentTime;
                        word.Word = model.word.Trim() ?? string.Empty;

                        returnModel.status = _context.SaveChanges() > 0 ? true : false;
                        returnModel.message = returnModel.status == true ? "success" : "failed";
                    }
                    else
                    {
                        returnModel.status = false;
                        returnModel.message = "Word not found";
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

        internal TutionCloudWeb.Models.PostModel.Note.EditNoteReturn editnote(PostModel.Note.EditNote model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.Note.EditNoteReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.tUsers.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var note = _context.tNotes.FirstOrDefault(z => z.NoteId == model.noteId && z.UserId == model.userId);
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

        internal TutionCloudWeb.Models.PostModel.Word.AddFavouriteWordReturn addfavouriteword(PostModel.Word.AddFavouriteWord model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.Word.AddFavouriteWordReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.tUsers.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
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

                        if (_context.SP_CHECK_FAVOURITE_WORD_ALREADY_ADDED(wordId, model.userId).FirstOrDefault() <= 0)
                        {
                            var favouriteWord = _context.tUserFavouriteWords.Create();
                            favouriteWord.CreatedDate = currentTime;
                            favouriteWord.UserId = model.userId;
                            favouriteWord.IsActive = true;
                            favouriteWord.WordId = wordId;
                            _context.tUserFavouriteWords.Add(favouriteWord);

                            //commented on 27/02/2019 by Basheer for as per the updated client feedback
                            //var skippedWord = _context.tUserSkippedWords.Where(z => z.UserId == model.userId && z.WordId == wordId).FirstOrDefault();
                            //if (skippedWord != null)
                            //{
                            //    _context.tUserSkippedWords.Remove(skippedWord);
                            //}
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

        internal Tuple<bool, string> deletWordFromFavourites(string id, long userId)
        {
            try
            {
                var wordId = Convert.ToInt64(id);
                var word = _context.tWords.FirstOrDefault(z => z.WordId == wordId);
                if (word != null)
                {
                    var row = _context.tUserFavouriteWords.Where(z => z.UserId == userId && z.WordId == wordId).FirstOrDefault();
                    if (row != null)
                    {
                        _context.tUserFavouriteWords.Remove(row);
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

        internal Tuple<bool, string> deletWordFromSkipList(string id, long userId)
        {
            try
            {
                var wordId = Convert.ToInt64(id);
                var word = _context.tWords.FirstOrDefault(z => z.WordId == wordId);
                if (word != null)
                {
                    var row = _context.tUserSkippedWords.Where(z => z.UserId == userId && z.WordId == wordId).FirstOrDefault();
                    if (row != null)
                    {
                        _context.tUserSkippedWords.Remove(row);
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

        internal TutionCloudWeb.Models.PostModel.Word.AddSkippedWordReturn addskippedword(PostModel.Word.AddSkippedWord model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.Word.AddSkippedWordReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.tUsers.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
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

                        if (_context.SP_CHECK_SKIPLIST_WORD_ALREADY_ADDED(wordId, model.userId).FirstOrDefault() <= 0)
                        {
                            var skippedWord = _context.tUserSkippedWords.Create();
                            skippedWord.CreatedDate = currentTime;
                            skippedWord.UserId = model.userId;
                            skippedWord.IsActive = true;
                            skippedWord.WordId = wordId;
                            _context.tUserSkippedWords.Add(skippedWord);

                            //commented on 27/02/2019 by Basheer for as per the updated client feedback
                            //var favouriteWord = _context.tUserFavouriteWords.Where(z => z.UserId == model.userId && z.WordId == wordId).FirstOrDefault();
                            //if (favouriteWord != null)
                            //{
                            //    _context.tUserFavouriteWords.Remove(favouriteWord);
                            //}
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

        internal TutionCloudWeb.Models.PostModel.Exam.CreateTestReturn createtest(PostModel.Exam.CreateTest model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.Exam.CreateTestReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.tUsers.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var currentTime = System.DateTime.UtcNow;
                    var newTest = _context.tExams.Create();
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
                    _context.tExams.Add(newTest);
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
                                var examWord = _context.tExamWords.Create();
                                examWord.ExamId = newTest.ExamId;
                                examWord.WordId = Convert.ToInt64(item.ToString());
                                _context.tExamWords.Add(examWord);
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

        internal TutionCloudWeb.Models.PostModel.Exam.SaveResultResponse SaveResultToDb(PostModel.Exam.SaveResult model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.Exam.SaveResultResponse();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var userId = Convert.ToInt64(model.userId);
                var examId = Convert.ToInt64(model.examId);
                List<TutionCloudWeb.Models.PostModel.Exam.ResultData> data = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<TutionCloudWeb.Models.PostModel.Exam.ResultData>>(model.resultData);
                var currentTime = System.DateTime.UtcNow;

                foreach (var item in data)
                {
                    var examRes = _context.tExamResults.Create();
                    examRes.ExamId = examId;
                    examRes.Guid = Guid.NewGuid();
                    examRes.IsCorrect = item.isCorrect;
                    examRes.Timestamp = currentTime;
                    examRes.WordId = Convert.ToInt64(item.wordId);
                    _context.tExamResults.Add(examRes);
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

        internal object createCrossword(string id, long userId)
        {
            byte[] b = Convert.FromBase64String(id);
            var strOriginal = System.Text.Encoding.UTF8.GetString(b);


            if (strOriginal != "")
            {
                var crossword = _context.tCrosswords.Create();
                crossword.CreatedTime = System.DateTime.UtcNow;
                crossword.IsDeleted = false;
                crossword.UserId = userId;
                _context.tCrosswords.Add(crossword);
                if (_context.SaveChanges() > 0)
                {
                    string[] values = strOriginal.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        var crosswordword = _context.tCrosswordWords.Create();
                        crosswordword.CrosswordId = crossword.CrosswordId;
                        crosswordword.IsDeleted = false;
                        crosswordword.WordId = Convert.ToInt64(values[i]);
                        _context.tCrosswordWords.Add(crosswordword);
                    }
                    _context.SaveChanges();
                    return crossword.CrosswordId;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                var randomwords = _context.SP_FETCH_RANDOM_WORDS(userId).ToList().Select(z => new TutionCloudWeb.Models.DataClass.SP_FETCH_RANDOM_WORDS(z)).ToList();
                if (randomwords != null)
                {
                    var crossword = _context.tCrosswords.Create();
                    crossword.CreatedTime = System.DateTime.UtcNow;
                    crossword.IsDeleted = false;
                    crossword.UserId = userId;
                    _context.tCrosswords.Add(crossword);
                    if (_context.SaveChanges() > 0)
                    {
                        foreach (var item in randomwords)
                        {
                            var crosswordword = _context.tCrosswordWords.Create();
                            crosswordword.CrosswordId = crossword.CrosswordId;
                            crosswordword.IsDeleted = false;
                            crosswordword.WordId = Convert.ToInt64(item.WORDID);
                            _context.tCrosswordWords.Add(crosswordword);
                        }
                        _context.SaveChanges();
                        return crossword.CrosswordId;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

        internal object populateModel(string id, long userId)
        {
            List<TutionCloudWeb.Models.PostModel.Crossword.WordDetails> wordList = new List<TutionCloudWeb.Models.PostModel.Crossword.WordDetails>();


            byte[] b = Convert.FromBase64String(id);
            var strOriginal = System.Text.Encoding.UTF8.GetString(b);

            dynamic data = System.Web.Helpers.Json.Decode(strOriginal);


            if (data.isNew)
            {
                if (!data.RandomCrossword)
                //if (strOriginal != "00")
                {
                    //string[] values = strOriginal.Split(',');
                    //for (int i = 0; i < values.Length; i++)
                    //{
                    //    wordList.Add(values[i].ToString());
                    //}
                    string wordsliststring = Convert.ToString(data.wordList);
                    //var words = _context.SP_FETCH_SELECTED_CROSSWORD_WORD_DETAILS(userId, strOriginal).ToList()
                    var words = _context.SP_FETCH_SELECTED_CROSSWORD_WORD_DETAILS(userId, wordsliststring).ToList()
                        .Select(z => new TutionCloudWeb.Models.DataClass.SP_FETCH_SELECTED_CROSSWORD_WORD_DETAILS(z)).ToList();
                    if (words != null)
                    {
                        foreach (var item in words)
                        {
                            var wordDetils = new TutionCloudWeb.Models.PostModel.Crossword.WordDetails();
                            wordDetils.CREATEDDATE = item.CREATEDDATE;
                            wordDetils.DEFINITION = item.DEFINITION;
                            wordDetils.HINT = item.HINT;
                            wordDetils.OVERALLSCORE = item.OVERALLSCORE;
                            wordDetils.PHRASE = item.PHRASE;
                            wordDetils.PRONOUNCIATION = item.PRONOUNCIATION;
                            wordDetils.UPDATEDDATE = item.UPDATEDDATE;
                            wordDetils.WORD = Regex.Replace(item.WORD, @"\s+", "");
                            wordDetils.WORDGUID = item.WORDGUID;
                            wordDetils.WORDID = item.WORDID;
                            wordDetils.ISFAVOURITE = item.ISFAVOURITE;
                            wordDetils.ISSKIPPED = item.ISSKIPPED;
                            wordList.Add(wordDetils);
                        }
                    }

                }
                else
                {
                    var randomwords = _context.SP_FETCH_RANDOM_WORDS(userId).ToList().Select(z => new TutionCloudWeb.Models.DataClass.SP_FETCH_RANDOM_WORDS(z)).ToList();
                    if (randomwords != null)
                    {
                        foreach (var item in randomwords)
                        {
                            var wordDetils = new TutionCloudWeb.Models.PostModel.Crossword.WordDetails();
                            wordDetils.CREATEDDATE = item.CREATEDDATE;
                            wordDetils.DEFINITION = item.DEFINITION;
                            wordDetils.HINT = item.HINT;
                            wordDetils.OVERALLSCORE = 0;
                            wordDetils.PHRASE = item.PHRASE;
                            wordDetils.PRONOUNCIATION = item.PRONOUNCIATION;
                            wordDetils.UPDATEDDATE = item.UPDATEDDATE;
                            wordDetils.WORD = Regex.Replace(item.WORD, @"\s+", "");
                            wordDetils.WORDGUID = Guid.Empty;
                            wordDetils.WORDID = item.WORDID;
                            wordDetils.ISFAVOURITE = item.ISFAVOURITE;
                            wordDetils.ISSKIPPED = item.ISSKIPPED;
                            wordList.Add(wordDetils);
                        }
                    }
                }
            }
            else
            {
                string wordsliststring = Convert.ToString(data.wordList);
                var words = _context.SP_FETCH_SELECTED_CROSSWORD_WORD_DETAILS(userId, wordsliststring).ToList()
                           .Select(z => new TutionCloudWeb.Models.DataClass.SP_FETCH_SELECTED_CROSSWORD_WORD_DETAILS(z)).ToList();
                if (words != null)
                {
                    foreach (var item in words)
                    {
                        var wordDetils = new TutionCloudWeb.Models.PostModel.Crossword.WordDetails();
                        wordDetils.CREATEDDATE = item.CREATEDDATE;
                        wordDetils.DEFINITION = item.DEFINITION;
                        wordDetils.HINT = item.HINT;
                        wordDetils.OVERALLSCORE = item.OVERALLSCORE;
                        wordDetils.PHRASE = item.PHRASE;
                        wordDetils.PRONOUNCIATION = item.PRONOUNCIATION;
                        wordDetils.UPDATEDDATE = item.UPDATEDDATE;
                        wordDetils.WORD = Regex.Replace(item.WORD, @"\s+", "");
                        wordDetils.WORDGUID = item.WORDGUID;
                        wordDetils.WORDID = item.WORDID;
                        wordDetils.ISFAVOURITE = item.ISFAVOURITE;
                        wordDetils.ISSKIPPED = item.ISSKIPPED;
                        wordList.Add(wordDetils);
                    }
                }
            }



            bool first = true;
            int cout = 1;
            List<TutionCloudWeb.Models.PostModel.Crossword.MainCross> list = new List<TutionCloudWeb.Models.PostModel.Crossword.MainCross>();
            List<TutionCloudWeb.Models.PostModel.Crossword.WordList> beforeAdd = new List<TutionCloudWeb.Models.PostModel.Crossword.WordList>();
            //List<string> wordList = new List<string> { "KAVITHA", "REVATHY", "NISAM", "ABHI", "SACHIN", "KD","Preetha" };
            wordList = wordList.OrderByDescending(x => x.WORD.Length).ThenBy(x => x.WORD).ToList();
            foreach (var item in wordList)
            {
                TutionCloudWeb.Models.PostModel.Crossword.MainCross xx = new TutionCloudWeb.Models.PostModel.Crossword.MainCross();
                xx.word = item.WORD.ToUpper();
                xx.slno = cout;
                xx.legth = item.WORD.Length;
                xx.sucessStatus = false;
                xx.wordId = Convert.ToInt64(item.WORDID);
                cout = cout + 1;
                list.Add(xx);
            }
            int maxCount = wordList[0].WORD.Length;
            if (maxCount % 2 == 0)
                maxCount = maxCount + 1;
            int firstPosition = maxCount / 2 + 1;
            TutionCloudWeb.Models.PostModel.Crossword.MainCross firstOne = list[0];
            firstOne.list = new List<TutionCloudWeb.Models.PostModel.Crossword.WordList>();
            char[] wordArray = firstOne.word.ToArray();
            for (int i = 0; i < firstOne.word.Length; i++)
            {
                TutionCloudWeb.Models.PostModel.Crossword.WordList firstWord = new TutionCloudWeb.Models.PostModel.Crossword.WordList();
                firstWord.letter = wordArray[i].ToString();
                firstWord.xAxis = firstPosition;
                firstWord.yAxis = i + 1;
                firstWord.first = first;
                if (first)
                {
                    firstWord.wordId = firstOne.wordId;
                }
                else
                {
                    firstWord.wordId = 0;
                }
                firstWord.isVertical = false;
                firstOne.list.Add(firstWord);
                beforeAdd.Add(firstWord);

                first = false;
            }
            first = true;
            firstOne.sucessStatus = true;

            List<TutionCloudWeb.Models.PostModel.Crossword.MainCross> secondList = list.Where(x => x.sucessStatus == false).ToList();
            string firstWordString = firstOne.word;
            #region Vertical
            foreach (var item in secondList)
            {
                int wordEqualPosition = 0;
                TutionCloudWeb.Models.PostModel.Crossword.MainCross mainWord = list.Where(x => x.word == firstWordString).FirstOrDefault();
                TutionCloudWeb.Models.PostModel.Crossword.MainCross newWord = list.Where(x => x.word == item.word).FirstOrDefault();
                char[] mainWordArray = mainWord.word.ToArray();
                char[] newWordArray = newWord.word.ToArray();
                int xPosition = 0;
                int yPosition = 0;
                int checkedLetterPosition = 0;
                for (int i = 0; i < mainWordArray.Count(); i++)
                {
                    string word = mainWordArray[i].ToString().ToUpper();//Main word letter
                    for (int j = 0; j < newWordArray.Count(); j++)
                    {
                        string secondWord = newWordArray[j].ToString().ToUpper();
                        if (word == secondWord)
                        {
                            var same = mainWord.list.Where(x => x.letter == word).FirstOrDefault();
                            xPosition = same.xAxis;
                            yPosition = same.yAxis;
                            checkedLetterPosition = j + 1;

                            if (((firstPosition) >= checkedLetterPosition) && (firstPosition >= (newWord.word.Length - checkedLetterPosition)))
                            {
                                if ((firstPosition == (newWord.word.Length - checkedLetterPosition)) && (firstPosition) >= checkedLetterPosition)
                                {
                                    maxCount = maxCount + 1;
                                }
                                if (checkedLetterPosition == newWord.word.Length)
                                {
                                    var check = beforeAdd.Where(x => x.xAxis == xPosition - 1 && x.yAxis == yPosition).FirstOrDefault();
                                    if (check == null)
                                    {
                                        wordEqualPosition = i + 1;
                                        j = newWordArray.Count();
                                        i = mainWordArray.Count();
                                    }
                                    else
                                        checkedLetterPosition = 0;
                                }
                                else
                                {
                                    var check = beforeAdd.Where(x => x.xAxis == xPosition + 1 && x.yAxis == (i + 1)).FirstOrDefault();
                                    if (check == null)
                                    {
                                        wordEqualPosition = i + 1;
                                        j = newWordArray.Count();
                                        i = mainWordArray.Count();
                                    }
                                    else
                                        checkedLetterPosition = 0;
                                }

                            }
                            else
                            {
                                checkedLetterPosition = 0;
                            }
                        }
                    }
                }
                if (checkedLetterPosition == 0)
                {
                    // can't be locate the word in this position , becouse it will over flow.
                }
                else
                {
                    int yPos = 1;
                    List<TutionCloudWeb.Models.PostModel.Crossword.WordList> beforeAdd1 = new List<TutionCloudWeb.Models.PostModel.Crossword.WordList>();
                    yPos = yPos + firstPosition - checkedLetterPosition;
                    for (int i = 0; i < newWordArray.Count(); i++)
                    {
                        TutionCloudWeb.Models.PostModel.Crossword.WordList secondWordTest = new TutionCloudWeb.Models.PostModel.Crossword.WordList();
                        if (i + 1 < checkedLetterPosition)
                        {
                            secondWordTest.letter = newWordArray[i].ToString();
                            secondWordTest.xAxis = yPos;
                            secondWordTest.yAxis = wordEqualPosition;
                        }
                        else if ((i + 1) == checkedLetterPosition)
                        {
                            secondWordTest.letter = newWordArray[i].ToString();
                            secondWordTest.xAxis = xPosition;
                            secondWordTest.yAxis = wordEqualPosition;
                        }
                        else
                        {
                            secondWordTest.letter = newWordArray[i].ToString();
                            secondWordTest.xAxis = yPos;
                            secondWordTest.yAxis = wordEqualPosition;
                        }
                        secondWordTest.first = first;
                        if (first)
                        {
                            secondWordTest.wordId = item.wordId;
                        }
                        else
                        {
                            secondWordTest.wordId = 0;
                        }
                        secondWordTest.isVertical = true;
                        //secondWordTest.wordId = firstOne.wordId;

                        beforeAdd1.Add(secondWordTest);
                        yPos = yPos + 1;
                        first = false;
                    }
                    first = true;
                    var success = list.Where(x => x.sucessStatus == true).ToList();
                    foreach (var xx in success)
                    {
                        List<TutionCloudWeb.Models.PostModel.Crossword.WordList> yy = xx.list.ToList();
                        var have = yy.Where(x => beforeAdd1.Any(y => y.xAxis == x.xAxis && y.yAxis == x.yAxis)).ToList();
                        if (have != null && have.Count < 2)
                        {
                            newWord.list = new List<TutionCloudWeb.Models.PostModel.Crossword.WordList>();
                            newWord.list = beforeAdd;
                            newWord.sucessStatus = true;

                            beforeAdd.AddRange(beforeAdd1);
                        }
                    }
                }
            }
            #endregion Vertical

            #region Horizontal
            List<TutionCloudWeb.Models.PostModel.Crossword.MainCross> thirdList = list.Where(x => x.sucessStatus == false).ToList();
            string secondWordString = list.Where(x => x.sucessStatus == true && x.word != firstOne.word).OrderByDescending(x => x.legth).Select(x => x.word).FirstOrDefault();
            if (secondWordString != null)
            {
                foreach (var item in thirdList)
                {
                    int wordEqualPosition = 0;
                    TutionCloudWeb.Models.PostModel.Crossword.MainCross mainWord = list.Where(x => x.word == secondWordString).FirstOrDefault();
                    TutionCloudWeb.Models.PostModel.Crossword.MainCross newWord = list.Where(x => x.word == item.word).FirstOrDefault();
                    char[] mainWordArray = mainWord.word.ToArray();
                    char[] newWordArray = newWord.word.ToArray();
                    int xPosition = 0;
                    int yPosition = 0;
                    int checkedLetterPosition = 0;
                    for (int i = 0; i < mainWordArray.Count(); i++)
                    {
                        string word = mainWordArray[i].ToString().ToUpper();//Main word letter
                        for (int j = 0; j < newWordArray.Count(); j++)
                        {
                            string secondWord = newWordArray[j].ToString().ToUpper();
                            if (word == secondWord)
                            {
                                var same = mainWord.list.Where(x => x.letter == word).FirstOrDefault();
                                xPosition = same.xAxis;
                                yPosition = same.yAxis;
                                checkedLetterPosition = j + 1;

                                bool ok = false;
                                List<TutionCloudWeb.Models.PostModel.Crossword.WordList> thisLetersXaxix = beforeAdd.Where(x => x.xAxis == xPosition).OrderByDescending(x => x.yAxis).ToList();
                                int firstPositionOfNewWord = yPosition + 1 - checkedLetterPosition;
                                if (firstPositionOfNewWord > 0 && thisLetersXaxix.Count > 1)
                                {
                                    foreach (var exists in thisLetersXaxix)
                                    {
                                        if (exists.yAxis == firstPositionOfNewWord)
                                        {
                                            ok = true;
                                            break;
                                        }
                                    }
                                    foreach (var itemss in thisLetersXaxix)
                                    {
                                        for (int z = yPosition; z < newWordArray.Count() + yPosition; z++)
                                        {
                                            if (itemss.yAxis == z)
                                            {
                                                ok = true;
                                                break;
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    ok = true;
                                }
                                if (ok == false)
                                {
                                    wordEqualPosition = i + 1;
                                    j = newWordArray.Count();
                                    i = mainWordArray.Count();
                                }
                                else
                                {
                                    checkedLetterPosition = 0;
                                }
                            }
                        }
                    }

                    if (checkedLetterPosition == 0)
                    {
                        // can't be locate the word in this position , becouse it will over flow.
                    }
                    else
                    {
                        int xPos = 1;
                        List<TutionCloudWeb.Models.PostModel.Crossword.WordList> beforeAdd1 = new List<TutionCloudWeb.Models.PostModel.Crossword.WordList>();
                        xPos = yPosition + 1 - checkedLetterPosition;
                        for (int i = 0; i < newWordArray.Count(); i++)
                        {
                            TutionCloudWeb.Models.PostModel.Crossword.WordList secondWordTest = new TutionCloudWeb.Models.PostModel.Crossword.WordList();
                            secondWordTest.letter = newWordArray[i].ToString();
                            secondWordTest.xAxis = wordEqualPosition;
                            secondWordTest.yAxis = xPos;
                            secondWordTest.first = first;
                            if (first)
                            {
                                secondWordTest.wordId = item.wordId;
                            }
                            else
                            {
                                secondWordTest.wordId = 0;
                            }
                            secondWordTest.isVertical = false;
                            beforeAdd1.Add(secondWordTest);
                            xPos = xPos + 1;
                            first = false;
                        }
                        first = true;
                        var success = list.Where(x => x.sucessStatus == true).ToList();
                        foreach (var xx in success)
                        {
                            List<TutionCloudWeb.Models.PostModel.Crossword.WordList> yy = xx.list.ToList();
                            var have = yy.Where(x => beforeAdd1.Any(y => y.xAxis == x.xAxis && y.yAxis == x.yAxis)).ToList();
                            if (have != null && have.Count < 2)
                            {
                                newWord.list = new List<TutionCloudWeb.Models.PostModel.Crossword.WordList>();
                                newWord.list = beforeAdd;
                                newWord.sucessStatus = true;
                                beforeAdd.AddRange(beforeAdd1);
                            }
                        }
                    }

                }
            }
            #endregion Horizontal

            TutionCloudWeb.Models.PostModel.Crossword.ListWordList model = new TutionCloudWeb.Models.PostModel.Crossword.ListWordList();
            model.list = new List<TutionCloudWeb.Models.PostModel.Crossword.WordList>();
            model.wordDetailsList = new List<PostModel.Crossword.WordDetails>();
            model.wordDetailsList = wordList;
            model.userId = userId;
            model.list = beforeAdd;
            model.maxCount = maxCount;
            model.isNew = Convert.ToBoolean(data.isNew);
            model.completeStatus = Convert.ToBoolean(data.completeStatus);
            model.crosswordId = Convert.ToInt64(data.crosswordId);
            model.html = "";
            var crosswordData = _context.tCrosswords.FirstOrDefault(z => z.CrosswordId == model.crosswordId);
            if (crosswordData != null)
            {
                model.html = crosswordData.CrosswordSaveString;
            }

            List<TutionCloudWeb.Models.PostModel.Crossword.MainCross> otherList = list.Where(x => x.sucessStatus == false).ToList();
            maxCount = maxCount + 1;
            foreach (var item in otherList)
            {
                model.maxCount = maxCount + 1;
                char[] otherOne = item.word.ToArray();
                for (int i = 0; i < otherOne.Count(); i++)
                {
                    TutionCloudWeb.Models.PostModel.Crossword.WordList letter = new TutionCloudWeb.Models.PostModel.Crossword.WordList();
                    letter.letter = otherOne[i].ToString();
                    letter.xAxis = model.maxCount;
                    letter.yAxis = i + 1;
                    letter.first = first;
                    if (first)
                    {
                        letter.wordId = item.wordId;
                    }
                    else
                    {
                        letter.wordId = 0;
                    }
                    letter.isVertical = false;
                    beforeAdd.Add(letter);
                    first = false;
                }
                first = true;
                item.sucessStatus = true;
                maxCount = maxCount + 1;
            }

            return model;
        }



        /// <summary>
        /// //////////////
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal TutionCloudWeb.Models.PostModel.Word.DeleteSelectedWordReturn deletSelectedWords(PostModel.Word.DeleteSelectedWord model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.Word.DeleteSelectedWordReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.tUsers.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
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
                    var res = _context.SP_DELETE_SELECTED_WORDS(user.UserId, wordIds);
                    if (res > 0)
                    {
                        try
                        {
                            _context.SP_UPDATE_CROSSWORD_STATUS(wordIds);
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

        internal TutionCloudWeb.Models.PostModel.Word.DeleteAllWordReturn deleteAllWords(PostModel.Word.DeleteAllWord model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.Word.DeleteAllWordReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.tUsers.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var res = _context.SP_DELETE_ALL_WORDS(user.UserId);
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

        internal TutionCloudWeb.Models.PostModel.Word.DeleteAllFavoutiteWordReturn deleteAllFavouriteWords(PostModel.Word.DeleteAllFavoutiteWord model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.Word.DeleteAllFavoutiteWordReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.tUsers.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var res = _context.SP_DELETE_ALL_WORDS_FROM_FAVOURITES(user.UserId);
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

        internal TutionCloudWeb.Models.PostModel.Word.DeleteSelectedFavoutiteWordReturn deletSelectedFavouriteWords(PostModel.Word.DeleteSelectedFavoutiteWord model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.Word.DeleteSelectedFavoutiteWordReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.tUsers.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var currentTime = System.DateTime.UtcNow;

                    string wordIds = model.wordIds;
                    var res = _context.SP_DELETE_SELECTED_WORDS_FROM_FAVOURITES(user.UserId, wordIds);
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

        internal TutionCloudWeb.Models.PostModel.Word.DeleteAllSkiplistWordReturn deleteAllSkiplistWords(PostModel.Word.DeleteAllSkiplistWord model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.Word.DeleteAllSkiplistWordReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.tUsers.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var res = _context.SP_DELETE_ALL_WORDS_FROM_SKIPLIST(user.UserId);
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

        internal TutionCloudWeb.Models.PostModel.Word.DeleteSelectedSkiplistWordReturn deletSelectedSkiplistWords(PostModel.Word.DeleteSelectedSkiplistWord model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.Word.DeleteSelectedSkiplistWordReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.tUsers.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var currentTime = System.DateTime.UtcNow;

                    string wordIds = model.wordIds;
                    var res = _context.SP_DELETE_SELECTED_WORDS_FROM_SKIPLIST(user.UserId, wordIds);
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

        internal TutionCloudWeb.Models.PostModel.OnlineCommunity.AddWordReturn addWordToDatabase(TutionCloudWeb.Models.PostModel.OnlineCommunity.AddWord model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.OnlineCommunity.AddWordReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.tUsers.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var word = _context.tWords.FirstOrDefault(z => z.WordId == model.wordId);
                    if (word != null)
                    {
                        using (var dbContextTransaction = _context.Database.BeginTransaction())
                        {
                            try
                            {
                                var currentTime = System.DateTime.UtcNow;
                                var newWord = _context.tWords.Create();
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
                                _context.tWords.Add(newWord);

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

        internal TutionCloudWeb.Models.PostModel.OnlineCommunity.AddAllWordReturn addAllWordToDatabase(PostModel.OnlineCommunity.AddAllWord model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.OnlineCommunity.AddAllWordReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {

                string wordUserId = model.wordId;
                string[] SendUserIdArry = wordUserId.Split(',');
                model.wordId = null;
                   for (int i = 0; i < SendUserIdArry.Length; i++)
                   {
                       long wordID = Convert.ToInt64(SendUserIdArry[i]);
                       var r2 = _context.tWords.Where(x => x.WordId == wordID).FirstOrDefault();
                       var r1 = _context.tWords.Where(z => z.UserId == model.userId && z.IsActive == true && z.Word == r2.Word).FirstOrDefault();
                       if(r1 == null)
                       {
                           if (model.wordId == null)
                           {
                               model.wordId = SendUserIdArry[i];
                           }
                           else
                           {
                               model.wordId = model.wordId + "," + SendUserIdArry[i];
                           }
                           //model.wordId = model.wordId + 
                       }
                   }

                
                var user = _context.tUsers.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    
                    var res = _context.SP_INSERT_ONLINECOMMUNITY_WORDS(model.userId, model.wordId);
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

        internal TutionCloudWeb.Models.PostModel.Crossword.AddCrosswordToDBReturn saveCrosswordToDB(PostModel.Crossword.AddCrosswordToDB model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.Crossword.AddCrosswordToDBReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var user = _context.tUsers.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {

                    if (Convert.ToBoolean(model.isNewCrossword))
                    {

                        byte[] b = Convert.FromBase64String(model.wordIdListString);
                        var strOriginal = System.Text.Encoding.UTF8.GetString(b);
                        if (strOriginal != "")
                        {
                            var crossword = _context.tCrosswords.Create();
                            crossword.CreatedTime = System.DateTime.UtcNow;
                            crossword.IsDeleted = false;
                            crossword.UserId = model.userId;

                            byte[] htmlb = Convert.FromBase64String(model.html);
                            var strOriginalhtml = System.Text.Encoding.UTF8.GetString(htmlb);

                            crossword.CrosswordSaveString = strOriginalhtml;
                            crossword.IsCompleted = Convert.ToBoolean(model.completeStatus);
                            _context.tCrosswords.Add(crossword);
                            if (_context.SaveChanges() > 0)
                            {
                                string[] values = strOriginal.Split(',');
                                for (int i = 0; i < values.Length; i++)
                                {
                                    var crosswordword = _context.tCrosswordWords.Create();
                                    crosswordword.CrosswordId = crossword.CrosswordId;
                                    crosswordword.IsDeleted = false;
                                    crosswordword.WordId = Convert.ToInt64(values[i]);
                                    _context.tCrosswordWords.Add(crosswordword);
                                }
                                _context.SaveChanges();
                                returnModel.status = true;
                                returnModel.message = "Success";
                            }
                        }
                    }
                    else
                    {
                        var crosswordId = Convert.ToInt64(model.crosswordId);
                        var crosswordData = _context.tCrosswords.FirstOrDefault(z => z.CrosswordId == crosswordId);
                        if (crosswordData != null)
                        {
                            byte[] htmlb = Convert.FromBase64String(model.html);
                            var strOriginalhtml = System.Text.Encoding.UTF8.GetString(htmlb);
                            crosswordData.CrosswordSaveString = strOriginalhtml;
                            crosswordData.IsCompleted = Convert.ToBoolean(model.completeStatus);
                            if (_context.SaveChanges() > 0)
                            {
                                returnModel.status = true;
                                returnModel.message = "Success";
                            }
                        }
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

        internal Tuple<bool, string> deleteCrossword(string id)
        {
            try
            {
                var crosswordId = Convert.ToInt64(id);
                var crossword = _context.tCrosswords.FirstOrDefault(z => z.CrosswordId == crosswordId);
                if (crossword != null)
                {
                    crossword.IsDeleted = true;
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
                    return new Tuple<bool, string>(false, "Crossword not found");
                }
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }
        }

        internal Tuple<bool, string> deleteAllCrossword(string id)
        {
            try
            {
                var userId = Convert.ToInt64(id);
                var user = _context.tUsers.FirstOrDefault(z => z.UserId == userId);
                if (user != null)
                {
                    var res = _context.SP_DELETE_ALL_USER_CROSSWORDS(userId).ToString();
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

        internal Tuple<bool, string> deleteTest(string id)
        {
            try
            {
                var testId = Convert.ToInt64(id);
                var test = _context.tExams.FirstOrDefault(z => z.ExamId == testId);
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

        internal Tuple<bool, string> deleteAllTest(string id)
        {
            try
            {
                var userId = Convert.ToInt64(id);
                var user = _context.tUsers.FirstOrDefault(z => z.UserId == userId);
                if (user != null)
                {
                    var res = _context.SP_DELETE_ALL_USER_TEST(userId).ToString();
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

        internal Tuple<bool, string> deletSelectedTest(string id, long userId)
        {
            try
            {
                //var userId = Convert.ToInt64(id);

                byte[] b = Convert.FromBase64String(id);

                var strOriginal = System.Text.Encoding.UTF8.GetString(b);

                var user = _context.tUsers.FirstOrDefault(z => z.UserId == userId);
                if (user != null)
                {
                    var res = _context.SP_DELETE_SELECTED_TEST(userId, strOriginal).ToString();
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

        internal Tuple<bool, string, string> resittest(string id, long userId)
        {
            bool status = false;
            string message = "failed";
            string examIdRes = "";

            byte[] b = Convert.FromBase64String(id);

            var examIdString = System.Text.Encoding.UTF8.GetString(b);
            var examId = Convert.ToInt64(examIdString);

            try
            {
                var user = _context.tUsers.FirstOrDefault(z => z.UserId == userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {

                    var exam = _context.tExams.FirstOrDefault(z => z.ExamId == examId);
                    if (exam != null)
                    {
                        var currentTime = System.DateTime.UtcNow;
                        var newTest = _context.tExams.Create();
                        newTest.CreatedDate = currentTime;
                        newTest.ExamGuid = Guid.NewGuid();
                        newTest.IsActive = true;
                        newTest.TestTime = exam.TestTime;
                        newTest.TimePerWord = exam.TimePerWord;
                        newTest.Title = exam.Title;
                        newTest.UserId = Convert.ToInt64(userId);
                        _context.tExams.Add(newTest);

                        status = _context.SaveChanges() > 0 ? true : false;
                        message = status == true ? "success" : "failed";
                        examIdRes = "";
                        if (status)
                        {
                            examIdRes = newTest.ExamId.ToString();
                            List<long> result = exam.tExamWords.ToList().Select(z => z.WordId).ToList();
                            if (result.Count > 0)
                            {
                                foreach (var item in result)
                                {
                                    var examWord = _context.tExamWords.Create();
                                    examWord.ExamId = newTest.ExamId;
                                    examWord.WordId = Convert.ToInt64(item.ToString());
                                    _context.tExamWords.Add(examWord);
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

        public void sendemail(PostModel.User.Forgotpassword model)
        {
            try
            {
                var filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/email-Template/index.html");

                var emailTemplate = System.IO.File.ReadAllText(filePath);

             
                var mBody = emailTemplate.Replace("{{fullname}}", model.message + "?id=" + model.guid);
                         
                var result = SendEmail("Forgot Password", mBody,model.reciever);
            }
            catch (Exception ex)
            {

            }
        }


        internal TutionCloudWeb.Models.PostModel.User.ForgotpasswordReturn SendEmail_OLD(string subject,string mailbody,string reciever)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.User.ForgotpasswordReturn();
            returnModel.status = false;
            returnModel.message = "failed";

            try
            {

                var status = false;

                SmtpClient client = new SmtpClient();

                string userName = "contacttuitioncloud@gmail.com";
                string password = "StewG1963!";


                //string userName = "tutioncloud.noreply@gmail.com";
                ////string password = "wordutopia";
                //string password = "StewG1963!";
                string fromName = "TutionCloud";
                MailAddress address = new MailAddress(userName, fromName);


                    MailMessage message = new MailMessage();
                    message.To.Add(new MailAddress(reciever, "Receiver"));
                    message.From = address;
                    message.Subject = subject;
                    message.IsBodyHtml = true;
                    message.Body = mailbody;
                    client.Host = "smtp.gmail.com";//ConfigurationManager.AppSettings["smptpserver"];
                    client.Port = 587;//Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
                    //client.Port = 465;
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = true;
                    //client.UseDefaultCredentials = true;
                    client.Credentials = new NetworkCredential(userName, password);
                    try
                    {
                        client.Send(message);
                        
                    }
                    catch (Exception e)
                    {
                        status = false;
                    }
                    returnModel.status = true;
                    returnModel.message = "Success";
                    return returnModel;


            }
            catch (Exception ex)
            {
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }


        internal TutionCloudWeb.Models.PostModel.User.ForgotpasswordReturn SendEmail(string subject, string mailbody, string reciever)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.User.ForgotpasswordReturn();
            returnModel.status = false;
            returnModel.message = "failed";

            try
            {

                var status = false;

                SmtpClient client = new SmtpClient();

                string userName = "contact@tuitioncloud.com";
                string password = "Logan$8563!";


                //string userName = "tutioncloud.noreply@gmail.com";
                ////string password = "wordutopia";
                //string password = "StewG1963!";
                string fromName = "TutionCloud";
                MailAddress address = new MailAddress(userName, fromName);


                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress(reciever, "Receiver"));
                message.From = address;
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = mailbody;
                client.Host = "smtp.ionos.co.uk";//ConfigurationManager.AppSettings["smptpserver"];
                client.Port = 587;//Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
                                  //client.Port = 465;
                client.EnableSsl = true;
                client.UseDefaultCredentials = true;
                //client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential(userName, password);
                try
                {
                    client.Send(message);

                }
                catch (Exception e)
                {
                    status = false;
                }
                returnModel.status = true;
                returnModel.message = "Success";
                return returnModel;


            }
            catch (Exception ex)
            {
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }



        internal TutionCloudWeb.Models.PostModel.User.ForgotpasswordReturn forgotpass(PostModel.User.Forgotpassword model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.User.ForgotpasswordReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var currentTime = System.DateTime.UtcNow;
                var user = _context.tForgotPasswords.Create();
                user.userid = model.userid;
                user.passwordguid = Guid.NewGuid();
                model.guid = Convert.ToString(user.passwordguid);
                user.timestamp = currentTime;
                user.status = true;
                _context.tForgotPasswords.Add(user);
                returnModel.status = _context.SaveChanges() > 0 ? true : false;
                returnModel.message = returnModel.status == true ? "success" : "failed";
                FormsAuthentication.SetAuthCookie(user.userid.ToString(), true);
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }

        internal TutionCloudWeb.Models.PostModel.User.ForgotpasswordReturn Updatepassword(PostModel.User.Forgotpassword model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.User.ForgotpasswordReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                //var guid = StringCipher.Decrypt(model.guid, salt);
                model.queryguid = Guid.Parse(model.guid);
                var user = _context.tForgotPasswords.Where(z => z.passwordguid == model.queryguid && z.status).FirstOrDefault();

                if (user==null)
                {
                    returnModel.status = false;
                    returnModel.message = "The link is expired";
                }
                else
                {
                    var data = _context.tUsers.Where(z => z.UserId == user.userid && z.IsActive).FirstOrDefault();
                    if (data != null)
                    {
                        data.Password = StringCipher.Encrypt(model.newpassword, salt);
                        returnModel.status = _context.SaveChanges() > 0 ? true : false;
                        returnModel.message = returnModel.status == true ? "success" : "failed";
                    }
                    if(returnModel.status)
                    {
                        var statuschange = _context.tForgotPasswords.Where(z => z.userid == user.userid && z.status && z.passwordguid == model.queryguid).FirstOrDefault();
                        if (statuschange != null)
                        {
                            statuschange.status = false;
                            returnModel.status = _context.SaveChanges() > 0 ? true : false;
                            returnModel.message = returnModel.status == true ? "success" : "failed";
                        }
                    }
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


        //............Quester.....................


        internal TutionCloudWeb.Models.PostModel.Word.AddFavouriteWordReturn Quester_addfavouriteword(PostModel.Word.AddFavouriteWord model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.Word.AddFavouriteWordReturn();
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

                            //commented on 27/02/2019 by Basheer for as per the updated client feedback
                            //var skippedWord = _context.tUserSkippedWords.Where(z => z.UserId == model.userId && z.WordId == wordId).FirstOrDefault();
                            //if (skippedWord != null)
                            //{
                            //    _context.tUserSkippedWords.Remove(skippedWord);
                            //}
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


        internal TutionCloudWeb.Models.PostModel.Exam.CreateTestReturn Quester_createtest(PostModel.Exam.CreateTest model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.Exam.CreateTestReturn();
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

        //jibin 11/17/2020
        internal TutionCloudWeb.Models.PostModel.User.ForgotpasswordReturn Quester_forgotpass(PostModel.User.Forgotpassword model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.User.ForgotpasswordReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                var currentTime = System.DateTime.UtcNow;
                var user = _context.Quester_ForgotPassword.Create();
                user.userid = model.userid;
                user.passwordguid = Guid.NewGuid();
                model.guid = Convert.ToString(user.passwordguid);
                user.timestamp = currentTime;
                user.status = true;
                _context.Quester_ForgotPassword.Add(user);
                returnModel.status = _context.SaveChanges() > 0 ? true : false;
                returnModel.message = returnModel.status == true ? "success" : "failed";
                FormsAuthentication.SetAuthCookie(user.userid.ToString(), true);
                return returnModel;
            }
            catch (Exception ex)
            {
                returnModel.status = false;
                returnModel.message = ex.Message;
                return returnModel;
            }
        }

        //jibin 11/17/2020

        //jibin 11/20/2020


        internal TutionCloudWeb.Models.PostModel.User.ForgotpasswordReturn Quester_Updatepassword(PostModel.User.Forgotpassword model)
        {
            var returnModel = new TutionCloudWeb.Models.PostModel.User.ForgotpasswordReturn();
            returnModel.status = false;
            returnModel.message = "failed";
            try
            {
                //var guid = StringCipher.Decrypt(model.guid, salt);
                model.queryguid = Guid.Parse(model.guid);
                var user = _context.Quester_ForgotPassword.Where(z => z.passwordguid == model.queryguid && z.status).FirstOrDefault();

                if (user == null)
                {
                    returnModel.status = false;
                    returnModel.message = "The link is expired";
                }
                else
                {
                    var data = _context.Quester_User.Where(z => z.UserId == user.userid && z.IsActive).FirstOrDefault();
                    if (data != null)
                    {
                        data.Password = model.newpassword;
                        returnModel.status = _context.SaveChanges() > 0 ? true : false;
                        returnModel.message = returnModel.status == true ? "success" : "failed";
                    }
                    if (returnModel.status)
                    {
                        var statuschange = _context.Quester_ForgotPassword.Where(z => z.userid == user.userid && z.status && z.passwordguid == model.queryguid).FirstOrDefault();
                        if (statuschange != null)
                        {
                            statuschange.status = false;
                            returnModel.status = _context.SaveChanges() > 0 ? true : false;
                            returnModel.message = returnModel.status == true ? "success" : "failed";
                        }
                    }
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

        //jibin 11/20/2020

    }
}