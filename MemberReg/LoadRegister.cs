using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberReg
{
    enum ReadStatus { Initiate, FirstName, LastName, Phone, ID };

    class LoadRegister
    {
        private string _path;

        public string Path
        {
            get { return _path; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException();
                }
                _path = value;
            }
        }

        public LoadRegister(string path)
        {
            Path = path; 
        }

        public List<Member> Load()
        {
            int index = -1;
            string line;
            List<Member> member = new List<Member>();

            ReadStatus readState = ReadStatus.Initiate;

            using (StreamReader memReader = new StreamReader(Path))
            {
                while ((line = memReader.ReadLine()) != null)
                {
                    if (line == null) 
                    {
                        continue;
                    }

                    if (line == "[Förnamn]")
                    {
                        readState = ReadStatus.FirstName;
                    }
                    else if (line == "[Efternamn]")
                    {
                        readState = ReadStatus.LastName;
                    }
                    else if (line == "[Telefonnummer]")
                    {
                        readState = ReadStatus.Phone;
                    }
                    else if (line == "[ID]")
                    {
                        readState = ReadStatus.ID;
                    }
                    else 
                    {
                        switch (readState)
                        { 
                            case ReadStatus.FirstName:
                                index++;
                                member.Add(new Member(line));
                                break;

                            case ReadStatus.LastName:
                                member[index].AddLastName(line);
                                break;

                            case ReadStatus.Phone:
                                member[index].AddPhone(int.Parse(line));
                                break;

                            case ReadStatus.ID:
                                member[index].AddId(int.Parse(line));
                                break;

                            default:
                                throw new Exception();
                        }
                    }
                }
            }

            return member;
        }

        public void Save(List<Member> members)
        {
            using (StreamWriter logMember = new StreamWriter(Path))
            {
                foreach (Member memb in members)
                {
                    logMember.WriteLine("[Förnamn]");
                    logMember.WriteLine(memb.FirstName);
                    logMember.WriteLine("[Efternamn]");
                    logMember.WriteLine(memb.LastName);
                    logMember.WriteLine("[Telefonnummer]");
                    logMember.WriteLine(memb.TelNumber);
                    logMember.WriteLine("[ID]");
                    logMember.WriteLine(memb.MemberId);
                }

                Console.WriteLine("Medlemmarna har sparats.");
            }
        }
    }
}
