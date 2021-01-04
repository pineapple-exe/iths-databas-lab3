﻿using SaintNicholas.Data;
using SaintNicholas.Data.DataHandlers;
using System;
using System.Collections.Generic;

namespace SaintNicholas.ConsoleApp.Screens
{
    class BehavioralRecordsScreens : Utils
    {
        public static void GingerBreadScreen(SaintNicholasDbContext context, bool naughty, int[] columnWidths, List<string> header)
        {
            string adjective = naughty ? "naughty" : "good";
            string suggestion = naughty ? "we respond to a potential dire need of gingerbread abundance" :
                                           "their exposure to gingerbread is restricted";

            List<Child> whoNeedsIt = BehavioralRecordsHandler.GeneralGingerBread(context, naughty);
            List<string> childStrings = ChildStrings(whoNeedsIt, columnWidths);

            if (childStrings == null)
            {
                Console.WriteLine("No such records.");
                Console.WriteLine("Press Enter to return to menu.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine($"Following children have been {adjective} the past three years.");
            Console.WriteLine($"It is therefore suggested that {suggestion}.");
            Console.WriteLine();

            PrintTable(columnWidths, header, childStrings);
        }

        private static List<string> RecordStrings(List<BehavioralRecord> theRecords, int[] columnWidths)
        {
            var theStrings = new List<string>();

            foreach (BehavioralRecord r in theRecords)
            {
                var recordValues = new List<string>
                {
                    Ellipsis(r.ChildID.ToString(), columnWidths[0]),
                    Ellipsis(r.Year.ToString(), columnWidths[1]),
                    Ellipsis(r.Naughty.ToString(), columnWidths[2]),
                };
                theStrings.Add(BuildRow(recordValues, columnWidths));
            }
            return theStrings;
        }

        public static void ProvideRecordsTable(SaintNicholasDbContext context, int[] columnWidths, List<string> header)
        {
            List<string> rows = RecordStrings(BehavioralRecordsHandler.RecordsTable(context), columnWidths);

            PrintTable(columnWidths, header, rows);
        }
    }
}