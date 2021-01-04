using SaintNicholas.ConsoleApp.Screens;
using SaintNicholas.Data;
using SaintNicholas.Data.DataHandlers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SaintNicholas.ConsoleApp.Interactives
{
    class ChildrenFunctions : Validators
    {
        public static void AddChild(SaintNicholasDbContext context, string[] propertyValues)
        {
            Console.WriteLine("Enter empty string to cancel this process.");

            if (!RepeatableReadline("Name: ", s => null, out propertyValues[0])) return;
            if (!RepeatableReadline("Gender (girl/boy/u): ", GenderValidator, out propertyValues[1])) return;
            if (!RepeatableReadline("Street address: ", s => null, out propertyValues[2])) return;
            if (!RepeatableReadline("Postal code: ", s => null, out propertyValues[3])) return;
            if (!RepeatableReadline("City: ", s => null, out propertyValues[4])) return;
            if (!RepeatableReadline("And lastly... Country: ", s => null, out propertyValues[5])) return;

            Child newChild = ChildrenHandler.AddData(context, propertyValues);

            Console.WriteLine($"Child successfully added to database, with Id {newChild.Id}.");
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }

        public static void EditChild(SaintNicholasDbContext context, string[] propertyValues)
        {
            string initialQ = "Specify Id of the child you wish to edit.";
            if (!RepeatableReadline(initialQ, ChildValidator, out string id))
            {
                return;
            }

            Child childToEdit = context.Children.Where(c => c.Id == int.Parse(id)).First();
            var propertySetters = ChildrenHandler.PropertySetters();

            Console.WriteLine("Enter empty string to keep old data.");

            RepeatableReadline("Name: ", s => null, out propertyValues[0]);
            RepeatableReadline("Gender (girl/boy/u): ", GenderValidator, out propertyValues[1]);
            RepeatableReadline("Street address: ", s => null, out propertyValues[2]);
            RepeatableReadline("Postal code: ", s => null, out propertyValues[3]);
            RepeatableReadline("City: ", s => null, out propertyValues[4]);
            RepeatableReadline("And lastly... Country: ", s => null, out propertyValues[5]);

            bool done = false;
            for (int i = 0; i < propertyValues.Length; i++)
            {
                if (!string.IsNullOrEmpty(propertyValues[i]))
                {
                    propertySetters[i](childToEdit, propertyValues[i]);
                    done = false;
                }
            }

            if (!done)
            {
                ChildrenHandler.UpdateData(context, childToEdit);

                Console.WriteLine("Child successfully edited.");
                Console.WriteLine("Press Enter to return to menu.");
                Console.ReadLine();
            }
        }

        public static void RemoveChild(SaintNicholasDbContext context)
        {
            Console.WriteLine("Enter empty string to cancel this process.");

            string initialQ = "Specify Id of the child you wish to remove from database.";
            if (!RepeatableReadline(initialQ, ChildValidator, out string id))
            {
                return;
            }
            ChildrenHandler.RemoveData(context, int.Parse(id));
            Console.WriteLine("Child successfully removed from database.");
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }

        public static void ViewChildren(SaintNicholasDbContext context, int[] columnWidths, List<string> header)
        {
            ChildrenScreens.ProvideChildrensTable(context, columnWidths, header);

            Console.WriteLine();
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }
    }
}
