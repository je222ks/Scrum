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
            List<Member> memberList = new List<Member>();
            

            do
            {
                switch (GetMenuChoice())
                {
                    case 0:
                        return;

                    case 1:
                        CreateMember(memberList);
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

        public static List<Member> CreateMember(List<Member> mem)
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

        private static void EditMember(List<Member> mem)
        { 
            
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
            Console.Write("Ange menyval [0-4]: ");

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

        private static Member MemberChoice(string header, List<Member> mems)
        {
            int index;

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n || {0} || \n", header);
            Console.ResetColor();
            Console.WriteLine(" 0. Avbryt");
            Console.WriteLine("------------------------------");

            for (int i = 0; i < mems.Count; i++)
            {
                Console.WriteLine(" {0}. \t{1}, {2}", i, mems[i].FirstName, mems[i].LastName);
            }

            Console.WriteLine("------------------------------");
            Console.Write(" Ange menyval [0-{0}]: ", mems.Count);

            do
            {
                try
                {
                    index = int.Parse(Console.ReadLine());

                    if (index == 0)
                    {
                        return null;
                    }
                    else if (index < 0 || index > mems.Count)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    break;
                }
                catch
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\nVad du har matat in är inte ett giltigt menyval.\n");
                    Console.WriteLine("\n\tVar god försök igen\t\n");
                    Console.Write("Ange menyval [0-{0}]: ", mems.Count);
                    Console.ResetColor();
                }

            } while (true);

            return mems[index - 1]; 
        }


    }
}
