using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegis.Models
{
    public class Rooms
    {
        public int Id { get; set; }
        [Display(Name = "Room Name")]
        public string Name { get; set; }
    }
}
