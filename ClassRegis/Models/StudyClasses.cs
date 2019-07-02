using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegis.Models
{
    public class StudyClasses
    {
        public int Id { get; set; }
        [Display(Name = "Student")]
        public int studentsId { get; set; }
        [ForeignKey("studentsId")]
        public virtual Students Student { get; set; }
        [Display(Name = "Class")]
        public int classId { get; set; }
        [ForeignKey("classId")]
        public virtual Classes Classes { get; set; }
    }
}
