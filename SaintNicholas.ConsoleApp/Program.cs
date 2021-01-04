using SaintNicholas.Data;
using System;
using System.Linq;

namespace SaintNicholas.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            InitialQ();
            Console.Clear();

            Menu menu = new Menu();
            ChristmasTree.MakeItSparkle(menu.ActivateMenu);
        }

        static void InitialQ()
        {
            SaintNicholasDbContext context = new SaintNicholasDbContext();
            if (context.Children.Count() == 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Before proceeding to menu:");
                Console.WriteLine("Do you want to add test data? (y/n)");

                while (true)
                {
                    if (Console.ReadLine().ToLower() == "y")
                    {
                        DataSeeding.CreateTestData(context);
                        Console.WriteLine("Data was added.");
                        Console.WriteLine("Press Enter to continue.");
                        Console.ReadLine();
                        return;
                    }
                    if (Console.ReadLine().ToLower() == "n")
                    {
                        return;
                    }
                }
            }
        }
    }
}