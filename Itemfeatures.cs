using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
    public class Itemfeatures
    {
        public long Id { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public long ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
