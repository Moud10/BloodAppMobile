using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BloodAPP.Models
{
    public class BloodUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string BloodGroup { get; set; }
        public string ImagePath { get; set; }
        public string FullLogoPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                {
                    return string.Empty;
                }
                string fullPath = string.Format("https://192.168.0.108:45455/{0}",ImagePath.Substring(2));
                return fullPath;
            }
        }
        public string imgTemp
        {
            get
            {
                return "https://thumbor.forbes.com/thumbor/fit-in/416x416/filters%3Aformat%28jpg%29/https%3A%2F%2Fspecials-images.forbesimg.com%2Fimageserve%2F5ec595d45f39760007b05c07%2F0x0.jpg%3Fbackground%3D000000%26cropX1%3D989%26cropX2%3D2480%26cropY1%3D74%26cropY2%3D1564";
            }
        }
        public int Date { get; set; }
        public object ImageArray { get; set; }
    }
}
