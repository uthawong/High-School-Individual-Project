using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace High_School_Individual_Project
{
    public class App
    {
        private DatabaseLogic DatabaseLogic { get; set; }

        public App()
        {
            DatabaseLogic = new();
        }
        public void Start()
        {
            Console.WriteLine();
            Console.WriteLine("*** WELCOME TO THE HIGH SCHOOL ***");
            bool continueProgram = true;
            while (continueProgram)
            {
                Console.WriteLine();

                Console.WriteLine("Select an option:");
                Console.WriteLine("1.  Show faculty members info:");
                Console.WriteLine("2.  Add new faculty members:");
                Console.WriteLine("3.  Add new students, add grades, and show teachers names of who graded & dates:");
                Console.WriteLine("4.  Show classes with new students:");
                Console.WriteLine("5.  Show amount of faculty members at each department:");
                Console.WriteLine("6.  List info of students:");
                Console.WriteLine("7.  Show list of all active courses:");
                Console.WriteLine("8.  Show salary of each department and average salary of each department:");
                Console.WriteLine("Enter your choice (1, 2, 3, 4, 5, 6, 7, or 8):");
                Console.Write("Type 'exit' and press 'ENTER' to leave the program.");
                Console.WriteLine();
                Console.WriteLine();

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        DatabaseLogic.ListFaculties();
                        break;

                    case "2":
                        DatabaseLogic.AddNewFaculty();
                        Console.WriteLine("New faculty member was added successfully.");
                        break;

                    case "3":
                        DatabaseLogic.AddNewStudents();
                        Console.WriteLine("New student and grades were added successfully.");
                        break;

                    case "4":
                        DatabaseLogic.ListClassesWithNewStudents();
                        break;

                    case "5":
                        DatabaseLogic.ListTeachersDepartments();
                        break;

                    case "6":
                        DatabaseLogic.ListStudents();
                        break;

                    case "7":
                        DatabaseLogic.ListActiveCourses();
                        break;

                    case "8":
                        DatabaseLogic.ListSalary();
                        break;

                    case "exit":
                        continueProgram = false;
                        Console.WriteLine("Exiting the program. Good Bye!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
    }
}