using AssetManagement.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace AssetManagement.Model
{
    [Table("TB_M_User")]
    public class User : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Phone_Number { get; set; }
        public string Address { get; set; }
        public DateTime Birth_Date { get; set; }
        public int Role_Id { get; set; }
    }
}
