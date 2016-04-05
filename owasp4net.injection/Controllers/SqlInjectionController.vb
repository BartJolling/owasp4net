Imports System.ComponentModel.DataAnnotations
Imports System.Data.SqlClient
Imports System.Net
Imports System.Web.Mvc
Imports owasp4net.injection.data

Namespace Controllers
    Public Class SqlInjectionController
        Inherits Controller

#Region "Vulnerable"

        Public Function Index() As ActionResult
            Dim categoryId As String = Request.QueryString("CategoryId") 'Not type safe
            Dim connectionString = ConfigurationManager.ConnectionStrings("NorthWindReadWrite").ConnectionString

            Dim northwindRepo As INorthWindRepository = New NorthWindRepositoryUnsafe(connectionString)
            Dim products = northwindRepo.LoadProducts(categoryId)

            ViewData("url") = Server.UrlDecode(Request.Url.ToString())
            Return View("Index", products)

        End Function

#End Region

#Region "Safer"
        ' GET: SqlInjection/InputValidation?CategoryId=1
        ' Use an input view model to validate input
        Function InputValidation(productCategory As ProductCategoryViewModel) As ActionResult

            If ModelState.IsValid Then
                Dim categoryId As String = productCategory.CategoryId
                Dim connectionString = ConfigurationManager.ConnectionStrings("NorthWindReadWrite").ConnectionString

                Dim northwindRepo As INorthWindRepository = New NorthWindRepositoryUnsafe(connectionString)
                Dim products = northwindRepo.LoadProducts(categoryId)

                ViewData("url") = Server.UrlDecode(Request.Url.ToString())
                Return View("Index", products)
            Else
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

        End Function

        ' GET: SqlInjection/ReadOnlyUser?CategoryId=1
        ' Use parameterized SQL queries
        Function ReadOnlyUser(categoryId As String) As ActionResult
            Dim connectionString = ConfigurationManager.ConnectionStrings("NorthWindReadOnly").ConnectionString

            Dim northwindRepo As INorthWindRepository = New NorthWindRepositoryUnsafe(connectionString)
            Dim products = northwindRepo.LoadProducts(categoryId)

            ViewData("url") = Server.UrlDecode(Request.Url.ToString())
            Return View("Index", products)

        End Function

        ' GET: SqlInjection/SqlParameters?CategoryId=1
        ' Use parameterized SQL queries
        Function SqlParameters(categoryId As String) As ActionResult
            Dim connectionString = ConfigurationManager.ConnectionStrings("NorthWindReadWrite").ConnectionString

            Dim northwindRepo As INorthWindRepository = New NorthWindRepositorySafe(connectionString)
            Dim products = northwindRepo.LoadProducts(categoryId)

            ViewData("url") = Server.UrlDecode(Request.Url.ToString())
            Return View("Index", products)

        End Function
#End Region

#Region "Safe"
        ' GET: SqlInjection/Safe?CategoryId=1
        Function Safe(productCategory As ProductCategoryViewModel) As ActionResult

            If ModelState.IsValid Then
                Dim categoryId As String = productCategory.CategoryId
                Dim connectionString = ConfigurationManager.ConnectionStrings("NorthWindReadOnly").ConnectionString

                Dim northwindRepo As INorthWindRepository = New NorthWindRepositorySafe(connectionString)
                Dim products = northwindRepo.LoadProducts(categoryId)

                ViewData("url") = Server.UrlDecode(Request.Url.ToString())
                Return View("Index", products)
            Else
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
                'Return View("Index", Enumerable.Empty(Of Product))
            End If

            ViewData("url") = Server.UrlDecode(Request.Url.ToString())

        End Function
#End Region

    End Class

End Namespace