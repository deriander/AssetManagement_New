using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using AssetManagement.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManagement.Model
{
    [Table("TB_M_Item")]
    public class Item : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Cpu{ get; set; }
        public string Gpu { get; set; }
        public string Ram { get; set; }
        public string Display { get; set; }
        public string Storage { get; set; }
        public string Os { get; set; }
        public bool Status { get; set; }
        public bool Is_Delete { get; set; }
        public DateTimeOffset Create_Date { get; set; }
        public Nullable<DateTimeOffset> Update_Date { get; set; }
        public Nullable<DateTimeOffset> Delete_Date { get; set; }
    }
}
