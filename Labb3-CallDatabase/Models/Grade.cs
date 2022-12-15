// .Net22 Daniel Svensson
using System;
using System.Collections.Generic;

namespace Labb3_CallDatabase.Models
{
    public partial class Grade
    {
        public int GradeId { get; set; }
        public int? CourseId { get; set; }
        public string? Grade1 { get; set; }
        public int? StudentId { get; set; }
        public DateTime? GradingDate { get; set; }

        public virtual Course? Course { get; set; }
    }
}
