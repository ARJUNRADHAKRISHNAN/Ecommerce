using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.Dto
{
    public class TokenInfoDto
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public long UserId { get; set; }
    }
}