using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orm
{
    class Program
    {
        static void Main(string[] args)
        {
            int xPlats = 45;
            int yPlats = 35;
            int bärXStorlek = 5;
            int bärYStorlek = 5;

            int spelHastighet = 100;

            bool spelasDet = true;
            bool rörsVäggen = false;

            Random slump = new Random();

            Console.SetCursorPosition(xPlats, yPlats);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine((char)213);

            ByggVägg();

            ConsoleKey utför = Console.ReadKey().Key;

            do
            {
                switch (utför)
                {

                    case ConsoleKey.LeftArrow:
                        Console.SetCursorPosition(xPlats, yPlats);
                        Console.Write(" ");
                        xPlats--;
                        break;

                    case ConsoleKey.UpArrow:
                        Console.SetCursorPosition(xPlats, yPlats);
                        Console.Write(" ");
                        yPlats--;
                        break;

                    case ConsoleKey.RightArrow:
                        Console.SetCursorPosition(xPlats, yPlats);
                        Console.Write(" ");
                        xPlats++;
                        break;

                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(xPlats, yPlats);
                        Console.Write(" ");
                        yPlats++;
                        break;

                }

                Console.SetCursorPosition(xPlats, yPlats);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine((char)213);

                rörsVäggen = RördeOrmenVäggen(xPlats, yPlats);

                if (rörsVäggen)
                {
                    spelasDet = false;
                    Console.SetCursorPosition(28, 20);
                    Console.WriteLine("Spelet är över.");

                }

                SättBärPositionPåSkärm(slump, out bärXStorlek, out bärYStorlek);
                MålaBär(bärXStorlek, bärYStorlek);

                if (Console.KeyAvailable) utför = Console.ReadKey().Key;
                System.Threading.Thread.Sleep(spelHastighet);

            } while (spelasDet);
           
        }

        private static void MålaBär(int bärXStorlek, int bärYStorlek)
        {
            Console.SetCursorPosition(bärXStorlek, bärYStorlek);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write((char)64);
        }

        private static void SättBärPositionPåSkärm(Random slump, out int bärXStorlek, out int bärYStorlek)
        {
            bärXStorlek = slump.Next(0+2, 70-2);
            bärYStorlek = slump.Next(0+2, 40-2);
        }

        private static bool RördeOrmenVäggen(int xPosition, int yPosition)
        {
            if (xPosition == 1 || xPosition == 70 || yPosition == 1 || yPosition == 40) return true; return false;
        }
        private static void ByggVägg()
        {
            for (int i = 1; i < 41; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(1, i);
                Console.Write("#");
                Console.SetCursorPosition(70, i);
                Console.Write("#");
            }
            for (int i = 1; i < 71; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(i, 1);
                Console.Write("#");
                Console.SetCursorPosition(i, 40);
                Console.Write("#");
            }
        }
    }
}
