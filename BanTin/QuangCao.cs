using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BanTin
{
    internal class QuangCao :  BanTin, New 
    {
        private string name { get; set; }
        private double time { get; set; }
        private string noidung { get; set; }
        private List<string> listChannels;
        private List<string> listDays;
        private List<TimeSet> listTime;
        public QuangCao(string name, double time, string noidung) : base(name, time, noidung)
        {
            this.name = name;
            this.noidung = noidung;
            if (time <= 60)
                this.time = time;
            else
                Console.WriteLine("Thời lượng quảng cáo không được quá 60s");
            listTime = new List<TimeSet>();
            listChannels = new List<string>();
            listDays = new List<string>();
        }
    }
}
