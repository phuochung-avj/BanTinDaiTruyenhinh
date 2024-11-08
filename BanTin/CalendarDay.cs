using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BanTin
{
    public class CalendarDay
    {
        public int Day { get; }
        public Calendar Calendar { get; set; }
        private List<Channel> listChannels;
        private List<TimeSet> listTimeSets;
        public CalendarDay(int day, Calendar calendar)
        {
            this.Day = day;
            Calendar = calendar;
            this.listChannels = new List<Channel>();
            this.listTimeSets = new List<TimeSet>();
        }

        public void setListChannels(List<Channel> input)
        {
            listChannels = input;
        }
        public List<Channel> getListChannels()
        {
            // Trả về bản sao của danh sách để ngăn chặn sự thay đổi từ bên ngoài
            return listChannels;
        }

        // Thêm bản tin vào kênh của CalendarDay

        public void addBanTinToChannel(BanTin banTin, Channel channel, string period, int day, int month)
        {
            channel.channelAddBanTin(banTin.getName(), period);
            listTimeSets.Add(new TimeSet(banTin.getTime(), period, day, month, channel.getName(), banTin.getName()));
        }


        public int getDay()
        {
            return Day;
        }



        public override string ToString()
        {
            return $"{Day}/{Calendar.getMonth()}/{Calendar.getYear()}";
        }

        
    }

}
