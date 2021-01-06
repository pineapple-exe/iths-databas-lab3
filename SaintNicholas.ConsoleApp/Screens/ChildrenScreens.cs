using SaintNicholas.Data;
using SaintNicholas.Data.DataHandlers;
using System.Collections.Generic;

namespace SaintNicholas.ConsoleApp.Screens
{
    class ChildrenScreens
    {
        public static void ProvideChildrensTable(SaintNicholasDbContext context, int[] columnWidths, List<string> header)
        {
            List<string> rows = Utils.ChildStrings(ChildrenHandler.ChildrenTable(context), columnWidths);

            Utils.PrintTable(columnWidths, header, rows);
        }
    }
}
