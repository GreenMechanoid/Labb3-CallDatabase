using System;
using System.Collections.Generic;

namespace Labb3_CallDatabase.Models
{
    public partial class StaffCourseConnection
    {
        public int ConnectionId { get; set; }
        public int? StaffId { get; set; }
        public int? CourseId { get; set; }

        public virtual Course? Course { get; set; }
        public virtual staff? Staff { get; set; }
    }
}
