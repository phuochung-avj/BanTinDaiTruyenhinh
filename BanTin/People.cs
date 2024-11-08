using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BanTin
{
    internal interface People
    {
        public static List<People> listPeople = new List<People>();
        string getName();
        void setName(string input);
        void setCompany(string input);
        void printAll();
        void removeP();
        void setMail(string input);
    }
}
