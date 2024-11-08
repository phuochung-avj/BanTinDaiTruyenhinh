using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BanTin
{
    public class Anchor : Author, People
    {
        private string name;
        private string mail;
        private string company;
        private int numOfDays;
        private List<string> listChannels = new List<string>();
        private static List<Anchor> anchors = new List<Anchor>();
        public Anchor(string name, string company, string mail) : base(name, company, mail)
        {
            this.name = name;
            this.mail = mail;
            this.company = company;
            this.numOfDays = 0;
            if (anchors == null)
                anchors = new List<Anchor>();
            anchors.Add(this);
            People.listPeople.Add(this);
        }

        public override void removeP()
        {
            anchors.Remove(this);
        }

        static public void printAllPeople()
        {
            foreach (Anchor author in anchors)
            {
                Console.WriteLine(author.getName());
            }
        }

        public void addChannelforAnchor(string input) 
        {
            listChannels.Add(input);
        }

        public override void printAll()
        {
            Console.WriteLine("Tên nguười dẫn chương trình: " + name);
            Console.WriteLine("Thuộc : " + company);
            Console.WriteLine("Email : " + mail);
            Console.WriteLine("Các kênh " + name + "chịu trách nhiệm :");
            if (listChannels == null || listChannels.Count == 0)
                Console.WriteLine("khong co du lieu");
            else
                foreach (string ichannel in listChannels)
                {
                    Console.WriteLine(ichannel);
                }
        }

        public static List<Anchor> getListAnchors()
        {
            return anchors;
        }
        public List<string> getListChannels()
        {
            return this.listChannels;
        }
        public List<string> getListDays()
        {
            return this.listChannels;
        }
    }
}



