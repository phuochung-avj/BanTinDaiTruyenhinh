using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BanTin
{
    internal class Reporter : Author, People
    {
        private string name;
        private string mail;
        private List<LiveStream> listNews;
        private string company;
        private static List<Reporter> reporters { get; set; }

        public Reporter(string name, string company, string mail) : base(name, company, mail)
        {
            this.name = name;
            this.mail = mail;
            this.listNews = new List<LiveStream> { };
            this.company = company;
            if (reporters == null) {
                reporters = new List<Reporter>();
                reporters.Add(this);
            }
            else
                reporters.Add(this);
            People.listPeople.Add(this);
        }

        public override void printAll()
        {
            Console.WriteLine("Tên phóng viên: " + name);
            Console.WriteLine("Thuộc : " + company);
            Console.WriteLine("Email : " + mail);
            Console.WriteLine("Số lượng bản tin phát sóng trực tiếp của tác giả : " + listNews.Count);
            Console.WriteLine("Các bản tin phát sóng trực tiế của tác giả " + name + " :");
            if (listNews == null || listNews.Count == 0)
                Console.WriteLine("khong co du lieu");
            else
                foreach (New banTin in listNews)
                {
                    Console.WriteLine(banTin.getName());
                }
        }


        public static void printAllPeople()
        {
            foreach (People author in reporters)
            {
                Console.WriteLine(author.getName());
            }
        }
        public override void removeP()
        {
            reporters.Remove(this);
        }
    }
}
