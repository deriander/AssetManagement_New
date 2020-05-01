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
    public class BorrowRepository : GeneralRepository<Borrow, MyContext>
    {
        private readonly MyContext _myContext;

        public BorrowRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            _configuration = configuration;
            _myContext = myContext;
        }

        DynamicParameters parameters = new DynamicParameters();
        IConfiguration _configuration { get; }

        public async Task<IEnumerable<Borrow>> GetApproval1()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procedureName = "SP_GetApproval1_Borrow";
                var data = await connection.QueryAsync<Borrow>(procedureName, commandType: CommandType.StoredProcedure);
                return data;
            }
        }

        public async Task<IEnumerable<Borrow>> GetApproval2()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procedureName = "SP_GetApproval2_Borrow";
                var data = await connection.QueryAsync<Borrow>(procedureName, commandType: CommandType.StoredProcedure);
                return data;
            }
        }
    }
}
