using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }
        
        [Required]    
        [Display(Name = "Course")]
        [StringLength(64, MinimumLength = 1, ErrorMessage = "Course name must be between 1 and 64 characters.")]
        public string CourseName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(64, MinimumLength = 1, ErrorMessage = "First name must be between 1 and 64 characters.")]
        public string InstructorFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(64, MinimumLength = 1, ErrorMessage = "Last name must be between 1 and 64 characters.")]
        public string InstructorLastName { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

    }
}