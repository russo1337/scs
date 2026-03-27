using System;
using System;
using System.Collections.Generic;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Data
{
    public class InMemoryRepository
    {
        public List<Student> Students { get; } = new List<Student>();
        public List<Teacher> Teachers { get; } = new List<Teacher>();
        public List<Course> Courses { get; } = new List<Course>();
        public List<Enrollment> Enrollments { get; } = new List<Enrollment>();
        public List<Grade> Grades { get; } = new List<Grade>();

        public void Seed()
        {
            // Teachers (initial + additional)
            Teachers.Add(new Teacher { Id = 1, FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@school.test" });
            Teachers.Add(new Teacher { Id = 2, FirstName = "Bob", LastName = "Smith", Email = "bob.smith@school.test" });
            Teachers.Add(new Teacher { Id = 3, FirstName = "Carla", LastName = "Nguyen", Email = "carla.nguyen@school.test" });
            Teachers.Add(new Teacher { Id = 4, FirstName = "David", LastName = "Brown", Email = "david.brown@school.test" });
            Teachers.Add(new Teacher { Id = 5, FirstName = "Eve", LastName = "Davis", Email = "eve.davis@school.test" });
            Teachers.Add(new Teacher { Id = 6, FirstName = "Frank", LastName = "Miller", Email = "frank.miller@school.test" });
            Teachers.Add(new Teacher { Id = 7, FirstName = "Grace", LastName = "Wilson", Email = "grace.wilson@school.test" });
            Teachers.Add(new Teacher { Id = 8, FirstName = "Hector", LastName = "Martinez", Email = "hector.martinez@school.test" });
            Teachers.Add(new Teacher { Id = 9, FirstName = "Ivy", LastName = "Garcia", Email = "ivy.garcia@school.test" });
            Teachers.Add(new Teacher { Id = 10, FirstName = "Jack", LastName = "Lopez", Email = "jack.lopez@school.test" });
            Teachers.Add(new Teacher { Id = 11, FirstName = "Kelly", LastName = "Anderson", Email = "kelly.anderson@school.test" });
            Teachers.Add(new Teacher { Id = 12, FirstName = "Liam", LastName = "Thomas", Email = "liam.thomas@school.test" });

            // Courses (initial + additional)
            Courses.Add(new Course { Id = 1, Name = "Mathematics", TeacherId = 1 });
            Courses.Add(new Course { Id = 2, Name = "History", TeacherId = 2 });
            Courses.Add(new Course { Id = 3, Name = "Physics", TeacherId = 1 });
            Courses.Add(new Course { Id = 4, Name = "Chemistry", TeacherId = 3 });
            Courses.Add(new Course { Id = 5, Name = "Biology", TeacherId = 4 });
            Courses.Add(new Course { Id = 6, Name = "Literature", TeacherId = 5 });
            Courses.Add(new Course { Id = 7, Name = "Art", TeacherId = 6 });
            Courses.Add(new Course { Id = 8, Name = "Music", TeacherId = 7 });
            Courses.Add(new Course { Id = 9, Name = "Physical Education", TeacherId = 8 });
            Courses.Add(new Course { Id = 10, Name = "Computer Science", TeacherId = 9 });
            Courses.Add(new Course { Id = 11, Name = "Economics", TeacherId = 10 });
            Courses.Add(new Course { Id = 12, Name = "Philosophy", TeacherId = 11 });
            Courses.Add(new Course { Id = 13, Name = "Geography", TeacherId = 12 });

            // Students (initial + at least 10 more)
            Students.Add(new Student { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" });
            Students.Add(new Student { Id = 2, FirstName = "Jane", LastName = "Roe", Email = "jane.roe@example.com" });
            Students.Add(new Student { Id = 3, FirstName = "Sam", LastName = "Doe", Email = "sam.doe@example.com" });
            Students.Add(new Student { Id = 4, FirstName = "Anna", LastName = "Lee", Email = "anna.lee@example.com" });
            Students.Add(new Student { Id = 5, FirstName = "Peter", LastName = "Parker", Email = "peter.parker@example.com" });
            Students.Add(new Student { Id = 6, FirstName = "Bruce", LastName = "Wayne", Email = "bruce.wayne@example.com" });
            Students.Add(new Student { Id = 7, FirstName = "Clark", LastName = "Kent", Email = "clark.kent@example.com" });
            Students.Add(new Student { Id = 8, FirstName = "Diana", LastName = "Prince", Email = "diana.prince@example.com" });
            Students.Add(new Student { Id = 9, FirstName = "Barry", LastName = "Allen", Email = "barry.allen@example.com" });
            Students.Add(new Student { Id = 10, FirstName = "Hal", LastName = "Jordan", Email = "hal.jordan@example.com" });
            Students.Add(new Student { Id = 11, FirstName = "Arthur", LastName = "Curry", Email = "arthur.curry@example.com" });
            Students.Add(new Student { Id = 12, FirstName = "Victor", LastName = "Stone", Email = "victor.stone@example.com" });
            Students.Add(new Student { Id = 13, FirstName = "Oliver", LastName = "Queen", Email = "oliver.queen@example.com" });

            // Enrollments (link many students to courses)
            Enrollments.Add(new Enrollment { StudentId = 1, CourseId = 1 });
            Enrollments.Add(new Enrollment { StudentId = 1, CourseId = 10 });
            Enrollments.Add(new Enrollment { StudentId = 2, CourseId = 2 });
            Enrollments.Add(new Enrollment { StudentId = 3, CourseId = 1 });
            Enrollments.Add(new Enrollment { StudentId = 4, CourseId = 6 });
            Enrollments.Add(new Enrollment { StudentId = 5, CourseId = 10 });
            Enrollments.Add(new Enrollment { StudentId = 6, CourseId = 7 });
            Enrollments.Add(new Enrollment { StudentId = 7, CourseId = 10 });
            Enrollments.Add(new Enrollment { StudentId = 8, CourseId = 8 });
            Enrollments.Add(new Enrollment { StudentId = 9, CourseId = 9 });
            Enrollments.Add(new Enrollment { StudentId = 10, CourseId = 3 });
            Enrollments.Add(new Enrollment { StudentId = 11, CourseId = 5 });
            Enrollments.Add(new Enrollment { StudentId = 12, CourseId = 4 });
            Enrollments.Add(new Enrollment { StudentId = 13, CourseId = 11 });
            Enrollments.Add(new Enrollment { StudentId = 2, CourseId = 10 });
            Enrollments.Add(new Enrollment { StudentId = 4, CourseId = 10 });

            // Grades (spread across students and courses)
            Grades.Add(new Grade { StudentId = 1, CourseId = 1, Value = 5.0, DateAssigned = DateTime.Today.AddDays(-10) });
            Grades.Add(new Grade { StudentId = 1, CourseId = 1, Value = 4.5, DateAssigned = DateTime.Today.AddDays(-5) });
            Grades.Add(new Grade { StudentId = 2, CourseId = 2, Value = 3.0, DateAssigned = DateTime.Today.AddDays(-2) });
            Grades.Add(new Grade { StudentId = 1, CourseId = 10, Value = 6.0, DateAssigned = DateTime.Today.AddDays(-1) });
            Grades.Add(new Grade { StudentId = 5, CourseId = 10, Value = 4.0, DateAssigned = DateTime.Today.AddDays(-3) });
            Grades.Add(new Grade { StudentId = 7, CourseId = 10, Value = 5.5, DateAssigned = DateTime.Today.AddDays(-8) });
            Grades.Add(new Grade { StudentId = 4, CourseId = 6, Value = 2.5, DateAssigned = DateTime.Today.AddDays(-15) });
            Grades.Add(new Grade { StudentId = 6, CourseId = 7, Value = 3.5, DateAssigned = DateTime.Today.AddDays(-20) });
            Grades.Add(new Grade { StudentId = 8, CourseId = 8, Value = 4.2, DateAssigned = DateTime.Today.AddDays(-12) });
            Grades.Add(new Grade { StudentId = 9, CourseId = 9, Value = 5.8, DateAssigned = DateTime.Today.AddDays(-4) });
            Grades.Add(new Grade { StudentId = 10, CourseId = 3, Value = 3.9, DateAssigned = DateTime.Today.AddDays(-7) });
            Grades.Add(new Grade { StudentId = 11, CourseId = 5, Value = 4.7, DateAssigned = DateTime.Today.AddDays(-6) });
            Grades.Add(new Grade { StudentId = 12, CourseId = 4, Value = 2.0, DateAssigned = DateTime.Today.AddDays(-9) });
            Grades.Add(new Grade { StudentId = 13, CourseId = 11, Value = 5.0, DateAssigned = DateTime.Today.AddDays(-11) });
        }
    }
}
