using System;

namespace SaintNicholas.Data.DataHandlers
{
    public class BehavioralRecordsHandler
    {
        public static void AddData(SaintNicholasDbContext context, string id, string hasBeenNaughty)
        {
            BehavioralRecord record = new BehavioralRecord
            {
                ChildID = int.Parse(id),
                Year = DateTime.Now.Year,
                Naughty = hasBeenNaughty.ToLower() == "y"
            };

            context.BehavioralRecords.Update(record);
            context.SaveChanges();
        }
    }
}
