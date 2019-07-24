using System.Collections.Generic;

namespace owasp4net.injection.domain
{
    public interface INorthWindRepository
    {
        IEnumerable<Product> LoadProducts(string categoryId);
        IEnumerable<Customer> LoadCustomers();
        Customer LoadCustomerById(string customerId);
        void SaveCustomer(Customer customer);
    }
}