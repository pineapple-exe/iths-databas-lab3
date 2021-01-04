using SaintNicholas.Data;
using SaintNicholas.Data.DataHandlers;
using System.Collections.Generic;

namespace SaintNicholas.ConsoleApp.Screens
{
    class ChildrenScreens : Utils
    {
        public static void ProvideChildrensTable(SaintNicholasDbContext context, int[] columnWidths, List<string> header)
        {
            List<string> rows = ChildStrings(ChildrenHandler.ChildrenTable(context), columnWidths);

            PrintTable(columnWidths, header, rows);
        }
    }
}
