using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Warehouse.Models;

namespace Warehouse.Services
{

    public class DbService : IDbService
    {

        private readonly IConfiguration _configuration;

        public DbService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> addToWarehouseAsync(ProductWarehouse warehouseProduct)
        {
            int idOrder;
            double price;

            using (var con = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var com = new SqlCommand("SELECT Price FROM Product WHERE IdProduct = @productId", con);
                com.Parameters.AddWithValue("@productId", warehouseProduct.IdProduct);
                await con.OpenAsync();
                var result = await com.ExecuteReaderAsync();

                //return result.HasRows;
                if (!result.HasRows)
                {
                    throw new Exception("There is no product with given ID.");
                }
                await result.ReadAsync();
                price = double.Parse((result["Price"].ToString()));
                com.Parameters.Clear();
                await result.CloseAsync();

            }

            using (var con = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var com = new SqlCommand("SELECT IdWarehouse FROM Warehouse WHERE IdWarehouse = @warehouseId", con);
                com.Parameters.AddWithValue("@warehouseId", warehouseProduct.IdWarehouse);
                await con.OpenAsync();
                var result = await com.ExecuteReaderAsync();
                //return result.HasRows;
                if (!result.HasRows)
                {
                    throw new Exception("There is no warehouse with given ID.");

                }
                com.Parameters.Clear();

            }

            // Amount validation is in model structure

            using (var con = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var com = new SqlCommand("SELECT TOP 1 [Order].IdOrder FROM [Order] " +
                "LEFT JOIN Product_Warehouse ON [Order].IdOrder = Product_Warehouse.IdOrder " +
                "WHERE [Order].IdProduct = @IdProduct " +
                "AND [Order].Amount = @Amount " +
                "AND Product_Warehouse.IdProductWarehouse IS NULL " +                      // sprawdzenie czy nie jest już zrealizowane
                "AND [Order].CreatedAt < @CreatedAt", con);

                com.Parameters.AddWithValue("IdProduct", warehouseProduct.IdProduct);
                com.Parameters.AddWithValue("Amount", warehouseProduct.Amount);
                com.Parameters.AddWithValue("CreatedAt", warehouseProduct.CreatedAt);

                await con.OpenAsync();
                var result = await com.ExecuteReaderAsync();
                //return result.HasRows;
                if (!result.HasRows)
                {
                    throw new Exception("There is no such order.");
                }
                com.Parameters.Clear();
                await result.ReadAsync();
                idOrder = int.Parse(result["IdOrder"].ToString());
                await result.CloseAsync();
            }


            using (var con = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                await con.OpenAsync();
                var transaction = await con.BeginTransactionAsync();
                SqlCommand command = con.CreateCommand();
                command.Connection = con;
                command.Transaction = transaction as SqlTransaction;
                try
                {

                    command.CommandText = "UPDATE [Order] SET FulfilledAt = @CreatedAt WHERE IdOrder = @IdOrder";
                    command.Parameters.AddWithValue("CreatedAt", warehouseProduct.CreatedAt);
                    command.Parameters.AddWithValue("IdOrder", idOrder);

                    int rowsUpdated = await command.ExecuteNonQueryAsync();

                    if (rowsUpdated < 1) throw new Exception("Error while setting the status of this order.");
                    command.Parameters.Clear();

                    command.CommandText = "INSERT INTO Product_Warehouse (idwarehouse, " +
                        "idproduct, idorder, amount, price, createdat)" +
                        " output INSERTED.idproductwarehouse VALUES" +
                        " (@warehouseID, @productId2, @orderId, @amount, @price, @createdAt)";

                    command.Parameters.AddWithValue("@warehouseID", warehouseProduct.IdWarehouse);
                    command.Parameters.AddWithValue("@createdAt", warehouseProduct.CreatedAt);
                    command.Parameters.AddWithValue("@orderId", idOrder);
                    command.Parameters.AddWithValue("@productId2", warehouseProduct.IdProduct);
                    command.Parameters.AddWithValue("@amount", warehouseProduct.Amount);
                    command.Parameters.AddWithValue("@price", price * warehouseProduct.Amount);

                    int rowsInserted = await command.ExecuteNonQueryAsync();

                    if (rowsInserted < 1) throw new Exception("No change to the record had been found since it entered the database.");

                    var primaryKey = int.Parse((await command.ExecuteScalarAsync()).ToString());
                    await transaction.CommitAsync();
                    await con.CloseAsync();
                    return primaryKey;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);
                    await con.CloseAsync();
                    try
                    {
                        await transaction.RollbackAsync();
                        await con.CloseAsync();
                        throw new Exception("Whoops, something went wrong.");
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                        await con.CloseAsync();
                        throw new Exception("Whoops, something went wrong.");
                    }
                }
            }
        }
    }
}
