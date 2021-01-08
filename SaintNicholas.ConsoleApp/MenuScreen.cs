using System;
using System.Collections.Generic;
using System.Text;

namespace SaintNicholas.ConsoleApp
{
    class MenuScreen
    {
        public MenuAlternative[] Menu { get; }
        public int PendingMenuChoice { get; set; }
        public static readonly string space = "              ";
        public static readonly string spaceCursor = "            * ";

        public MenuScreen(MenuAlternative[] alternatives)
        {
            Menu = alternatives;
            PendingMenuChoice = 0;
        }

        public void PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            for (int i = 0; i < Menu.Length; i++)
            {
                if (PendingMenuChoice == i)
                {
                    Console.WriteLine(spaceCursor + Menu[i].Label);
                }
                else
                {
                    Console.WriteLine(space + Menu[i].Label);
                }
            }
        }
    }
}
