using BoardAndBarber.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace BoardAndBarber.Data
{
    public class CustomerRepository
    {
        static List<Customer> _customers = new List<Customer>();

        public void Add(Customer customerToAdd)
        {
            int newId = 1;
            if (_customers.Count > 0)
            {
                //get the next id by finding the max current id
                newId = _customers.Select(customer => customer.Id).Max() + 1;
            }

            customerToAdd.Id = newId;

            _customers.Add(customerToAdd);
        }

        public List<Customer> GetAll()
        {
            return _customers;
        }

        public Customer GetById(int id)
        {
            var connection = new SqlConnection("Server = localhost; Database = BoardAndBarber; Trusted_Connection = True;");
            connection.Open();

            var command = connection.CreateCommand();
            var query = $@"select *
                          from Customers
                          where id = {id}";

            command.CommandText = query;

            //run this query, and i don't care about the results
            //command.ExecuteNonQuery();

            //run this query, and only return the top row's leftmost column
            //command.ExecuteScalar();

            //run this query and give me the results one row at a time
            var reader = command.ExecuteReader();
            //sql server has executed the command and is waiting to give us results
            //reader.Read(); <-- boolean value based on if data was found in database

            if (reader.Read())
            {
                var customerFromDb = new Customer();
                //do something
                customerFromDb.Id = (int) reader["Id"]; //explicit conversion/cast, throws exception on failure
                customerFromDb.Name = reader["Name"] as string; // implicit cast/conversion, returns a null on failure
                customerFromDb.Birthday = DateTime.Parse(reader["Birthday"].ToString()); //parsing
                customerFromDb.FavoriteBarber = reader["FavoriteBarber"].ToString(); //make it a string
                customerFromDb.Notes = reader["Notes"].ToString();

                return customerFromDb;
            }
            else
            {
                return null;
            }
        }

        public Customer Update(int id, Customer customer)
        {
            var customerToUpdate = GetById(id);

            customerToUpdate.Birthday = customer.Birthday;
            customerToUpdate.FavoriteBarber = customer.FavoriteBarber;
            customerToUpdate.Name = customer.Name;
            customerToUpdate.Notes = customer.Notes;

            return customerToUpdate;
        }

        public void Remove(int id)
        {
            var customerToDelete = GetById(id);

            _customers.Remove(customerToDelete);
        }
    }
}
