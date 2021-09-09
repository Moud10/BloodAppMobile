using System;
using System.Collections.Generic;
using System.Text;

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
        public int Date { get; set; }
        public object ImageArray { get; set; }
    }
}
