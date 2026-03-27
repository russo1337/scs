using System;

namespace SchoolManagementSystem.Models
{
    public class Grade
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public double Value { get; set; }
        public DateTime DateAssigned { get; set; }
    }
}
