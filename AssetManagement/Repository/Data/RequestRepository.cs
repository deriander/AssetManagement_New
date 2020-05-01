using AssetManagement.Context;
using AssetManagement.Model;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Repository.Data
{
    public class RequestRepository : GeneralRepository<Request, MyContext>
    {
        private readonly MyContext _myContext;

        public RequestRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            _configuration = configuration;
            _myContext = myContext;
        }

        DynamicParameters parameters = new DynamicParameters();
        IConfiguration _configuration { get; }

        public async Task<IEnumerable<Request>> GetApproval1()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procedureName = "SP_GetApproval1_Request";
                var data = await connection.QueryAsync<Request>(procedureName, commandType: CommandType.StoredProcedure);
                return data;
            }
        }

        public async Task<IEnumerable<Request>> GetApproval2()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procedureName = "SP_GetApproval2_Request";
                var data = await connection.QueryAsync<Request>(procedureName, commandType: CommandType.StoredProcedure);
                return data;
            }
        }
    }
}
