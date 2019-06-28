using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegis.Models.ViewModel
{
    public class AdminVM 
    {
        public IEnumerable<Subjects> Subjects { get; set; }
        public IEnumerable<Rooms> Rooms { get; set; }
        public IEnumerable<ApplicationUser> ApplicationUser { get; set; }
        

    }
}
