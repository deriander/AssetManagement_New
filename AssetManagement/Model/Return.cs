using AssetManagement.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Model
{
    [Table("TB_T_Return")]
    public class Return : IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset Return_Date { get; set; }
        public string Condition { get; set; }
        public string Status { get; set; }

        public User User { get; set; }
        [ForeignKey("User")]
        public int User_Id { get; set; }

        public Item Item { get; set; }
        [ForeignKey("Item")]
        public int Item_Id { get; set; }

    }
}
