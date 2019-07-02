﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegis.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        [NotMapped]
        public string RoleUser { get; set; }
        


    }
}
