Imports System.Net
Imports System.Web.Mvc
Imports owasp4net.injection.data

Namespace Controllers
    Public Class CodeInjectionController
        Inherits Controller

        ' GET: CodeInjection
        Function Index() As ActionResult
            Dim connectionString = ConfigurationManager.ConnectionStrings("NorthWindReadOnly").ConnectionString

            Dim northwindRepo As INorthWindRepository = New NorthWindRepositorySafe(connectionString)
            Dim customers = northwindRepo.LoadCustomers()

            ViewData("url") = Server.UrlDecode(Request.Url.ToString())
            Return View("Index", customers)
        End Function

        ' GET: CodeInjection/Details/5
        Function Details(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        ' GET: CodeInjection/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: CodeInjection/Create
        <HttpPost()>
        Function Create(ByVal collection As FormCollection) As ActionResult
            Try
                ' TODO: Add insert logic here

                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function

        ' GET: CodeInjection/Edit/5
        Function Edit(ByVal id As String) As ActionResult

            If String.IsNullOrWhiteSpace(id) OrElse id.Length > 5 Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim connectionString = ConfigurationManager.ConnectionStrings("NorthWindReadOnly").ConnectionString

            Dim northwindRepo As INorthWindRepository = New NorthWindRepositorySafe(connectionString)
            Dim customer = northwindRepo.LoadCustomerById(id)

            ViewData("url") = Server.UrlDecode(Request.Url.ToString())
            Return View("Edit", customer)
        End Function

        ' POST: CodeInjection/Edit/5
        <HttpPost()>
        Function Edit(ByVal id As Integer, ByVal collection As FormCollection) As ActionResult
            Try
                ' TODO: Add update logic here

                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function

        ' GET: CodeInjection/Delete/5
        Function Delete(ByVal id As Integer) As ActionResult
            Return View()
        End Function

        ' POST: CodeInjection/Delete/5
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