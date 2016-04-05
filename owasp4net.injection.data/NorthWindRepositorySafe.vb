Imports System.Data.SqlClient
Imports owasp4net.injection.data

Public Class NorthWindRepositorySafe
    Implements INorthWindRepository

    Private _connectionString As String

    Public Sub New(connectionString As String)
        If String.IsNullOrWhiteSpace(connectionString) Then
            Throw New ArgumentNullException("connectionString")
        End If

        Me._connectionString = connectionString
    End Sub

    Public Function LoadProducts(categoryId As String) As IEnumerable(Of Product) Implements INorthWindRepository.LoadProducts
        Dim products As New List(Of Product)

        Dim query As String = "SELECT * FROM Products"

        If Not String.IsNullOrWhiteSpace(categoryId) Then
            query &= " WHERE CategoryId = @categoryId"
        End If

        Using connection = New SqlConnection(Me._connectionString)
            Dim command As New SqlCommand(query, connection)
            command.Parameters.Add(New SqlParameter("categoryId", categoryId))

            command.Connection.Open()
            Dim reader = command.ExecuteReader()

            While reader.Read()
                products.Add(New Product() With {
                    .ProductID = CInt(reader(0)),
                    .ProductName = CStr(If(IsDBNull(reader(1)), Nothing, reader(1))),
                    .SupplierID = CInt(If(IsDBNull(reader(2)), -1, reader(2))),
                    .CategoryID = CInt(If(IsDBNull(reader(3)), -1, reader(3))),
                    .QuantityPerUnit = CStr(If(IsDBNull(reader(4)), Nothing, reader(4))),
                    .UnitPrice = CDec(If(IsDBNull(reader(5)), -1, reader(5))),
                    .UnitsInStock = CInt(If(IsDBNull(reader(6)), -1, reader(6))),
                    .UnitsOnOrder = CInt(If(IsDBNull(reader(7)), -1, reader(7))),
                    .ReorderLevel = CInt(If(IsDBNull(reader(8)), -1, reader(8))),
                    .Discontinued = CBool(If(IsDBNull(reader(9)), False, reader(9)))
                })
            End While
        End Using

        Return products
    End Function

    Public Function LoadCustomers() As IEnumerable(Of Customer) Implements INorthWindRepository.LoadCustomers
        Dim customers As New List(Of Customer)

        Using connection = New SqlConnection(Me._connectionString)
            Dim command As New SqlCommand("SELECT * FROM Customers", connection)
            command.Connection.Open()

            Dim reader = command.ExecuteReader()

            While reader.Read()
                customers.Add(ReadCustomer(reader))
            End While
        End Using

        Return customers
    End Function

    Public Function LoadCustomerById(customerId As String) As Customer Implements INorthWindRepository.LoadCustomerById
        Using connection = New SqlConnection(Me._connectionString)
            Dim command As New SqlCommand("SELECT * FROM Customers WHERE CustomerId = @customerId", connection)
            command.Parameters.Add(New SqlParameter("customerId", customerId))
            command.Connection.Open()

            Dim reader = command.ExecuteReader()
            If reader.Read() Then
                Return ReadCustomer(reader)
            Else
                Return Nothing
            End If
        End Using

    End Function

    Private Function ReadCustomer(reader As SqlDataReader) As Customer
        Return New Customer() With {
                    .CustomerId = CStr(reader(0)),
                    .CompanyName = CStr(If(IsDBNull(reader(1)), Nothing, reader(1))),
                    .ContactName = CStr(If(IsDBNull(reader(2)), -Nothing, reader(2))),
                    .ContactTitle = CStr(If(IsDBNull(reader(3)), Nothing, reader(3))),
                    .Address = CStr(If(IsDBNull(reader(4)), Nothing, reader(4))),
                    .City = CStr(If(IsDBNull(reader(5)), Nothing, reader(5))),
                    .Region = CStr(If(IsDBNull(reader(6)), Nothing, reader(6))),
                    .PostalCode = CStr(If(IsDBNull(reader(7)), Nothing, reader(7))),
                    .Country = CStr(If(IsDBNull(reader(8)), Nothing, reader(8))),
                    .Phone = CStr(If(IsDBNull(reader(9)), Nothing, reader(9))),
                    .Fax = CStr(If(IsDBNull(reader(10)), Nothing, reader(10)))
                }
    End Function
End Class
