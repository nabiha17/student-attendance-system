namespace AttendanceSystem.Operations;

using AttendanceSystem.Entities;
using AttendanceSystem.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

public class AdminOperations
{

    public static Dictionary<string, int> GetCreationInfo(string item)
    {
        Dictionary<string, int> info = new Dictionary<string, int>();

        Console.Write($" {item} Name: ");
        var name = Console.ReadLine().Trim();

        Console.Write($" {item} Fees: ");
        var fees = int.Parse(Console.ReadLine().Trim());

        info.Add(name, fees);

        return info;
    }

    public static string GetAdminOperation(List<string> adminOperation, string userName)
    {
        Helper.WriteColorLine($" Welcome [{userName}]. Your available admin Operations", ConsoleColor.Green);
        Console.WriteLine();

        for (int i = 0; i < adminOperation.Count; i++)
        {
            var serialNo = i + 1;

            Console.WriteLine($" {serialNo}. {adminOperation[i]}");
        }

        Helper.PrintHorizontal();

        while (true)
        {
            Console.WriteLine();
            Console.Write(" Select your operation: ");

            var operationNo = Console.ReadLine().Trim();

            var isNumeric = Regex.IsMatch(operationNo, @"^\d+$");

            if (operationNo != null && isNumeric && int.Parse(operationNo) > 0 && int.Parse(operationNo) <= adminOperation.Count)
                return adminOperation[int.Parse(operationNo) - 1];

            else
                Helper.WriteColorLine(" [Invalid operation, Try again]", ConsoleColor.Red);
        }

    }

    public static void CreateCourse(string name, int fees)
    {
        TrainningDbContext db = new TrainningDbContext();
        var check = db.Courses.Where(c => c.Name == name).ToList();

        if (check.Count > 0)
        {
            Console.WriteLine(" Course already exists. Try again.");
        }
        else
        {
            var course = new Course();
            course.Name = name;
            course.Fees = fees;

            db.Courses.Add(course);

            db.SaveChanges();
            Console.WriteLine("---------------------------------------------------- ");
            Helper.WriteColorLine($" [Success], Course: {name} is created with fees: {fees}.", ConsoleColor.Green);
            Console.WriteLine("---------------------------------------------------- ");
        }

       
    }

    public static void CreateStudent(string name, string userName, string password)
    {
        TrainningDbContext db = new TrainningDbContext();
        var check = db.Students.Where(c => c.UserName == userName).ToList();

        if (check.Count > 0)
        {
            Helper.WriteColorLine(" [This teacher user name exists, Try different one]", ConsoleColor.Red);
        }
        else
        {
            var student = new Student();
            student.Name = name;
            student.UserName = userName;
            student.Password = password;

            db.Students.Add(student);

            db.SaveChanges();

            Helper.PrintHorizontal();
            Helper.WriteColorLine($" [Success], Student: {name} is created with userName : {userName} and password: {password}", ConsoleColor.Green);
            Helper.PrintHorizontal();
        }       
    }

    public static void CreateTeacher(string name, string userName, string password)
    {
        TrainningDbContext db = new TrainningDbContext();
        var check = db.Teachers.Where(c => c.UserName == userName).ToList();

        if (check.Count > 0)
        {
            Helper.WriteColorLine($" [This student user name: {userName} exists, try different one]", ConsoleColor.Green);
        }
        else
        {
            var teacher = new Teacher();
            teacher.Name = name;
            teacher.UserName = userName;
            teacher.Password = password;

            db.Teachers.Add(teacher);

            db.SaveChanges();

            Console.WriteLine("---------------------------------------------------------------------------------------- ");
            Helper.WriteColorLine($" [Success, Teacher: {name} is created with userName : {userName} and password: {password}]", ConsoleColor.Green);
            Console.WriteLine("---------------------------------------------------------------------------------------- ");
        }
    }

    public static void HandleEntityCreation(string operation)
    {
        if (operation == "Create Course")
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write(" Course Name: ");
                var courseName = Console.ReadLine().Trim();
                Console.WriteLine();
                Console.Write(" Course Fees: ");
                var courseFees = Console.ReadLine().Trim();

                if (!string.IsNullOrEmpty(courseName) && !string.IsNullOrEmpty(courseFees))
                {
                    CreateCourse(courseName, int.Parse(courseFees));
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Helper.WriteColorLine(" [Invalid course name or course fee, Try again]", ConsoleColor.Red);
                }
            }
        }
        else if (operation == "Create Teacher")
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write(" Teacher Name: ");
                var name = Console.ReadLine().Trim();
                Console.WriteLine();
                Console.Write(" Teacher Username: ");
                var username = Console.ReadLine().Trim();
                Console.WriteLine();
                Console.Write(" Teacher password: ");
                var password = Console.ReadLine().Trim();

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    CreateTeacher(name, username, password);
                    break;
                }
                else
                {
                    Helper.WriteColorLine(" [Invalid Name or username or password, Try again]", ConsoleColor.Red);
                }
            }
        }
        else if (operation == "Create Student")
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write(" Student Name: ");
                var name = Console.ReadLine().Trim();
                Console.WriteLine();
                Console.Write(" Student Username: ");
                var username = Console.ReadLine().Trim();
                Console.WriteLine();
                Console.Write(" Student password: ");
                var password = Console.ReadLine().Trim();

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    CreateStudent(name, username, password);
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Helper.WriteColorLine(" [Invalid name or username or password, Try again]", ConsoleColor.Red);
                }
            }
        }
    }

    public static void HandleTeacherAssignment()
    {
        while (true)
        {
            Console.WriteLine();
            Console.Write(" Teacher username: ");
            var teacherUserName = Console.ReadLine().Trim();

            Console.WriteLine();
            Console.Write(" Course name: ");
            var courseName = Console.ReadLine().Trim();

            if (!string.IsNullOrEmpty(teacherUserName) && !string.IsNullOrEmpty(courseName))
            {
                var db = new TrainningDbContext();

                var teacher = db.Teachers.Where(x => x.UserName == teacherUserName).FirstOrDefault();

                var course = db.Courses.Where(x => x.Name == courseName).FirstOrDefault();

                if (teacher != null && course != null)
                {
                    course.TeacherEnrollments = new List<TeacherEnrollment>();
                    var enrollment = new TeacherEnrollment();
                    enrollment.Teacher = teacher;
                    enrollment.Course = course;
                    course.TeacherEnrollments.Add(enrollment);
                     
                    db.SaveChanges();
                    Helper.PrintHorizontal();
                    Helper.WriteColorLine($" [Success, {teacherUserName} assigned as a instructor of {courseName} course]", ConsoleColor.Green);
                    Helper.PrintHorizontal();
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Helper.WriteColorLine(" [Invalid teacher or course name]", ConsoleColor.Red);
                }
                
            }
            else
            {
                Console.WriteLine();
                Helper.WriteColorLine(" [Invalid teacher or course name]", ConsoleColor.Red);
            }
        }
    }

    public static void HandleStudentAssignment()
    {
        while (true)
        {
            Console.WriteLine();
            Console.Write(" Student username: ");
            var studentUserName = Console.ReadLine().Trim();

            Console.WriteLine();
            Console.Write(" Course name: ");
            var courseName = Console.ReadLine().Trim();

            if (!string.IsNullOrEmpty(studentUserName) && !string.IsNullOrEmpty(courseName))
            {
                var db = new TrainningDbContext();

                var student = db.Students.Where(x => x.UserName == studentUserName).FirstOrDefault();

                var course = db.Courses.Where(x => x.Name == courseName).FirstOrDefault();

                if (student != null && course != null)
                {
                    course.StudentEnrollments = new List<StudentEnrollment>();

                    var enrollment = new StudentEnrollment();

                    enrollment.Student = student;

                    enrollment.Course = course;

                    course.StudentEnrollments.Add(enrollment);

                    Helper.PrintHorizontal();

                    Helper.WriteColorLine($" [{student.Name} has been enrolled to {course.Name} successfully]", ConsoleColor.Green);

                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine();

                    Helper.WriteColorLine(" [Unexpected error]", ConsoleColor.Red);
                }
                break;
            }
            else
            {
                Console.WriteLine();

                Helper.WriteColorLine(" [Invalid student and course name]", ConsoleColor.Red);
            }
        }
    }

    public static void HandleSetClassSchedule()
    {

        var db = new TrainningDbContext();

        var classScheduleList = new List<ClassSchedule>();

        var classDetails = new ClassDetails();
     
        var course = new Course();

        var courseName = default(string);

        var dayOfWeek = default(DayOfWeek);

        var classStartTime = default(TimeSpan);

        var classEndTime = default(TimeSpan);

        var courseStartDate = default(DateTime);

        var courseEndDate = default(DateTime);

        var decision = default(string);

        // Course name input
        while (true)
        {
            Console.WriteLine();
            Helper.WriteColorLine(" [Set class schedule operation :]", ConsoleColor.Cyan);
            Console.WriteLine();
            Console.Write(" Course name: ");
            courseName = Console.ReadLine().Trim();

            if (!string.IsNullOrEmpty(courseName))
            {
                course = db.Courses.Where(c => c.Name == courseName).Include( x => x.ClassDetails ).ThenInclude( x => x.ClassSchedules).FirstOrDefault();

                if (course != null)
                {
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Helper.WriteColorLine(" [Course is not found]", ConsoleColor.Red);
                }
            }
        }

        (courseStartDate, courseEndDate) = Helper.GetCourseDuration();

        // course Day and Time input
        while (true)
        {  
               var classSchedule = new ClassSchedule();

            dayOfWeek = Helper.GetDayOfWeek();

            (classStartTime, classEndTime) = Helper.GetClassStartEndTime();


            while (true)
            {
                Console.WriteLine();
                Console.Write(" Do you want to add more day time? Yes or No: ");

                decision = Console.ReadLine().Trim();

                if (!string.IsNullOrEmpty(decision) && decision.ToUpper() == "YES" || decision.ToUpper() == "NO")
                {
                    break;
                }
            }

            classSchedule.DayOfWeek = dayOfWeek;

            classSchedule.ClassStartTime = classStartTime;

            classSchedule.ClassEndTime = classEndTime;

            classScheduleList.Add(classSchedule);

            classDetails.ClassSchedules = classScheduleList;

            if (decision.ToUpper() == "NO")
            {
                break;
            }
        }

        var totalClassNumber = default(string);

        while (true)
        {
            Console.WriteLine();
            Console.Write(" Total number of class: ");

            totalClassNumber = Console.ReadLine().Trim();

            if (!string.IsNullOrEmpty(totalClassNumber))
            {
                break;
            }
        }        
        
        classDetails.TotalClass = totalClassNumber;

        classDetails.Course = course;

        classDetails.CourseStartDate = courseStartDate;

        classDetails.CourseEndDate = courseEndDate;

        course.ClassDetails.Add(classDetails);

        db.SaveChanges();

        Helper.WriteColorLine($" [You successfully set a schedule for {course.Name}]", ConsoleColor.Green);

        Console.WriteLine();

        Helper.PrintHorizontal();

        Console.WriteLine(" Your schedule - ");

        Console.WriteLine($" Course start date: {courseStartDate}");

        Console.WriteLine($" Course end date: {courseEndDate}");

        Console.WriteLine($" Class day of week: {dayOfWeek}");

        foreach (var item in classScheduleList)
        {
            Console.WriteLine($" Class timing {item.ClassStartTime} - {item.ClassEndTime} on {item.DayOfWeek}");
        }

        Console.WriteLine($" Total class number: {totalClassNumber}");

        Console.WriteLine();
    }
}
