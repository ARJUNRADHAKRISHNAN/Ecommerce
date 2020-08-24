using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
    public class Item
    {
        public long Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public float Price { get; set; }
        [StringLength(250)]
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public long BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public long SubcategoryId { get; set; }
        public virtual Subcategory Subcategory { get; set; }
        public virtual ICollection<Itemfeatures> Itemfeatures { get; set; }
    }
}
