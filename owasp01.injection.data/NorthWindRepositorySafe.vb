Imports System.Data.SqlClient
Imports owasp01.injection.data

Public Class NorthWindRepositorySafe
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
            query &= " WHERE CategoryId = @categoryId"
        End If

        Using connection = New SqlConnection(Me._connectionString)
            Dim command As New SqlCommand(query, connection)
            command.Connection.Open()
            command.Parameters.Add(New SqlParameter("categoryId", CategoryId))

            Dim reader = command.ExecuteReader()

            While reader.Read()
                products.Add(New Product() With {
                    .ProductID = reader(0),
                    .ProductName = If(IsDBNull(reader(1)), -1, reader(1)),
                    .SupplierID = If(IsDBNull(reader(2)), -1, reader(2)),
                    .CategoryID = If(IsDBNull(reader(3)), -1, reader(3)),
                    .QuantityPerUnit = If(IsDBNull(reader(4)), -1, reader(4)),
                    .UnitPrice = If(IsDBNull(reader(5)), -1, reader(5)),
                    .UnitsInStock = If(IsDBNull(reader(6)), -1, reader(6)),
                    .UnitsOnOrder = If(IsDBNull(reader(7)), -1, reader(7)),
                    .ReorderLevel = If(IsDBNull(reader(8)), -1, reader(8)),
                    .Discontinued = If(IsDBNull(reader(9)), -1, reader(9))
                })
            End While
        End Using

        Return products
    End Function

End Class
