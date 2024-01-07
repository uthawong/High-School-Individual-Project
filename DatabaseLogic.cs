
using High_School_Individual_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace High_School_Individual_Project
{
    public class DatabaseLogic
    {
        private HighSchoolContext Context { get; set; }
        public DatabaseLogic()
        {
            Context = new();
        }
        //case 1
        public void ListFaculties()
        {
            Console.WriteLine($"All faculty members listed (Name - Department - Years of Employment):");

            var faculties = Context.Faculties.ToList();
            var roles = Context.Roles.ToList();

            var facultyRoles = faculties.Join(roles,
                faculty => faculty.FkRoleId,
                role => role.RoleId,
                (faculty, role) => new { Faculty = faculty, Role = role });

            foreach (var facultyRole in facultyRoles)
            {
                DateTime currentDate = DateTime.Now;

                int yearsOfEmployment = currentDate.Year - facultyRole.Faculty.DateOfEmployment.Year;

                Console.WriteLine($"{facultyRole.Faculty.FirstName} {facultyRole.Faculty.LastName} - {facultyRole.Role.Role1} - {yearsOfEmployment} years of employment");

            }
        }
        //case 2
        public void AddNewFaculty()
        {
            HighSchoolContext context = new HighSchoolContext();

            Console.WriteLine("Type in the first name of the new faculty member:");
            string facultyFirstName = Console.ReadLine();

            Console.WriteLine("Type in the last name of the new faculty member:");
            string facultyLastName = Console.ReadLine();

            Console.WriteLine("Type the date of birth (YYYY-MM-DD) of the new faculty member. Ex. 1984-11-20:");
            string facultyDoB = Console.ReadLine();
            DateTime fdate = DateTime.Parse(facultyDoB);

            Console.WriteLine("Type the date of employment (YYYY-MM-DD) of the new faculty member. Ex. 2022-01-03:");
            string facultyDoE = Console.ReadLine();
            DateTime fEmploymentDate = DateTime.Parse(facultyDoE);

            Console.WriteLine("Type the salary in US Dollars: ");
            int fSalary = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Type the role ID number of the new faculty member. \n1 (Principal): \n2 (Admin): \n3 (Teacher): \n4 (Cafeteria): \n5 (Janitor): \n6 (IT): ");
            int facultyRoleId = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Type the Department Id number: \n1 = Principal (Principal) \n2 = Student Affairs (Admin) \n3 = Teaching & Learning (Teacher) \n4 = Technology Services (IT) \n5 = Business & Facilities (Cafeteria or Janitor)");
            int facultyDepartmentId = Int32.Parse(Console.ReadLine());

            var newFaculty = new Faculty
            {
                FirstName = facultyFirstName,
                LastName = facultyLastName,
                DateOfBirth = fdate,
                DateOfEmployment = fEmploymentDate,
                Salary = fSalary,
                FkRoleId = facultyRoleId,
                FkDepartmentId = facultyDepartmentId
            };
            context.Faculties.Add(newFaculty);
            context.SaveChanges();

            Console.WriteLine($"New faculty member added: {newFaculty.FirstName} {newFaculty.LastName}, \nDate Of Birth: {newFaculty.DateOfBirth:yyyy.MM.dd}, \nDate Of Employment: {newFaculty.DateOfEmployment:yyyy.MM.dd}, \nSalary: ${newFaculty.Salary},  \nRole ID: {newFaculty.FkRoleId}, \nDepartment ID: {newFaculty.FkDepartmentId}");
            Console.ReadKey();
        }
        //case 3
        public void AddNewStudents()
        {
            HighSchoolContext context = new HighSchoolContext();

            Console.WriteLine("Type in the first name of the new student:");
            string studentFirstName = Console.ReadLine();

            Console.WriteLine("Type in the last name of the new student:");
            string studentLastName = Console.ReadLine();

            Console.WriteLine("Type the date of birth (YYYY-MM-DD) of the new student. Ex. 1984-11-20:");
            string studentDoB = Console.ReadLine();
            DateTime dateOfBirth = DateTime.Parse(studentDoB);

            Console.WriteLine("Type the major of the new student: \nAvailable options: \nPhotography \nIT \nMath \nScience");
            string studentMajor = Console.ReadLine();

            Console.WriteLine("Type the grade for the course: \nAvailable options: 5, 4, 3, 2, 1 (5 is the Highest grade, 1 is the Lowest grade)");
            int studentGradeinfo = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Type the date when the grade was set (YYYY-MM-DD). Ex. 1984-11-20: ");
            string studentDateofgrade = Console.ReadLine();
            DateTime dateOfGrade = DateTime.Parse(studentDateofgrade);

            Console.WriteLine("Type the name of the teacher who set the grade: \nAvailable options: \nPhotography: Bruce Wayne \nIT: Clark Kent \nMath: Jonathan Crane \nScience: Hal Jordan");
            string studentGradebyteacher = Console.ReadLine();

            Console.WriteLine("Type the class number of the new student: \n4 = Photography \n5 = IT \n6 = Math \n7 = Science");
            int studentClassId = Int32.Parse(Console.ReadLine());

            var newStudent = new Student
            {
                FirstName = studentFirstName,
                LastName = studentLastName,
                DateOfBirth = dateOfBirth,
                Major = studentMajor,
                GradeInfo = studentGradeinfo,
                DateOfGrade = dateOfGrade,
                GradeByTeacher = studentGradebyteacher,
                FkClassId = studentClassId
            };

            context.Students.Add(newStudent);
            context.SaveChanges();

            Console.WriteLine($"New student added: {newStudent.FirstName} {newStudent.LastName}, \nDate Of Birth: {newStudent.DateOfBirth:yyyy-MM-dd}, \nMajor: {newStudent.Major}, \nGrade: {newStudent.GradeInfo},\nDate of when grade was set: {newStudent.DateOfGrade:yyyy-MM-dd}, \nTeacher who set the grade: {newStudent.GradeByTeacher}, \nClass ID: {newStudent.FkClassId}");
            Console.ReadKey();
        }
        //case 4
        public void ListClassesWithNewStudents()
        {
            var classWithStudents = Context.Classes
                .Include(c => c.Students)
                .ToList();

            foreach (var classes in classWithStudents)
            {

                Console.WriteLine($"Class: {classes.Class1}");

                foreach (var student in classes.Students)
                {
                    Console.WriteLine($"Student: {student.FirstName} {student.LastName}");
                }

                Console.WriteLine();
            }
        }    
        //case 5
        public void ListTeachersDepartments()
        {
            var faculties = Context.Faculties.ToList();
            var roles = Context.Roles.ToList();

            var teachersRoles = faculties.Join(roles,
                faculty => faculty.FkRoleId,
                role => role.RoleId,
                (faculty, role) => new { Faculty = faculty, Role = role });

            var groupedTeachers = teachersRoles.GroupBy(tr => tr.Role.Role1);

            foreach (var group in groupedTeachers)
            {
                Console.WriteLine($"Department: {group.Key} - Number of faculty members: {group.Count()}");

                foreach (var teacherRole in group)
                {
                    Console.WriteLine($"{teacherRole.Faculty.FirstName} {teacherRole.Faculty.LastName} ");
                }

                Console.WriteLine(); 
            }
        }
        //case 6
        public void ListStudents()
        {
            {
                Console.WriteLine($"All students listed (Name - Date of Birth - Major):");
            }
            var students = Context.Students.ToList();
            foreach (var student in students)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName} - {student.DateOfBirth:yyyy-MM-dd} - {student.Major}");
            }
        }
        //case 7
        public void ListActiveCourses()
        {
            {
                Console.WriteLine($"Courses that are currently offered:");
            }
            var courses = Context.Courses.ToList();
            foreach (var course in courses)
            {
                Console.WriteLine($"{course.CourseName}");
            }
        }
        //case 8
        public void ListSalary()
        {
            var departments = Context.Departments
                .Include(d => d.Faculties)
                .Select(d => new
                {
                    DepartmentName = d.DepartmentName,
                    TotalPayout = d.Faculties.Sum(f => f.Salary),
                    AveragePayout = Math.Round(d.Faculties.Average(f => f.Salary) ?? 0, 2)
                }).ToList();
            foreach (var departmentinfo in departments)
            {
                Console.WriteLine($"Department Name: {departmentinfo.DepartmentName}");
                Console.WriteLine($"Total payout per month: ${departmentinfo.TotalPayout}");
                Console.WriteLine($"Average payout per month: ${departmentinfo.AveragePayout}");
                Console.WriteLine();
            }

            //EXAMPLE OF STORED PROCEDURE

//          ALTER PROCEDURE[dbo].[GetStudentInfo]
//          @StudentId INT
//          AS
//          BEGIN
//          SELECT
//          FirstName,
//          LastName,
//          DateOfBirth,
//          Major,
//          GradeInfo,
//          DateOfGrade,
//          GradeByTeacher

//          FROM Student
//          WHERE StudentId = @StudentId
//          END;

//          Exec GetStudentInfo @StudentId = 1






            //EXAMPLE OF TRANSACTION FALL.
            //IN THIS CASE CHANGING THE GRADE TO "5" FOR STUDENT WITH STUDENT ID: 1

            //            BEGIN TRY

            //            BEGIN TRANSACTION;

            //            UPDATE Student
            //            SET GradeInfo = '5'
            //            WHERE StudentID = 1;

            //            COMMIT;
            //            END TRY
            //            BEGIN CATCH

            //            ROLLBACK;

            //            PRINT 'Ett fel inträffade: ' + ERROR_MESSAGE();
            //            END CATCH;

        }
    }
}
