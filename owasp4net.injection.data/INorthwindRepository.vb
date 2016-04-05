Public Interface INorthWindRepository
    Function LoadProducts(categoryId As String) As IEnumerable(Of Product)
    Function LoadCustomers() As IEnumerable(Of Customer)
    Function LoadCustomerById(customerId As String) As Customer
End Interface
