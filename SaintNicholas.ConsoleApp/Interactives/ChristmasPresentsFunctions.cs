using SaintNicholas.ConsoleApp.Screens;
using SaintNicholas.Data;
using SaintNicholas.Data.DataHandlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaintNicholas.ConsoleApp.Interactives
{
    class ChristmasPresentsFunctions : Validators
    {
        public static void AddPresent(SaintNicholasDbContext context)
        {
            Console.WriteLine("Enter empty string to cancel.");

            string[] propertyValues = new string[3];
            string initialQ = "Quantity: ";

            if (!RepeatableReadline(initialQ, IntegerValidator, out string quantity) || quantity == "0")
            {
                return;
            }
            if (!RepeatableReadline("Contents: ", s => null, out propertyValues[0]))
            {
                return;
            }
            if (!RepeatableReadline("For gender (girl/boy/u): ", GenderValidator, out propertyValues[1]))
            {
                return;
            }
            if (!RepeatableReadline("And lastly... For naughty children (y/n): ", BoolValidator, out propertyValues[2]))
            {
                return;
            }
            ChristmasPresentsHandler.AddData(int.Parse(quantity), propertyValues, context);
            Console.WriteLine("Presents successfully added to database.");
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }

        public static void CheckDemands(SaintNicholasDbContext context)
        {
            Demands demands = Demands.CheckDemands(context);
            ChristmasPresentsScreens.ScreenDemands(demands);

            Console.WriteLine();
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }

        public static void MatchPresents(SaintNicholasDbContext context)
        {
            int matches = ChristmasPresentsHandler.Match(context);
            context.SaveChanges();

            if (matches > 0)
            {
                string singularOrPlural = matches == 1 ? "present has been paired with an adequate receiver." :
                                                                                  "presents have been paired with adequate receivers.";
                Console.WriteLine($"{matches} {singularOrPlural}");
            }
            else
            {
                Console.WriteLine("No matches could be made.");
            }
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }

        public static void ViewPresents(SaintNicholasDbContext context, int[] columnWidths, List<string> header)
        {
            ChristmasPresentsScreens.ProvidePresentsTable(context, columnWidths, header);

            Console.WriteLine();
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }
    }
}
