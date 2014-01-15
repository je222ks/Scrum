using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberReg
{
    class Member
    {
        private string _firstName;
        private string _lastName;
        private int _telNumber;
        private int memberId = 0;
        List<object> _memberList = new List<object>();

        public string FirstName
        {
            get { return _firstName; }
            set 
            {
                if (String.IsNullOrWhiteSpace(value)) 
                {
                    throw new ArgumentException();
                }
                _firstName = value;
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException();
                }
                _lastName = value;
            }
        }

        public int TelNumber
        {
            get { return _telNumber; }
            set
            {
                if (value.GetType() != typeof(int))
                {
                    throw new ArgumentException();
                }
                _telNumber = value;
            }
        }

        public int MemberId
        {
            get { return memberId; }
        }

        public ReadOnlyCollection<object> MemberList
        {
            get { return _memberList.AsReadOnly(); }
        }

        public Member(string firstName, string lastName, int telNumber) 
        {
            FirstName = firstName;
            LastName = lastName;
            TelNumber = telNumber;
        }

        public void Add(object member)
        {
            _memberList.Add(member);
            memberId++;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0}\t{1}\tTel-Nummer: {2}\t Medlemsnummer: {3}", FirstName, LastName, TelNumber, MemberId);

            return sb.ToString();
        }
    }
}
