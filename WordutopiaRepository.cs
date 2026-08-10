using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TutionCloudWeb.Model.Wordutopia;
using TutionCloudWeb.Models.Database;

namespace TutionCloudWeb.Repository.Wordutopia
{
    public class WordutopiaRepository
    {
        TutionCloudEntities TutionCloudEntities = new TutionCloudEntities();
        /// <summary>
        /// Name : Words
        /// CreateBy : SIBI
        /// CreateON : 13/03/2019
        /// UpdatedBy : non
        /// Details : non 
        /// </summary>
        /// <returns></returns>
        public async Task<WordsModel> Words(WordsModel model)
        {
            try {
                model.totalWordCount = TutionCloudEntities.SP_FETCH_USER_TOTAL_WORD_COUNT(model.userId, string.Empty).FirstOrDefault().ToString();
                return model;            
            }
            catch(Exception ex)
            {
                model.Message = ex.Message;

                return model;
            }
        }

        /// <summary>
        /// Name : Addword
        /// CreateBy : SIBI
        /// CreateON : 14/03/2019
        /// UpdatedBy : non
        /// Details : non 
        /// </summary>
        /// <returns></returns>
        public async Task<WordsModel> Addword(WordsModel model)
        {
            WordsModel WordsModel = new WordsModel();

            WordsModel.status = false;
            WordsModel.message = "failed";
            try
            {
                var user = TutionCloudEntities.tUsers.FirstOrDefault(z => z.UserId == model.userId && z.IsActive && z.IsSuspended == false);
                if (user != null)
                {
                    var currentTime = System.DateTime.UtcNow;
                    var newWord = TutionCloudEntities.tWords.Create();
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
                    TutionCloudEntities.tWords.Add(newWord);

                    WordsModel.status = TutionCloudEntities.SaveChanges() > 0 ? true : false;
                    WordsModel.message = WordsModel.status == true ? "success" : "failed";
                }
                else
                {
                    WordsModel.status = false;
                    WordsModel.message = "Invalid user";
                }
                return WordsModel;
            }
            catch (Exception ex)
            {
                WordsModel.status = false;
                WordsModel.message = ex.Message;
                return WordsModel;
            }
        }

        /// <summary>
        /// Name : deletWord
        /// CreateBy : SIBI
        /// CreateON : 14/03/2019
        /// UpdatedBy : non
        /// Details : non 
        /// </summary>
        /// <returns></returns>
        internal Tuple<bool, string> deletWord(string id)
        {
            try
            {
                var wordId = Convert.ToInt64(id);
                var word = TutionCloudEntities.tWords.FirstOrDefault(z => z.WordId == wordId);
                if (word != null)
                {
                    word.IsActive = false;
                    word.UpdatedDate = System.DateTime.UtcNow;
                    if (TutionCloudEntities.SaveChanges() > 0)
                    {
                        try
                        {
                            TutionCloudEntities.SP_UPDATE_CROSSWORD_STATUS(id);
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

        /// <summary>
        /// Name : WordCount
        /// CreateBy : SIBI
        /// CreateON : 14/03/2019
        /// UpdatedBy : non
        /// Details : non 
        /// </summary>
        /// <returns></returns>
        public string WordCount(long userId, string searchText)
        {
            try
            {
                var count = TutionCloudEntities.SP_FETCH_USER_TOTAL_WORD_COUNT(userId, searchText ?? string.Empty).FirstOrDefault().ToString();
                return count;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        
    }
}