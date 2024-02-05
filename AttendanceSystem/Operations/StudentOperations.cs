namespace AttendanceSystem.Operations;

using AttendanceSystem.Entities;
using AttendanceSystem.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

public class StudentOperations
{
    public static void HandleAttendance(int id)
    {
        var db = new TrainningDbContext();

        var courseIds = new List<int>();

        var enrolledCourses = new List<Course>();

        var studentEnrollments = db.Courses.Select(x => x.StudentEnrollments.Where(x => x.StudentId == id));

        foreach (var items in studentEnrollments)
        {
            foreach (var item in items)
            {
                courseIds.Add(item.CourseId);
            }
        }

        foreach (var courseId in courseIds)
        {
            var course = db.Courses.Where(x => x.Id == courseId).Include(x => x.ClassDetails).ThenInclude(x => x.ClassSchedules).FirstOrDefault();

            enrolledCourses.Add(course);
        }



        if (enrolledCourses.Count > 0)
        {
            Console.WriteLine(" Your Courses: ");

            Console.WriteLine(" ");

            for (int i = 0; i < enrolledCourses.Count; i++)
            {
                var serialNo = i + 1;

                Console.WriteLine($" {serialNo}: {enrolledCourses[i].Name}");
            }

            while (true)
            {
                Console.WriteLine(" ");

                Console.Write(" Select a course for attendance: ");

                var number = Console.ReadLine().Trim();

                var isNumeric = Regex.IsMatch(number, @"^\d+$");

                if (!string.IsNullOrEmpty(number) && isNumeric && int.Parse(number) > 0 && int.Parse(number) <= enrolledCourses.Count)
                {
                    var selectedCourse = enrolledCourses[int.Parse(number) - 1];

                    var attendanceDuplicates = db.Courses
                        .Select(x => x.Attendances.Where(x => x.CourseId == selectedCourse.Id && x.StudentId == id));

                    var isDuplicate = false;

                    foreach (var items in attendanceDuplicates)
                    {
                        foreach (var item in items)
                        {
                            if (item.AttendanceTime < DateTime.Now && item.AttendanceTime > DateTime.Now.Date)
                            {
                                isDuplicate = true;
                            }
                        }
                    }

                    if (!isDuplicate)
                    {
                        var classDetails = selectedCourse.ClassDetails.Where(x => x.CourseId == selectedCourse.Id).FirstOrDefault();

                        var classSchedules = classDetails.ClassSchedules;

                        var todayDate = DateTime.Now;

                        var isTodayClassDay = false;

                        var isItClassTime = false;

                        foreach (var schedule in classSchedules)
                        {
                            if (schedule.DayOfWeek == todayDate.DayOfWeek)
                            {
                                isTodayClassDay = true;

                                var startTime = schedule.ClassStartTime;

                                var endTime = schedule.ClassEndTime;

                                var currentTime = DateTime.Now.TimeOfDay;

                                if (currentTime > startTime && currentTime < endTime)
                                {
                                    isItClassTime = true;
                                }

                            }
                        }

                        if (todayDate > classDetails.CourseStartDate && todayDate < classDetails.CourseEndDate && isTodayClassDay && isItClassTime)
                        {
                            var attendance = new Attendance();

                            var std = db.Students.Where(x => x.Id == id).FirstOrDefault();

                            attendance.Course = db.Courses.Where(x => x.Id == selectedCourse.Id).Include(x => x.Attendances).FirstOrDefault();

                            attendance.Student = std;

                            attendance.Status = "√";

                            attendance.AttendanceTime = DateTime.UtcNow;

                            attendance.StudentName = std.Name;

                            selectedCourse.Attendances.Add(attendance);

                            db.SaveChanges();

                            Console.WriteLine();
                            Console.WriteLine("------------------------------------------------------------------------");
                            Helper.WriteColorLine($" [Success], Attendance given for {selectedCourse.Name} on {DateTime.Now.DayOfWeek}. ", ConsoleColor.Green);
                            Console.WriteLine("------------------------------------------------------------------------");
                        }
                        else
                        {
                            Console.WriteLine("------------------------------------------------------------------------");
                            Helper.WriteColorLine(" [You can not can’t give attendance outside of date & class time.] ", ConsoleColor.Red);
                            Console.WriteLine("------------------------------------------------------------------------");
                        }
                    }
                    else
                    {
                        Console.WriteLine("------------------------------------------------------------------------");
                        Helper.WriteColorLine(" [Student is not allowed to give attendance more than once.]", ConsoleColor.Red);
                        Console.WriteLine("------------------------------------------------------------------------");

                    }
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Helper.WriteColorLine(" [Invalid input, Try again]", ConsoleColor.Red);
                }
            }


        }
        else
        {
            Console.WriteLine(" You have not enrolled in any courses yet.");
        }
    }
}
