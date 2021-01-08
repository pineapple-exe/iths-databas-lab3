using SaintNicholas.ConsoleApp.Interactives;
using SaintNicholas.Data;
using System;
using System.Collections.Generic;
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
        ViewRecords,

        ClearConsole
    }
    class Menu
    {
        private static readonly string firstAlt = "Children    ";
        private static readonly string secondAlt = "Christmas presents        ";
        private static readonly string thirdAlt = "Children's behavior         ";

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

        private static readonly MenuAlternative[] mainMenu = new MenuAlternative[]
        {
            new MenuAlternative(firstAlt, MenuCommand.None),
            new MenuAlternative(secondAlt, MenuCommand.None),
            new MenuAlternative(thirdAlt, MenuCommand.None)
        };
        private static readonly MenuAlternative[] firstAltMenu = new MenuAlternative[]
        {
            new MenuAlternative(firstFirst, MenuCommand.AddChild),
            new MenuAlternative(firstSecond, MenuCommand.EditChild),
            new MenuAlternative(firstThird, MenuCommand.RemoveChild),
            new MenuAlternative(firstFourth, MenuCommand.ViewChildren)
        };
        private static readonly MenuAlternative[] secondAltMenu = new MenuAlternative[]
        {
            new MenuAlternative(secondFirst, MenuCommand.AddPresent),
            new MenuAlternative(secondSecond, MenuCommand.CheckDemands),
            new MenuAlternative(secondThird, MenuCommand.MatchPresents),
            new MenuAlternative(secondFourth, MenuCommand.ViewPresents)
        };
        private static readonly MenuAlternative[] thirdAltMenu = new MenuAlternative[]
        {
            new MenuAlternative(thirdFirst, MenuCommand.SetBehavior),
            new MenuAlternative(thirdSecond, MenuCommand.AddPresent),
            new MenuAlternative(thirdThird, MenuCommand.AddPresent),
            new MenuAlternative(thirdFourth, MenuCommand.AddPresent)
        };

        private static readonly MenuAlternative[][] subMenuParty = new MenuAlternative[][]
        {
            firstAltMenu,
            secondAltMenu, 
            thirdAltMenu
        };

        private Stack<MenuScreen> menuStack = new Stack<MenuScreen>();

        private Thread t;
        private static MenuCommand chosenCommand = MenuCommand.None;

        public Menu()
        {
            menuStack.Push(new MenuScreen(mainMenu));
            t = new Thread(Listen);
            t.Start();
        }

        private void FillMenu(MenuAlternative[] alternatives)
        {
            menuStack.Push(new MenuScreen(alternatives));
        }

        static void Cancel()
        {
            chosenCommand = MenuCommand.None;
            Console.Clear();
        }

        void ExecuteMenuChoice(MenuCommand menuCommand)
        {
            Console.Clear();

            string[] propertyValuesC = new string[6];
            int[] columnWidthsC = new int[] { 5, 20, 6, 25, 10, 15, 20 };
            var headerC = new List<string>() { "Id", "Name", "Gender", "StreetAddress", "PostalCode", "City", "Country" };

            int[] columnWidthsP = new int[] { 5, 23, 9, 15, 10, 11 };
            var headerP = new List<string>() { "Id", "Contents", "ForGender", "ForNaughtyChild", "ReceiverId", "HandOutYear" };

            int[] columnWidthsR = new int[] { 7, 5, 7 };
            var headerR = new List<string>() { "ChildID", "Year", "Naughty" };

            SaintNicholasDbContext context = new SaintNicholasDbContext();

            switch (menuCommand)
            {
                case MenuCommand.AddChild:

                    ChildrenFunctions.AddChild(context, propertyValuesC);
                    break;

                case MenuCommand.EditChild:

                    ChildrenFunctions.EditChild(context, propertyValuesC);
                    break;

                case MenuCommand.RemoveChild:

                    ChildrenFunctions.RemoveChild(context);
                    break;

                case MenuCommand.ViewChildren:

                    ChildrenFunctions.ViewChildren(context, columnWidthsC, headerC);
                    break;



                case MenuCommand.AddPresent:

                    ChristmasPresentsFunctions.AddPresent(context);
                    break;

                case MenuCommand.CheckDemands:

                    ChristmasPresentsFunctions.CheckDemands(context);
                    break;

                case MenuCommand.MatchPresents:

                    ChristmasPresentsFunctions.MatchPresents(context);
                    break;

                case MenuCommand.ViewPresents:

                    ChristmasPresentsFunctions.ViewPresents(context, columnWidthsP, headerP);
                    break;



                case MenuCommand.SetBehavior:

                    BehavioralRecordsFunctions.SetBehavior(context);
                    break;

                case MenuCommand.MoreGingerbread:

                    BehavioralRecordsFunctions.CheckGingerBreadDemand(context, true, columnWidthsC, headerC);
                    break;

                case MenuCommand.LessGingerbread:

                    BehavioralRecordsFunctions.CheckGingerBreadDemand(context, false, columnWidthsC, headerC);
                    break;

                case MenuCommand.ViewRecords:

                    BehavioralRecordsFunctions.ViewRecords(context, columnWidthsR, headerR);
                    break;


                case MenuCommand.ClearConsole:

                    Console.Clear();
                    break;
            }
            Cancel();
            t = new Thread(Listen);
            t.Start();
        }

        public void ActivateMenu()
        {
            menuStack.Peek().PrintMenu();

            if (chosenCommand != MenuCommand.None)
            {
                ExecuteMenuChoice(chosenCommand);
            }
        }

        void Listen()
        {
            while (true)
            {
                ConsoleKeyInfo keyPress = Console.ReadKey();

                if (keyPress.Key == ConsoleKey.DownArrow && menuStack.Peek().PendingMenuChoice < menuStack.Peek().Menu.Length - 1)
                {
                    menuStack.Peek().PendingMenuChoice++;
                }

                if (keyPress.Key == ConsoleKey.UpArrow && menuStack.Peek().PendingMenuChoice > 0)
                {
                    menuStack.Peek().PendingMenuChoice--;
                }

                if (keyPress.Key == ConsoleKey.Enter)
                {
                    if (menuStack.Peek().Menu == mainMenu)
                    {
                        FillMenu(subMenuParty[menuStack.Peek().PendingMenuChoice]);
                    }
                    else
                    {
                        chosenCommand = menuStack.Peek().Menu[menuStack.Peek().PendingMenuChoice].Command;
                        break;
                    }
                }

                if (keyPress.Key == ConsoleKey.Backspace)
                {
                    chosenCommand = MenuCommand.ClearConsole;
                    menuStack.Pop();
                }
            }
        }
    }
}
