﻿@ModelType ChangePasswordViewModel
@Code
    ViewBag.Title = "Change Password"
End Code

<h2>@ViewBag.Title.</h2>

@Using Html.BeginForm("ChangePassword", "Manage", FormMethod.Post, New With { .class = "form-horizontal", .role = "form" })
    @Html.AntiForgeryToken()
    
    @<text>
    <h4>Change Password Form</h4>
    <hr />
    @Html.ValidationSummary("", New With { .class = "text-danger" })
    <div class="form-group">
        @Html.LabelFor(Function(m) m.OldPassword, New With { .class = "col-md-2 control-label" })
        <div class="col-md-10">
            @Html.PasswordFor(Function(m) m.OldPassword, New With { .class = "form-control" })
        </div>
    </div>
    <div class="form-group">
        @Html.LabelFor(Function(m) m.NewPassword, New With {.class = "col-md-2 control-label"})
        <div class="col-md-10">
            @Html.PasswordFor(Function(m) m.NewPassword, New With {.class = "form-control"})
        </div>
    </div>
    <div class="form-group">
        @Html.LabelFor(Function(m) m.ConfirmPassword, New With { .class = "col-md-2 control-label" })
        <div class="col-md-10">
            @Html.PasswordFor(Function(m) m.ConfirmPassword, New With { .class = "form-control" })
        </div>
    </div>
    <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
            <input type="submit" value="Change password" class="btn btn-default" />
        </div>
    </div>
    </text>
End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section