using System;
using System.Collections.Generic;
using System.Linq;

namespace SaintNicholas.Data.DataHandlers
{
    public class BehavioralRecordsHandler
    {
        public static void AddData(SaintNicholasDbContext context, string id, string hasBeenNaughty)
        {
            int year = DateTime.Now.Year;
            int intId = int.Parse(id);
            bool naughty = hasBeenNaughty.ToLower() == "y";

            if (context.BehavioralRecords.Any(c => c.ChildID == intId && c.Year == year))
            {
                BehavioralRecord record = context.BehavioralRecords.Where(c => c.ChildID == intId && c.Year == year).First();
                record.Naughty = naughty;
                context.BehavioralRecords.Update(record);
            }
            else
            {
                BehavioralRecord record = new BehavioralRecord
                {
                    ChildID = intId,
                    Year = year,
                    Naughty = naughty
                };
                context.BehavioralRecords.Add(record);
            }
            context.SaveChanges();
        }

        private static List<List<BehavioralRecord>> ThirdTimesACharm(SaintNicholasDbContext context)
        {
            int currentYear = DateTime.Now.Year;

            var step1 = context.BehavioralRecords.Where(r => r.Year == currentYear - 3).ToList();
            var step2 = context.BehavioralRecords.Where(r => r.Year == currentYear - 2).ToList();
            var step3 = context.BehavioralRecords.Where(r => r.Year == currentYear - 1).ToList();

            var threeLastYears = new List<List<BehavioralRecord>>
            {
                step1,
                step2,
                step3
            };

            return threeLastYears;
        }

        public static List<Child> GeneralGingerBread(SaintNicholasDbContext context, bool naughty)
        {
            List<List<BehavioralRecord>> threeLastYears = ThirdTimesACharm(context);

            var year1 = threeLastYears[0].Where(r => r.Naughty == naughty).Select(r => r.ChildID);
            var year2 = threeLastYears[1].Where(r => r.Naughty == naughty).Select(r => r.ChildID);
            var year3 = threeLastYears[2].Where(r => r.Naughty == naughty).Select(r => r.ChildID);
            var needsIt = year1
                .Where(r => year2.Contains(r) && year3.Contains(r))
                .ToList();

            if (needsIt.Count < 1)
            {
                return null;
            }

            var theChildren = context.Children.Where(c => needsIt.Contains(c.Id)).ToList();
            return theChildren;
        }
    }
}
