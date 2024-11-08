using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BanTin
{
    public class LiveStream : BanTin, New
    {

        private string name { get; set; }
        private double time { get; set; }
        private string noidung { get; set; }
        private string reporterName { get; set; }
        private List<string> listChannels;
        private List<string> listDays;
        private List<TimeSet> listTime;

        public LiveStream(string name, double time, string noidung) : base(name, time, noidung)
        {
            this.reporterName = " chua co du lieu phong vien ";
            this.name = name;
            this.noidung = noidung;
            if (time <= 60)
                this.time = time;
            else
                Console.WriteLine("thoi luong quang cao khong duoc qua 60s");
            listTime = new List<TimeSet>();
            listChannels = new List<string>();
            listDays = new List<string>();
        }
    }
}
