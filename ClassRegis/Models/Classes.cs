using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRegis.Models
{

    public class Classes
    {
        public int Id { get; set; }
        [Display(Name = "Class Name")]
        public string Name { get; set; }

        [Display(Name = "Room number")]
        public int roomId { get; set; }
        [ForeignKey("roomId")]
        public virtual Rooms Rooms { get; set; }

        [Display(Name = "Subject name")]
        public int subjectId { get; set; }
        [ForeignKey("subjectId")]
        public virtual Subjects Subjects { get; set; }

        [Display(Name = "Teacher name")]
        public int teacherId { get; set; }
        [ForeignKey("teacherId")]
        public virtual Teachers Teachers { get; set; }
    }
}

