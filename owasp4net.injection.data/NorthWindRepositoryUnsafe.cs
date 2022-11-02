using Microsoft.Data.SqlClient;
using owasp4net.injection.domain;
using System;
using System.Collections.Generic;

namespace owasp4net.injection.data
{
    public class NorthWindRepositoryUnsafe : INorthWindRepository
    {
        private string _connectionString;

        public NorthWindRepositoryUnsafe(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException("connectionString");

            this._connectionString = connectionString;
        }

        public IEnumerable<Product> LoadProducts(string CategoryId)
        {
            List<Product> products = new List<Product>();

            string query = "SELECT * FROM Products";

            if (!string.IsNullOrWhiteSpace(CategoryId))
                query += " WHERE CategoryId = " + CategoryId;

            using (var connection = new SqlConnection(this._connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                    products.Add(new Product()
                    {
                        ProductID = System.Convert.ToInt32(reader[0]),
                        ProductName = System.Convert.ToString(System.Convert.IsDBNull(reader[1]) ? null : reader[1]),
                        SupplierID = System.Convert.ToInt32(System.Convert.IsDBNull(reader[2]) ? -1 : reader[2]),
                        CategoryID = System.Convert.ToInt32(System.Convert.IsDBNull(reader[3]) ? -1 : reader[3]),
                        QuantityPerUnit = System.Convert.ToString(System.Convert.IsDBNull(reader[4]) ? null : reader[4]),
                        UnitPrice = System.Convert.ToDecimal(System.Convert.IsDBNull(reader[5]) ? -1 : reader[5]),
                        UnitsInStock = System.Convert.ToInt32(System.Convert.IsDBNull(reader[6]) ? -1 : reader[6]),
                        UnitsOnOrder = System.Convert.ToInt32(System.Convert.IsDBNull(reader[7]) ? -1 : reader[7]),
                        ReorderLevel = System.Convert.ToInt32(System.Convert.IsDBNull(reader[8]) ? -1 : reader[8]),
                        Discontinued = System.Convert.ToBoolean(System.Convert.IsDBNull(reader[9]) ? false : reader[9])
                    });
            }

            return products;
        }

        public IEnumerable<Customer> LoadCustomers()
        {
            throw new NotImplementedException();
        }

        public Customer LoadCustomerById(string customerId)
        {
            throw new NotImplementedException();
        }

        public void SaveCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}