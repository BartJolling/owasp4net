Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Function Index() As ActionResult
        Return View()
    End Function

    Function About() As ActionResult
        ViewData("Message") = "OWASP Top 10 - 01. Injection"

        Return View()
    End Function

    Function Contact() As ActionResult
        ViewData("Message") = "Contact the author"

        Return View()
    End Function
End Class
