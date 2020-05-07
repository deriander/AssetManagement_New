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
    public class ReturnRepository : GeneralRepository<Return, MyContext>
    {
        private readonly MyContext _myContext;

        public ReturnRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            _configuration = configuration;
            _myContext = myContext;
        }

        DynamicParameters parameters = new DynamicParameters();
        IConfiguration _configuration { get; }

        public async Task<IEnumerable<ReturnVM>> GetReturnUser(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procedureName = "SP_GetReturnUser_Return";
                parameters.Add("@id", id);
                var data = await connection.QueryAsync<ReturnVM>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return data;
            }
        }

        public async Task<IEnumerable<ReturnVM>> GetReturnAdmin()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procedureName = "SP_GetReturnAdmin_Return";
                var data = await connection.QueryAsync<ReturnVM>(procedureName, commandType: CommandType.StoredProcedure);
                return data;
            }
        }

        public async Task<Return> GetReturnByItemId(int item_id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procedureName = "SP_GetReturnByItemId_Return";
                parameters.Add("@item_id", item_id);
                var data = await connection.QueryFirstOrDefaultAsync<Return>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return data;
            }
        }
    }
}
