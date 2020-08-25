using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.Dto
{
    public class BookingDto
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
       
        public String Name { get; set; }
        
        public String MobileNo { get; set; }
     
        public String EmailId { get; set; }
       
        public String Address { get; set; }
       
        public String Remarks { get; set; }
        public long BrandId { get; set; }
        public virtual BrandDto Brand { get; set; }
        public long CategoryId { get; set; }
        public virtual CategoryDto Category { get; set; }
        public long ItemId { get; set; }
        public virtual ItemDto Item { get; set; }
        public bool IsActive { get; set; }
    }
}