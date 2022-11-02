using Microsoft.AspNetCore.Mvc;
using owasp4net.injection.data;
using owasp4net.injection.domain;
using owasp4net.injection.Models;
using System.Net;

namespace owasp4net.injection.Controllers
{
    public class SqlInjectionController : Controller
    {
        private readonly IConfiguration configuration;

        public SqlInjectionController(IConfiguration config)
        {
            configuration = config;
        }

        #region "Vulnerable"

        public IActionResult Index([FromQuery(Name = "CategoryId")] string categoryId)
        {
            var connectionString = configuration.GetConnectionString("NorthWindReadWrite");

            INorthWindRepository northwindRepo = new NorthWindRepositoryUnsafe(connectionString);
            var products = northwindRepo.LoadProducts(categoryId);

            ViewData["url"] = WebUtility.UrlDecode(Request.Path.ToString() + (Request.QueryString.HasValue ? Request.QueryString.Value : string.Empty));
            return View("Index", products);
        }

        #endregion

        #region "Safer"

        // GET: SqlInjection/InputValidation?CategoryId=1
        // Use an input view model to validate input
        public IActionResult InputValidation(ProductCategoryViewModel productCategory)
        {
            if (ModelState.IsValid)
            {
                string categoryId = productCategory.CategoryId;
                var connectionString = configuration.GetConnectionString("NorthWindReadWrite");

                INorthWindRepository northwindRepo = new NorthWindRepositoryUnsafe(connectionString);
                var products = northwindRepo.LoadProducts(categoryId);

                ViewData["url"] = WebUtility.UrlDecode(Request.Path.ToString() + (Request.QueryString.HasValue ? Request.QueryString.Value : string.Empty));
                return View("Index", products);
            }
            else
                return BadRequest();
        }

        // GET: SqlInjection/ReadOnlyUser?CategoryId=1
        // Use parameterized SQL queries
        public IActionResult ReadOnlyUser(string categoryId)
        {
            var connectionString = configuration.GetConnectionString("NorthWindReadOnly");

            INorthWindRepository northwindRepo = new NorthWindRepositoryUnsafe(connectionString);
            var products = northwindRepo.LoadProducts(categoryId);

            ViewData["url"] = WebUtility.UrlDecode(Request.Path.ToString() + (Request.QueryString.HasValue ? Request.QueryString.Value : string.Empty));
            return View("Index", products);
        }

        // GET: SqlInjection/SqlParameters?CategoryId=1
        // Use parameterized SQL queries
        public IActionResult SqlParameters(string categoryId)
        {
            var connectionString = configuration.GetConnectionString("NorthWindReadWrite");

            INorthWindRepository northwindRepo = new NorthWindRepositorySafe(connectionString);
            var products = northwindRepo.LoadProducts(categoryId);

            ViewData["url"] = WebUtility.UrlDecode(Request.Path.ToString() + (Request.QueryString.HasValue ? Request.QueryString.Value : string.Empty));
            return View("Index", products);
        }

        #endregion

        #region "Safe"

        // GET: SqlInjection/Safe?CategoryId=1
        public IActionResult Safe(ProductCategoryViewModel productCategory)
        {
            if (ModelState.IsValid)
            {
                string categoryId = productCategory.CategoryId;
                var connectionString = configuration.GetConnectionString("NorthWindReadOnly");

                INorthWindRepository northwindRepo = new NorthWindRepositorySafe(connectionString);
                var products = northwindRepo.LoadProducts(categoryId);

                ViewData["url"] = WebUtility.UrlDecode(Request.Path.ToString() + (Request.QueryString.HasValue ? Request.QueryString.Value : string.Empty));
                return View("Index", products);
            }
            else
                return BadRequest();
        }

        #endregion
    }
}