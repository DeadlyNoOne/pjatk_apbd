using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Warehouse.Models;

namespace Warehouse.Services
{

    public class DbServiceWithProc : IDbServiceWithProc
    {

        private readonly IConfiguration _configuration;

        public DbServiceWithProc(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> addToWarehouseAsync(ProductWarehouse warehouseProduct)
        {

            using (var con = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                await con.OpenAsync();
                SqlCommand command = con.CreateCommand();
                command.Connection = con;
                command.CommandText = "AddProductToWarehouse";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@IdProduct", warehouseProduct.IdProduct);
                command.Parameters.AddWithValue("@CreatedAt", warehouseProduct.CreatedAt);
                command.Parameters.AddWithValue("@IdWarehouse", warehouseProduct.IdWarehouse);
                command.Parameters.AddWithValue("@Amount", warehouseProduct.Amount);

                var primaryKey = int.Parse((await command.ExecuteScalarAsync()).ToString());
                return primaryKey;


            }
        }
    }
}
