using owasp4net.injection.domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace owasp4net.injection.data
{
    public class NorthWindRepositorySafe : INorthWindRepository
    {
        private string _connectionString;

        public NorthWindRepositorySafe(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException("connectionString");

            this._connectionString = connectionString;
        }

        public IEnumerable<Product> LoadProducts(string categoryId)
        {
            List<Product> products = new List<Product>();

            string query = "SELECT * FROM Products";

            if (!string.IsNullOrWhiteSpace(categoryId))
                query += " WHERE CategoryId = @categoryId";

            using (var connection = new SqlConnection(this._connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add(new SqlParameter("categoryId", categoryId));

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
            List<Customer> customers = new List<Customer>();

            using (var connection = new SqlConnection(this._connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Customers", connection);
                command.Connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                    customers.Add(ReadCustomer(reader));
            }

            return customers;
        }

        public Customer LoadCustomerById(string customerId)
        {
            using (var connection = new SqlConnection(this._connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Customers WHERE CustomerId = @customerId", connection);
                command.Parameters.Add(new SqlParameter("customerId", customerId));
                command.Connection.Open();

                var reader = command.ExecuteReader();
                if (reader.Read())
                    return ReadCustomer(reader);
                else
                    return null;
            }
        }

        private Customer ReadCustomer(SqlDataReader reader)
        {
            return new Customer()
            {
                CustomerId = System.Convert.ToString(reader[0]),
                CompanyName = System.Convert.ToString(System.Convert.IsDBNull(reader[1]) ? null : reader[1]),
                ContactName = System.Convert.ToString(System.Convert.IsDBNull(reader[2]) ? null : reader[2]),
                ContactTitle = System.Convert.ToString(System.Convert.IsDBNull(reader[3]) ? null : reader[3]),
                Address = System.Convert.ToString(System.Convert.IsDBNull(reader[4]) ? null : reader[4]),
                City = System.Convert.ToString(System.Convert.IsDBNull(reader[5]) ? null : reader[5]),
                Region = System.Convert.ToString(System.Convert.IsDBNull(reader[6]) ? null : reader[6]),
                PostalCode = System.Convert.ToString(System.Convert.IsDBNull(reader[7]) ? null : reader[7]),
                Country = System.Convert.ToString(System.Convert.IsDBNull(reader[8]) ? null : reader[8]),
                Phone = System.Convert.ToString(System.Convert.IsDBNull(reader[9]) ? null : reader[9]),
                Fax = System.Convert.ToString(System.Convert.IsDBNull(reader[10]) ? null : reader[10])
            };
        }

        public void SaveCustomer(Customer customer)
        {
            using (var connection = new SqlConnection(this._connectionString))
            {
                SqlCommand command = new SqlCommand(@"UPDATE [dbo].[Customers]
                SET [CompanyName] = @CompanyName
                   ,[ContactName] = @ContactName
                   ,[ContactTitle] = @ContactTitle
                   ,[Address] = @Address
                   ,[City] = @City
                   ,[Region] = @Region
                   ,[PostalCode] = @PostalCode
                   ,[Country] = @Country
                   ,[Phone] = @Phone
                   ,[Fax] = @Fax
                WHERE [CustomerId] = @CustomerId", connection);

                command.Parameters.Add(new SqlParameter("CompanyName", customer.CompanyName));
                command.Parameters.Add(new SqlParameter("ContactName", (object)customer.ContactName ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("ContactTitle", (object)customer.ContactTitle ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("Address", (object)customer.Address ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("City", (object)customer.City ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("Region", (object)customer.Region ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("PostalCode", (object)customer.PostalCode ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("Country", (object)customer.Country ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("Phone", (object)customer.Phone ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("Fax", (object)customer.Fax ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("CustomerId", customer.CustomerId));

                int rowsAffected = 0;
                try
                {
                    command.Connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error during update of customer with id " + customer.CustomerId, ex);
                }

                if (rowsAffected != 1)
                    throw new Exception("Error during update of customer with id " + customer.CustomerId);
            }
        }
    }

}
