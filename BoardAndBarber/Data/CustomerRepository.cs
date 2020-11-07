using BoardAndBarber.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace BoardAndBarber.Data
{
    public class CustomerRepository
    {
        readonly string _connectionString;
        public CustomerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BoardAndBarber");
        }

        public void Add(Customer customerToAdd)
        {
            var sql = @"INSERT INTO [dbo].[Customers]
                               ([Name]
                               ,[Birthday]
                               ,[FavoriteBarber]
                               ,[Notes])
                         Output inserted.Id
                         VALUES
                               (@name, @birthday, @favoritebarber, @notes)";
        
            using var db = new SqlConnection(_connectionString);

            var newId = db.ExecuteScalar<int>(sql, customerToAdd);

            customerToAdd.Id = newId;
        }

        public IEnumerable<Customer> GetAll()
        {
            using var db = new SqlConnection(_connectionString);

            try
            {
                var customers = db.Query<Customer>("select * from Customers");

                return customers;
            }
            catch (SqlException)
            {
                Console.WriteLine("Sql is broken");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                Console.WriteLine("GetAll was called");
            }
        }

        public Customer GetById(int customerId)
        {
            using var db = new SqlConnection(_connectionString);

            var query = @"select *
                      from Customers
                      where id = @cid";

            var parameters = new { cid = customerId };

            var customer = db.QueryFirstOrDefault<Customer>(query, parameters);

            return customer;
        }

        public Customer Update(int id, Customer customer)
        {
            var sql = @"UPDATE [dbo].[Customers]
                          SET [Name] = @name
                             ,[Birthday] = @birthday
                             ,[FavoriteBarber] = @favoritebarber
                             ,[Notes] = @notes
                        Output inserted.*
                        WHERE id = @id";

            using var db = new SqlConnection(_connectionString);
            
            // if property names should match the name of the property or variable on the right, you don't have to specify.
            // you can leave off the property name if the name on the right and the propery name match.
            var parameters = new 
            { 
                customer.Name, // Name = customer.Name
                customer.Birthday, // Birthday = customer.Birthday
                customer.FavoriteBarber, // FavoriteBarber = customer.FavoriteBarber
                customer.Notes, // Notes = customer.Notes
                id // id = id
            }; 

            var updatedCustomer = db.QueryFirstOrDefault<Customer>(sql, parameters);

            return updatedCustomer;
        }

        public void Remove(int customerId)
        {
            var sql = @"DELETE 
                        FROM [dbo].[Customers]
                        WHERE Id = @id";

            using var db = new SqlConnection(_connectionString);

            db.Execute(sql, new { id = customerId});
        }
    }
}
