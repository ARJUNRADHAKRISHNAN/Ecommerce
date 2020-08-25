using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.Dto
{
    public class ItemfeaturesDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public long ItemId { get; set; }
        public virtual ItemDto Item { get; set; }
    }
}