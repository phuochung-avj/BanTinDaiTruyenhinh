using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BanTin
{
    public interface New
    {
        public static List<New> listBanTins = new List<New>();
        double getTime();
        void setTime(string period, string nameChannel, int inputDay, int inputMonth);
        void print();
        List<string> getListDays();
        List<string> getListChannels();
        List<TimeSet> getListTime();
        string getName();
        void setName(string name);
        void setChanelName(string input);
        void setNoiDung(string input);
        void setTime(double time);
        string getNoiDung();
    }
}
