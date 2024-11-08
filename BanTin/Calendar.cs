using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BanTin
{
    public class Calendar
    {
        private int year;
        private int month;
        private int day;
        private List<CalendarDay> days;
        private static List<Calendar> canlendar2024;

        public Calendar(int year, int month)
        {
            this.year = year;
            this.month = month;
            this.days = GenerateCalendarDays(year, month);
            if (canlendar2024 == null)
            {
                canlendar2024 = new List<Calendar> { };
                canlendar2024.Add(this);
            }
            canlendar2024.Add(this);
        }
        public static List<Calendar> getCanlendar2024()
        {
            return canlendar2024;
        }


        public List<CalendarDay> getDays()
        {
            return days;
        }

        // Phương thức để lấy CalendarDay dựa trên month và day
        public static CalendarDay getCalendarDay(int day, int month)
        {
            // Kiểm tra xem danh sách các Calendar có tồn tại hay không
            if (canlendar2024 != null)
            {
                // Duyệt qua từng Calendar trong danh sách
                foreach (Calendar calendar in canlendar2024)
                {
                    // Duyệt qua từng CalendarDay trong Calendar
                    foreach (CalendarDay calendarDay in calendar.days)
                    {
                        // Kiểm tra điều kiện tìm kiếm
                        if (calendarDay.Day == day && calendarDay.Calendar.getMonth() == month)
                        {
                            return calendarDay; // Trả về CalendarDay tìm thấy
                        }
                    }
                }
            }

            // Trường hợp không tìm thấy
            return null;
        }

        public int getYear()
        {
            return year;
        }
        public int getMonth()
        {
            return month;
        }


        private List<CalendarDay> GenerateCalendarDays(int year, int month)
        {
            List<CalendarDay> calendarDays = new List<CalendarDay>();

            // Tạo một DateTime đại diện cho ngày đầu tiên của tháng
            DateTime firstDayOfMonth = new DateTime(year, month, 1);

            // Tạo một DateTime đại diện cho ngày cuối cùng của tháng
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            // Lặp qua tất cả các ngày từ ngày đầu tiên đến ngày cuối cùng của tháng và thêm vào danh sách
            for (DateTime currentDate = firstDayOfMonth; currentDate <= lastDayOfMonth; currentDate = currentDate.AddDays(1))
            {
                // Tạo một đối tượng CalendarDay mới với giá trị ngày và tham chiếu tới đối tượng Calendar gốc
                CalendarDay calendarDay = new CalendarDay(currentDate.Day, this);
                calendarDays.Add(calendarDay);
            }

            return calendarDays;
        }

    }
}
