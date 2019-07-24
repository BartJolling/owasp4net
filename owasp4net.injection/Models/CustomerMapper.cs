using owasp4net.injection.domain;

namespace owasp4net.injection.Models
{
    static class CustomerMapper
    {
        public static CustomerViewModel ToViewModel(this Customer customer)
        {
            return new CustomerViewModel()
            {
                Address = customer.Address,
                City = customer.City,
                CompanyName = customer.CompanyName,
                ContactTitle = customer.ContactTitle,
                ContactName = customer.ContactName,
                Country = customer.Country,
                CustomerId = customer.CustomerId,
                Fax = customer.Fax,
                Phone = customer.Phone,
                PostalCode = customer.PostalCode,
                Region = customer.Region
            };
        }

        public static Customer ToDomainModel(this CustomerViewModel customerViewModel)
        {
            return new Customer()
            {
                Address = customerViewModel.Address,
                City = customerViewModel.City,
                CompanyName = customerViewModel.CompanyName,
                ContactTitle = customerViewModel.ContactTitle,
                ContactName = customerViewModel.ContactName,
                Country = customerViewModel.Country,
                CustomerId = customerViewModel.CustomerId,
                Fax = customerViewModel.Fax,
                Phone = customerViewModel.Phone,
                PostalCode = customerViewModel.PostalCode,
                Region = customerViewModel.Region
            };
        }
    }
}