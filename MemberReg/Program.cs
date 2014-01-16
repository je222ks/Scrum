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
            memberList = LoadLog();

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
                        EditMember(memberList);
                        break;

                    case 3:
                        DeleteMember(memberList);
                        break;

                    case 4:
                        ViewMember(memberList);
                        break;

                    default:
                        throw new Exception();
                }
            } while (true);
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
            int memId = 0;
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
                //telNr = int.Parse(Console.ReadLine());
                if ((int.TryParse(Console.ReadLine(), out telNr) && telNr > 0))
                {
                    flip = false;
                }

                memId = NewMemberId(mem); // Ngt problem här då "index" blir 0 lr negativt...

            } while (flip);

            mem.Add(new Member(fName, lName, telNr, memId));

            SaveMembers(mem);

            return mem;
        }

        private static void DeleteMember(List<Member> members)
        {
            if (members == null)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n||     Det finns inga medlemmar att ta bort     ||\n");
                Console.ResetColor();
                ContinueOnKeyPressed();
                return;
            }

            ConsoleKey keyPress;

            do
            {
                Member memb = MemberChoice("Välj en medlem att ta bort", members);

                if (memb == null)
                {
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n Är du säker på att du vill ta bort '{0} {1}'?  [ J / N ]", memb.FirstName, memb.LastName);
                Console.ResetColor();

                keyPress = Console.ReadKey().Key;          

                if (keyPress == ConsoleKey.J)
                {
                    members.Remove(memb);
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\n||     Medlemmen har tagits bort     ||\n");
                    Console.ResetColor();
                    ContinueOnKeyPressed();
                }

                SaveMembers(members);
            } while (true);
        }

        private static void EditMember(List<Member> mem)
        {
            Member memb;
            int index;
            string fN;
            string lN;
            int pN;

            memb = MemberChoice("Välj en medlem  för redigering", mem);
            // Lägga till try/catch?


            Console.Clear();
            Console.WriteLine("Data i dagsläget: {0} {1}\t{2}", memb.FirstName, memb.LastName, memb.TelNumber);
            Console.WriteLine("Vad önskar Ni editera?");
            Console.WriteLine("0. Avsluta\n1. Förnamn\n2. Efternamn\n3. Telefonnummer");
            Console.Write("Data för editering: ");

            if (int.TryParse(Console.ReadLine(), out index) && index >= 0 && index <= 3)
            {
                switch (index)
                {
                    case 0:
                        break;

                    case 1:
                        Console.Write("Ange ett nytt förnamn: ");
                        fN = Console.ReadLine();
                        memb.FirstName = fN;
                        break;

                    case 2:
                        Console.Write("Ange ett nytt efternamn: ");
                        lN = Console.ReadLine();
                        memb.LastName = lN;
                        break;

                    case 3:
                        Console.Write("Ange nytt telefonnummer: ");
                        if ((int.TryParse(Console.ReadLine(), out pN) && pN > 0))
                        {
                            memb.TelNumber = pN;
                        }
                        break;

                    default:
                        throw new Exception();
                }
            }

            SaveMembers(mem);
        }

        private static int GetMenuChoice()
        {
            int index;

            // Inläsning av menyval, och kontroll så att det är ett giltigt val.
            Console.Clear();
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
            List<Member> mem = new List<Member>();
            LoadRegister log = new LoadRegister("memberlist.txt");

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
                Console.WriteLine(" {0}. \t{1}, {2}", i + 1, mems[i].FirstName, mems[i].LastName);
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

        private static int NewMemberId(List<Member> members)
        {
            if (members.Count < 1)
            {
                return 1;
            }
            else
            {
                int lastAdded = members.Count - 1;
                int lastAddedId = members[lastAdded].MemberId;
                return lastAddedId += 1;
            }
        }

        private static void SaveMembers(List<Member> members)
        {
            LoadRegister log = new LoadRegister("memberlist.txt");

            Console.Clear();

            if (members == null)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n||     Det finns inga medlemmar att spara     ||\n");
                Console.ResetColor();
                ContinueOnKeyPressed();
            }
            else if (members != null)
            {
                try
                {
                    log.Save(members);
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\n||     Medlemarna har sparats     ||\n");
                    Console.ResetColor();
                    ContinueOnKeyPressed();
                }
                catch
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n||     Ett fel inträffade då medlemmarna skulle sparas     ||\n");
                    Console.ResetColor();
                    ContinueOnKeyPressed();
                }
            }
        }

        private static void ViewMember(List<Member> members)
        {
            RenderMember renderMember = new RenderMember();

            renderMember.Render(members);

            ContinueOnKeyPressed();
        }
    }
}
