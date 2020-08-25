using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.Dto
{
    public class NewlaunchproductDto
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long NewlaunchId { get; set; }
        public virtual ItemDto Item { get; set; }
        public virtual NewlaunchDto Newlaunch { get; set; }
        public bool IsActive { get; set; }
    }
}