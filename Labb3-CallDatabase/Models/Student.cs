﻿using System;
using System.Collections.Generic;

namespace Labb3_CallDatabase.Models
{
    public partial class Student
    {
        public int StudentId { get; set; }
        public string? Fullname { get; set; }
        public DateTime? Dateofbirth { get; set; }
        public string? Classnumber { get; set; }
        public string? Adress { get; set; }
        public string? PostalCode { get; set; }
    }
}
