@ModelType owasp4net.injection.CustomerViewModel
@Code
    ViewData("Title") = "Edit"
End Code

<h2>Edit</h2>
<div>
    
    @Html.ActionLink("⇐ Back to List", "Index")
</div>
@Using (Html.BeginForm())
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
        <h4>Edit existing customer record</h4>
        <hr />
        @Html.ValidationSummary(False, "", New With {.class = "text-danger"})

        <div class="form-group">
            @Html.LabelFor(Function(c) c.CompanyName, New With {.class = "control-label col-sm-2"})
            @Html.TextBoxFor(Function(c) c.CompanyName, New With {.class = "form-control col-sm-10"})
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(c) c.ContactName, New With {.class = "control-label col-sm-2"})
            @Html.TextBoxFor(Function(c) c.ContactName, New With {.class = "form-control col-sm-10"})
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(c) c.ContactTitle, New With {.class = "control-label col-sm-2"})
            @Html.TextBoxFor(Function(c) c.ContactTitle, New With {.class = "form-control col-sm-10"})
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(c) c.Address, New With {.class = "control-label col-sm-2"})
            @Html.TextBoxFor(Function(c) c.Address, New With {.class = "form-control col-sm-10"})
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(c) c.City, New With {.class = "control-label col-sm-2"})
            @Html.TextBoxFor(Function(c) c.City, New With {.class = "form-control col-sm-10"})
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(c) c.Region, New With {.class = "control-label col-sm-2"})
            @Html.TextBoxFor(Function(c) c.Region, New With {.class = "form-control col-sm-10"})
        </div>
         <div class="form-group">
             @Html.LabelFor(Function(c) c.PostalCode, New With {.class = "control-label col-sm-2"})
             @Html.TextBoxFor(Function(c) c.PostalCode, New With {.class = "form-control col-sm-10"})
         </div>
        <div class="form-group">
            @Html.LabelFor(Function(c) c.Country, New With {.class = "control-label col-sm-2"})
            @Html.TextBoxFor(Function(c) c.Country, New With {.class = "form-control col-sm-10"})
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(c) c.Phone, New With {.class = "control-label col-sm-2"})
            @Html.TextBoxFor(Function(c) c.Phone, New With {.class = "form-control col-sm-10"})
        </div>
         <div class="form-group">
             @Html.LabelFor(Function(c) c.Fax, New With {.class = "control-label col-sm-2"})
             @Html.TextBoxFor(Function(c) c.Fax, New With {.class = "form-control col-sm-10"})
         </div>
         <div class="form-group">
             @Html.HiddenFor(Function(c) c.CustomerId, New With {.class = "form-control col-sm-10"})
             <input type="submit" value="Save" class="btn btn-success col-sm-offset-2" col-sm-10 />
         </div>
        <div class="col-sm-offset-2 col-sm-10">
            
        </div>
    </div>
End Using

<p>
<p>
    <h3>Script Injection</h3>
    Insert below statement in the address field. It will replace the link to the start page with one to google.com
    <pre>@("<script>$(""a[href='/']"").attr(""href"",""http://www.google.com"")</script>")</pre>
</p>
@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
