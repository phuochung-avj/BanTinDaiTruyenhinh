using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Text;

namespace BanTin
{
    public class Service
    {
        private string userPath = "C:\\Users\\ASUS\\source\\tét\\bantin\\BanTin-master\\BanTin\\users.json";
        public Boolean Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }
            else
            {
                try
                {
                    // Đọc nội dung của tệp JSON vào một chuỗi
                    string json = File.ReadAllText(userPath);
                    // Deserialize chuỗi JSON thành một đối tượng tương ứng
                    Dictionary<string, List<Dictionary<string, string>>> jsonObject = JsonSerializer.Deserialize<Dictionary<string, List<Dictionary<string, string>>>>(json);

                    // Truy cập vào danh sách các user từ jsonObject
                    if (jsonObject.ContainsKey("user"))
                    {
                        foreach (Dictionary<string, string> user in jsonObject["user"])
                        {
                            // Truy cập vào thuộc tính username và password của user
                            string _username = user["username"];
                            string _password = user["password"];

                            if (_username != null && _password != null && username == _username && password == _password)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No 'user' key found in the JSON data.");
                        return false;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }

    }
}