using AssetManagement.Context;
using AssetManagement.Model;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Repository.Data
{
    public class UserRepository : GeneralRepository<User, MyContext>
    {
        private readonly MyContext _myContext;

        public UserRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            _configuration = configuration;
            _myContext = myContext;
        }

        DynamicParameters parameters = new DynamicParameters();
        IConfiguration _configuration { get; }

        public async Task<User> SignIn(User model)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procedureName = "SP_SignIn_User";
                parameters.Add("@email", model.Email);
                var data = await connection.QueryFirstOrDefaultAsync<User>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return data;
            }
        }
    }
}
