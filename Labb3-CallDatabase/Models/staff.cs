using System;
using System.Collections.Generic;

namespace Labb3_CallDatabase.Models
{
    public partial class staff
    {
        public staff()
        {
            StaffCourseConnections = new HashSet<StaffCourseConnection>();
        }

        public int StaffId { get; set; }
        public string? Fullname { get; set; }
        public string? Occupation { get; set; }
        public double? Salary { get; set; }
        public DateTime? Dateofbirth { get; set; }
        public string? Adress { get; set; }
        public string? PostalCode { get; set; }

        public virtual ICollection<StaffCourseConnection> StaffCourseConnections { get; set; }
    }
}
