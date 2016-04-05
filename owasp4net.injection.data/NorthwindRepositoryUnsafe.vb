Imports System.Data.SqlClient
Imports owasp4net.injection.data

Public Class NorthWindRepositoryUnsafe
    Implements INorthWindRepository

    Private _connectionString As String

    Public Sub New(connectionString As String)
        If String.IsNullOrWhiteSpace(connectionString) Then
            Throw New ArgumentNullException("connectionString")
        End If

        Me._connectionString = connectionString
    End Sub

    Public Function LoadProducts(CategoryId As String) As IEnumerable(Of Product) Implements INorthWindRepository.LoadProducts

        Dim products As New List(Of Product)

        Dim query As String = "SELECT * FROM Products"

        If Not String.IsNullOrWhiteSpace(CategoryId) Then
            query &= " WHERE CategoryId = " & CategoryId
        End If

        Using connection = New SqlConnection(Me._connectionString)
            Dim command As New SqlCommand(query, connection)
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
        Throw New NotImplementedException()
    End Function

    Public Function LoadCustomerById(customerId As String) As Customer Implements INorthWindRepository.LoadCustomerById
        Throw New NotImplementedException()
    End Function
End Class
