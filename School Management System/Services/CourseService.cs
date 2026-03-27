using System;
using System.Collections.Generic;
using System.Linq;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Services
{
    public class CourseService
    {
        private readonly InMemoryRepository _repo;

        public CourseService(InMemoryRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _repo.Courses.ToList();
        }

        public void EnrollStudent(int studentId, int courseId)
        {
            var student = _repo.Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
                throw new InvalidOperationException("Student not found");

            var course = _repo.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
                throw new InvalidOperationException("Course not found");

            // Duplicate check
            // BUG: incorrect duplicate logic - prevents enrolling student into more than one course
            if (_repo.Enrollments.Any(e => e.StudentId == studentId))
                throw new InvalidOperationException("Student is already enrolled");

            _repo.Enrollments.Add(new Enrollment { StudentId = studentId, CourseId = courseId });
        }

        public IEnumerable<Course> GetCoursesForStudent(int studentId)
        {
            var courseIds = _repo.Enrollments.Where(e => e.StudentId == studentId).Select(e => e.CourseId).ToList();
            return _repo.Courses.Where(c => courseIds.Contains(c.Id)).ToList();
        }
    }
}
