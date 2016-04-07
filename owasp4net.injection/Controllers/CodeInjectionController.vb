Imports System.Net
Imports System.Web.Mvc
Imports owasp4net.injection.data
Imports owasp4net.injection.CustomerMapper

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
            Dim customerViewModel = northwindRepo.LoadCustomerById(id).ToViewModel()


            ViewData("url") = Server.UrlDecode(Request.Url.ToString())
            Return View("Edit", customerViewModel)
        End Function

        ' POST: CodeInjection/Edit/5
        <HttpPost()>
        Function Edit(<Bind(Include:="CustomerId,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")> ByVal customerViewModel As CustomerViewModel) As ActionResult

            If Not ModelState.IsValid Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Try
                ' TODO: Add update logic here
                Dim connectionString = ConfigurationManager.ConnectionStrings("NorthWindReadWrite").ConnectionString
                Dim northwindRepo As INorthWindRepository = New NorthWindRepositorySafe(connectionString)

                northwindRepo.SaveCustomer(customerViewModel.ToDomainModel())

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