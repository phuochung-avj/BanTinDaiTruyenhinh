using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BanTin
{
    internal class PhuongThuc
    {
        private static string filePath = "C:\\Users\\ASUS\\source\\tét\\bantin\\BanTin-master\\BanTin\\data.json";
        public static BanTinList banTinListNew = new BanTinList();
        // Hàm khởi tạo
        public static void createCalendar()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // tao lich cac ngay trong nam 2024
            List<Calendar> calendarOf2024 = new List<Calendar>();
            for (int i = 0; i < 12; i++)
            {
                Calendar calendar = new Calendar(2024, i + 1);
                calendarOf2024.Add(calendar);

                List<CalendarDay> days = calendar.getDays();
            }
        }

        // Phương thức chức năng
        // Tạo mới 1 Channel ( đồng thời thêm vào các ngày luôn )
        public static void addChannel(string nameofChannel, int numberOfNews)
        {
            foreach (Calendar iCalendar in Calendar.getCanlendar2024())
            {
                foreach (CalendarDay calendarDay in iCalendar.getDays())
                {
                    calendarDay.getListChannels().Add(new Channel(nameofChannel, numberOfNews));
                }
            }
        }
        // Tạo mới 1 BanTin
        public static void QuanLiBanTin()
        {
            while (true)
            {
                Console.WriteLine("Quản lý bản tin: ");
                Console.WriteLine("1. Thêm bản tin");
                Console.WriteLine("2. Thêm quảng cáo");
                Console.WriteLine("3. Thêm bản tin trực tiếp");
                Console.WriteLine("4. Sửa bản tin");
                Console.WriteLine("5. Xóa bản tin");
                Console.WriteLine("6. Đặt thời gian chiếu");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn chức năng (nhập số từ 0 đến 5): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Bạn đã chọn Thêm bản tin");
                        nhapBanTin();
                        break;
                    case "2":
                        try
                        {
                            Console.WriteLine("Bạn đã chọn thêm quảng cáo");
                            Console.WriteLine("Tên quảng cáo :");
                            string name1 = Console.ReadLine();
                            Console.WriteLine("Thời lượng quảng cáo :");
                            double time1 = double.Parse(Console.ReadLine());
                            Console.WriteLine("Nội dung quảng cáo :");
                            string noiDung1 = Console.ReadLine();
                            Console.WriteLine(banTinListNew);
                            QuangCao iQC = new QuangCao(name1, time1, noiDung1);
                        }
                        catch (Exception ex)
                        {
                            // Xử lý ngoại lệ ở đây
                            Console.WriteLine("Đã xảy ra một ngoại lệ: " + ex.Message);
                        }
                        break;
                    case "3":
                        try
                        {
                            Console.WriteLine("Bạn đã chọn thêm bản tin trực tiếp");
                            Console.WriteLine("Tên bản tin :");
                            string name2 = Console.ReadLine();
                            Console.WriteLine("Thời lượng bản tin :");
                            double time2 = double.Parse(Console.ReadLine());
                            Console.WriteLine("Nội dung bản tin :");
                            string noiDung2 = Console.ReadLine();
                            Console.WriteLine(banTinListNew);
                            LiveStream ilive = new LiveStream(name2, time2, noiDung2);
                        }
                        catch (Exception ex)
                        {
                            // Xử lý ngoại lệ ở đây
                            Console.WriteLine("Đã xảy ra một ngoại lệ: " + ex.Message);
                        }
                        break;

                    case "4":
                        Console.WriteLine("Bạn đã chọn sửa bản tin");
                        suaBanTin();
                        break;

                    case "5":
                        Console.WriteLine("Bạn đã chọn sửa bản tin");
                        string iName = Console.ReadLine();
                        foreach (New News in New.listBanTins)
                            New.listBanTins.Remove(News);
                        break;

                    case "6":
                        Console.WriteLine("Đặt thời gian chiếu");
                        setTimeforNew();
                        break;
                    case "0":
                        Console.WriteLine("Tạm biệt!");
                        Program.Menu();
                        break;

                    default:
                        Console.WriteLine("Chức năng không hợp lệ. Hãy chọn lại.");
                        break;
                }

                Console.WriteLine(); // In một dòng trắng để tạo định dạng
            }
        }

        public static void QuanLiPeople()
        {
            while (true)
            {
                Console.WriteLine("Quản lý nhân sự: ");
                Console.WriteLine("1. Thêm nhân sự");
                Console.WriteLine("2. Sửa nhân sự");
                Console.WriteLine("3. Xóa nhân sự");
                Console.WriteLine("4. Hiển thị phạm vi bản tin của nhân sự");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn chức năng (nhập số từ 0 đến 5): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Bạn đã chọn Thêm nhân sự");
                        themNhanSu();
                        break;

                    case "2":
                        Console.WriteLine("Bạn đã chọn sửa bản tin");
                        suaNhanSu();
                        break;

                    case "3":
                        Console.WriteLine("Bạn đã chọn xoá nhân sự");
                        bool found = false;
                        foreach (People check in People.listPeople)
                        {
                            Console.WriteLine(check.getName());
                        }
                        while (!found)
                        {
                            Console.WriteLine("Nhập tên nhân sự muốn xoá ");
                            string iname = Console.ReadLine();

                            foreach (People check in People.listPeople)
                            {
                                if (check.getName() == iname)
                                {
                                    People.listPeople.Remove(check);
                                    check.removeP();
                                    Console.WriteLine("Xoá thành công " + iname);
                                    found = true;
                                    break;
                                }
                            }

                            if (!found)
                            {
                                Console.WriteLine("Không tìm thấy nhân sự có tên " + iname);
                                Console.WriteLine("Bạn có muốn tiếp tục xoá nhân sự không? (Y/N)");
                                string input = Console.ReadLine();

                                if (input.ToUpper() == "N")
                                {
                                    Console.WriteLine("Quay lại menu");
                                    found = true; // Đánh dấu tìm thấy để thoát khỏi vòng lặp
                                }
                            }
                        }
                        break;

                    case "4":
                        Console.WriteLine("Bạn đã chọn Hiển thị phạm vi bản tin của nhân sự");
                        foreach (People check in People.listPeople)
                        {
                            Console.WriteLine(check.getName());
                        }
                        bool found1 = false;
                        while (!found1)
                        {
                            Console.WriteLine("Nhập tên nhân sự muốn hiển thị bản tin ");
                            string iname = Console.ReadLine();

                            foreach (People check in People.listPeople)
                            {
                                if (check.getName() == iname)
                                {
                                    check.printAll();
                                    break;
                                }
                            }

                            if (!found1)
                            {
                                Console.WriteLine("Không tìm thấy nhân sự có tên " + iname);
                                Console.WriteLine("Bạn có muốn tiếp tục không? (Y/N)");
                                string input = Console.ReadLine();

                                if (input.ToUpper() == "N")
                                {
                                    Console.WriteLine("Quay lại menu");
                                    found = true; // Đánh dấu tìm thấy để thoát khỏi vòng lặp
                                    break;
                                }
                            }
                        }
                        break;
                    case "0":
                        Console.WriteLine("Tạm biệt!");
                        Program.Menu();
                        break;

                    default:
                        Console.WriteLine("Chức năng không hợp lệ. Hãy chọn lại.");
                        break;
                }

                Console.WriteLine(); // In một dòng trắng để tạo định dạng
            }
        }

        public static void suaNhanSu()
        {
            int ipeople = 0;
            do
            {
                Console.WriteLine("Nhập loại nhân sự: 1. Tác giả / 2. Người dẫn chương trình / 3. Phóng viên trực tiếp");
                try
                {
                    ipeople = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Giá trị không hợp lệ. Vui lòng nhập một số nguyên.");
                    continue;
                }
            }
            while (ipeople < 1 || ipeople > 3);

            if (ipeople == 1)
            {
                Author.printAllPeople();
            }
            else if (ipeople == 2)
            {
                Anchor.printAllPeople();
            }
            else
            {
                Reporter.printAllPeople();
            }

            Console.WriteLine("Nhập tên nhân sự muốn thao tác:");
            string icheck = Console.ReadLine();

            bool isPersonFound = false;
            foreach (People check in People.listPeople)
            {
                if (check != null && check.getName() == icheck)
                {
                    isPersonFound = true;
                    int choice = 0;
                    do
                    {
                        Console.WriteLine("Menu chọn thuộc tính:");
                        Console.WriteLine("1. Thay đổi tên");
                        Console.WriteLine("2. Thay đổi công ty");
                        Console.WriteLine("3. Thay đổi email");
                        Console.WriteLine("4. Hiển thị tất cả bản tin thuộc về " + icheck + ":");
                        Console.WriteLine("0. Thoát");
                        Console.WriteLine("Lựa chọn của bạn:");
                        try
                        {
                            choice = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Giá trị không hợp lệ. Vui lòng nhập một số nguyên.");
                            continue;
                        }

                        switch (choice)
                        {
                            case 1:
                                Console.WriteLine("Nhập tên mới:");
                                string name = Console.ReadLine();
                                check.setName(name);
                                Console.WriteLine("Tên đã được cập nhật thành công.");
                                break;
                            case 2:
                                Console.WriteLine("Nhập công ty mới:");
                                string company = Console.ReadLine();
                                check.setCompany(company);
                                Console.WriteLine("Công ty đã được cập nhật thành công.");
                                break;
                            case 3:
                                Console.WriteLine("Nhập email mới:");
                                string mail = Console.ReadLine();
                                check.setMail(mail);
                                Console.WriteLine("Email đã được cập nhật thành công.");
                                break;
                            case 4:
                                Console.WriteLine("Hiển thị tất cả bản tin thuộc về " + icheck + ":");
                                // Thực hiện hiển thị bản tin cho nhân sự có tên là icheck
                                break;
                            case 0:
                                Console.WriteLine("Thoát khỏi menu.");
                                break;
                            default:
                                Console.WriteLine("Lựa chọn không hợp lệ, vui lòng chọn lại.");
                                break;
                        }

                        Console.WriteLine();
                    }
                    while (choice != 0);
                    break;
                }
            }

            if (!isPersonFound)
            {
                Console.WriteLine("Nhân sự không tồn tại, vui lòng nhập lại.");
                suaNhanSu();
            }
            QuanLiPeople();
        }

        public static void themNhanSu()
        {
            bool continueInput = true;

            do
            {
                Console.WriteLine("Nhập loại nhân sự: 1. Tác giả / 2. Người dẫn chương trình / 3. Phóng viên trực tiếp");
                int ipeople = int.Parse(Console.ReadLine());

                Console.WriteLine("Nhập tên:");
                string name = Console.ReadLine();

                Console.WriteLine("Nhập tên công ty:");
                string company = Console.ReadLine();

                Console.WriteLine("Nhập mail:");
                string mail = Console.ReadLine();

                foreach (People check in People.listPeople)
                {
                    if (name == check.getName())
                    {
                        Console.WriteLine("Nhân sự đã tồn tại");
                        break;
                    }
                }

                if (ipeople == 1)
                {
                    Author iAuthor = new Author(name, company, mail);
                    Console.WriteLine("Tạo thành công Tác giả " + name);
                }
                else if (ipeople == 2)
                {
                    Anchor iAuthor = new Anchor(name, company, mail);
                    Console.WriteLine("Tạo thành công Người dẫn chương trình " + name);
                }
                else if (ipeople == 3)
                {
                    Reporter iAuthor = new Reporter(name, company, mail);
                    Console.WriteLine("Tạo thành công Phóng viên trực tiếp " + name);
                }
                else
                {
                    Console.WriteLine("Dữ liệu không đúng, mời nhập lại");
                    continue; // Quay lại vòng lặp để yêu cầu nhập lại thông tin
                }

                Console.WriteLine("1. Nhập tiếp");
                Console.WriteLine("2. Thoát");
                Console.WriteLine("Lựa chọn của bạn:");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        continueInput = true;
                        break;
                    case 2:
                        continueInput = false;
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ, mời nhập lại");
                        continue; // Quay lại vòng lặp để yêu cầu nhập lại lựa chọn
                }
            } while (continueInput);
        }
        public static void QuanLiSetTime() 
        {

            while (true)
            {
                Console.WriteLine("Quản lý lịch chiếu bản tin truyền hình : ");
                Console.WriteLine("1. Đặt thời gian cho bản tin ");
                Console.WriteLine("2. Đổi vị trí của 2 bản tin");
                Console.WriteLine("3. Xoá lịch chiếu của bản tin truyền hình");
                Console.WriteLine("4. Hiển thị thông tin lịch chiếu bản tin theo ngày ");
                Console.WriteLine("5. Hiển thị tất cả thông tin lịch chiếu của bản tin ");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn chức năng (nhập số từ 0 đến 4): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Bạn đã chọn Đặt thời gian cho bản tin");
                        setTimeforNew();
                        break;

                    case "2":
                        Console.WriteLine("Bạn đã chọn Đổi vị trí của 2 bản tin");
                        SwapBanTinWithDelegate();
                        break;

                    case "3":
                        Console.WriteLine("Bạn đã chọn Xoá lịch chiếu của bản tin truyền hình");
                        TimeSet.deleteTimeSet();
                        break;

                    case "4":
                        Console.WriteLine("Bạn đã chọn Hiển thị thông tin lịch chiếu bản tin theo ngày");
                        printAsDay();
                        break;
                    case "5":
                        Console.WriteLine("Bạn đã chọn Hiển thị tất cả thông tin lịch chiếu của bản tin");
                        foreach (New index in New.listBanTins)
                        { Console.Write(index.getName() + " "); }
                        // Nhập tên bản tin
                        Console.WriteLine("Nhập tên bản tin:");
                        string nameofnew = Console.ReadLine();
                        int maxTime2 = 0;
                        foreach (New newsItem in New.listBanTins)
                        {
                            if (nameofnew == newsItem.getName())
                            {
                                foreach (TimeSet check in TimeSet.listAllTimeSet) 
                                {
                                    if (check.NameBanTin == nameofnew)
                                        Console.WriteLine(check.ToString());
                                }
                                break;
                            }
                            if (maxTime2 == 3)
                            {
                                QuanLiSetTime();
                                break;
                            }
                         maxTime2++;
                         Console.WriteLine("Nhập sai hoặc bản tin không tồn tại , vui lòng nhập lại ");
                        }
                        break;
                    case "0":
                        Console.WriteLine("Tạm biệt!");
                        Program.Menu();
                        break;

                    default:
                        Console.WriteLine("Chức năng không hợp lệ. Hãy chọn lại.");
                        break;
                }

                Console.WriteLine(); // In một dòng trắng để tạo định dạng
            }
        }


        public static void printAsDay()
        {
            Console.WriteLine("Nhập ngày/tháng muốn in lịch : ");
            Console.WriteLine("Ngày: ");
            int day = int.Parse(Console.ReadLine());
            Console.WriteLine("Tháng: ");
            int month = int.Parse(Console.ReadLine());
            //In thông tin của CalendarDay
            Console.WriteLine("Ban tin ngày " + day + "/" + month + " gồm: ");
            Console.WriteLine("Bản tin sáng : ");
            foreach (Channel iChannel in Calendar.getCalendarDay(day, month).getListChannels())
            {
                Console.WriteLine("-----");
                Console.WriteLine(iChannel.getName() + " : ");
                foreach (New iNew in iChannel.Sang)
                {
                    Console.WriteLine(iNew.getName());
                    foreach (TimeSet x in iNew.getListTime())
                    {
                        if (x.NameBanTin == iNew.getName() && x.NameChannel == iChannel.getName() && x.Period == "sang")
                            Console.WriteLine("Chiếu lúc : " + x.getTimeStart().ToString("HH:mm:ss"));
                    }
                }
            }
            Console.WriteLine("Bản tin tối : ");
            foreach (Channel iChannel in Calendar.getCalendarDay(day, month).getListChannels())
            {
                foreach (New iNew in iChannel.Toi)
                {
                    Console.WriteLine(iNew.getName());
                    foreach (TimeSet x in iNew.getListTime())
                    {
                        if (x.NameBanTin == iNew.getName() && x.NameChannel == iChannel.getName() && x.Period == "toi")
                            Console.WriteLine("Chiếu lúc : " + x.getTimeStart().ToString("HH:mm:ss"));
                    }
                }
            }
        }
        public static void setTimeforNew()
        {
            {
                // Nhập tên kênh
                Console.WriteLine("Nhập tên kênh: VTV1, VTV2, VTV3:");
                string nameofChannel = "";
                int maxTime1 = 0;
                bool channelFound = false;

                while (maxTime1 < 3)
                {
                    nameofChannel = Console.ReadLine();
                    foreach (Channel check in Channel.getChanels())
                    {
                        if (nameofChannel == check.getName())
                        {
                            channelFound = true;
                            break;
                        }
                    }

                    if (channelFound)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Nhập sai hoặc kênh không tồn tại, vui lòng nhập lại");
                        maxTime1++;
                    }
                }

                if (!channelFound)
                {
                    QuanLiSetTime();
                    return;
                }

                // In ra danh sách các bản tin
                foreach (New index in New.listBanTins)
                {
                    Console.Write(index.getName() + " ");
                }
                Console.WriteLine();

                // Nhập tên bản tin
                Console.WriteLine("Nhập tên bản tin:");
                string nameofnew = "";
                int maxTime2 = 0;
                bool newsFound = false;

                while (maxTime2 < 3)
                {
                    nameofnew = Console.ReadLine();
                    foreach (New newsItem in New.listBanTins)
                    {
                        if (nameofnew == newsItem.getName())
                        {
                            newsFound = true;
                            break;
                        }
                    }

                    if (newsFound)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Nhập sai hoặc bản tin không tồn tại, vui lòng nhập lại");
                        maxTime2++;
                    }
                }

                if (!newsFound)
                {
                    QuanLiSetTime();
                    return;
                }

                // Nhập khoảng thời gian
                Console.WriteLine("Nhập khoảng thời gian (sang/toi):");
                string period = "";
                while (true)
                {
                    period = Console.ReadLine();
                    if (period == "sang" || period == "toi")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Nhập sai, vui lòng nhập lại");
                    }
                }

                // Nhập ngày
                Console.WriteLine("Nhập ngày:");
                int day;
                while (!int.TryParse(Console.ReadLine(), out day) || day < 1 || day > 31)
                {
                    Console.WriteLine("Ngày không hợp lệ! Nhập lại:");
                }

                // Nhập tháng
                Console.WriteLine("Nhập tháng:");
                int month;
                while (!int.TryParse(Console.ReadLine(), out month) || month < 1 || month > 12)
                {
                    Console.WriteLine("Tháng không hợp lệ! Nhập lại:");
                }

                // Gọi phương thức setTimeAndBanTinForChannel với dữ liệu đã nhập
                setTimeAndBanTinForChannel(nameofChannel, nameofnew, period, day, month);
            }

        }

        public static void setTimeAndBanTinForChannel(string nameofChannel, string nameofnew, string period, int day, int month)
        {
            CalendarDay calendarDay = Calendar.getCalendarDay(day, month);
            if (calendarDay != null)
            {
                List<Channel> channels = new List<Channel>();
                foreach (Channel channel in calendarDay.getListChannels())
                {
                    channels.Add(channel);
                }

                foreach (Channel channel in channels)
                {
                    if (channel.getName() == nameofChannel)
                    {
                        List<New> news = New.listBanTins;
                        foreach (New newsItem in news)
                        {
                            if (nameofnew == newsItem.getName())
                            {
                                channel.setTimeAndAddBanTin(newsItem, period, day, month);
                                break;
                            }
                        }
                        break;
                    }
                }
                calendarDay.setListChannels(channels); // Cập nhật danh sách kênh trong calendarDay
            }
            else
            {
                // Xử lý trường hợp ngày không hợp lệ
                Console.WriteLine("Ngày không hợp lệ!");
            }
        }


        public static void QuanLiKenh()
        {
            Console.WriteLine("---- Quản lý kênh ----");
            Console.WriteLine("---- 1. Đặt người dẫn chương trình ");
            Console.WriteLine("---- 2. Tổng thời gian mỗi bản tin được phát sóng trong kênh ");
            Console.WriteLine("---- 3. Thoát ");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("--------------------------------------");

            Console.WriteLine("Nhập lựa chọn của bạn:");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    foreach (Anchor check in Anchor.getListAnchors())
                    {
                        Console.WriteLine(check.getName());
                    }
                    Console.WriteLine("Nhập tên người dẫn chương trình :");
                    string nameofAnchor = "";
                    int maxTime1 = 0;
                    foreach (Anchor check in Anchor.getListAnchors())
                    {
                        nameofAnchor = Console.ReadLine();
                        if (nameofAnchor == check.getName())
                        {
                            nameofAnchor = check.getName();
                            break;
                        }
                        if (maxTime1 == 3)
                        {
                            QuanLiKenh();
                            break;
                        }
                        maxTime1++;
                        Console.WriteLine("Nhập sai hoặc kênh không tồn tại , vui lòng nhập lại ");
                    }
                    Console.WriteLine("Nhập tên kênh: VTV1 , VTV2 , VTV3 :");
                    string nameofChannel = "";
                    int maxTime2 = 0;
                    foreach (Channel check in Channel.getChanels())
                    {
                        nameofChannel = Console.ReadLine();
                        if (nameofChannel == check.getName())
                        {
                            check.setAnchor(nameofAnchor);
                            break;
                        }
                        if (maxTime2 == 3)
                        {
                            QuanLiKenh();
                            break;
                        }

                        maxTime2++;
                        Console.WriteLine("Nhập sai hoặc kênh không tồn tại , vui lòng nhập lại ");
                    }

                    break;
                case "2":
                    Console.WriteLine("Nhập tên kênh: VTV1 , VTV2 , VTV3 :");
                    string nameofChannelx = "";
                    int maxTime2x = 0;
                    foreach (Channel check in Channel.getChanels())
                    {
                        nameofChannelx = Console.ReadLine();
                        if (nameofChannelx == check.getName())
                        {
                            TimeSet.totalTime(check.getName());
                            break;
                        }
                        if (maxTime2x == 3)
                        {
                            QuanLiKenh();
                            break;
                        }

                        maxTime2x++;
                        Console.WriteLine("Nhập sai hoặc kênh không tồn tại , vui lòng nhập lại ");
                    }
                    break;
                case "3":
                    Console.WriteLine("Thoát");
                    Program.Menu();
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ");
                    break;
            }
        }

        public static void QuanLiTheLoai()
        {
            Console.WriteLine("---- Quản lý Thể Loại ----");
            Console.WriteLine("---- 1. Hiển thị danh sách thể loại ");
            Console.WriteLine("---- 2. Hiển thị các bản tin thuộc thể loại ");
            Console.WriteLine("---- 3. Thoát ");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("--------------------------------------");

            Console.WriteLine("Nhập lựa chọn của bạn:");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Category.printAllCategory();
                    break;
                case "2":
                    Console.WriteLine("Nhập tên thể loại: ");
                    string nameofCategory = Console.ReadLine(); // Nhập tên thể loại một lần
                    bool categoryFound = false;
                    foreach (Category check in Category.getCategories())
                    {
                        if (nameofCategory == check.getName())
                        {
                            check.printAllBanTin();
                            categoryFound = true;
                            break;
                        }
                    }
                    if (!categoryFound)
                    {
                        Console.WriteLine("Tên thể loại không hợp lệ hoặc không tồn tại.");
                    }
                    break;
                case "3":
                    Console.WriteLine("Thoát");
                    Program.Menu();
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ");
                    break;
            }
        }
        public static void nhapBanTin()
        {
            bool continueInput = true;

            while (continueInput)
            {
                try { 
                Console.WriteLine("Tên bản tin :");
                string name = Console.ReadLine();
                Console.WriteLine("Thời lượng bản tin :");
                double time = double.Parse(Console.ReadLine());
                Console.WriteLine("Nội dung bản tin :");
                string noiDung = Console.ReadLine();
                banTinListNew = DeserializeJsonToObject<BanTinList>(filePath);
                Console.WriteLine(banTinListNew);
                BanTin bantinNew = new BanTin(name, time, noiDung);
                banTinListNew.Add(bantinNew);
                // Serialize
                // SerializeObjectToJsonFile(bantinNew, filePath);
                Category belongstoCategory = null;
                bool validCategory = false;
                foreach (Category iTheLoai in Category.getCategories())
                {
                    Console.WriteLine(iTheLoai.getName());
                }
                while (!validCategory)
                {
                    Console.WriteLine("Thể loại của bản tin :");
                    string categoryName = Console.ReadLine();
                    belongstoCategory = GetCategoryByName(categoryName);
                    if (belongstoCategory == null)
                    {
                        Console.WriteLine("Thể loại không tồn tại. Vui lòng chọn các lựa chọn sau ");
                        Console.WriteLine("1.Tạo thể loại mới ");
                        Console.WriteLine("2.Nhập lại");
                        int choice1 = int.Parse(Console.ReadLine());
                        switch (choice1)
                        {
                            case 1:
                                Console.WriteLine("Nhập tên thể loại muốn tạo :");
                                string nameCategory = Console.ReadLine();
                                Category addCategory = new Category(nameCategory);
                                Console.WriteLine("Đã tạo thành công " + categoryName);

                                bantinNew.setCategoryName(categoryName);
                                break;
                            case 2:
                                break;
                            default:
                                Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại.");
                                break;
                        }
                    }
                    else
                    {
                        validCategory = true;
                    }
                }

                Author belongstoAuthor = null;
                bool validAuthor = false;
                foreach (Author iTacGia in Author.getAuthors())
                {
                    Console.WriteLine(iTacGia.getName());
                }
                while (!validAuthor)
                {
                    Console.WriteLine("Tác giả của bản tin :");
                    string authorName = Console.ReadLine();
                    belongstoAuthor = getAuthorByName(authorName);

                    if (belongstoAuthor == null)
                    {
                        Console.WriteLine("Tác giả không tồn tại. Vui lòng nhập lại tên tác giả.");
                        Console.WriteLine("1.Tạo tác giả mới");
                        Console.WriteLine("2.Nhập lại ");
                        int choice1 = int.Parse(Console.ReadLine());
                        switch (choice1)
                        {
                            case 1:
                                Console.WriteLine("Nhập thông tin để tạo mới tác giả :");
                                Console.WriteLine("Nhập tên công ty :");
                                string inputCompany = Console.ReadLine();
                                Console.WriteLine("Nhập email :");
                                string inputEmail = Console.ReadLine();
                                Author addAuthor = new Author(authorName, inputCompany, inputEmail);
                                Console.WriteLine("Đã tạo thành công " + authorName);
                                bantinNew.setAuthorName(authorName);
                                break;
                            case 2:
                                break;
                            default:
                                Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại.");
                                break;
                        }
                    }
                    else
                    {
                        validAuthor = true;
                    }
                }

                Console.WriteLine("1. Nhập tiếp");
                Console.WriteLine("2. Thoát");
                Console.WriteLine("Lựa chọn của bạn: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        continueInput = true;
                        break;
                    case 2:
                        continueInput = false;
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le, lam on nhap lai");
                        break;
                }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
                    Console.WriteLine("Vui lòng nhập lại.");
                }
            }
        }
        public static void suaBanTin()
        {
            foreach (New iNew in New.listBanTins)
            {
                Console.WriteLine(iNew.getName());
            }
            Console.WriteLine("Tên bản tin: ");
            string thisNew = Console.ReadLine();
            Console.WriteLine("Chọn thông tin muốn sửa: ");
            Console.WriteLine("1. Tên ");
            Console.WriteLine("2. Nội dung ");
            Console.WriteLine("3. Thời gian ");
            Console.WriteLine("0. Thoát ");
            string choice2 = Console.ReadLine();
            switch (choice2)
            {
                case "1":
                    Console.WriteLine("Nhập tên mới cho Bản Tin: ");
                    try
                    {
                        string newName = Console.ReadLine();
                        foreach (New inNew in New.listBanTins)
                        {
                            if (inNew.getName() == thisNew)
                            {
                                inNew.setName(newName);
                                foreach (TimeSet thoigian in TimeSet.listAllTimeSet)
                                {
                                    if (thoigian.NameBanTin == thisNew)
                                        thoigian.setBanTinOfTimeSet(newName);
                                }
                            }
                            inNew.print();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi: " + ex.Message);
                    }
                    break;


                case "2":
                    Console.WriteLine("Nhập nội dung mới cho Bản Tin: ");
                    try
                    {
                        string newContent = Console.ReadLine();
                        foreach (New inNew in New.listBanTins)
                        {
                            if (inNew.getName() == thisNew)
                            {
                                inNew.setNoiDung(newContent); 
                                inNew.print(); 
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi: " + ex.Message);
                    }
                    break;
                case "3":
                    Console.WriteLine("Nhập thời gian chiếu mới: ");
                    try
                    {
                        double newTime = double.Parse(Console.ReadLine());
                        foreach (New inNew in New.listBanTins)
                        {
                            if (inNew.getName() == thisNew)
                            {
                                inNew.setTime(newTime);
                                foreach (TimeSet thoigian in TimeSet.listAllTimeSet)
                                {
                                    if (thoigian.NameBanTin == thisNew)
                                        thoigian.setTime(newTime);
                                    inNew.print();
                                }
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi: " + ex.Message);
                    }
                    break;

                case "0":
                    Console.WriteLine("Thoát");
                    break;

                default:
                    Console.WriteLine("Chức năng không hợp lệ. Hãy chọn lại.");
                    break;
            }

        }

        //đổi vị trí 2 bản tin trong 1 kênh
        // Phương thức hỗ trợ
        public static void SwapBanTin()
        {
            Console.WriteLine("Ngày: ");
            int day = int.Parse(Console.ReadLine());
            Console.WriteLine("Tháng: ");
            int month = int.Parse(Console.ReadLine());
            Console.WriteLine("Nhập tên của kênh: ");
            string nameChannel = Console.ReadLine();
            Console.WriteLine("Chiếu vào buổi : ");
            string period = Console.ReadLine();

            Console.WriteLine("Bản tin ngày " + day + "/" + month + " gồm: ");
            CalendarDay calendarDay = Calendar.getCalendarDay(day, month);
            if (calendarDay == null)
            {
                Console.WriteLine("Không tìm thấy thông tin cho ngày này.");
                return;
            }

            List<Channel> channels = calendarDay.getListChannels();
            Channel targetChannel = null;
            foreach (Channel channel in channels)
            {
                if (channel.getName() == nameChannel)
                {
                    targetChannel = channel;
                    break;
                }
            }
            if (targetChannel == null)
            {
                Console.WriteLine("Không tìm thấy kênh có tên này.");
                return;
            }

            foreach (New newsItem in targetChannel.getListPeriod(period))
            {
                Console.WriteLine(newsItem.getName());
                foreach (TimeSet timeSet in newsItem.getListTime())
                {
                    if (timeSet.NameBanTin == newsItem.getName() && timeSet.NameChannel == targetChannel.getName() && timeSet.Period == period)
                    {
                        Console.WriteLine("Chiếu lúc : " + timeSet.getTimeStart().ToString("HH:mm:ss"));
                    }
                }
            }

            Console.WriteLine("Nhập tên bản tin muốn đổi vị trí: ");
            Console.WriteLine("Bản Tin 1: ");
            string nameNew1 = Console.ReadLine();
            Console.WriteLine("Bản Tin 2: ");
            string nameNew2 = Console.ReadLine();

            List<New> newsList = targetChannel.getListPeriod(period);
            int index1 = -1;
            int index2 = -1;

            for (int i = 0; i < newsList.Count; i++)
            {
                if (newsList[i].getName() == nameNew1)
                {
                    index1 = i;
                }
                if (newsList[i].getName() == nameNew2)
                {
                    index2 = i;
                }
            }

            if (index1 != -1 && index2 != -1)
            {
                New tempNews = newsList[index1];
                New temp = newsList[index1];
                newsList[index1] = newsList[index2];
                newsList[index2] = temp;

                DateTime iTimeStart;

                for (int currentIndex = 0; currentIndex < targetChannel.getListPeriod(period).Count; currentIndex++)
                {
                    New newsItem = targetChannel.getListPeriod(period)[currentIndex];
                    New previousNewsItem = currentIndex > 0 ? targetChannel.getListPeriod(period)[currentIndex - 1] : null;
                    string newsItemName = newsItem.getName();
                    string previousNewsItemName = previousNewsItem != null ? previousNewsItem.getName() : null;

                    if (period == "sang")
                    {
                        iTimeStart = new DateTime(2024, month, day, 8, 00, 00);
                    }
                    else
                    {
                        iTimeStart = new DateTime(2024, month, day, 16, 00, 00);
                    }

                    double jtime = 0;

                    foreach (TimeSet sTime in TimeSet.listAllTimeSet)
                    {
                        if (previousNewsItemName == sTime.NameBanTin && nameChannel == sTime.NameChannel && day == sTime.getDay() && month == sTime.getMonth())
                        {
                            iTimeStart = sTime.getTimeStart();
                            jtime = sTime.getTime();
                        }
                    }

                    foreach (TimeSet sTime in TimeSet.listAllTimeSet)
                    {
                        if (newsItemName == sTime.NameBanTin && nameChannel == sTime.NameChannel && day == sTime.getDay() && month == sTime.getMonth())
                            sTime.setTimeStart(iTimeStart.AddSeconds(jtime));
                    }
                }
                Console.WriteLine("Đã đổi vị trí và timeStart của hai bản tin trong danh sách.");
            }

            else
            {
                Console.WriteLine("Không tìm thấy bản tin cần đổi vị trí.");
            }
            foreach (New newsItem in targetChannel.getListPeriod(period))
            {
                Console.WriteLine(newsItem.getName());
                foreach (TimeSet timeSet in newsItem.getListTime())
                {
                    if (timeSet.NameBanTin == newsItem.getName() && timeSet.NameChannel == targetChannel.getName())
                    {
                        Console.WriteLine("Chiếu lúc : " + timeSet.getTimeStart().ToString("HH:mm:ss"));
                    }
                }
            }
        }
        public static void SerializeObjectToJsonFile<T>(T obj, string filePath)
        {
            // Serialize đối tượng thành chuỗi JSON
            string json = JsonSerializer.Serialize(obj, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All)
            });

            try
            {
                // Ghi chuỗi JSON vào tệp văn bản
                File.WriteAllText(filePath, json, System.Text.Encoding.UTF8);
                Console.WriteLine($"Dữ liệu JSON đã được ghi vào file {filePath}");
            }
            catch (IOException ex)
            {
                Console.WriteLine("Lỗi ghi: " + ex.Message);
            }
        }

        public static T DeserializeJsonToObject<T>(string filePath)
        {
            try
            {
                // Đọc dữ liệu từ tệp JSON
                string jsonData = File.ReadAllText(filePath);

                // Deserialize dữ liệu từ tệp thành đối tượng có kiểu dữ liệu T
                T deserializedObject = JsonSerializer.Deserialize<T>(jsonData);

                if (deserializedObject != null)
                {
                    // Hiển thị thông tin của đối tượng sau khi deserialize
                    Console.WriteLine($"Đối tượng đã được deserialize từ tệp {filePath}:");
                    Console.WriteLine(deserializedObject);

                    // Trả về đối tượng đã deserialize
                    return deserializedObject;
                }
                else
                {
                    Console.WriteLine("Dữ liệu deserialized là null.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi đọc dữ liệu từ tệp {filePath}: {ex.Message}");
            }

            // Trả về một giá trị mặc định nếu có lỗi xảy ra hoặc không deserialize được
            return default(T);
        }

        public static Author getAuthorByName(string authorName)
        {
            foreach (Author author in Author.getAuthors())
            {
                if (author.getName() == authorName)
                {
                    return author;
                }
            }

            return null; // Trả về null nếu không tìm thấy đối tượng Category có tên tương ứng
        }
        public static Category GetCategoryByName(string categoryName)
        {
            foreach (Category category in Category.getCategories())
            {
                if (category.name == categoryName)
                {
                    return category;
                }
            }

            return null; // Trả về null nếu không tìm thấy đối tượng Category có tên tương ứng
        }

        public delegate void SwapNewsItems(List<New> newsList, int index1, int index2);

        public static void SwapBanTinWithDelegate()
        {
            Console.WriteLine("Ngày: ");
            int day = int.Parse(Console.ReadLine());
            Console.WriteLine("Tháng: ");
            int month = int.Parse(Console.ReadLine());
            Console.WriteLine("Nhập tên của kênh: ");
            string nameChannel = Console.ReadLine();
            Console.WriteLine("Chiếu vào buổi : ");
            string period = Console.ReadLine();

            Console.WriteLine("Bản tin ngày " + day + "/" + month + " gồm: ");
            CalendarDay calendarDay = Calendar.getCalendarDay(day, month);
            if (calendarDay == null)
            {
                Console.WriteLine("Không tìm thấy thông tin cho ngày này.");
                return;
            }

            List<Channel> channels = calendarDay.getListChannels();
            Channel targetChannel = null;
            foreach (Channel channel in channels)
            {
                if (channel.getName() == nameChannel)
                {
                    targetChannel = channel;
                    break;
                }
            }
            if (targetChannel == null)
            {
                Console.WriteLine("Không tìm thấy kênh có tên này.");
                return;
            }

            foreach (New newsItem in targetChannel.getListPeriod(period))
            {
                Console.WriteLine(newsItem.getName());
                foreach (TimeSet timeSet in newsItem.getListTime())
                {
                    if (timeSet.NameBanTin == newsItem.getName() && timeSet.NameChannel == targetChannel.getName() && timeSet.Period == period)
                    {
                        Console.WriteLine("Chiếu lúc : " + timeSet.getTimeStart().ToString("HH:mm:ss"));
                    }
                }
            }

            List<New> newsList = targetChannel.getListPeriod(period);

            Console.WriteLine("Nhập tên bản tin muốn đổi vị trí: ");
            Console.WriteLine("Bản Tin 1: ");
            string nameNew1 = Console.ReadLine();
            Console.WriteLine("Bản Tin 2: ");
            string nameNew2 = Console.ReadLine();

            int index1 = -1;
            int index2 = -1;

            for (int i = 0; i < newsList.Count; i++)
            {
                if (newsList[i].getName() == nameNew1)
                {
                    index1 = i;
                }
                if (newsList[i].getName() == nameNew2)
                {
                    index2 = i;
                }
            }

            if (index1 != -1 && index2 != -1)
            {
                SwapNewsItems swapDelegate = SwapNewsItemsInList;
                swapDelegate(newsList, index1, index2);

                UpdateTimeStartForChannel(targetChannel, nameChannel, period, day, month, newsList);

                Console.WriteLine("Đã đổi vị trí và timeStart của hai bản tin trong danh sách.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy bản tin cần đổi vị trí.");
            }

            PrintNewsItems(targetChannel, period);
        }

        private static void SwapNewsItemsInList(List<New> newsList, int index1, int index2)
        {
            New temp = newsList[index1];
            newsList[index1] = newsList[index2];
            newsList[index2] = temp;
        }
        private static void UpdateTimeStartForChannel(Channel channel, string nameChannel, string period, int day, int month, List<New> newsList)
        {
            DateTime iTimeStart;

            for (int currentIndex = 0; currentIndex < channel.getListPeriod(period).Count; currentIndex++)
            {
                New newsItem = channel.getListPeriod(period)[currentIndex];
                New previousNewsItem = currentIndex > 0 ? channel.getListPeriod(period)[currentIndex - 1] : null;
                string newsItemName = newsItem.getName();
                string previousNewsItemName = previousNewsItem != null ? previousNewsItem.getName() : null;

                if (period == "sang")
                {
                    iTimeStart = new DateTime(2024, month, day, 8, 00, 00);
                }
                else
                {
                    iTimeStart = new DateTime(2024, month, day, 16, 00, 00);
                }

                double jtime = 0;

                foreach (TimeSet sTime in TimeSet.listAllTimeSet)
                {
                    if (previousNewsItemName == sTime.NameBanTin && nameChannel == sTime.NameChannel && day == sTime.getDay() && month == sTime.getMonth())
                    {
                        iTimeStart = sTime.getTimeStart();
                        jtime = sTime.getTime();
                    }
                }

                foreach (TimeSet sTime in TimeSet.listAllTimeSet)
                {
                    if (newsItemName == sTime.NameBanTin && nameChannel == sTime.NameChannel && day == sTime.getDay() && month == sTime.getMonth())
                        sTime.setTimeStart(iTimeStart.AddSeconds(jtime));
                }
            }
        }

        private static void PrintNewsItems(Channel targetChannel, string period)
        {
            foreach (New newsItem in targetChannel.getListPeriod(period))
            {
                Console.WriteLine(newsItem.getName());
                foreach (TimeSet timeSet in newsItem.getListTime())
                {
                    if (timeSet.NameBanTin == newsItem.getName() && timeSet.NameChannel == targetChannel.getName())
                    {
                        Console.WriteLine("Chiếu lúc : " + timeSet.getTimeStart().ToString("HH:mm:ss"));
                    }
                }
            }
        }
    }
}
