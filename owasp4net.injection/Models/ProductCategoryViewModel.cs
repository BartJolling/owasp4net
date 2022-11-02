using System.ComponentModel.DataAnnotations;

#nullable disable

namespace owasp4net.injection.Models
{
    public class ProductCategoryViewModel
    {
        [StringLength(5)]
        public string CategoryId { get; set; }
    }
}

#nullable enable