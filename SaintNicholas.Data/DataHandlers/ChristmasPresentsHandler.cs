using System;
using System.Collections.Generic;
using System.Linq;

namespace SaintNicholas.Data.DataHandlers
{
    public static class ChristmasPresentsHandler
    {
        public static void AddData(int quantity, string[] propertyValues, SaintNicholasDbContext context)
        {
            for (int i = 0; i < quantity; i++)
            {
                ChristmasPresent newPresent = new ChristmasPresent();

                for (int j = 0; j < propertyValues.Length; j++)
                {
                    switch (j)
                    {
                        case 0:
                            newPresent.Contents = propertyValues[j];
                            break;

                        case 1:
                            newPresent.ForGender = propertyValues[j];
                            break;

                        case 2:
                            newPresent.ForNaughtyChild = propertyValues[j].ToLower() == "y";
                            break;

                        case 3:
                            newPresent.HandOutYear = int.Parse(propertyValues[j]);
                            break;
                    }
                }
                context.ChristmasPresents.Add(newPresent);
                context.SaveChanges();
            }
        }

        private static int MatchXyz(SaintNicholasDbContext context, List<ChristmasPresent> anyPresents, List<ChristmasPresent> girlPresents, List<ChristmasPresent> boyPresents, List<int> othersID, List<int> girlsID, List<int> boysID)
        {
            int highestCommonDenominator = Math.Min(anyPresents.Count(), othersID.Count());
            int matches = highestCommonDenominator;

            for (int i = 0; i < highestCommonDenominator; i++)
            {
                anyPresents[i].Receiver = othersID[i];
                context.ChristmasPresents.Update(anyPresents[i]);

                anyPresents.Remove(anyPresents[i]);
            }

            highestCommonDenominator = Math.Min(girlPresents.Count(), girlsID.Count());
            matches += highestCommonDenominator;

            for (int j = 0; j < highestCommonDenominator; j++)
            {
                girlPresents[j].Receiver = girlsID[j];
                context.ChristmasPresents.Update(anyPresents[j]);

                girlPresents.Remove(girlPresents[j]);
                girlsID.Remove(girlsID[j]);
            }

            highestCommonDenominator = Math.Min(boyPresents.Count(), boysID.Count());
            matches += highestCommonDenominator;

            for (int k = 0; k < highestCommonDenominator; k++)
            {
                boyPresents[k].Receiver = boysID[k];
                context.ChristmasPresents.Update(anyPresents[k]);

                boyPresents.Remove(boyPresents[k]);
                boysID.Remove(boysID[k]);
            }

            if ((girlsID.Count > 0 || boysID.Count > 0) && anyPresents.Count > 0)
            {
                List<int> restID = girlsID.Concat(boysID).ToList();

                highestCommonDenominator = Math.Min(anyPresents.Count(), restID.Count());
                matches += highestCommonDenominator;

                for (int l = 0; l < highestCommonDenominator; l++)
                {
                    anyPresents[l].Receiver = restID[l];
                    context.ChristmasPresents.Update(anyPresents[l]);
                }
            }
            return matches;
        }

        public static int Match(SaintNicholasDbContext context)
        {
            int year = DateTime.Now.Year;

            var alreadyGotPresents = context.ChristmasPresents.Where(p => p.HandOutYear == year).Select(p => p.Receiver);

            var girlsToServeID = context.Children
                .Where(c => !alreadyGotPresents.Contains(c.Id) && c.Gender.ToLower() == "girl")
                .Select(c => c.Id)
                .ToList();

            var boysToServeID = context.Children
                .Where(c => !alreadyGotPresents.Contains(c.Id) && c.Gender.ToLower() == "boy")
                .Select(c => c.Id)
                .ToList();

            var othersToServeID = context.Children
                .Where(c => !alreadyGotPresents.Contains(c.Id) && c.Gender.ToLower() == "u")
                .Select(c => c.Id)
                .ToList();

            var naughtyChildrenID = context.BehavioralRecords.Where(r => r.Naughty).Select(r => r.ChildID);
            var goodChildrenID = context.BehavioralRecords.Where(r => !r.Naughty).Select(r => r.ChildID);

            var naughtyGirlsID = naughtyChildrenID.Where(i => girlsToServeID.Contains(i)).ToList();
            var naughtyBoysID = naughtyChildrenID.Where(i => boysToServeID.Contains(i)).ToList();
            var naughtyOthersID = naughtyChildrenID.Where(i => othersToServeID.Contains(i)).ToList();

            var goodGirlsID = goodChildrenID.Where(i => girlsToServeID.Contains(i)).ToList();
            var goodBoysID = goodChildrenID.Where(i => boysToServeID.Contains(i)).ToList();
            var goodOthersID = goodChildrenID.Where(i => othersToServeID.Contains(i)).ToList();

            var goodGirlPresents = context.ChristmasPresents.Where(p => p.HandOutYear == year && !p.ForNaughtyChild && p.ForGender.ToLower() == "girl" && !p.Receiver.HasValue).ToList();
            var goodBoyPresents = context.ChristmasPresents.Where(p => p.HandOutYear == year && !p.ForNaughtyChild && p.ForGender.ToLower() == "boy" && !p.Receiver.HasValue).ToList();
            var goodAnyPresents = context.ChristmasPresents.Where(p => p.HandOutYear == year && !p.ForNaughtyChild && p.ForGender.ToLower() == "u" && !p.Receiver.HasValue).ToList();

            var naughtyGirlPresents = context.ChristmasPresents.Where(p => p.HandOutYear == year && p.ForNaughtyChild && p.ForGender.ToLower() == "girl" && !p.Receiver.HasValue).ToList();
            var naughtyBoyPresents = context.ChristmasPresents.Where(p => p.HandOutYear == year && p.ForNaughtyChild && p.ForGender.ToLower() == "boy" && !p.Receiver.HasValue).ToList();
            var naughtyAnyPresents = context.ChristmasPresents.Where(p => p.HandOutYear == year && p.ForNaughtyChild && p.ForGender.ToLower() == "u" && !p.Receiver.HasValue).ToList();

            int matches = MatchXyz(context, naughtyAnyPresents, naughtyGirlPresents, naughtyBoyPresents, naughtyOthersID, naughtyGirlsID, naughtyBoysID);
            matches += MatchXyz(context, goodAnyPresents, goodGirlPresents, goodBoyPresents, goodOthersID, goodGirlsID, goodBoysID);

            context.SaveChanges();
            return matches;
        }
    }
}
