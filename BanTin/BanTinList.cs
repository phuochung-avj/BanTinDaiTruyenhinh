using System;
using System.Collections.Generic;
using System.Text.Json;

namespace BanTin
{
    public class BanTinList
    {
        public BanTinList()
        {
        }

        public List<BanTin> Bantins { get; set; } = new List<BanTin>();

        public string SerializeToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        }

        // Phương thức để deserialize chuỗi JSON thành đối tượng BanTinList
        public static BanTinList DeserializeFromJson(string json)
        {
            return JsonSerializer.Deserialize<BanTinList>(json);
        }

        public void Add(BanTin banTin)
        {
            Bantins.Add(banTin);
        }
    }
}
