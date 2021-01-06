using SaintNicholas.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaintNicholas.ConsoleApp.Screens
{
    class Utils
    {
        internal static string Ellipsis(string propertyValue, int maxLength)
        {
            if (propertyValue.Length > maxLength)
            {
                propertyValue = propertyValue.Substring(0, maxLength - 3) + "...";
            }
            return propertyValue;
        }

        internal static string BuildRow(List<string> objectValues, int[] columnWidths)
        {
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < objectValues.Count; i++)
            {
                sBuilder.Append(string.Format("{0," + columnWidths[i] + "}", objectValues[i]) + " | ");
            }
            return sBuilder.ToString();
        }

        internal static List<string> ChildStrings(List<Child> theChildren, int[] columnWidths)
        {
            var theStrings = new List<string>();

            foreach (Child c in theChildren)
            {
                var childValues = new List<string>
                {
                    Ellipsis(c.Id.ToString(), columnWidths[0]),
                    Ellipsis(c.Name.ToString(), columnWidths[1]),
                    Ellipsis(c.Gender.ToString(), columnWidths[2]),
                    Ellipsis(c.StreetAddress.ToString(), columnWidths[3]),
                    Ellipsis(c.PostalCode.ToString(), columnWidths[4]),
                    Ellipsis(c.City.ToString(), columnWidths[5]),
                    Ellipsis(c.Country.ToString(), columnWidths[6])
                };
                theStrings.Add(BuildRow(childValues, columnWidths));
            }
            return theStrings;
        }

        internal static void PrintTable(int[] columnWidths, List<string> header, List<string> rows)
        {
            string columnLabels = BuildRow(header, columnWidths);
            string frame = "";

            for (int i = 0; i < columnLabels.Length - 1; i++)
            {
                frame += "-";
            }

            Console.WriteLine(frame);
            Console.WriteLine(columnLabels);
            Console.WriteLine(frame);

            foreach (string s in rows)
            {
                Console.WriteLine(s);
            }
        }
    }
}
