using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SaintNicholas.ConsoleApp
{
    static class ChristmasTree
    {
		static readonly ConsoleColor pineNeedleColor = ConsoleColor.DarkGreen;
		static readonly ConsoleColor starColor = ConsoleColor.Yellow;
		static readonly ConsoleColor treeTrunkColor = ConsoleColor.DarkYellow;

		static string[] undressed;
		static List<string> dressed;

		static char[] decorations;
		static ConsoleColor[] decorColorAlternatives;

		static readonly char decorationSpot = '#';

		private static void CreateTree()
        {
			undressed = new string[]
			{
				"                      .^.",
				"                     < * >",
				"                     ' V '",
				"                      /#\\",
				"                     /###\\",
				"                    /#####\\",
				"                   /#######\\",
				"                  /#########\\",
				"                 /###########\\",
				"                /#############\\",
				"               /###############\\",
				"              /#################\\",
				"             (###################)",
				"              `-._____________.-´",
				"                     [   ]"
			};
		}

		private static void CollectDecorations(params char[] decor)
        {
			decorations = decor;
        }

		private static void CollectDecorColors(params ConsoleColor[] colorTheme)
        {
			decorColorAlternatives = colorTheme;
        }

		static void PrettyThings()
        {
			CollectDecorations(' ', ' ', ' ', ' ', ' ', ' ', '*', 'i', 'o', '@', '+', '.', '.');
			CollectDecorColors(ConsoleColor.DarkYellow, ConsoleColor.Blue, ConsoleColor.DarkMagenta, ConsoleColor.Yellow, ConsoleColor.White);
		}

		static void DressUndress(Random random)
        {
			for (int i = 0; i < undressed.Length; i++)
			{
				dressed.Add("");
			}

			for (int i = 0; i < undressed.Length; i++)
			{
				foreach (char c in undressed[i])
				{
					if (c == decorationSpot)
					{
						int decorIndex = random.Next(0, decorations.Length);
						dressed[i] += decorations[decorIndex];
					}
					else
					{
						dressed[i] += c;
					}
				}
			}
		}

		public static void MakeItSparkle(Action activateMenu)
		{
			CreateTree();
			dressed = undressed.ToList();

			PrettyThings();
			ConsoleColor decorColor;
			Random random = new Random();

			while (true)
			{
				Console.CursorVisible = false;
				Console.SetCursorPosition(0, 0);
				dressed.Clear();

				DressUndress(random);

				Console.WriteLine();

				for (int i = 0; i < dressed.Count; i++)
				{
					if (i < 3)
					{
						Console.ForegroundColor = starColor;
						Console.WriteLine(dressed[i]);
					}
					else if (i > 2 && i < 14)
					{
						for (int j = 0; j < dressed[i].Length; j++)
						{
							if (undressed[i][j] == decorationSpot)
							{
								decorColor = decorColorAlternatives[random.Next(0, decorColorAlternatives.Length)];
								Console.ForegroundColor = decorColor;
							}
							else
							{
								Console.ForegroundColor = pineNeedleColor;
							}
							Console.Write(dressed[i][j]);
						}
						Console.WriteLine();
					}
					else
					{
						Console.ForegroundColor = treeTrunkColor;
						Console.WriteLine(dressed[i]);
					}
				}
				Thread.Sleep(500);
				activateMenu();
			}
		}
	}
}
