﻿using SaintNicholas.ConsoleApp.Interactives;
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

        void ExecuteMenuChoice(MenuCommand menuCommand)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

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