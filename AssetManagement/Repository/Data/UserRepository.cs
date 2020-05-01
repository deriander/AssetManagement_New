using AssetManagement.Context;
using AssetManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Repository.Data
{
    public class UserRepository : GeneralRepository<User, MyContext>
    {
        public UserRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
