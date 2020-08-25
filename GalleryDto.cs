using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.Dto
{
    public class GalleryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public bool IsActive { get; set; }
    }
}