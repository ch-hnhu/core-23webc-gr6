using System.Collections.Generic;
namespace core_group_ex_01.Models
{
    //Kieu
    // tao class AppConfig de luu cau hinh tu file appsettings.json
    // file appsettings.json la file text. De chuong trinh map du lieu Json -> object C# thi can tao class nay
    public class AppConfig
    {
        public long MaxFileSize { get; set; }
        public List<string> BannerIPs { get; set; } = new();
    }
}
