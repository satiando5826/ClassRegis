using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegis.Models.ViewModel
{
    public class ClasssesSearchViewModel
    {

        public List<Classes> Classes { get; set; }
        public List<Students> Students { get; set; }
        public Classes CurrClasses { get; set; }
        public StudyClasses StudyClasses { get; set; }
        public List<StudyClasses> lstStudyClasses { get; set; }
    }
}

