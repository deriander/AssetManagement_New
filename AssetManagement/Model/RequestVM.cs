using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Model
{
    public class RequestVM
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public Boolean Approval_1 { get; set; }
        public Boolean Approval_2 { get; set; }
        public string Status_Approval { get; set; }
        public DateTimeOffset Request_Date { get; set; }
        public string Brand { get; set; }
        public string Cpu { get; set; }
        public string Gpu { get; set; }
        public string Ram { get; set; }
        public string Display { get; set; }
        public string Storage { get; set; }
        public string Os { get; set; }
        public int User_Id { get; set; }
    }
}
