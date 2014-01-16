using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberReg
{
    class RenderMember
    {
        public void Render(List<Member> memberList)
        {
            // Renderar alla medlemmar
            Console.Clear();
            RenderHead("Medlemmar");
            foreach (Member mem in memberList)
            {
                Console.WriteLine(String.Format("Namn: {0} {1}\tTel-Nummer: {2}\tID: {3}", mem.FirstName, mem.LastName, mem.TelNumber, mem.MemberId));
            }
        }

        public void Render(Member mem)
        {
            Console.WriteLine(String.Format("Namn: {0} {1}\tTel-Nummer: {2}\tID: {3}", mem.FirstName, mem.LastName, mem.TelNumber, mem.MemberId));
        }

        private void RenderHead(string header)
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("||   {0}   ||", header);
            Console.ResetColor();
        }
    }
}
