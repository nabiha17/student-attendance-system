namespace AttendanceSystem;

using AttendanceSystem.Entities;
using AttendanceSystem.Operations;
using AttendanceSystem.Utilities;
using System.Text.RegularExpressions;

public class Program
{
    public static void Main()
    {
        var users = Data.GetUsersList();

        var adminOperations = Data.GetAdminOperationsList();

        Helper.DisplayWelcomeMessage(users);

        while (true)
        {
            var input = Console.ReadLine().Trim();

            var isNumeric = Regex.IsMatch(input, @"^\d+$");

            if (!string.IsNullOrEmpty(input) && isNumeric && int.Parse(input) > 0 && int.Parse(input) < 4)
            {
                var trimed = input.Trim();
                var userType = users[int.Parse(trimed) - 1];

                while (true)
                {
                    var credentials = Helper.GetCredentials(userType);

                    var admin = default(Admin);

                    var student = default(Student);

                    var teacher = default(Teacher);


                    if (userType == "Admin")
                    {
                        admin = Helper.GetAdmin(credentials);

                        if (admin != null)
                        {
                            while (true)
                            {
                                var operationName = AdminOperations.GetAdminOperation(adminOperations, admin.Name);

                                if (operationName.Contains("Create"))
                                {
                                    AdminOperations.HandleEntityCreation(operationName);
                                }
                                else if (operationName.Contains("Assign a teacher in a course"))
                                {
                                    AdminOperations.HandleTeacherAssignment();
                                }
                                else if (operationName.Contains("Assign a student in a course"))
                                {
                                    AdminOperations.HandleStudentAssignment();
                                }
                                else if (operationName.Contains("Set class schedule for a course"))
                                {
                                    AdminOperations.HandleSetClassSchedule();
                                }

                                var willContinue = Helper.CheckContinuity();

                                if (!willContinue)
                                {
                                    Console.WriteLine();

                                    Helper.WriteColorLine(" [Program Ends]", ConsoleColor.Yellow);

                                    break;
                                }
                            }
                            break;
                        }
                        else
                        {
                            Helper.DisplayInvalidCredentials();
                        }

                        break;
                    }
                    else if (userType == "Student")
                    {
                        student = Helper.GetStudent(credentials);

                        if (student != null)
                        {
                            while (true)
                            {
                                StudentOperations.HandleAttendance(student.Id);

                                var willContinue = Helper.CheckContinuity();

                                if (!willContinue)
                                {
                                    Console.WriteLine();

                                    Helper.WriteColorLine(" [Program Ends]", ConsoleColor.DarkGreen);

                                    break;
                                }
                            }
                            break;
                        }
                        else
                        {
                            Helper.DisplayInvalidCredentials();
                        }
                    }
                    else if (userType == "Teacher")
                    {
                        teacher = Helper.GetTeacher(credentials);

                        if (teacher != null)
                        {
                            while (true)
                            {
                                TeacherOperations.DisplayAttendanceDetails(teacher.Id, teacher.Name);

                                var willContinue = Helper.CheckContinuity();

                                if (!willContinue)
                                {
                                    Console.WriteLine();

                                    Helper.WriteColorLine(" [Program Ends]", ConsoleColor.DarkGreen);

                                    break;
                                }
                            }
                            break;
                        }
                        else
                        {
                            Helper.DisplayInvalidCredentials();
                        }
                    }
                    else
                    {
                        Helper.WriteColorLine(" [Invalid user ! try again]", ConsoleColor.Red);
                    }
                }

                break;
            }
            else
            {
                Console.WriteLine();
                Helper.WriteColorLine(" [Invalid user type number, select between 1 to 3]", ConsoleColor.Red);
                Console.WriteLine();
                Console.Write(" Please enter a valid user type ( input : 1 or 2 or 3 ): ");
            }
        }
    }
}