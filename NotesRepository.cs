using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TutionCloudWeb.Model.Wordutopia;
using TutionCloudWeb.Models.Database;

namespace TutionCloudWeb.Repository.Wordutopia
{
    public class NotesRepository
    {
        TutionCloudEntities TutionCloudEntities = new TutionCloudEntities();
        /// <summary>
        /// Name : Notes
        /// CreateBy : SIBI
        /// CreateON : 13/03/2019
        /// UpdatedBy : non
        /// Details : non 
        /// </summary>
        /// <returns></returns>
        public async Task<Notes> Notes(Notes model)
        {
            try
            {
                model.totalNoteCount =  TutionCloudEntities.SP_FETCH_USER_TOTAL_NOTE_COUNT(model.userId, string.Empty).FirstOrDefault().ToString();
                return model;
            }
            catch (Exception ex)
            {
                model.Message = ex.Message;

                return model;
            }
           
        }

        /// <summary>
        /// Name : NoteCount
        /// CreateBy : SIBI
        /// CreateON : 15/03/2019
        /// UpdatedBy : non
        /// Details : non 
        /// </summary>
        /// <returns></returns>
        /// 
        public string NoteCount(long userId, string searchText)
        {
            try
            {
                var count = TutionCloudEntities.SP_FETCH_USER_TOTAL_NOTE_COUNT(userId, searchText ?? string.Empty).FirstOrDefault().ToString();
                return count;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        /// <summary>
        /// Name : deletNote
        /// CreateBy : SIBI
        /// CreateON : 15/03/2019
        /// UpdatedBy : non
        /// Details : non 
        /// </summary>
        /// <returns></returns>
        /// 
        internal Tuple<bool, string> deletNote(string id)
        {
            try
            {
                var noteId = Convert.ToInt64(id);
                var note = TutionCloudEntities.tNotes.FirstOrDefault(z => z.NoteId == noteId);
                if (note != null)
                {
                    note.IsActive = false;
                    note.UpdatedDate = System.DateTime.UtcNow;
                    if (TutionCloudEntities.SaveChanges() > 0)
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

    }
}