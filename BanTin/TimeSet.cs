using System;
using System.Collections.Generic;

using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BanTin
{
    public class TimeSet
    {
        private double time;
        private string period;
        private string nameChannel;
        private string nameBanTin;
        private int day;
        private int month;
        private DateTime timeStart;
        private string calendarShow;
        public static List<TimeSet> listAllTimeSet = new List<TimeSet>();
        private TimeSpan timeEnd { get; set; }
        public string Period { get { return period; } set { period = value; } }
        public string NameChannel
        {
            get { return nameChannel; }
            set { nameChannel = value; }
        }
        public string NameBanTin { get { return nameBanTin; } set { nameBanTin = value; } }
        public int Day { get { return day; } set { day = value; } }

        public TimeSet(double time, string period, int day, int month, string nameChannel, string nameBanTin)
        {
            this.day = day;
            this.month = month;
            this.time = time;
            this.period = period;
            this.nameChannel = nameChannel;
            this.nameBanTin = nameBanTin;
            listAllTimeSet.Add(this);
            calendarShow = day + "/" + month;
        }
        public void setTime(double input) { this.time = input; }
        public double getTime() { return time; }
        public void setTimeStart(DateTime input)
        {
            timeStart = input;
        }
        public int getDay() { return day; }
        public int getMonth() { return month; }
        public void setChannelOfTimeSet(string input)
        {
            nameChannel = input;
        }
        public string getChannelOfTimeSet()
        {
            return nameChannel;
        }
        public void setBanTinOfTimeSet(string input)
        {
            nameBanTin = input;
        }
        public void setDayOfTimeSet(string input)
        {
            calendarShow = input;
        }
        public DateTime getTimeStart()
        {
            return timeStart;
        }

        public override string ToString()
        {
            return "Thoi luong: " + time + "s" + "\n" +
                   "Khung chieu: " + period + "\n" +
                   "Thời gian bat dau: " + timeStart + " tại kênh " + nameChannel +
                   " Ngày: " + day + "/" + month;
        }
        public static void deleteTimeSet()
        {
            // Nhập tên kênh
            Console.WriteLine("Nhập tên kênh:");
            string nameofChannel = Console.ReadLine();

            // Nhập tên bản tin
            Console.WriteLine("Nhập tên bản tin:");
            string nameofnew = Console.ReadLine();

            // Nhập thời gian
            Console.WriteLine("Nhập khoảng thời gian (sang/toi):");
            string period = Console.ReadLine();

            // Nhập ngày và tháng
            Console.WriteLine("Nhập ngày:");
            int day;
            while (!int.TryParse(Console.ReadLine(), out day) || day < 1 || day > 31)
            {
                Console.WriteLine("Ngày không hợp lệ! Nhập lại:");
            }

            Console.WriteLine("Nhập tháng:");
            int month;
            while (!int.TryParse(Console.ReadLine(), out month) || month < 1 || month > 12)
            {
                Console.WriteLine("Tháng không hợp lệ! Nhập lại:");
            }

            foreach (TimeSet index in listAllTimeSet)
            {
                if (nameofChannel == index.nameChannel && nameofnew == index.nameBanTin && period == index.period && day == index.day && month == index.month)
                {
                    listAllTimeSet.Remove(index);
                    Console.WriteLine("Xoá thành công");
                    break;
                }
            }
        }

        public static void totalTime(string nameChannel)
        {
            // Lọc danh sách các bản tin thuộc kênh 
            IEnumerable<TimeSet> filteredItems = listAllTimeSet.Where(item => item.NameChannel == nameChannel);

            // Tạo một danh sách tạm để lưu tổng thời gian của mỗi bản tin trong kênh "VTV"
            Dictionary<string, double> tempTotalTimes = new Dictionary<string, double>();

            // Duyệt qua danh sách và tính tổng thời gian cho mỗi bản tin của kênh "VTV"
            foreach (TimeSet item in filteredItems)
            {
                if (tempTotalTimes.ContainsKey(item.NameBanTin))
                {
                    tempTotalTimes[item.NameBanTin] += item.getTime();
                }
                else
                {
                    tempTotalTimes[item.NameBanTin] = item.getTime();
                }
            }

            // In ra tổng thời gian của mỗi bản tin trong kênh "VTV"
            Console.WriteLine($"{nameChannel} gồm:");
            foreach (KeyValuePair<string, double> entry in tempTotalTimes)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value} giây");
            }
        }
    }


}
