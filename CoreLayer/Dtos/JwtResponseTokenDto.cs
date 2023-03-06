using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos
{
    public class JwtResponseTokenDto
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
