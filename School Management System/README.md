## School Management System - Business Requirements

### Overview

This README documents business requirements and expected behavior for the simple School Management System. The implementation uses in-memory lists only (no database, no external packages). The project is organized into:

- `Models/` — domain classes (`Student`, `Teacher`, `Course`, `Enrollment`, `Grade`).
- `Data/InMemoryRepository.cs` — in-memory datastore and `Seed()` method.
- `Services/` — business layer: `StudentService`, `CourseService`, `GradeService`.
- `Program.cs` — console UI only; business logic must remain in services for testability.

### Global domain rules

- Student must have non-empty `FirstName`, `LastName`, and `Email`.
- Teacher must have non-empty `FirstName`, `LastName`, and `Email`.
- Course must have non-empty `Name` and a valid existing `TeacherId`.
- Grade values must be between 1.0 and 6.0 inclusive.
- A grade can only be assigned if the student is enrolled in the course.
- Duplicate enrollments (same student + same course) are not allowed.
- Average calculations must return `0.0` when there are no grades to average.
- Searching by last name must be case-insensitive.
- All service methods should throw meaningful exceptions (type and message) for invalid operations.

### Repository: `InMemoryRepository`

- Purpose: hold in-memory lists for `Students`, `Teachers`, `Courses`, `Enrollments`, `Grades` and provide a `Seed()` method that populates initial data.
- `Seed()` requirements:
  - Populate a reasonable set of sample data for every list (students, teachers, courses, enrollments, grades).
  - IDs must be unique within each model type.
  - Enrollments and grades in seed data must reference existing student and course IDs.
  - `Seed()` should be idempotent for fresh repository instances (called once on a new `InMemoryRepository` instance).

### Services: general requirements

- Services depend on a repository instance (injected via constructor).
- All business rules are enforced in the service layer (not in `Program.cs`).
- Methods must be deterministic: same inputs + same repository state => same outputs.
- Methods must be simple to unit test (avoid static state or random values inside methods).
- Validation failures or invalid operations must throw `InvalidOperationException` (or another clear exception) with a message describing the problem.

#### StudentService

- Constructor: `StudentService(InMemoryRepository repo)`
  - Stores provided repository reference; does not modify it.

- `GetAllStudents()`
  - Returns an `IEnumerable<Student>` of all students currently in the repository.
  - Must return a snapshot or a new list (mutating the returned collection should not corrupt repository state).
  - If there are no students, return an empty collection.

- `FindStudentsByLastName(string lastName)`
  - Behavior: return students whose last name contains the given `lastName` search term (case-insensitive).
  - Trim whitespace from `lastName` before searching.
  - If `lastName` is null, empty, or whitespace, return an empty result (do not return all students).
  - Matching must be case-insensitive and should match partial strings (e.g., search "do" matches "Doe").

- `GetStudentById(int id)`
  - Return the `Student` with the given `id`.
  - If not found, throw `InvalidOperationException` with a message like: "Student with id {id} not found".

#### CourseService

- Constructor: `CourseService(InMemoryRepository repo)`
  - Stores provided repository reference.

- `GetAllCourses()`
  - Returns all courses currently in the repository.
  - If there are none, return an empty collection.

- `EnrollStudent(int studentId, int courseId)`
  - Preconditions:
    - `studentId` must reference an existing student; otherwise throw `InvalidOperationException("Student not found")`.
    - `courseId` must reference an existing course; otherwise throw `InvalidOperationException("Course not found")`.
  - Duplicate enrollment prevention:
    - If an enrollment already exists for the same `studentId` and `courseId`, throw `InvalidOperationException("Student is already enrolled in the course")`.
    - Do not treat enrollment in other courses as a duplicate; a student may be enrolled in many different courses.
  - On success:
    - Add a new `Enrollment` object to the repository with `StudentId` and `CourseId`.
    - Do not modify other repository lists.

- `GetCoursesForStudent(int studentId)`
  - Preconditions: if the student does not exist, return empty collection or throw — prefer returning empty collection in this function for convenience.
  - Behavior: Return all courses where an `Enrollment` exists with the given `studentId`.
  - Return an empty collection if no enrollments exist for the student.

#### GradeService

- Constructor: `GradeService(InMemoryRepository repo)`
  - Stores provided repository reference.

- `AssignGrade(int studentId, int courseId, double value)`
  - Preconditions:
    - `studentId` must reference an existing student; otherwise throw `InvalidOperationException("Student not found")`.
    - `courseId` must reference an existing course; otherwise throw `InvalidOperationException("Course not found")`.
    - `value` must be a valid number in the allowed range.
  - Range validation:
    - `value` must be between `1.0` and `6.0` inclusive.
    - If `value` is out of range, throw `InvalidOperationException("Grade value must be between 1.0 and 6.0")`.
  - Enrollment check:
    - The student must already be enrolled in the course (there must exist an `Enrollment` with matching `StudentId` and `CourseId`).
    - If not enrolled, throw `InvalidOperationException("Student is not enrolled in the course")`.
  - On success:
    - Create a new `Grade` with `StudentId`, `CourseId`, `Value`, and `DateAssigned` (use UTC or `DateTime.Now` consistently across the app).
    - Append the grade to the repository's `Grades` list.

- `GetGradesForStudent(int studentId)`
  - Return an `IEnumerable<Grade>` listing all grades for the given student.
  - Order the results in descending order by `DateAssigned` (most recent first).
  - If the student has no grades, return an empty collection.

- `GetAverageGradeForStudent(int studentId)`
  - Calculate the arithmetic mean of all grades for the specified student.
  - If the student has no grades, return `0.0`.
  - Must be deterministic and consistent with `GetGradesForStudent` contents.

- `GetAverageGradeForCourse(int courseId)`
  - Calculate the arithmetic mean of all grades assigned in the specified course (i.e., all grades where `Grade.CourseId == courseId`).
  - If the course has no grades, return `0.0`.

### Console UI (`Program.cs`) expectations

- The console program is only responsible for I/O and calling service methods.
- Input parsing and validation should be done before calling service methods (e.g., ensure IDs and numeric grades parse successfully); however, services must not rely on the UI for validation.
- The console should print descriptive error messages when service methods throw exceptions.
- The console should not contain business logic beyond simple validation and formatting.

#### Error messages and exceptions

- Use clear and consistent `InvalidOperationException` messages for business-rule failures, such as:
  - "Student not found"
  - "Course not found"
  - "Student is already enrolled in the course"
  - "Student is not enrolled in the course"
  - "Grade value must be between 1.0 and 6.0"
  - "Student with id {id} not found"

### Testability notes

- Keep services small and focused; each public method should be unit-testable in isolation by constructing an `InMemoryRepository` and injecting it.
- Seed deterministic data for tests (tests should create repository instances and populate only the data they need).
- Make sure methods do not depend on static state or on `DateTime.Now` without a way to inject or assert around it. If the service uses current date for `DateAssigned`, tests should only assert presence and not exact timestamp or use an injectable clock in more advanced scenarios.

#### Examples (expected behavior)

- Calling `FindStudentsByLastName("doe")` should return students whose last name includes "doe" regardless of case (e.g. "Doe", "doe", "DOE").
- Calling `EnrollStudent(1, 1)` when student 1 already enrolled in course 1 must throw a duplicate-enrollment exception.
- Calling `AssignGrade(1, 1, 5.5)` when student 1 is enrolled in course 1 must add a grade and subsequent `GetAverageGradeForStudent(1)` should reflect the new grade.
- Calling `GetAverageGradeForCourse(99)` for a non-existent or ungraded course should return `0.0`.

### Change policy

- Business rules should be centralized in the service layer. If a rule must change, update service logic and corresponding unit tests.
- Avoid introducing silent failures: prefer throwing exceptions with clear messages when preconditions are violated.

