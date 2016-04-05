@ModelType owasp4net.injection.data.Customer
@Code
    ViewData("Title") = "Edit"
End Code

<h2>Edit</h2>

@Using (Html.BeginForm())
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
        <h4>Customer</h4>
        <hr />
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
        <div class="form-group">
            @Html.LabelFor(Function(c) c.CustomerId)
            @Html.TextBoxFor(Function(c) c.CustomerId, New With {.class = "form-control", .disabled = "disabled"})
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(c) c.CompanyName)
            @Html.TextBoxFor(Function(c) c.CompanyName, New With {.class = "form-control"})
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(c) c.ContactName)
            @Html.TextBoxFor(Function(c) c.ContactName, New With {.class = "form-control"})
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(c) c.ContactTitle)
            @Html.TextBoxFor(Function(c) c.ContactTitle, New With {.class = "form-control"})
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(c) c.Address)
            @Html.TextBoxFor(Function(c) c.Address, New With {.class = "form-control"})
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(c) c.City)
            @Html.TextBoxFor(Function(c) c.City, New With {.class = "form-control"})
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(c) c.Region)
            @Html.TextBoxFor(Function(c) c.Region, New With {.class = "form-control"})
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(c) c.Country)
            @Html.TextBoxFor(Function(c) c.Country, New With {.class = "form-control"})
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(c) c.Phone)
            @Html.TextBoxFor(Function(c) c.Phone, New With {.class = "form-control"})
        </div>
         <div class="form-group">
             @Html.LabelFor(Function(c) c.Fax)
             @Html.TextBoxFor(Function(c) c.Fax, New With {.class = "form-control"})
         </div>
        <div class="col-md-offset-2 col-md-10">
            <input type="submit" value="Save" class="btn btn-default" />
        </div>
    </div>
End Using

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
