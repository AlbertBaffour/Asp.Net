using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        [Display(Name ="Enrollement Date")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }
        public string Email { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public string name{
            get { return FirstMidName + " " + LastName; }
        }
    }
}

