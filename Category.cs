using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
    public class Category
    {
        public long Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Subcategory> Subcategories { get; set; }
    }
}
