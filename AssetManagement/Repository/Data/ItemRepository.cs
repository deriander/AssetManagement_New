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
    public class ItemRepository : GeneralRepository<Item, MyContext>
    {
        private readonly MyContext _myContext;

        public ItemRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            _configuration = configuration;
            _myContext = myContext;
        }

        DynamicParameters parameters = new DynamicParameters();
        IConfiguration _configuration { get; }

        public async Task<List<Item>> GetItemAdmin()
        {
            return await _myContext.Set<Item>().Where(x => x.Is_Delete == false).ToListAsync();
        }

        public async Task<IEnumerable<Request>> GetItemUser()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procedureName = "SP_GetItemUser_Item";
                var data = await connection.QueryAsync<Request>(procedureName, commandType: CommandType.StoredProcedure);
                return data;
            }
        }


    }
}

