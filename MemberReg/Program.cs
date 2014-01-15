using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberReg
{
    class Program
    {
        static void Main(string[] args)
        {
            List<object> memberList = new List<object>();
            

            do
            {
                switch (GetMenuChoice())
                {
                    case 0:
                        return;

                    case 1:

                        break;

                    case 2:

                        break;

                    case 3:

                        break;

                    case 4:

                        break;

                    default:
                        throw new Exception();
                }
            } while (true) ;
        }


        private static void ContinueOnKeyPressed()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\nTryck ned tangent för att rensa fönstret.\n");
            Console.ResetColor();
            Console.ReadKey(true);
            Console.Clear();
        }

        public List<Member> CreateMember(List<Member> mem)
        {
            string fName;
            string lName;
            int telNr;
            int memId;
            bool flip = true;

            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("|        Lägg till ny medlem      |");
            Console.WriteLine("-----------------------------------");
            Console.ResetColor();

            do
            {
                Console.Write("Ange förnamn: ");
                fName = Console.ReadLine();

                Console.Write("Ange efternamn: ");
                lName = Console.ReadLine();

                Console.Write("Ange tel-nummer: ");
                // telNr = int.Parse(Console.ReadLine());
                if ((int.TryParse(Console.ReadLine(), out telNr) && telNr > 0))
                {
                    flip = false;   
                }

                memId = mem[mem.Count - 1].MemberId + 1;

            }while(flip);

            mem.Add(new Member(fName, lName, telNr, memId));

            return mem;
        }

        private static int GetMenuChoice()
        {
            int index;

            // Inläsning av menyval, och kontroll så att det är ett giltigt val.
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("|            Medlemsregister           |");
            Console.WriteLine("----------------------------------------");
            Console.ResetColor();

            Console.WriteLine("\n - Arkiv -----------------------------\n");
            Console.WriteLine(" 0. Avsluta \n");
            Console.WriteLine(" 1. Lägg till ny medlem \n");
            Console.WriteLine(" 2. Redigera medlem \n");
            Console.WriteLine(" 3. Ta bort medlem \n");
            Console.WriteLine(" 4. Visa alla medlemmar \n");
            Console.WriteLine("\n ========================================\n");
            Console.Write("Ange menyval [0-5]: ");

            do
            {
                try
                {
                    index = int.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\nVad du har matat in är inte ett giltigt menyval.\n");
                    Console.WriteLine("\n\tVar god försök igen\t\n");
                    Console.Write("Ange menyval [0-4]: ");
                    Console.ResetColor();
                }

            } while (true);

            return index;
        }

        private static List<Member> LoadLog()
        {
            LoadRegister log = new LoadRegister("memberlist.txt");
            List<Member> mem = new List<Member>();

            try
            {
                mem = log.Load();
            }
            catch
            {
                Console.WriteLine("Någonting gick fel då medlemslistan skulle hämtas");
            }

            return mem;
        }

    }
}
