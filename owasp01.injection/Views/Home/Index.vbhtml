@Code
    ViewData("Title") = "Home Page"
End Code

<div class="jumbotron">
    <h1>Injection</h1>
    <p>Injection flaws, such as SQL, OS, and LDAP injection, occur when untrusted data is sent to an interpreter as part of a command or query. The attacker’s hostile data can trick the interpreter into executing unintended commands or accessing unauthorised data.</p>
</div>

<div class="row">
    <div class="col-md-4">
        <h2>SQL Injection</h2>
        <p>SQL injection is a code injection technique, used to attack data-driven applications, in which malicious SQL statements are inserted into an entry field for execution. </p>
        <p>@Html.ActionLink("SQL Injection Demo", "Index", "SqlInjection", Nothing, New With {.class = "btn btn-default"})</p>
    </div>
    <div class="col-md-4">
        <h2>Code Injection</h2>
        <p>Code injection is the exploitation of a computer bug that is caused by processing invalid data. Injection is used by an attacker to introduce (or "inject") code into a vulnerable computer program and change the course of execution.</p>
        <p><a class="btn btn-default" href="https://en.wikipedia.org/wiki/Code_injection">Learn more &raquo;</a></p>
    </div>
    <div class="col-md-4">
        <h2>Other</h2>
        <p>You can easily find a web hosting company that offers the right mix of features and price for your applications.</p>
        <p><a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301867">Learn more &raquo;</a></p>
    </div>
</div>
