using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
    public class Booking
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        [StringLength(50)]
        public String Name { get; set; }
        [StringLength(50)]
        public String MobileNo {get;set;}
        [StringLength(50)]
        public String EmailId { get; set;}
        [StringLength(500)]
        public String Address { get; set; }
        [StringLength(20000)]
        public String Remarks { get; set; }
        public long ItemId { get; set; }
        public virtual Item Item { get; set; }
        public bool IsActive { get; set; }

    }
}
