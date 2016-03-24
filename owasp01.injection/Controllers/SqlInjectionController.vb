Imports System.ComponentModel.DataAnnotations
Imports System.Data.SqlClient
Imports System.Web.Mvc

Namespace Controllers
    Public Class SqlInjectionController
        Inherits Controller

        Public Function Index() As ActionResult
            Return Unsafe()
        End Function

#Region "Vulnerable"

        ' GET: SqlInjection/Unsafer
        Function Unsafe() As ActionResult
            Dim connectionString = ConfigurationManager.ConnectionStrings("NorthWindReadWrite").ConnectionString

            ViewData("url") = Server.UrlDecode(Request.Url.ToString())
            Dim categoryId As String = Request.QueryString("CategoryId") 'Not type safe

            Dim products = LoadProductsUnsafe(connectionString, categoryId)
            Return View("Index", products)

        End Function

        Private Function LoadProductsUnsafe(connectionString As String, CategoryId As String) As IEnumerable(Of Product)

            Dim products As New List(Of Product)

            Dim query As String = "SELECT * FROM Products"

            If Not String.IsNullOrWhiteSpace(CategoryId) Then
                query &= " WHERE CategoryId = " & CategoryId
            End If

            Using connection = New SqlConnection(connectionString)
                Dim command As New SqlCommand(query, connection)
                command.Connection.Open()
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
#End Region

#Region "Safer"
        ' GET: SqlInjection/Safer1?CategoryId=1
        Function Safer1(categoryId As String) As ActionResult
            Dim connectionString = ConfigurationManager.ConnectionStrings("NorthWindReadWrite").ConnectionString

            ViewData("url") = Server.UrlDecode(Request.Url.ToString())

            Dim products = LoadProductsUnsafe(connectionString, categoryId)
            Return View("Index", products)

        End Function

        ' GET: SqlInjection/Safer2?CategoryId=1
        Function Safer2(categoryId As String) As ActionResult
            Dim connectionString = ConfigurationManager.ConnectionStrings("NorthWindReadWrite").ConnectionString

            ViewData("url") = Server.UrlDecode(Request.Url.ToString())

            Dim products = LoadProductsUnsafe(connectionString, categoryId)
            Return View("Index", products)

        End Function
#End Region

#Region "Safe"
        ' GET: SqlInjection/Safe?CategoryId=1
        Function Safe(categoryModel As CategoryModel) As ActionResult
            Dim connectionString = ConfigurationManager.ConnectionStrings("NorthWindReadWrite").ConnectionString

            ViewData("url") = Server.UrlDecode(Request.Url.ToString())

            If ModelState.IsValid Then
                Dim products = LoadProductsSafe(connectionString, categoryModel.categoryId)
                Return View("Index", products)
            Else
                Return View("Index", Enumerable.Empty(Of Product))
            End If

        End Function


        Private Function LoadProductsSafe(connectionString As String, CategoryId As String) As IEnumerable(Of Product)

            Dim products As New List(Of Product)

            Dim query As String = "SELECT * FROM Products"

            If Not String.IsNullOrWhiteSpace(CategoryId) Then
                query &= " WHERE CategoryId = " & CategoryId
            End If

            Using connection = New SqlConnection(connectionString)
                Dim command As New SqlCommand(query, connection)
                command.Connection.Open()
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
#End Region

    End Class

    Public Class CategoryModel
        <StringLength(3)>
        Public Property categoryId As String
    End Class

End Namespace