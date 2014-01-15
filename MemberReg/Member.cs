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
            set
            {
                memberId++;
                value = memberId;
            }
        }

        public ReadOnlyCollection<object> MemberList
        {
            get { return _memberList.AsReadOnly(); }
        }

        public void AddId(int id)
        {
            memberId++;
            MemberId = id;
        }

        public void AddLastName(string lName)
        {
            LastName = lName;
        }

        public void AddPhone(int num)
        {
            TelNumber = num;
        }

        public Member(string name)
        {
            FirstName = name;
        }

        public Member(string firstName, string lastName, int telNumber) 
        {
            FirstName = firstName;
            LastName = lastName;
            TelNumber = telNumber;
        }

        //public override string ToString()
        //{
        //    StringBuilder sb = new StringBuilder();

        //    sb.AppendFormat("{0}\t{1}\tTel-Nummer: {2}\t Medlemsnummer: {3}", FirstName, LastName, TelNumber, MemberId);

        //    return sb.ToString();
        //}
    }
}
