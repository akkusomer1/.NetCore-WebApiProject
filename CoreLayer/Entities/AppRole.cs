using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
    public class AppRole : BaseEntity
    {
        public string RoleName { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
    }
}
