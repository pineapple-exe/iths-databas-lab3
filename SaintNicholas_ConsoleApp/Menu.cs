using SaintNicholas.Data;
using SaintNicholas.Data.DataHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SaintNicholas.ConsoleApp
{
    enum MenuCommand
    {
        None,

        AddChild,
        EditChild,
        RemoveChild,
        ViewChildren,

        AddPresent,
        CheckDemands,
        MatchPresents,
        ViewPresents,

        SetBehavior,
        MoreGingerbread,
        LessGingerbread,
        ViewRecords
    }
    class Menu
    {
        private static readonly string firstAlt = "Children    ";
        private static readonly string secondAlt = "Christmas presents        ";
        private static readonly string thirdAlt = "Children's behavior         ";
        private static readonly string invisibilityCloak = "               ";

        private static readonly string firstFirst = "Add child   ";
        private static readonly string firstSecond = "Edit child                ";
        private static readonly string firstThird = "Remove child                ";
        private static readonly string firstFourth = "View children                ";

        private static readonly string secondFirst = "Add present ";
        private static readonly string secondSecond = "Check demands             ";
        private static readonly string secondThird = "Match presents with children";
        private static readonly string secondFourth = "View presents                ";

        private static readonly string thirdFirst = "Set behavior";
        private static readonly string thirdSecond = "Who needs more gingerbread";
        private static readonly string thirdThird = "Who needs less gingerbread";
        private static readonly string thirdFourth = "View records                ";

        private static readonly string[] mainMenu = new string[] { firstAlt, secondAlt, thirdAlt, invisibilityCloak };
        private static readonly string[] firstAltMenu = new string[] { firstFirst, firstSecond, firstThird, firstFourth };
        private static readonly string[] secondAltMenu = new string[] { secondFirst, secondSecond, secondThird, secondFourth };
        private static readonly string[] thirdAltMenu = new string[] { thirdFirst, thirdSecond, thirdThird, thirdFourth };

        private static readonly string[][] subMenuParty = new string[][] { firstAltMenu, secondAltMenu, thirdAltMenu };
        private string[] currentMenu = mainMenu;

        private static readonly string space = "              ";
        private static readonly string spaceCursor = "            * ";

        private int pendingMenuChoice = 0;
        private Thread t;
        private static MenuCommand chosenMenu = MenuCommand.None;

        public Menu()
        {
            t = new Thread(Listen);
            t.Start();
        }

        private void FillMenu(string[] alternatives)
        {
            currentMenu = alternatives;
        }

        static void Cancel()
        {
            chosenMenu = MenuCommand.None;
            Console.Clear();
        }

        private static string GenderValidator(string input)
        {
            string[] genderAlternatives = new string[] { "girl", "boy", "u" };

            if (!genderAlternatives.Contains(input.ToLower()))
            {
                return "Please insert one of the following expressions: girl, boy, u.";
            }
            return null;
        }

        private static string BoolValidator(string input)
        {
            string[] boolAlternatives = new string[] { "y", "n" };

            if (!boolAlternatives.Contains(input.ToLower()))
            {
                return "Assign boolean value with y or n.";
            }
            return null;
        }

        private static string IntegerValidator(string input)
        {
            if (!int.TryParse(input, out int ignoreMe))
            {
                return "Must be an integer.";
            }
            return null;
        }

        private static string ChildValidator(string input)
        {
            SaintNicholasDbContext context = new SaintNicholasDbContext();

            string intMessage = IntegerValidator(input);
            if (IntegerValidator(input) != null)
            {
                return intMessage;
            }
            if (!context.Children.Any(c => c.Id == int.Parse(input)))
            {
                return "Child with given Id does not exist in database.";
            }
            return null;
        }

        private static bool RepeatableReadline(string question, Func<string, string> validator, out string result)
        {
            while (true)
            {
                Console.WriteLine(question);
                string input = Console.ReadLine();

                if (input == "")
                {
                    result = null;
                    return false;
                }

                string validationMessage = validator(input);

                if (string.IsNullOrEmpty(validationMessage))
                {
                    result = input;
                    return true;
                }
                else
                {
                    Console.WriteLine(validationMessage);
                }
            }
        }

        private static void AddChild(SaintNicholasDbContext context, string[] propertyValues)
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

        private static void EditChild(SaintNicholasDbContext context, string[] propertyValues)
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

        private static void RemoveChild(SaintNicholasDbContext context)
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

        private static string Ellipsis(string propertyValue, int maxLength)
        {
            if (propertyValue.Length > maxLength)
            {
                propertyValue = propertyValue.Substring(0, maxLength - 3) + "...";
            }
            return propertyValue;
        }

        private static string BuildRow(List<string> objectValues, int[] columnWidths)
        {
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < objectValues.Count; i++)
            {
                sBuilder.Append(string.Format("{0," + columnWidths[i] + "}", objectValues[i]) + " | ");
            }
            return sBuilder.ToString();
        }

        private static void PrintTable(int[] columnWidths, List<string> header, List<string> rows)
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

        private static List<string> ChildStrings(List<Child> theChildren, int[] columnWidths)
        {
            var theStrings = new List<string>();

            foreach (Child c in theChildren)
            {
                var childValues = new List<string>
                {
                    Ellipsis(c.Id.ToString(), columnWidths[0]),
                    Ellipsis(c.Name.ToString(), columnWidths[1]),
                    Ellipsis(c.StreetAddress.ToString(), columnWidths[2]),
                    Ellipsis(c.PostalCode.ToString(), columnWidths[3]),
                    Ellipsis(c.City.ToString(), columnWidths[4]),
                    Ellipsis(c.Country.ToString(), columnWidths[5])
                };
                theStrings.Add(BuildRow(childValues, columnWidths));
            }
            return theStrings;
        }

        private static void ViewChildren(SaintNicholasDbContext context, int[] columnWidths, List<string> header)
        {
            List<string> rows = ChildStrings(ChildrenHandler.ChildrenTable(context), columnWidths);

            PrintTable(columnWidths, header, rows);

            Console.WriteLine();
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }

        private static void AddPresent(SaintNicholasDbContext context)
        {
            Console.WriteLine("Enter empty string to cancel.");

            string[] propertyValues = new string[3];
            string initialQ = "Quantity: ";

            if (!RepeatableReadline(initialQ, IntegerValidator, out string quantity) || quantity == "0")
            {
                return;
            }
            if (!RepeatableReadline("Contents: ", s => null, out propertyValues[0]))
            {
                return;
            }
            if (!RepeatableReadline("For gender (girl/boy/u): ", GenderValidator, out propertyValues[1]))
            {
                return;
            }
            if (!RepeatableReadline("And lastly... For naughty children (y/n): ", BoolValidator, out propertyValues[2]))
            {
                return;
            }
            ChristmasPresentsHandler.AddData(int.Parse(quantity), propertyValues, context);
            Console.WriteLine("Presents successfully added to database.");
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }

        private static void DemandsDetails(int[] behavioralDemands, Dictionary<Gender, int>[] genderedDemands, params string[] typeNotes)
        {
            for (int i = 0; i < behavioralDemands.Length; i++)
            {
                Console.WriteLine($" {typeNotes[i]}: {behavioralDemands[i]} =");
                Console.WriteLine($"        Girls: {genderedDemands[i][Gender.Girl]}");
                Console.WriteLine($"        Boys: {genderedDemands[i][Gender.Boy]}");
                Console.WriteLine($"        Others: {genderedDemands[i][Gender.Other]}");
                Console.WriteLine();
            }
        }

        private static void CheckDemands(SaintNicholasDbContext context)
        {
            Demands demands = Demands.CheckDemands(context);

            if (demands.Diff > 0)
            {
                Console.WriteLine($"Christmas present needs yet to be matched: {demands.Diff}");
                Console.WriteLine("(Behavior is required to determine quality of presents.)");
                Console.WriteLine();

                int[] behavioralDemands = new int[] { demands.FunNum, demands.DullNum, demands.BlankNum };
                Dictionary<Gender, int>[] genderedDemands = new Dictionary<Gender, int>[] { demands.GoodGendersNum, demands.NaughtyGendersNum, demands.UnevaluatedGendersNum };
                DemandsDetails(behavioralDemands, genderedDemands, "Fun presents", "Dull presents", "Children with unknown behavior");

                Console.WriteLine("Use [Match presents with children] to keep these numbers reflective of the production demand.");
            }
            else
            {
                Console.WriteLine("The amount of presents is sufficient for this year!");
            }
            Console.WriteLine();
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }

        private static void MatchPresents(SaintNicholasDbContext context)
        {
            int matches = ChristmasPresentsHandler.Match(context);
            context.SaveChanges();

            if (matches > 0)
            {
                string singularOrPlural = matches == 1 ? "present has been paired with an adequate receiver." :
                                                                                  "presents have been paired with adequate receivers.";
                Console.WriteLine($"{matches} {singularOrPlural}");
            }
            else
            {
                Console.WriteLine("No matches could be made.");
            }
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }

        private static List<string> ChristmasPresentStrings(List<ChristmasPresent> thePresents, int[] columnWidths)
        {
            var theStrings = new List<string>();
            foreach (ChristmasPresent p in thePresents)
            {
                var christmasPresentValues = new List<string>
                {
                    Ellipsis(p.Id.ToString(), columnWidths[0]),
                    Ellipsis(p.Contents.ToString(), columnWidths[1]),
                    Ellipsis(p.ForGender.ToString(), columnWidths[2]),
                    Ellipsis(p.ForNaughtyChild.ToString(), columnWidths[3]),
                    Ellipsis(p.ReceiverId.ToString(), columnWidths[4]),
                    Ellipsis(p.HandOutYear.ToString(), columnWidths[5])
                };
                theStrings.Add(BuildRow(christmasPresentValues, columnWidths));
            }
            return theStrings;
        }

        private static void ViewPresents(SaintNicholasDbContext context, int[] columnWidths, List<string> header)
        {
            List<string> rows = ChristmasPresentStrings(ChristmasPresentsHandler.PresentsTable(context), columnWidths);
            
            PrintTable(columnWidths, header, rows);

            Console.WriteLine();
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }

        private static void SetBehavior(SaintNicholasDbContext context)
        {
            Console.WriteLine("Enter empty string to cancel.");
            if (!RepeatableReadline("Specify Id of child.", ChildValidator, out string id))
            {
                return;
            }
            if (!RepeatableReadline("Has been naughty this year (y/n): ", BoolValidator, out string propertyValue))
            {
                return;
            }
            else
            {
                BehavioralRecordsHandler.AddData(context, id, propertyValue);
                Console.WriteLine("Record successfully added.");
                Console.WriteLine("Press Enter to return to menu.");
                Console.ReadLine();
            }
        }

        private static void CheckGingerBreadDemand(SaintNicholasDbContext context, bool naughty, int[] columnWidths, List<string> header)
        {
            string adjective = naughty ? "naughty" : "good";
            string suggestion = naughty ? "we respond to a potential dire need of gingerbread abundance" :
                                           "their exposure to gingerbread is restricted";

            List<Child> whoNeedsIt = BehavioralRecordsHandler.GeneralGingerBread(context, naughty);
            List<string> childStrings = ChildStrings(whoNeedsIt, columnWidths);

            if (childStrings == null)
            {
                Console.WriteLine("No such records.");
                Console.WriteLine("Press Enter to return to menu.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine($"Following children have been {adjective} the past three years.");
            Console.WriteLine($"It is therefore suggested that {suggestion}.");
            Console.WriteLine();

            PrintTable(columnWidths, header, childStrings);

            Console.WriteLine();
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }

        private static List<string> RecordStrings(List<BehavioralRecord> theRecords, int[] columnWidths)
        {
            var theStrings = new List<string>();

            foreach (BehavioralRecord r in theRecords)
            {
                var recordValues = new List<string>
                {
                    Ellipsis(r.ChildID.ToString(), columnWidths[0]),
                    Ellipsis(r.Year.ToString(), columnWidths[1]),
                    Ellipsis(r.Naughty.ToString(), columnWidths[2]),
                };
                theStrings.Add(BuildRow(recordValues, columnWidths));
            }
            return theStrings;
        }

        private static void ViewRecords(SaintNicholasDbContext context, int[] columnWidths, List<string> header)
        {
            List<string> rows = RecordStrings(BehavioralRecordsHandler.RecordsTable(context), columnWidths);

            PrintTable(columnWidths, header, rows);

            Console.WriteLine();
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }

        void ExecuteMenuChoice(MenuCommand menuCommand)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            string[] propertyValuesC = new string[6];
            int[] columnWidthsC = new int[] { 5, 20, 25, 10, 15, 20 };
            var headerC = new List<string>() { "Id", "Name", "StreetAddress", "PostalCode", "City", "Country" };

            int[] columnWidthsP = new int[] { 5, 23, 9, 15, 10, 11 };
            var headerP = new List<string>() { "Id", "Contents", "ForGender", "ForNaughtyChild", "ReceiverId", "HandOutYear" };

            int[] columnWidthsR = new int[] { 7, 5, 7 };
            var headerR = new List<string>() { "ChildID", "Year", "Naughty" };

            SaintNicholasDbContext context = new SaintNicholasDbContext();

            switch (menuCommand)
            {
                case MenuCommand.AddChild:

                    AddChild(context, propertyValuesC);
                    break;

                case MenuCommand.EditChild:

                    EditChild(context, propertyValuesC);
                    break;

                case MenuCommand.RemoveChild:

                    RemoveChild(context);
                    break;

                case MenuCommand.ViewChildren:

                    ViewChildren(context, columnWidthsC, headerC);
                    break;



                case MenuCommand.AddPresent:

                    AddPresent(context);
                    break;

                case MenuCommand.CheckDemands:

                    CheckDemands(context);
                    break;

                case MenuCommand.MatchPresents:

                    MatchPresents(context);
                    break;

                case MenuCommand.ViewPresents:

                    ViewPresents(context, columnWidthsP, headerP);
                    break;



                case MenuCommand.SetBehavior:

                    SetBehavior(context);
                    break;

                case MenuCommand.MoreGingerbread:

                    CheckGingerBreadDemand(context, true, columnWidthsC, headerC);
                    break;

                case MenuCommand.LessGingerbread:

                    CheckGingerBreadDemand(context, false, columnWidthsC, headerC);
                    break;

                case MenuCommand.ViewRecords:

                    ViewRecords(context, columnWidthsR, headerR);
                    break;
            }
            Cancel();
            t = new Thread(Listen);
            t.Start();
        }

        public void ActivateMenu()
        {
            Console.WriteLine();

            for (int i = 0; i < currentMenu.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;

                if (pendingMenuChoice == i)
                {
                    Console.WriteLine(spaceCursor + currentMenu[i]);
                }
                else
                {
                    Console.WriteLine(space + currentMenu[i]);
                }
            }

            if (chosenMenu != MenuCommand.None)
            {
                ExecuteMenuChoice(chosenMenu);
            }
        }

        void Listen()
        {
            while (true)
            {
                ConsoleKeyInfo keyPress = Console.ReadKey();

                if (currentMenu == mainMenu && keyPress.Key == ConsoleKey.DownArrow)
                {
                    if (pendingMenuChoice != Array.IndexOf(mainMenu, invisibilityCloak) - 1)
                    {
                        pendingMenuChoice++;
                    }
                }

                else if (keyPress.Key == ConsoleKey.DownArrow && pendingMenuChoice < currentMenu.Length - 1)
                {
                    pendingMenuChoice++;
                }

                if (keyPress.Key == ConsoleKey.UpArrow && pendingMenuChoice > 0)
                {
                    pendingMenuChoice--;
                }

                if (keyPress.Key == ConsoleKey.Enter)
                {
                    if (currentMenu == mainMenu)
                    {
                        FillMenu(subMenuParty[pendingMenuChoice]);
                    }
                    else
                    {
                        MenuCommand menuCommand = 0;

                        if (currentMenu == firstAltMenu)
                        {
                            switch (pendingMenuChoice)
                            {
                                case 0:
                                    menuCommand = MenuCommand.AddChild;
                                    break;
                                case 1:
                                    menuCommand = MenuCommand.EditChild;
                                    break;
                                case 2:
                                    menuCommand = MenuCommand.RemoveChild;
                                    break;
                                case 3:
                                    menuCommand = MenuCommand.ViewChildren;
                                    break;
                            }
                        }

                        if (currentMenu == secondAltMenu)
                        {
                            switch (pendingMenuChoice)
                            {
                                case 0:
                                    menuCommand = MenuCommand.AddPresent;
                                    break;
                                case 1:
                                    menuCommand = MenuCommand.CheckDemands;
                                    break;
                                case 2:
                                    menuCommand = MenuCommand.MatchPresents;
                                    break;
                                case 3:
                                    menuCommand = MenuCommand.ViewPresents;
                                    break;
                            }
                        }

                        if (currentMenu == thirdAltMenu)
                        {
                            switch (pendingMenuChoice)
                            {
                                case 0:
                                    menuCommand = MenuCommand.SetBehavior;
                                    break;
                                case 1:
                                    menuCommand = MenuCommand.MoreGingerbread;
                                    break;
                                case 2:
                                    menuCommand = MenuCommand.LessGingerbread;
                                    break;
                                case 3:
                                    menuCommand = MenuCommand.ViewRecords;
                                    break;
                            }
                        }

                        chosenMenu = menuCommand;
                        break;
                    }
                }

                if (keyPress.Key == ConsoleKey.Backspace)
                {
                    if (pendingMenuChoice == Array.IndexOf(mainMenu, invisibilityCloak))
                    {
                        pendingMenuChoice--;
                    }
                    FillMenu(mainMenu);
                }
            }
        }
    }
}
