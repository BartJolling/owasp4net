Imports System.ComponentModel.DataAnnotations

Public Class CustomerViewModel
    <StringLength(5)>
    Public Property CustomerId As String

    <StringLength(40)>
    Public Property CompanyName As String

    <StringLength(30)>
    Public Property ContactName As String

    <StringLength(30)>
    Public Property ContactTitle As String

    <StringLength(80)>
    <AllowHtml>
    Public Property Address As String

    <StringLength(15)>
    Public Property City As String

    <StringLength(15)>
    Public Property Region As String

    <StringLength(10)>
    Public Property PostalCode As String

    <StringLength(15)>
    Public Property Country As String

    <StringLength(24)>
    Public Property Phone As String

    <StringLength(24)>
    Public Property Fax As String
End Class
