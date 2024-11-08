using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BanTin
{
    public class Author : People
    {
        private string company;
        private string name;
        private string mail;
        private int numOfNews;
        private List<BanTin> news = new List<BanTin>();
        private static List<Author> authors = new List<Author>();
        public Author(string name, string company, string mail)
        {
            this.name = name;
            this.company = company;
            this.mail = mail;
            // xet so luong ban tin cua tac gia
            if (news != null)
                this.numOfNews = news.Count;
            else
                this.numOfNews = 0;
            // xet danh sach tat ca cac tac gia
            if (authors == null)
                authors = new List<Author>();
            else
                authors.Add(this);
            People.listPeople.Add(this);
        }

        public virtual void printAll()
        {
            Console.WriteLine("Tên tác giả: " + name);
            Console.WriteLine("Thuộc : " + company);
            Console.WriteLine("Email : " + mail);
            Console.WriteLine("Số lượng bài báo : " + numOfNews);
            Console.WriteLine("Các bài báo của tác giả " + name + " :");
            if (news == null || news.Count == 0)
                Console.WriteLine("khong co du lieu");
            else
                foreach (New banTin in news)
                {
                    Console.WriteLine(banTin.getName());
                }
        }

        public virtual void removeP() 
        { 
            authors.Remove(this);
        }
        public void setName(string input){name = input;}

        public void setCompany(string input){ company = input; }

        public void setMail(string input){ mail = input; }


        public static void printAllPeople()
        {
            foreach (People author in authors)
            {
                Console.WriteLine(author.getName());
            }
        }

        public virtual void setCategory(string banTinName)
        {
            bool check = false;
            BanTin foundBanTin = null;
            foreach (New banTin in New.listBanTins)
            {
                if (banTin.getName() == banTinName)
                {
                    check = true;
                    foundBanTin = (BanTin)banTin;
                    break;
                }
            }

            if (check && foundBanTin != null || foundBanTin is BanTin)
            {
                news.Add(foundBanTin);
                foundBanTin.setAuthorName(this.name);
            }
        }

        public List<BanTin> getNews()
        {
            if (news == null)
            {
                news = new List<BanTin>();
            }
            return news;
        }

        public static List<Author> getAuthors()
        {
            if (authors == null)
            {
                authors = new List<Author>();
            }
            return authors;
        }

        public string getName()
        {
            return name;
        }

        public void setAuthor(string input)
        {
            if (news == null)
            {
                news = new List<BanTin>();
            }
            foreach (BanTin index in BanTin.getListNew())
            {
                if (index.getName() == input) 
                { 
                news.Add(index);
                index.setAuthorName(this.name); 
                }
            }
            numOfNews = news.Count;
        }


    }
}
