using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BanTin
{
    internal class Category
    {
        private static int i = 1;
        public string name { get; set; }
        private List<New> news { get; set; }
        private static List<Category> categories = new List<Category>(); 


        public Category(string name)
        {
            this.name = name;
            //kiem tra list BanTin
            if (news == null)
                news = new List<New>();

            if (categories == null)
            {
                categories = new List<Category>();
                categories.Add(this);
            }
            categories.Add(this);
        }

        public string getName() 
        {
            return name;
        }

        public List<New> getNews()
        {
            if (news == null)
            {
                news = new List<New>();
            }
            return news;
        }
        public static List<Category> getCategories()
        {
            if (categories == null)
            {
                categories = new List<Category>();
            }
            return categories;
        }



        public void printAllBanTin()
        {
            Console.WriteLine("The loai " + name + " gom cac ban tin :");
            if (news == null || news.Count == 0)
                Console.WriteLine("khong co du lieu");
            else
                foreach (New banTin in news)
                {
                    Console.WriteLine(banTin.getName());
                }
        }



        public void setCategory(string banTinName)
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
                foundBanTin.setCategoryName(this.name);
            }
        }

        public void removeCategory(string banTinName)
        {
            foreach (New banTin in New.listBanTins)
            {
                if (banTin.getName() == banTinName)
                {
                    news.Remove(banTin);
                    break;
                }
            }
        }


        static public void printAllCategory()
        {
            foreach (Category category in categories)
            {
                Console.WriteLine("The loai " + i + " : " + category.name);
                i++;
            }
        }



    }
}
