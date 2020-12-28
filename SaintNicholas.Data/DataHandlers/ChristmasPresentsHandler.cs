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
                newPresent.HandOutYear = DateTime.Now.Year;

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
                    }
                }
                context.ChristmasPresents.Add(newPresent);
                context.SaveChanges();
            }
        }

        private static int MatchXyz(SaintNicholasDbContext context, List<ChristmasPresent> neutralPresents, List<ChristmasPresent> girlPresents, List<ChristmasPresent> boyPresents, List<int> othersID, List<int> girlsID, List<int> boysID)
        {
            int highestCommonDenominator = Math.Min(neutralPresents.Count, othersID.Count);
            int matches = highestCommonDenominator;

            List<ChristmasPresent> neutralPresentsMatched = new List<ChristmasPresent>();

            for (int i = 0; i < highestCommonDenominator; i++)
            {
                neutralPresents[i].ReceiverId = othersID[i];
                context.ChristmasPresents.Update(neutralPresents[i]);

                neutralPresentsMatched.Add(neutralPresents[i]);
            }

            highestCommonDenominator = Math.Min(girlPresents.Count, girlsID.Count);
            matches += highestCommonDenominator;

            List<int> servedGirlsNBoys = new List<int>();

            for (int j = 0; j < highestCommonDenominator; j++)
            {
                girlPresents[j].ReceiverId = girlsID[j];
                context.ChristmasPresents.Update(girlPresents[j]);

                servedGirlsNBoys.Add(girlsID[j]);
            }

            highestCommonDenominator = Math.Min(boyPresents.Count, boysID.Count);
            matches += highestCommonDenominator;

            for (int k = 0; k < highestCommonDenominator; k++)
            {
                boyPresents[k].ReceiverId = boysID[k];
                context.ChristmasPresents.Update(boyPresents[k]);

                servedGirlsNBoys.Add(boysID[k]);
            }

            List<int> restID = girlsID.Concat(boysID).Except(servedGirlsNBoys).ToList();
            List<ChristmasPresent> neutralPresentsRest = neutralPresents.Except(neutralPresentsMatched).ToList();

            if (restID.Count > 0 && neutralPresentsRest.Count > 0)
            {
                highestCommonDenominator = Math.Min(neutralPresentsRest.Count, restID.Count);
                matches += highestCommonDenominator;

                for (int l = 0; l < highestCommonDenominator; l++)
                {
                    neutralPresentsRest[l].ReceiverId = restID[l];
                    context.ChristmasPresents.Update(neutralPresentsRest[l]);
                }
            }
            return matches;
        }

        public static int Match(SaintNicholasDbContext context)
        {
            int year = DateTime.Now.Year;

            var alreadyGotPresents = context.ChristmasPresents.Where(p => p.HandOutYear == year).Select(p => p.ReceiverId);

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

            var naughtyChildrenID = context.BehavioralRecords.Where(r => r.Naughty && r.Year == year).Select(r => r.ChildID);
            var goodChildrenID = context.BehavioralRecords.Where(r => !r.Naughty && r.Year == year).Select(r => r.ChildID);

            var naughtyGirlsID = naughtyChildrenID.Where(i => girlsToServeID.Contains(i)).ToList();
            var naughtyBoysID = naughtyChildrenID.Where(i => boysToServeID.Contains(i)).ToList();
            var naughtyOthersID = naughtyChildrenID.Where(i => othersToServeID.Contains(i)).ToList();

            var goodGirlsID = goodChildrenID.Where(i => girlsToServeID.Contains(i)).ToList();
            var goodBoysID = goodChildrenID.Where(i => boysToServeID.Contains(i)).ToList();
            var goodOthersID = goodChildrenID.Where(i => othersToServeID.Contains(i)).ToList();

            var goodGirlPresents = context.ChristmasPresents.Where(p => p.HandOutYear == year && !p.ForNaughtyChild && p.ForGender.ToLower() == "girl" && !p.ReceiverId.HasValue).ToList();
            var goodBoyPresents = context.ChristmasPresents.Where(p => p.HandOutYear == year && !p.ForNaughtyChild && p.ForGender.ToLower() == "boy" && !p.ReceiverId.HasValue).ToList();
            var goodNeutralPresents = context.ChristmasPresents.Where(p => p.HandOutYear == year && !p.ForNaughtyChild && p.ForGender.ToLower() == "u" && !p.ReceiverId.HasValue).ToList();

            var naughtyGirlPresents = context.ChristmasPresents.Where(p => p.HandOutYear == year && p.ForNaughtyChild && p.ForGender.ToLower() == "girl" && !p.ReceiverId.HasValue).ToList();
            var naughtyBoyPresents = context.ChristmasPresents.Where(p => p.HandOutYear == year && p.ForNaughtyChild && p.ForGender.ToLower() == "boy" && !p.ReceiverId.HasValue).ToList();
            var naughtyNeutralPresents = context.ChristmasPresents.Where(p => p.HandOutYear == year && p.ForNaughtyChild && p.ForGender.ToLower() == "u" && !p.ReceiverId.HasValue).ToList();

            int matches = MatchXyz(context, naughtyNeutralPresents, naughtyGirlPresents, naughtyBoyPresents, naughtyOthersID, naughtyGirlsID, naughtyBoysID);
            matches += MatchXyz(context, goodNeutralPresents, goodGirlPresents, goodBoyPresents, goodOthersID, goodGirlsID, goodBoysID);

            context.SaveChanges();
            return matches;
        }
    }
}
