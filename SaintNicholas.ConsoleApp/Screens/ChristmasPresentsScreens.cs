using System;
using System.Collections.Generic;
using SaintNicholas.Data;
using SaintNicholas.Data.DataHandlers;
using SaintNicholas.Data.Entities;

namespace SaintNicholas.ConsoleApp.Screens
{
    class ChristmasPresentsScreens
    {
        private static void DemandsDetails(int[] behavioralDemands, Dictionary<Gender, int>[] genderedDemands, params string[] typeNotes)
        {
            for (int i = 0; i < behavioralDemands.Length; i++)
            {
                Console.WriteLine($" {typeNotes[i]}: {behavioralDemands[i]} =");
                Console.WriteLine($"        Girls: {genderedDemands[i][Gender.Girl]}");
                Console.WriteLine($"        Boys: {genderedDemands[i][Gender.Boy]}");
                Console.WriteLine($"        Others: {genderedDemands[i][Gender.Other]}");
                Console.WriteLine();
            }
        }

        public static void ScreenDemands(Demands demands)
        {
            if (demands.Diff > 0)
            {
                Console.WriteLine($"Christmas present needs yet to be matched: {demands.Diff}");
                Console.WriteLine("(Behavior is required to determine quality of presents.)");
                Console.WriteLine();

                int[] behavioralDemands = new int[] { demands.FunNum, demands.DullNum, demands.BlankNum };
                Dictionary<Gender, int>[] genderedDemands = new Dictionary<Gender, int>[] { demands.GoodGendersNum, demands.NaughtyGendersNum, demands.UnevaluatedGendersNum };
                DemandsDetails(behavioralDemands, genderedDemands, "Fun presents", "Dull presents", "Children with unknown behavior");

                Console.WriteLine("Use [Match presents with children] to keep these numbers reflective of the production demand.");
            }
            else
            {
                Console.WriteLine("The amount of presents is sufficient for this year!");
            }
        }

        private static List<string> ChristmasPresentStrings(List<ChristmasPresent> thePresents, int[] columnWidths)
        {
            var theStrings = new List<string>();
            foreach (ChristmasPresent p in thePresents)
            {
                var christmasPresentValues = new List<string>
                {
                    Utils.Ellipsis(p.Id.ToString(), columnWidths[0]),
                    Utils.Ellipsis(p.Contents.ToString(), columnWidths[1]),
                    Utils.Ellipsis(p.ForGender.ToString(), columnWidths[2]),
                    Utils.Ellipsis(p.ForNaughtyChild.ToString(), columnWidths[3]),
                    Utils.Ellipsis(p.ReceiverId.ToString(), columnWidths[4]),
                    Utils.Ellipsis(p.HandOutYear.ToString(), columnWidths[5])
                };
                theStrings.Add(Utils.BuildRow(christmasPresentValues, columnWidths));
            }
            return theStrings;
        }

        public static void ProvidePresentsTable(SaintNicholasDbContext context, int[] columnWidths, List<string> header)
        {
            List<string> rows = ChristmasPresentStrings(ChristmasPresentsHandler.PresentsTable(context), columnWidths);

            Utils.PrintTable(columnWidths, header, rows);
        }
    }
}
