using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegis.Models.ViewModel
{
    public class ClassViewModel
    {
        public IEnumerable<Subjects> Subjects { get; set; }
        public IEnumerable<Rooms> Rooms { get; set; }
        public List<Teachers> Teachers { get; set; }
        public Teachers accountTeacher { get; set; }
        public Classes Classes { get; set; }

        public StudyClasses StudyClasses { get; set; }
    }
}
