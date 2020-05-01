using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Base
{
    public interface IEntity
    {
        int Id { get; set; }
        //bool Is_Delete { get; set; }
        //DateTimeOffset Delete_Date { get; set; }
    }
}
