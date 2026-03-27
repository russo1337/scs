using System;
using System.Collections.Generic;
using System.Linq;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Services
{
    public class GradeService
    {
        private readonly InMemoryRepository _repo;

        public GradeService(InMemoryRepository repo)
        {
            _repo = repo;
        }

        public void AssignGrade(int studentId, int courseId, double value)
        {
            var student = _repo.Students.FirstOrDefault(s => s.Id == studentId) ?? throw new InvalidOperationException("Student not found");
            var course = _repo.Courses.FirstOrDefault(c => c.Id == courseId) ?? throw new InvalidOperationException("Course not found");

            // Check enrollment
            var enrolled = _repo.Enrollments.Any(e => e.StudentId == studentId && e.CourseId == courseId);
            if (!enrolled)
                throw new InvalidOperationException("Student is not enrolled in the course");

            // BUG: incorrect range check - excludes 6.0
            if (value < 1.0 || value >= 6.0)
                throw new InvalidOperationException("Grade value must be between 1.0 and 6.0");

            _repo.Grades.Add(new Grade { StudentId = studentId, CourseId = courseId, Value = value, DateAssigned = DateTime.UtcNow });
        }

        public IEnumerable<Grade> GetGradesForStudent(int studentId)
        {
            return _repo.Grades.Where(g => g.StudentId == studentId).OrderByDescending(g => g.DateAssigned).ToList();
        }

        public double GetAverageGradeForStudent(int studentId)
        {
            var grades = _repo.Grades.Where(g => g.StudentId == studentId).ToList();
            if (!grades.Any())
                return 0.0;
            return grades.Sum(g => g.Value) / grades.Count;
        }

        public double GetAverageGradeForCourse(int courseId)
        {
            // BUG: incorrect filter - uses StudentId instead of CourseId
            var grades = _repo.Grades.Where(g => g.StudentId == courseId).ToList();
            if (!grades.Any())
                return 0.0;
            return grades.Sum(g => g.Value) / grades.Count;
        }
    }
}
