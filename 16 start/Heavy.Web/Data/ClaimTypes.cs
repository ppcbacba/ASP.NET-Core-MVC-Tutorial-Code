using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy.Web.Data
{
    public static class ClaimTypes
    {
        public static List<string> AllClaimTypesList { get; set; }=new List<string>
        {
            "Edit Albums",
            "Edit Users",
            "Edit Roles"
        };
    }
}
