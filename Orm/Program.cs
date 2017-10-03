using System;

namespace Orm
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "";
            while (s.ToLower() != "ja")
            {
                Console.Write("Välkommen till Snake! \nSpelet går ut på att du styr en orm som rör sig på skärmen och belönas med poäng för de bär du äter upp." +
                "\nDu förlorar spelet om du nuddar skärmens kanter.\nDu styr ormen med piltangenterna på datorns tangentbord:" +
                "\n\nUppåtpil för att röra sig uppåt\nNeråtpil för att röra sig neråt\nVänsterpil för att röra sig till vänster" +
                "\nHögerpil för att röra sig till höger\n\nÄr du redo, Svara ja för att börja spela och nej för att avsluta.\n");

                string svar = "";
                svar = Console.ReadLine();

                if (svar.ToLower() == "ja")
                {
                    Console.Clear();
                    Ormen();
                }
                else
                {
                    return;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(28, 21);
                Console.Write("\n      Vill du avsluta spelet eller återvända till startskärmen?\n      Svara ja för att avsluta och nej för att återvända till startskärmen.\n");
                s = Console.ReadLine();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
            }
        }
        private static void Ormen()
        {
            Console.WindowHeight = 40;
            Console.WindowWidth = 70;
            int xPlats = 45;
            int yPlats = 35;
            int uppÄtnaBär = 0;
            decimal spelHastighet = 100m;
            bool spelasDet = true;
            bool rörsVäggen = false;
            bool ärBäretUppätet = false;

            //Skapar variabeln slump genom Classen Random 
            Random slump = new Random();

            //Gör muspekaren osynlig.
            Console.CursorVisible = false;

            //Fäster ett bär på skärmen genom slump.
            SättBärPositionPåSkärmen(slump, out int bärXStorlek, out int bärYStorlek);
            MålaBär(bärXStorlek, bärYStorlek);

            //Bygger vägg genom metoden ByggVägg som definierar var väggen ska utryckas på skärmen genom en for-loop.
            ByggVägg();

            //Ta emot knapptryckningar för ormens förflyttning på skärmen genom att byta plats via switchen vilket pågår sålänge spelet körs genom en while-loop och en bool.
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

                //Fäster ormen på skärmen med position och karaktärsdrag.
                Console.SetCursorPosition(xPlats, yPlats);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("s");

                rörsVäggen = RördeOrmenVäggen(xPlats, yPlats);
                //Upptäcker om ormen rör väggen med en bool vilket avslutar spelomgången om ormen befinner sig i en ruta som består av min vägg.
                if (rörsVäggen)
                {
                    spelasDet = false;
                    Console.SetCursorPosition(28, 20);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Spelet är över.");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(28, 21);
                    Console.Write("Dina poäng är: " + uppÄtnaBär * 50 + "!");

                }

               //Upptäcker om ormen ätit bäret via en bool.
                ärBäretUppätet = BestämOmBäretÄrUppätet(xPlats, yPlats, bärXStorlek, bärYStorlek);

                //En bool placerar slumpmässigt ut ett nytt bär om det blivit uppätet via slumpmetod och en method som beskriver bäret.
                if (ärBäretUppätet)
                {
                    SättBärPositionPåSkärmen(slump, out bärXStorlek, out bärYStorlek);
                    MålaBär(bärXStorlek, bärYStorlek);
                    //Håller koll på antalet bär som blivit slukade.
                    uppÄtnaBär++;
                    //Ökar spelhastigheten(ormen) vid uppätet bär.
                    spelHastighet *= .925m;
                }

                if (Console.KeyAvailable) utför = Console.ReadKey().Key;
                //Saktar ner spelhastigheten för att göra det spelbart efter en knapptryckning.
                System.Threading.Thread.Sleep(Convert.ToInt32(spelHastighet));

            } while (spelasDet);
        }
        private static bool RördeOrmenVäggen(int xPosition, int yPosition)
        {
            if (xPosition == 1 || xPosition == 70 || yPosition == 1 || yPosition == 40) return true; return false;
        }
        private static void ByggVägg()
        {
            for (int i = 1; i < 41; i++)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(1, i);
                Console.Write("■");
                Console.SetCursorPosition(70, i);
                Console.Write("■");
            }
            for (int i = 1; i < 71; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(i, 1);
                Console.Write("■");
                Console.SetCursorPosition(i, 40);
                Console.Write("■");
            }
        }
        private static void SättBärPositionPåSkärmen(Random slump, out int bärXStorlek, out int bärYStorlek)
        {
            bärXStorlek = slump.Next(0 + 2, 70 - 2);
            bärYStorlek = slump.Next(0 + 2, 40 - 2);
        }
        private static void MålaBär(int bärXStorlek, int bärYStorlek)
        {
            Console.SetCursorPosition(bärXStorlek, bärYStorlek);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("¤");
        }
        private static bool BestämOmBäretÄrUppätet(int xPlats, int yPlats, int bärXStorlek, int bärYStorlek)
        {
            if (xPlats == bärXStorlek && yPlats == bärYStorlek) return true; return false;
        }
    }
}