using SaintNicholas.Data;
using SaintNicholas.Data.Entities;
using SaintNicholas.Data.DataHandlers;
using System;
using System.Collections.Generic;

namespace SaintNicholas.ConsoleApp.Screens
{
    class BehavioralRecordsScreens
    {
        static readonly List<string> headerR = new List<string>() { "ChildID", "Year", "Naughty" };
        static readonly int[] columnWidthsR = new int[] { 7, 5, 7 };

        public static void GingerBreadScreen(SaintNicholasDbContext context, bool naughty)
        {
            string adjective = naughty ? "naughty" : "good";
            string suggestion = naughty ? "we respond to a potential dire need of gingerbread abundance" :
                                           "their exposure to gingerbread is restricted";

            List<Child> whoNeedsIt = BehavioralRecordsHandler.GeneralGingerBread(context, naughty);

            if (whoNeedsIt == null)
            {
                Console.WriteLine("No such records.");
                Console.WriteLine("Press Enter to return to menu.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine($"Following children have been {adjective} the past three years.");
            Console.WriteLine($"It is therefore suggested that {suggestion}.");
            Console.WriteLine();

            ChildrenScreens.ProvideChildrensTable(whoNeedsIt);
        }

        private static List<string> RecordStrings(List<BehavioralRecord> theRecords, int[] columnWidths)
        {
            var theStrings = new List<string>();

            foreach (BehavioralRecord r in theRecords)
            {
                var recordValues = new List<string>
                {
                    Utils.Ellipsis(r.ChildID.ToString(), columnWidths[0]),
                    Utils.Ellipsis(r.Year.ToString(), columnWidths[1]),
                    Utils.Ellipsis(r.Naughty.ToString(), columnWidths[2]),
                };
                theStrings.Add(Utils.BuildRow(recordValues, columnWidths));
            }
            return theStrings;
        }

        public static void ProvideRecordsTable(SaintNicholasDbContext context)
        {
            List<string> rows = RecordStrings(BehavioralRecordsHandler.RecordsTable(context), columnWidthsR);

            Utils.PrintTable(columnWidthsR, headerR, rows);
        }
    }
}
