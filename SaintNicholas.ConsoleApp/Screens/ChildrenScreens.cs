using SaintNicholas.Data;
using SaintNicholas.Data.DataHandlers;
using SaintNicholas.Data.Entities;
using System.Collections.Generic;

namespace SaintNicholas.ConsoleApp.Screens
{
    class ChildrenScreens
    {
        static readonly List<string> headerC = new List<string>() { "Id", "Name", "Gender", "StreetAddress", "PostalCode", "City", "Country" };
        static readonly int[] columnWidthsC = new int[] { 5, 20, 6, 25, 10, 15, 20 };

        public static void ProvideAllChildren(SaintNicholasDbContext context)
        {
            ProvideChildrensTable(ChildrenHandler.ChildrenTable(context));
        }

        public static void ProvideChildrensTable(List<Child> theChildren)
        {
            List<string> childStrings = Utils.ChildStrings(theChildren, columnWidthsC);
            Utils.PrintTable(columnWidthsC, headerC, childStrings);
        }
    }
}
