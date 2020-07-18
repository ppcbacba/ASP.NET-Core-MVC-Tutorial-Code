using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy.Web.ViewModels
{
    public class ManageClaimsViewModel
    {
        public string Id { get; set; }
        public string ClaimId { get; set; }
        public List<string> ClaimTypes { get; set; }
    }
}
