using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RebeccaResources.Data.Entities
{
    public class StoreUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string LastName { get; set; }

    }
}
