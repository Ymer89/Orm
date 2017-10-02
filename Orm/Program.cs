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
            int[] xPlats = new int[50];
               xPlats[0] = 45;
            int[] yPlats = new int[50];
               yPlats[0] = 35;
            int bärXStorlek = 5;
            int bärYStorlek = 5;
            int uppÄtnaBär = 0;

            decimal spelHastighet = 100m;

            bool spelasDet = true;
            bool rörsVäggen = false;
            bool ärBäretuppätet = false;

            Random slump = new Random();

            Console.CursorVisible = false;

            MålaOrm(uppÄtnaBär, xPlats, yPlats, out xPlats, out yPlats);

            SättBärPositionPåSkärm(slump, out bärXStorlek, out bärYStorlek);
            MålaBär(bärXStorlek, bärYStorlek);

            ByggVägg();

            ConsoleKey utför = Console.ReadKey().Key;

            do
            {
                switch (utför)
                {

                    case ConsoleKey.LeftArrow:
                        Console.SetCursorPosition(xPlats[0], yPlats[0]);
                        Console.Write(" ");
                        xPlats[0]--;
                        break;

                    case ConsoleKey.UpArrow:
                        Console.SetCursorPosition(xPlats[0], yPlats[0]);
                        Console.Write(" ");
                        yPlats[0]--;
                        break;

                    case ConsoleKey.RightArrow:
                        Console.SetCursorPosition(xPlats[0], yPlats[0]);
                        Console.Write(" ");
                        xPlats[0]++;
                        break;

                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(xPlats[0], yPlats[0]);
                        Console.Write(" ");
                        yPlats[0]++;
                        break;

                }

                MålaOrm(uppÄtnaBär, xPlats, yPlats, out xPlats, out yPlats);
                rörsVäggen = RördeOrmenVäggen(xPlats[0], yPlats[0]);

                if (rörsVäggen)
                {
                    spelasDet = false;
                    Console.SetCursorPosition(28, 20);
                    Console.WriteLine("Spelet är över.");

                }

                ärBäretuppätet = BestämOmbäretärUppätet(xPlats[0], yPlats[0], bärXStorlek, bärYStorlek);
                if (ärBäretuppätet)
                {
                    SättBärPositionPåSkärm(slump, out bärXStorlek, out bärYStorlek);
                    MålaBär(bärXStorlek, bärYStorlek);
                    uppÄtnaBär++;
                    spelHastighet *= .925m;
                }
                

                if (Console.KeyAvailable) utför = Console.ReadKey().Key;
                System.Threading.Thread.Sleep(Convert.ToInt32(spelHastighet));

            } while (spelasDet);
           
        }

        private static void MålaOrm(int uppÄtnaBär, int[] xPlatsIn, int[] yPlatsIn, out int[] xPlatsUt, out int[] yPlatsUt)
        {

            Console.SetCursorPosition(xPlatsIn[0], yPlatsIn[0]);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine((char)213);

            for (int i = 1; i < uppÄtnaBär + 1; i++)
            {
                Console.SetCursorPosition(xPlatsIn[i], yPlatsIn[i]);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("o");
            }

            Console.SetCursorPosition(xPlatsIn[uppÄtnaBär + 1], yPlatsIn[uppÄtnaBär + 1]);
            Console.WriteLine(" ");

            for (int i = uppÄtnaBär + 1; i > 0; i--)
            {
                xPlatsIn[i] = xPlatsIn[i - 1];
                yPlatsIn[i] = yPlatsIn[i - 1];
            }

            xPlatsUt = yPlatsIn;
            yPlatsUt = yPlatsIn;
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
        private static void SättBärPositionPåSkärm(Random slump, out int bärXStorlek, out int bärYStorlek)
        {
            bärXStorlek = slump.Next(0 + 2, 70 - 2);
            bärYStorlek = slump.Next(0 + 2, 40 - 2);
        }
        private static void MålaBär(int bärXStorlek, int bärYStorlek)
        {
            Console.SetCursorPosition(bärXStorlek, bärYStorlek);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write((char)64);
        }
        private static bool BestämOmbäretärUppätet(int xPlats, int yPlats, int bärXStorlek, int bärYStorlek)
        {
            if (xPlats == bärXStorlek && yPlats == bärYStorlek) return true; return false;
        }
    }
}
