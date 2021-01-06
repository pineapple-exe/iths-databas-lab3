using SaintNicholas.Data;
using System;
using System.Linq;

namespace SaintNicholas.ConsoleApp.Interactives
{
    class Validators
    {
        internal static string GenderValidator(string input)
        {
            string[] genderAlternatives = new string[] { "girl", "boy", "u" };

            if (!genderAlternatives.Contains(input.ToLower()))
            {
                return "Please insert one of the following expressions: girl, boy, u.";
            }
            return null;
        }

        internal static string BoolValidator(string input)
        {
            string[] boolAlternatives = new string[] { "y", "n" };

            if (!boolAlternatives.Contains(input.ToLower()))
            {
                return "Assign boolean value with y or n.";
            }
            return null;
        }

        internal static string IntegerValidator(string input)
        {
            if (!int.TryParse(input, out int ignoreMe))
            {
                return "Must be an integer.";
            }
            return null;
        }

        internal static string ChildValidator(string input)
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

        internal static bool RepeatableReadline(string question, Func<string, string> validator, out string result)
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
    }
}
