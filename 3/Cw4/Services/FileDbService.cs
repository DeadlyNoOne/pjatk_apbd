using Cw4.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cw4.Services
{
    public class FileDbService : IFileDbService
    {
        private string _conString = @" Server=127.0.0.1,1433;
                                                  Database=PJATK;
                                                  User Id=SA;
                                                  Password=Hasloto123";
        public Animal AddAnimal(Animal animal)
        {
            using (var con = new SqlConnection(_conString))
            {
                var com = new SqlCommand(
                    $"INSERT INTO Animal (Name, Description, Category, Area) OUTPUT inserted.IdAnimal " +
                    $"VALUES (@param2, @param3, @param4, @param5) ", con);
                com.Parameters.AddWithValue("@param2", animal.Name);
                com.Parameters.AddWithValue("@param3", animal.Description);
                com.Parameters.AddWithValue("@param4", animal.Category);
                com.Parameters.AddWithValue("@param5", animal.Area);
                con.Open();
                var dr = com.ExecuteReader();
                if(dr.Read())
                animal.IdAnimal = int.Parse(dr["IdAnimal"].ToString());
            }

            return animal;
        }

        public int DeleteAnimal(string idAnimal)
        {
            using (var con = new SqlConnection(_conString))
            {
                var com = new SqlCommand($"SELECT COUNT(*) as count FROM Animal WHERE IdAnimal = @IdAnimal ", con);
                com.Parameters.AddWithValue("@IdAnimal", idAnimal);
                con.Open();
                var dr = com.ExecuteReader();

                while (dr.Read())
                {
                    if (int.Parse(dr["count"].ToString()) == 0) return 1;
                }
            }
            using (var con = new SqlConnection(_conString))
            {
                var com = new SqlCommand($"DELETE FROM Animal WHERE IdAnimal = @IdAnimal ", con);
                com.Parameters.AddWithValue("@IdAnimal", idAnimal);
                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine(dr.ToString());
                }
            }

            return 0;
        }

        public Animal GetAnimal(string idAnimal)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Animal> GetAnimals(string orderBy)
        {
            var res = new List<Animal>();
            //System.Data.SqlClient
            using (var con = new SqlConnection(_conString))
            {
                var com = new SqlCommand($"SELECT * FROM Animal ORDER BY case @param " +
                                                                        $"when 'Name' then Name " +
                                                                        $"when 'Description' then Description " +
                                                                        $"when 'Category' then Category " +
                                                                        $"when 'Area' then Area " +
                                                                        $"end ASC", con);

                if (orderBy != null)
                {
                    com.Parameters.AddWithValue("@param", orderBy);
                }
                else
                {
                    com.Parameters.AddWithValue("@param", "Name");
                }
                 
                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    res.Add(new Animal
                    {
                        IdAnimal = int.Parse(dr["IdAnimal"].ToString()),
                        Name = dr["Name"].ToString(),
                        Description = dr["Description"].ToString(),
                        Category = dr["Category"].ToString(),
                        Area = dr["Area"].ToString(),
                    }); ;
                }
            }
            return res;
        }


        public int UpdateAnimal(Animal animal, string idAnimal)
        {

            using (var con = new SqlConnection(_conString))
            {
                var com = new SqlCommand($"SELECT COUNT(*) as count FROM Animal WHERE IdAnimal = @IdAnimal ", con);
                com.Parameters.AddWithValue("@IdAnimal", idAnimal);
                con.Open();
                var dr = com.ExecuteReader();

                while (dr.Read())
                {
                    if (int.Parse(dr["count"].ToString()) == 0) return 1;
                }
            }


            using (var con = new SqlConnection(_conString))
            {
                var com = new SqlCommand(
                    $"UPDATE Animal SET Name = @name, Description = @description, Category = @category, Area = @area" +
                    $" WHERE IdAnimal = @IdAnimal ", con);
                com.Parameters.AddWithValue("@name", animal.Name);
                com.Parameters.AddWithValue("@description", animal.Description);
                com.Parameters.AddWithValue("@category", animal.Category);
                com.Parameters.AddWithValue("@area", animal.Area);
                com.Parameters.AddWithValue("@IdAnimal", idAnimal);
                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine(dr.ToString());
                }
            }

            return 0;
        }


        }
    }
