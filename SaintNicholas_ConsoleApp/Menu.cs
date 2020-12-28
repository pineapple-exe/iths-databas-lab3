using SaintNicholas.Data;
using SaintNicholas.Data.DataHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SaintNicholas_ConsoleApp
{
    enum MenuCommand
    {
        None,

        AddChild,
        EditChild,
        RemoveChild,

        AddPresent,
        CheckDemands,
        MatchPresents,

        SetStatus,
        MoreGingerbread,
        LessGingerbread
    }
    class Menu
    {
        private static readonly string firstAlt = "Children   ";
        private static readonly string secondAlt = "Christmas presents        ";
        private static readonly string thirdAlt = "Children's behavior         ";

        private static readonly string firstFirst = "Add child";
        private static readonly string firstSecond = "Edit child                ";
        private static readonly string firstThird = "Remove child                ";

        private static readonly string secondFirst = "Add present";
        private static readonly string secondSecond = "Check demands             ";
        private static readonly string secondThird = "Match presents with children";

        private static readonly string thirdFirst = "Set status";
        private static readonly string thirdSecond = "Who needs more gingerbread";
        private static readonly string thirdThird = "Who needs less gingerbread";

        private static readonly string[] mainMenu = new string[] { firstAlt, secondAlt, thirdAlt };
        private static readonly string[] firstAltMenu = new string[] { firstFirst, firstSecond, firstThird };
        private static readonly string[] secondAltMenu = new string[] { secondFirst, secondSecond, secondThird };
        private static readonly string[] thirdAltMenu = new string[] { thirdFirst, thirdSecond, thirdThird };

        private static readonly string[][] subMenuParty = new string[][] { firstAltMenu, secondAltMenu, thirdAltMenu };
        private string[] currentMenu = mainMenu;

        private static readonly string space = "              ";
        private static readonly string spaceCursor = "           *  ";

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

            ChildrenHandler.AddData(context, propertyValues);

            Console.WriteLine("Child successfully added to database.");
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

        private static void SetStatus(SaintNicholasDbContext context)
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
                Console.WriteLine($"Presents yet to be handed out: {demands.Diff}");
                Console.WriteLine("Out of which...");
                Console.WriteLine();

                int[] behavioralDemands = new int[] { demands.FunNum, demands.DullNum, demands.BlankNum };
                Dictionary<Gender, int>[] genderedDemands = new Dictionary<Gender, int>[] { demands.GoodGendersNum, demands.NaughtyGendersNum, demands.UnevaluatedGendersNum };
                DemandsDetails(behavioralDemands, genderedDemands, "Fun presents", "Dull presents", "Unknown quality presents");

                Console.WriteLine("These numbers represent existent or non-existent presents;");
                Console.WriteLine("use [Match presents with children] to keep these numbers reflective of the production demand.");
            }
            else
            {
                Console.WriteLine("The amount of presents is sufficient for this year!");
            }
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

        private static string Ellipsis(string propertyValue, int maxLength)
        {
            if (propertyValue.Length > maxLength)
            {
                propertyValue = propertyValue.Substring(0, maxLength - 3) + "...";
            }
            return propertyValue;
        }

        private static List<string> ChildStrings(List<Child> theChildren)
        {
            var theStrings = new List<string>();

            foreach (Child c in theChildren)
            {
                string id = Ellipsis(c.Id.ToString(), 5);
                string name = Ellipsis(c.Name.ToString(), 15);
                string street = Ellipsis(c.StreetAddress.ToString(), 25);
                string zip = Ellipsis(c.PostalCode.ToString(), 10);
                string city = Ellipsis(c.City.ToString(), 15);
                string country = Ellipsis(c.Country.ToString(), 20);

                string childString = $"{id, -5} | {name, -15} | {street, -25} | {zip, -10} | {city, -15} | {country, -20}";

                theStrings.Add(childString);
            }
            return theStrings;
        }

        private static void CheckGingerBreadDemand(SaintNicholasDbContext context, bool naughty)
        {
            string adjective = naughty ? "naughty" : "good";
            string suggestion = naughty ? "we respond to a potential dire need of gingerbread abundance" :
                                           "their exposure to gingerbread is restricted";

            List<Child> whoNeedsIt = BehavioralRecordsHandler.GeneralGingerBread(context, naughty);
            List<string> childStrings = ChildStrings(whoNeedsIt);

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

            string columnLabels = $"{"Id", -5} | {"Name", -15} | {"StreetAddress", -25} | {"PostalCode", -10} | {"City", -15} | {"Country", -20}";
            string frame = "";
            for (int i = 0; i < columnLabels.Length; i++)
            {
                frame += "-";
            }

            Console.WriteLine(frame);
            Console.WriteLine(columnLabels);
            Console.WriteLine(frame);

            foreach (string s in childStrings)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine();
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }

        void ExecuteMenuChoice(MenuCommand menuCommand)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            string[] propertyValuesC = new string[6]; //The number of insertable Child properties.
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

                case MenuCommand.AddPresent:

                    AddPresent(context);
                    break;

                case MenuCommand.CheckDemands:

                    CheckDemands(context);
                    break;

                case MenuCommand.MatchPresents:

                    MatchPresents(context);
                    break;

                case MenuCommand.SetStatus:

                    SetStatus(context);
                    break;

                case MenuCommand.MoreGingerbread:

                    CheckGingerBreadDemand(context, true);
                    break;

                case MenuCommand.LessGingerbread:

                    CheckGingerBreadDemand(context, false);
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

                if (keyPress.Key == ConsoleKey.DownArrow && pendingMenuChoice < currentMenu.Length - 1)
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
                            }
                        }

                        if (currentMenu == thirdAltMenu)
                        {
                            switch (pendingMenuChoice)
                            {
                                case 0:
                                    menuCommand = MenuCommand.SetStatus;
                                    break;
                                case 1:
                                    menuCommand = MenuCommand.MoreGingerbread;
                                    break;
                                case 2:
                                    menuCommand = MenuCommand.LessGingerbread;
                                    break;
                            }
                        }

                        chosenMenu = menuCommand;
                        break;
                    }
                }

                if (keyPress.Key == ConsoleKey.Backspace)
                {
                    FillMenu(mainMenu);
                }
            }
        }
    }
}
