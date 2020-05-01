using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Model
{
    public class BorrowVM
    {
        public int Id { get; set; }
        public DateTimeOffset Borrow_Date { get; set; }
        public Boolean Approval_1 { get; set; }
        public Boolean Approval_2 { get; set; }
        public string Status_Approval { get; set; }
        public int User_Id { get; set; }
        public int Item_Id { get; set; }
        public Boolean Status { get; set; }
    }
}
