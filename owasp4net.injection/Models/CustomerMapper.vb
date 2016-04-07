Imports System.Runtime.CompilerServices
Imports owasp4net.injection.data

Module CustomerMapper

    <Extension()>
    Public Function ToViewModel(ByVal customer As Customer) As CustomerViewModel
        Return New CustomerViewModel With {
            .Address = customer.Address,
            .City = customer.City,
            .CompanyName = customer.CompanyName,
            .ContactTitle = customer.ContactTitle,
            .ContactName = customer.ContactName,
            .Country = customer.Country,
            .CustomerId = customer.CustomerId,
            .Fax = customer.Fax,
            .Phone = customer.Phone,
            .PostalCode = customer.PostalCode,
            .Region = customer.Region
            }
    End Function

    <Extension()>
    Public Function ToDomainModel(ByVal customerViewModel As CustomerViewModel) As Customer
        Return New Customer With {
            .Address = customerViewModel.Address,
            .City = customerViewModel.City,
            .CompanyName = customerViewModel.CompanyName,
            .ContactTitle = customerViewModel.ContactTitle,
            .ContactName = customerViewModel.ContactName,
            .Country = customerViewModel.Country,
            .CustomerId = customerViewModel.CustomerId,
            .Fax = customerViewModel.Fax,
            .Phone = customerViewModel.Phone,
            .PostalCode = customerViewModel.PostalCode,
            .Region = customerViewModel.Region
            }
    End Function
End Module
