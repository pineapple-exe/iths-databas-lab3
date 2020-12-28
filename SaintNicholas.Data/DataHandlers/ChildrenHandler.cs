using System;
using System.Linq;

namespace SaintNicholas.Data.DataHandlers
{
    public static class ChildrenHandler
    {
        public static Action<Child, string>[] PropertySetters()
        {
            return new Action<Child, string>[]
            {
                (c, s) => c.Name = s,
                (c, s) => c.Gender = s,
                (c, s) => c.StreetAddress = s,
                (c, s) => c.PostalCode = s,
                (c, s) => c.City = s,
                (c, s) => c.Country = s
            };
        }

        public static void AddData(SaintNicholasDbContext context, string[] values)
        {
            Child newChild = new Child();
            var propertySetters = PropertySetters();

            for (int j = 0; j < values.Length; j++)
            {
                propertySetters[j](newChild, values[j]);
            }
            context.Children.Add(newChild);
            context.SaveChanges();
        }

        public static void RemoveData(SaintNicholasDbContext context, int id)
        {
            if (context.BehavioralRecords.Select(b => b.ChildID).Contains(id))
            {
                context.BehavioralRecords.RemoveRange(context.BehavioralRecords.Where(b => b.Child.Id == id));
            }
            if (context.ChristmasPresents.Select(p => p.Receiver.Id).Contains(id))
            {
                context.ChristmasPresents.RemoveRange(context.ChristmasPresents.Where(p => p.Receiver.Id == id));
            }
            context.Children.Remove(context.Children.Where(x => x.Id == id).First());
            context.SaveChanges();
        }

        public static void UpdateData(SaintNicholasDbContext context, Child child)
        {
            context.Children.Update(child);
            context.SaveChanges();
        }
    }
}
