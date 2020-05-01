using AssetManagement.Context;
using AssetManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Repository.Data
{
    public class ReturnRepository : GeneralRepository<Return, MyContext>
    {

        public ReturnRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
