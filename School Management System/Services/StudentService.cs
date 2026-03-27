using System;
using System.Collections.Generic;
using System.Linq;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Services
{
    public class StudentService
    {
        private readonly InMemoryRepository _repo;

        public StudentService(InMemoryRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _repo.Students.ToList();
        }

        public IEnumerable<Student> FindStudentsByLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                return Enumerable.Empty<Student>();

            // BUG: accidentally searching FirstName instead of LastName
            var key = lastName.Trim().ToLowerInvariant();
            return _repo.Students.Where(s => (s.FirstName ?? string.Empty).ToLowerInvariant().Contains(key)).ToList();
        }

        public Student GetStudentById(int id)
        {
            var student = _repo.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                throw new InvalidOperationException($"Student with id {id} not found");
            return student;
        }
    }
}
