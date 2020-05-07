using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Model
{
    public class ReturnVM
    {
        // table user
        public string Fullname { get; set; }

        // table return
        public int Id { get; set; }
        public DateTimeOffset Return_Date { get; set; }
        public string Condition { get; set; }
        public string Status { get; set; }
        public int User_Id { get; set; }
        public int Item_Id { get; set; }

        // table item
        public string Brand { get; set; }
        public string Cpu { get; set; }
        public string Gpu { get; set; }
        public string Ram { get; set; }
        public string Display { get; set; }
        public string Storage { get; set; }
        public string Os { get; set; }
    }
}
