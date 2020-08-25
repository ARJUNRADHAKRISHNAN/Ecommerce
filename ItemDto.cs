using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.Dto
{
    public class ItemDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public long BrandId { get; set; }
        public virtual BrandDto Brand { get; set; }
        public long CategoryId { get; set; }
        public virtual CategoryDto Category { get; set; }
        public long SubcategoryId { get; set; }
        public virtual SubcategoryDto Subcategory { get; set; }
        public virtual ICollection<ItemfeaturesDto> Itemfeatures { get; set; }
    }
}