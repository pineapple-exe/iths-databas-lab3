using SaintNicholas.ConsoleApp.Screens;
using SaintNicholas.Data;
using SaintNicholas.Data.DataHandlers;
using System;
using System.Collections.Generic;

namespace SaintNicholas.ConsoleApp.Interactives
{
    class BehavioralRecordsFunctions : Validators
    {
        public static void SetBehavior(SaintNicholasDbContext context)
        {
            Console.WriteLine("Enter empty string to cancel.");
            if (!RepeatableReadline("Specify Id of child.", ChildValidator, out string id))
            {
                return;
            }
            if (!RepeatableReadline("Has been naughty this year (y/n): ", BoolValidator, out string propertyValue))
            {
                return;
            }
            else
            {
                BehavioralRecordsHandler.AddData(context, id, propertyValue);
                Console.WriteLine("Record successfully added.");
                Console.WriteLine("Press Enter to return to menu.");
                Console.ReadLine();
            }
        }

        public static void CheckGingerBreadDemand(SaintNicholasDbContext context, bool naughty, int[] columnWidths, List<string> header)
        {
            BehavioralRecordsScreens.GingerBreadScreen(context, naughty, columnWidths, header);

            Console.WriteLine();
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }

        public static void ViewRecords(SaintNicholasDbContext context, int[] columnWidths, List<string> header)
        {
            BehavioralRecordsScreens.ProvideRecordsTable(context, columnWidths, header);

            Console.WriteLine();
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }
    }
}
