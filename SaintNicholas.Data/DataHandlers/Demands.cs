using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaintNicholas.Data.DataHandlers
{
    public enum Gender
    {
        Girl,
        Boy,
        Other
    }

    public class Demands
    {
        public int Diff { get; set; }

        public int FunNum { get; set; }
        public int DullNum { get; set; }
        public int BlankNum { get; set; }

        public Dictionary<Gender, int> GoodGendersNum { get; set; }
        public Dictionary<Gender, int> NaughtyGendersNum { get; set; }
        public Dictionary<Gender, int> UnevaluatedGendersNum { get; set; }

        public static Demands CheckDemands(SaintNicholasDbContext context)
        {
            Demands demands = new Demands();
            int year = DateTime.Now.Year;

            List<int> receiversID = context.ChristmasPresents.Where(p => p.ReceiverId.HasValue && p.HandOutYear == year).Select(p => (int)p.ReceiverId).ToList();

            int childrenTotal = context.Children.Select(c => c.Id).Count();
            int alreadyGotPresents = receiversID.Count();

            demands.Diff = childrenTotal - alreadyGotPresents;

            List<int> naughtyChildrenID = context.BehavioralRecords.Where(r => r.Year == year && r.Naughty && !receiversID.Contains(r.ChildID)).Select(r => r.ChildID).ToList();
            List<int> wellBehavedChildrenID = context.BehavioralRecords.Where(r => r.Year == year && !r.Naughty && !receiversID.Contains(r.ChildID)).Select(r => r.ChildID).ToList();
            List<int> unevaluatedChildrenID = context.Children
                .Where(c => !naughtyChildrenID.Contains(c.Id) && !wellBehavedChildrenID.Contains(c.Id) && !receiversID.Contains(c.Id))
                .Select(c => c.Id)
                .ToList();

            demands.FunNum = wellBehavedChildrenID.Count();
            demands.DullNum = naughtyChildrenID.Count();
            demands.BlankNum = demands.Diff - (demands.FunNum + demands.DullNum);

            demands.GoodGendersNum = new Dictionary<Gender, int>
            {
                [Gender.Girl] = context.Children.Count(c => wellBehavedChildrenID.Contains(c.Id) && c.Gender.ToLower() == "girl"),
                [Gender.Boy] = context.Children.Count(c => wellBehavedChildrenID.Contains(c.Id) && c.Gender.ToLower() == "boy"),
                [Gender.Other] = context.Children.Count(c => wellBehavedChildrenID.Contains(c.Id) && c.Gender.ToLower() == "u")
            };

            demands.NaughtyGendersNum = new Dictionary<Gender, int>
            {
                [Gender.Girl] = context.Children.Count(c => naughtyChildrenID.Contains(c.Id) && c.Gender.ToLower() == "girl"),
                [Gender.Boy] = context.Children.Count(c => naughtyChildrenID.Contains(c.Id) && c.Gender.ToLower() == "boy"),
                [Gender.Other] = context.Children.Count(c => naughtyChildrenID.Contains(c.Id) && c.Gender.ToLower() == "u")
            };

            demands.UnevaluatedGendersNum = new Dictionary<Gender, int>
            {
                [Gender.Girl] = context.Children.Count(c => unevaluatedChildrenID.Contains(c.Id) && c.Gender.ToLower() == "girl"),
                [Gender.Boy] = context.Children.Count(c => unevaluatedChildrenID.Contains(c.Id) && c.Gender.ToLower() == "boy"),
                [Gender.Other] = context.Children.Count(c => unevaluatedChildrenID.Contains(c.Id) && c.Gender.ToLower() == "u")
            };

            return demands;
        }
    }
}
