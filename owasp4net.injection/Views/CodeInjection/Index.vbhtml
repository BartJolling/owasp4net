@ModelType IEnumerable(Of owasp4net.injection.data.Customer)
@Code
    ViewData("Title") = "Customers"
End Code

<script src="/Scripts/jquery-1.10.2.js"></script>

<h2>Customers</h2>

@*<p>
    @Html.ActionLink("Create New", "Create")
</p>*@

<table class="table">
    <tr>
        <th>Customer Id</th>
        <th>Company Name</th>
        <th>Contact Name</th>
        <th>Contact Title</th>
        <th>Address</th>
        <th>City</th>
        <th>Region</th>
        <th>Postal Code</th>
        <th>Country</th>
        <th>Phone</th>
        <th>Fax</th>
        <th>Action</th>
    </tr>

@For Each item In Model
    @<tr>
         <td>@item.CustomerId</td>
         <td>@item.CompanyName</td>
         <td>@item.ContactName</td>
         <td>@item.ContactTitle</td>
         <td>@Html.Raw(item.Address)</td>
         <td>@item.City</td>
         <td>@item.Region</td>
         <td>@item.PostalCode</td>
         <td>@item.Country</td>
         <td>@item.Phone</td>
         <td>@item.Fax</td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.CustomerId}) @*|
    @Html.ActionLink("Details", "Details", New With {.id = item.PrimaryKey}) |
    @Html.ActionLink("Delete", "Delete", New With {.id = item.PrimaryKey})*@
        </td>
    </tr>
Next

</table>
