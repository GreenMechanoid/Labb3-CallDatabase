using System;
using System.Collections.Generic;

namespace Labb3_CallDatabase.Models
{
    public partial class Course
    {
        public Course()
        {
            Grades = new HashSet<Grade>();
            StaffCourseConnections = new HashSet<StaffCourseConnection>();
        }

        public int CourseId { get; set; }
        public string? Schoolyear { get; set; }
        public string? Coursename { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<StaffCourseConnection> StaffCourseConnections { get; set; }
    }
}
