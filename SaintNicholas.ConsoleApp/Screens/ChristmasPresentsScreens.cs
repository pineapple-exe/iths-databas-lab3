using System;
using System.Collections.Generic;
using SaintNicholas.Data;
using SaintNicholas.Data.DataHandlers;
using SaintNicholas.Data.Entities;

namespace SaintNicholas.ConsoleApp.Screens
{
    class ChristmasPresentsScreens
    {
        static readonly List<string> headerP = new List<string>() { "Id", "Contents", "ForGender", "ForNaughtyChild", "ReceiverId", "HandOutYear" };
        static readonly int[] columnWidthsP = new int[] { 5, 23, 9, 15, 10, 11 };

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

        private static List<string> ChristmasPresentStrings(List<ChristmasPresent> thePresents)
        {
            var theStrings = new List<string>();
            foreach (ChristmasPresent p in thePresents)
            {
                var christmasPresentValues = new List<string>
                {
                    Utils.Ellipsis(p.Id.ToString(), columnWidthsP[0]),
                    Utils.Ellipsis(p.Contents.ToString(), columnWidthsP[1]),
                    Utils.Ellipsis(p.ForGender.ToString(), columnWidthsP[2]),
                    Utils.Ellipsis(p.ForNaughtyChild.ToString(), columnWidthsP[3]),
                    Utils.Ellipsis(p.ReceiverId.ToString(), columnWidthsP[4]),
                    Utils.Ellipsis(p.HandOutYear.ToString(), columnWidthsP[5])
                };
                theStrings.Add(Utils.BuildRow(christmasPresentValues, columnWidthsP));
            }
            return theStrings;
        }

        public static void ProvidePresentsTable(SaintNicholasDbContext context)
        {
            List<string> rows = ChristmasPresentStrings(ChristmasPresentsHandler.PresentsTable(context));

            Utils.PrintTable(columnWidthsP, headerP, rows);
        }
    }
}
