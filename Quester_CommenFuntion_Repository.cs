using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutionCloudWeb.Repository.Quester
{
    public class Quester_CommenFuntion_Repository : Quester_BaseRepository
    {
        Quester_BaseRepository Quester_BaseRepository = new Quester_BaseRepository();
        public long Quester_Word_Count_UserId(long userId)
        {
            long count = 0;

            try
            {
                 count = _context.Quester_Word.Where(x => x.UserId == userId && x.IsActive == true).ToList().Select(x => x.WordId).Count();
            }
            catch(Exception ex)
            {
                Quester_BaseRepository.Quester_Exception_Save(ex.Message, "Quester_CommenFuntion_Repository", "Quester_Word_Count_UserId");
            }
            return count;
        }

        public long Quester_Questions_Tested_Count(long userId,long wordId)
        {
            try
            {

                var result = _context.Quester_TimeOfTested_Word(userId, wordId).FirstOrDefault();

                long count = Convert.ToInt64(result.Value);


                return count;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}