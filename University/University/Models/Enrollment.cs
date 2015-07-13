using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public sealed class Enrollment
    {
        [Key, Column(Order = 1)]
        public int CourseID { get; set; }

        [Key, Column(Order = 2)]
        public int StudentID { get; set; }
        
        public Grade? Grade { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}