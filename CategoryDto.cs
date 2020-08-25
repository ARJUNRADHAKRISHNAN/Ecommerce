using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.Dto
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool IsSelected { get; set; }
        public bool IsActive { get; set; }

        public ICollection<SubcategoryDto> Subcategories { get; set; }
    }
}