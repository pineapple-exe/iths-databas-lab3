using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaintNicholas.Data.DataHandlers
{
    public class Demands
    {
        public int Diff { get; set; }

        public int FunNumDemand { get; set; }
        public int DullNumDemand { get; set; }
        public int BlankNumDemand { get; set; }

        public int GoodGirls { get; set; }
        public int GoodBoys { get; set; }

        public int NaughtyGirls { get; set; }
        public int NaughtyBoys { get; set; }

        public int UnevaluatedGirls { get; set; }
        public int UnevaluatedBoys { get; set; }

        public static Demands CheckDemands(SaintNicholasDbContext context)
        {
            Demands demands = new Demands();
            int year = DateTime.Now.Year;
            List<int> receiversID = context.ChristmasPresents.Where(p => p.Receiver.HasValue && p.HandOutYear == year).Select(p => p.Id).ToList();

            int childrenTotal = context.Children.Select(c => c.Id).Count();
            int alreadyGotPresents = receiversID.Count();
            int lonelyPresents = context.ChristmasPresents.Where(p => p.HandOutYear == year).Count(p => !p.Receiver.HasValue);

            int childrenNoPresents = childrenTotal - alreadyGotPresents;
            demands.Diff = childrenNoPresents - lonelyPresents;

            List<int> naughtyChildrenID = context.BehavioralRecords.Where(r => r.Year == year && r.Naughty).Select(r => r.ChildID).ToList();
            List<int> wellBehavedChildrenID = context.BehavioralRecords.Where(r => r.Year == year && !r.Naughty).Select(r => r.ChildID).ToList();
            List<int> unevaluatedChildrenID = context.Children
                .Where(c => !naughtyChildrenID.Contains(c.Id) && !wellBehavedChildrenID.Contains(c.Id))
                .Select(c => c.Id)
                .ToList();

            demands.FunNumDemand = wellBehavedChildrenID.Count(id => !receiversID.Contains(id));
            demands.DullNumDemand = naughtyChildrenID.Count(id => !receiversID.Contains(id));
            demands.BlankNumDemand = demands.Diff - (demands.FunNumDemand + demands.DullNumDemand);

            demands.GoodGirls = context.Children.Count(c => wellBehavedChildrenID.Contains(c.Id) && c.Gender.ToLower() == "girl");
            demands.GoodBoys = context.Children.Count(c => wellBehavedChildrenID.Contains(c.Id) && c.Gender.ToLower() == "boy");

            demands.NaughtyGirls = context.Children.Count(c => naughtyChildrenID.Contains(c.Id) && c.Gender.ToLower() == "girl");
            demands.NaughtyBoys = context.Children.Count(c => naughtyChildrenID.Contains(c.Id) && c.Gender.ToLower() == "boy");

            demands.UnevaluatedGirls = context.Children.Count(c => unevaluatedChildrenID.Contains(c.Id) && c.Gender.ToLower() == "girl");
            demands.UnevaluatedBoys = context.Children.Count(c => unevaluatedChildrenID.Contains(c.Id) && c.Gender.ToLower() == "boy");

            return demands;
        }
    }
}
