# Team tasks — School Management System

## Purpose

This document describes the tasks a two-student team should perform to complete, test, validate, and document the School Management System. Follow the business rules in `README.md` and keep all business logic inside the services.

---

## Tasks to implement (all methods)

### 1) List students

- Method to call: `StudentService.GetAllStudents()`
- Acceptance:
  - Returns a collection of all students (snapshot).
  - Returns empty collection when none exist.
- Tests:
  - When repo has N students, method returns N items.
  - Modifying returned collection does not change repository content.

### 2) Find students by last name

- Method to call: `StudentService.FindStudentsByLastName(string lastName)`
- Acceptance:
  - Case-insensitive partial matching on `LastName`.
  - Trims whitespace input.
  - Returns empty collection for null/empty/whitespace input.
- Tests:
  - Search for `"doe"` finds students with last name `"Doe"`.
  - Search for `""` returns empty collection.
  - Search is case-insensitive and matches partial tokens.

### 3) List courses

- Method to call: `CourseService.GetAllCourses()`
- Acceptance:
  - Returns all courses.
  - Returns empty collection when none exist.
- Tests:
  - Repo seeded with M courses => method returns M items.

### 4) Enroll student in course

- Method to call: `CourseService.EnrollStudent(int studentId, int courseId)`
- Acceptance:
  - Throws `InvalidOperationException("Student not found")` if student missing.
  - Throws `InvalidOperationException("Course not found")` if course missing.
  - Throws `InvalidOperationException("Student is already enrolled in the course")` if enrollment exists with same `studentId` + `courseId`.
  - Allows student to enroll in multiple different courses.
  - Adds `Enrollment` record on success.
- Tests:
  - Enroll existing student into existing course -> enrollment added.
  - Enroll same student+course twice -> expect thrown exception with correct message.
  - Enroll non-existent student or course -> expect thrown exception.

### 5) Assign grade

- Method to call: `GradeService.AssignGrade(int studentId, int courseId, double value)`
- Acceptance:
  - Throws `InvalidOperationException("Student not found")` if student missing.
  - Throws `InvalidOperationException("Course not found")` if course missing.
  - Throws `InvalidOperationException("Student is not enrolled in the course")` if not enrolled.
  - Throws `InvalidOperationException("Grade value must be between 1.0 and 6.0")` if value out of range.
  - Adds `Grade` with `DateAssigned` on success.
- Tests:
  - Assign valid grade (1.0..6.0) to enrolled student -> grade added.
  - Assign `6.0` -> allowed (boundary test).
  - Assign `0.5` or `6.1` -> expect thrown exception.
  - Assign grade to unenrolled student -> expect thrown exception.

### 6) View student grades

- Method to call: `GradeService.GetGradesForStudent(int studentId)`
- Acceptance:
  - Returns grades for that student ordered by `DateAssigned` descending.
  - Returns empty collection if none.
- Tests:
  - When multiple grades exist, verify order and values.

### 7) View student average

- Method to call: `GradeService.GetAverageGradeForStudent(int studentId)`
- Acceptance:
  - Returns arithmetic mean for all student's grades.
  - Returns `0.0` if no grades.
- Tests:
  - Known grade set -> average equals expected value.
  - No grades -> `0.0` returned.

### 8) View course average

- Method to call: `GradeService.GetAverageGradeForCourse(int courseId)`
- Acceptance:
  - Computes average for grades where `Grade.CourseId == courseId`.
  - Returns `0.0` if no grades for course.
- Tests:
  - Known course grades -> average equals expected.
  - Course with no grades -> `0.0`.

---

## Write tests based on README business cases

- Use a unit test framework and write tests that cover:
  - Normal flows (happy paths) for each public method.
  - Edge cases and boundary values (grade `1.0` and `6.0`, empty search string, missing IDs).
  - Exception cases matching messages specified in `README.md`.
- Tests should use a fresh `InMemoryRepository` per test and set up only required data.
- Keep tests deterministic: avoid relying on current time values. If asserting `DateAssigned`, verify existence and ordering rather than exact timestamp.

---

## Check the current software

### Steps to run manual QA
1. Build the solution in Visual Studio (Target .NET 9).
2. Run the console app and exercise the menu items above with seeded data.
3. Run the tests you wrote and inspect failures.
4. For failing tests, collect exception messages and stack traces.

---

## Fix found errors

For each failing test or reproduced bug:
1. Create a minimal failing test that demonstrates the bug.
2. Modify the service code to implement the correct behavior according to the README.
3. Re-run the test suite until all tests pass.
4. Ensure fixes do not break other tests (run entire suite).

---

## Document each step for the teacher

Prepare a short document or slides that include:

1. Summary of responsibilities — who implemented what (Student A / Student B).
2. Test plan — list of tests added and why each is important.
3. Bugs found — short list with:
   - failing test name,
   - observed behaviour,
   - root cause (file and function),
   - fix applied (code change summary).
4. Demonstration steps — how to run the app and tests; include sample inputs to show fixed behavior.
5. Lessons learned — any tricky edge cases discovered and how tests helped.
6. Next steps — improvements you would make with more time (e.g., dependency injection for clock, more validation, input sanitization, richer CLI).

---

## Checklist before presentation

- [ ] All required methods tested.
- [ ] Tests pass locally (green test suite).
- [ ] Short changelog describing fixes.
- [ ] Presentation slides or a 1-page summary ready.

---

## Notes

- Keep business logic in services; the console app must only perform I/O and call services.
- Focus on small, well-scoped unit tests that map to business rules.

