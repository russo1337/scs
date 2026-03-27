using System;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Services;

var repo = new InMemoryRepository();
repo.Seed();

var studentService = new StudentService(repo);
var courseService = new CourseService(repo);
var gradeService = new GradeService(repo);

bool exit = false;
while (!exit)
{
    Console.WriteLine();
    Console.WriteLine("School Management System");
    Console.WriteLine("1) List students");
    Console.WriteLine("2) Find students by last name");
    Console.WriteLine("3) List courses");
    Console.WriteLine("4) Enroll student in course");
    Console.WriteLine("5) Assign grade");
    Console.WriteLine("6) View student grades");
    Console.WriteLine("7) View student average");
    Console.WriteLine("8) View course average");
    Console.WriteLine("0) Exit");
    Console.Write("Select option: ");
    var input = Console.ReadLine();

    try
    {
        switch (input)
        {
            case "1":
                var all = studentService.GetAllStudents();
                foreach (var s in all)
                    Console.WriteLine($"{s.Id}: {s.FirstName} {s.LastName} <{s.Email}>");
                break;
            case "2":
                Console.Write("Last name to search: ");
                var last = Console.ReadLine();
                var found = studentService.FindStudentsByLastName(last ?? string.Empty);
                foreach (var s in found)
                    Console.WriteLine($"{s.Id}: {s.FirstName} {s.LastName} <{s.Email}>");
                break;
            case "3":
                var courses = courseService.GetAllCourses();
                foreach (var c in courses)
                {
                    var teacher = repo.Teachers.Find(t => t.Id == c.TeacherId);
                    Console.WriteLine($"{c.Id}: {c.Name} (Teacher: {teacher?.FirstName} {teacher?.LastName})");
                }
                break;
            case "4":
                Console.Write("Student Id: ");
                if (!int.TryParse(Console.ReadLine(), out var sid)) { Console.WriteLine("Invalid id"); break; }
                Console.Write("Course Id: ");
                if (!int.TryParse(Console.ReadLine(), out var cid)) { Console.WriteLine("Invalid id"); break; }
                courseService.EnrollStudent(sid, cid);
                Console.WriteLine("Enrolled.");
                break;
            case "5":
                Console.Write("Student Id: ");
                if (!int.TryParse(Console.ReadLine(), out var sgid)) { Console.WriteLine("Invalid id"); break; }
                Console.Write("Course Id: ");
                if (!int.TryParse(Console.ReadLine(), out var gcid)) { Console.WriteLine("Invalid id"); break; }
                Console.Write("Grade value: ");
                if (!double.TryParse(Console.ReadLine(), out var val)) { Console.WriteLine("Invalid value"); break; }
                gradeService.AssignGrade(sgid, gcid, val);
                Console.WriteLine("Grade assigned.");
                break;
            case "6":
                Console.Write("Student Id: ");
                if (!int.TryParse(Console.ReadLine(), out var vg)) { Console.WriteLine("Invalid id"); break; }
                var grades = gradeService.GetGradesForStudent(vg);
                foreach (var g in grades)
                    Console.WriteLine($"Course {g.CourseId}: {g.Value} on {g.DateAssigned:d}");
                break;
            case "7":
                Console.Write("Student Id: ");
                if (!int.TryParse(Console.ReadLine(), out var avgSid)) { Console.WriteLine("Invalid id"); break; }
                var avg = gradeService.GetAverageGradeForStudent(avgSid);
                Console.WriteLine($"Average: {avg:F2}");
                break;
            case "8":
                Console.Write("Course Id: ");
                if (!int.TryParse(Console.ReadLine(), out var avgCid)) { Console.WriteLine("Invalid id"); break; }
                var cavg = gradeService.GetAverageGradeForCourse(avgCid);
                Console.WriteLine($"Course Average: {cavg:F2}");
                break;
            case "0":
                exit = true;
                break;
            default:
                Console.WriteLine("Unknown option");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}
