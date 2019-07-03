using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegis.Models
{
    public class Students
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public virtual ICollection<StudyClasses> StudyClasses { get; set; }


    }
}
