using SaintNicholas.ConsoleApp.Screens;
using SaintNicholas.Data;
using SaintNicholas.Data.DataHandlers;
using System;

namespace SaintNicholas.ConsoleApp.Interactives
{
    class BehavioralRecordsFunctions
    {
        public static void SetBehavior(SaintNicholasDbContext context)
        {
            Console.WriteLine("Enter empty string to cancel.");
            if (!Validators.RepeatableReadline("Specify Id of child.", Validators.ChildValidator, out string id))
            {
                return;
            }
            if (!Validators.RepeatableReadline("Has been naughty this year (y/n): ", Validators.BoolValidator, out string propertyValue))
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

        public static void CheckGingerBreadDemand(SaintNicholasDbContext context, bool naughty)
        {
            BehavioralRecordsScreens.GingerBreadScreen(context, naughty);

            Console.WriteLine();
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }

        public static void ViewRecords(SaintNicholasDbContext context)
        {
            BehavioralRecordsScreens.ProvideRecordsTable(context);

            Console.WriteLine();
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }
    }
}
