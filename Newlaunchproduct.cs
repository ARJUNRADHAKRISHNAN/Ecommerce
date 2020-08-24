using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model
{
    public class Newlaunchproduct
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long NewlaunchId { get; set; }
        public virtual Newlaunch Newlaunch { get; set; }
        public virtual Item Item { get; set; }
        public bool IsActive { get; set; }
    }
}
