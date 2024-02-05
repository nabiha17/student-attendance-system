namespace AttendanceSystem.Operations;

using AttendanceSystem.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

public class TeacherOperations
{
    public static void DisplayAttendanceDetails(int id, string name)
    {
        var db = new TrainningDbContext();

        var enrolledCourseIds = db.Courses.Select(x => x.TeacherEnrollments.Where(x=> x.TeacherId == id).Select(x=> x.CourseId));       

        var courseIds = new List<int>();

        foreach (var enrolledCourseId in enrolledCourseIds)
        {
            foreach (var item in enrolledCourseId)
            {
                courseIds.Add(item);
            }
        }

        var courses = db.Courses.Where(x => courseIds.Contains(x.Id)).Include( x=> x.ClassDetails).ThenInclude( x => x.ClassSchedules).ToArray();

        if (courses.Length > 0)
        {
            Console.WriteLine(" ");
            Console.WriteLine(" Your courses: ");
            Console.WriteLine(" ");

            for (int i = 0; i < courses.Count(); i++)
            {
                var serialNo = i + 1;
                var c = courses[i];
                Console.WriteLine($" {serialNo}: {c.Name}");
            }
        }
        else
        {
            Console.WriteLine("---------------------");
            Console.WriteLine(" No courses for you.");
            Console.WriteLine("---------------------");
        }

        Console.WriteLine(" ");



        while (true)
        {
            Console.WriteLine();
            Console.Write(" Select your course number: ");

            var selectedCourseNo = Console.ReadLine().Trim();
            
            var isNumeric = Regex.IsMatch(selectedCourseNo, @"^\d+$");

            if (!string.IsNullOrEmpty(selectedCourseNo) && isNumeric  && int.Parse(selectedCourseNo) > 0 && int.Parse(selectedCourseNo) <= courses.Count())
            {
                var selectedCourseId = courses[int.Parse(selectedCourseNo) - 1].Id;

                var attendances = db.Courses.Select(x => x.Attendances.Where(x => x.CourseId == selectedCourseId));

                if (attendances.ToArray().Length > 0)
                {
                    Console.WriteLine(" ");
                    foreach (var attendance in attendances)
                    {
                        foreach (var a in attendance)
                        {
                            Helper.PrintHorizontal();
                            Console.WriteLine($" Name: {a.StudentName} | Attendance status : {a.Status} | Attendance time: {a.AttendanceTime}");
                            Helper.PrintHorizontal();
                            Console.WriteLine(" ");
                        }
                    }
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Helper.WriteColorLine(" [No attendance available.]", ConsoleColor.Red);
                }
            }
            else
            {
                Console.WriteLine();
                Helper.WriteColorLine(" [Invalid input, Try again]", ConsoleColor.Red);
            }
        }       
        
    }
}
