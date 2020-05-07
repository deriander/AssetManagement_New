using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Model
{
    public class BorrowVM
    {
        // table user
        public string Fullname { get; set; }
        public string Email { get; set; }

        // table borrow
        public int Id { get; set; }
        public DateTimeOffset Borrow_Date { get; set; }
        public Boolean Approval_1 { get; set; }
        public Boolean Approval_2 { get; set; }
        public string Status_Approval { get; set; }
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
