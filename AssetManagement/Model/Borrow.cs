using AssetManagement.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Model
{
    [Table("TB_T_Borrow")]
    public class Borrow : IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset Borrow_Date { get; set; }
        public Boolean Approval_1 { get; set; }
        public Boolean Approval_2 { get; set; }
        public string Status_Approval { get; set; }

        public User User { get; set; }
        [ForeignKey("User")]
        public int User_Id { get; set; }

        public Item Item { get; set; }
        [ForeignKey("Item")]
        public int Item_Id { get; set; }
    }
}
