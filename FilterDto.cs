
using Kendo.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.Dto
{
    public class FilterDto : DataSourceRequest
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long CategoryId { get; set; }
        public long SubcategoryId { get; set; }
        public long BrandId { get; set; }
        public string Keyword { get; set; }
        public float Minval { get; set; }
        public float Maxval { get; set; }
        public DateTime Fromdate { get; set; }
        public DateTime Todate { get; set; }
        public List<long> Categories { get; set; }
        public List<long> Subcategories { get; set; }
        public List<long> Brands { get; set; }
        public ICollection<long> CategoryIds { get; set; }
        public ICollection<long> BrandIds { get; set; }
    }
}