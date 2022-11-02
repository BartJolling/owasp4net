using Microsoft.AspNetCore.Mvc;
using owasp4net.injection.data;
using owasp4net.injection.domain;
using owasp4net.injection.Models;
using System.Net;

namespace owasp4net.injection.Controllers
{
    public class CodeInjectionController : Controller
    {

        private readonly IConfiguration configuration;

        public CodeInjectionController(IConfiguration config)
        {
            configuration = config;
        }

        // GET: CodeInjection
        public IActionResult Index()
        {
            var connectionString = configuration.GetConnectionString("NorthWindReadOnly");

            INorthWindRepository northwindRepo = new NorthWindRepositorySafe(connectionString);
            var customers = northwindRepo.LoadCustomers();

            ViewData["url"] = WebUtility.UrlDecode(Request.Path.ToString() + (Request.QueryString.HasValue ? Request.QueryString.Value : string.Empty));
            return View("Index", customers);
        }

        // GET: CodeInjection/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: CodeInjection/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CodeInjection/Create
        [HttpPost()]
        public IActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CodeInjection/Edit/5
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || id.Length > 5)
                return BadRequest();

            var connectionString = configuration.GetConnectionString("NorthWindReadOnly");

            INorthWindRepository northwindRepo = new NorthWindRepositorySafe(connectionString);
            var customerViewModel = northwindRepo.LoadCustomerById(id).ToViewModel();


            ViewData["url"] = WebUtility.UrlDecode(Request.Path.ToString() + (Request.QueryString.HasValue ? Request.QueryString.Value : string.Empty));
            return View("Edit", customerViewModel);
        }

        // POST: CodeInjection/Edit/5
        [HttpPost()]
        public IActionResult Edit([Bind("CustomerId,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                // TODO: Add update logic here
                var connectionString = configuration.GetConnectionString("NorthWindReadWrite");
                INorthWindRepository northwindRepo = new NorthWindRepositorySafe(connectionString);

                northwindRepo.SaveCustomer(customerViewModel.ToDomainModel());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CodeInjection/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: CodeInjection/Delete/5
        [HttpPost()]
        public IActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}