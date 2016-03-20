Imports System.Data.SqlClient
Imports System.Web.Mvc

Namespace Controllers
    Public Class SqlInjectionController
        Inherits Controller

        ' GET: SqlInjection
        Function Index() As ActionResult

            ViewData("url") = Server.UrlDecode(Request.Url.ToString())
            Dim categoryId As String = Request.QueryString("CategoryId")

            Dim products = LoadProducts(categoryId)
            Return View(products)

        End Function

        Private Function LoadProducts(CategoryId As String) As IEnumerable(Of Product)
            Dim connectionString = ConfigurationManager.ConnectionStrings("NorthWind").ConnectionString

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

        ' GET: SqlInjection/Details/5
        Function Details(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        ' GET: SqlInjection/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: SqlInjection/Create
        <HttpPost()>
        Function Create(ByVal collection As FormCollection) As ActionResult
            Try
                ' TODO: Add insert logic here

                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function

        ' GET: SqlInjection/Edit/5
        Function Edit(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        ' POST: SqlInjection/Edit/5
        <HttpPost()>
        Function Edit(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
            Try
                ' TODO: Add update logic here

                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function

        ' GET: SqlInjection/Delete/5
        Function Delete(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        ' POST: SqlInjection/Delete/5
        <HttpPost()>
        Function Delete(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
            Try
                ' TODO: Add delete logic here

                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function
    End Class
End Namespace