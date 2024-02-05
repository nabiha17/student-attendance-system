namespace AttendanceSystem.Utilities;

using AttendanceSystem.Entities;
using System.Text.RegularExpressions;

public class Helper
{
    public static bool CheckContinuity()
    {
        while (true)
        {
            Console.WriteLine();
            Console.Write(" Do you want to continue? Yes or No: ");
            var ans = Console.ReadLine().Trim();
            var boolList = Data.GetBooleans();

            if (ans != null)
            {
                if (boolList.Contains(ans.ToLower()))
                {
                    if (ans.ToLower() == "yes")
                        return true;
                    else
                        return false;
                }
                else
                {
                    Console.WriteLine();
                    Helper.WriteColorLine(" [Invalid input]", ConsoleColor.Red);
                }
            }
            else
            {
                Console.WriteLine();
                Helper.WriteColorLine(" [Invalid input]", ConsoleColor.Red);
            }
        }
    }
    public static void PrintHorizontal()
    {
        Console.WriteLine();
        Console.WriteLine("------------------------------------------------------------------");
        Console.WriteLine();
    }

    public static void WriteColorLine(string message, ConsoleColor color)
    {
        var pieces = Regex.Split(message, @"(\[[^\]]*\])");

        for (int i = 0; i < pieces.Length; i++)
        {
            string piece = pieces[i];

            if (piece.StartsWith("[") && piece.EndsWith("]"))
            {
                Console.ForegroundColor = color;
                piece = piece.Substring(1, piece.Length - 2);
            }

            Console.Write(piece);
            Console.ResetColor();
        }

        Console.WriteLine();
    }

    public static void DisplayInvalidCredentials()
    {
        Console.WriteLine(" ");

        WriteColorLine(" [Invalid Credentials, Try again]", ConsoleColor.Red);

        Console.WriteLine(" ");
    }

    public static void DisplayWelcomeMessage(List<string> users)
    {
        Console.WriteLine();

        WriteColorLine("[----------------------------------]", ConsoleColor.Green);

        WriteColorLine("  [Welcome to Izak Attendance System]", ConsoleColor.Green);

        WriteColorLine("[----------------------------------]", ConsoleColor.Green);

        Console.WriteLine();

        Console.WriteLine("  User types for our system: ");

        Console.WriteLine();

        for (int i = 0; i < users.Count; i++)
        {
            Console.WriteLine($"  {i + 1}.  {users[i]}");
        }

        Console.WriteLine();

        Console.Write("  Select User Type (1/2/3): ");
    }

    public static Admin GetAdmin(Dictionary<string, string> credentials)
    {
        var trainningDbContext = new TrainningDbContext();

        var admin = default(Admin);

        foreach (var keyValuePair in credentials)
        {
            admin = trainningDbContext.Admins.Where(x => x.UserName == keyValuePair.Key && x.Password == keyValuePair.Value).FirstOrDefault();
        }

        return admin;
    }

    public static Teacher GetTeacher(Dictionary<string, string> credentials)
    {
        var trainningDbContext = new TrainningDbContext();

        var teacher = default(Teacher);

        foreach (var keyValuePair in credentials)
        {
            teacher = trainningDbContext.Teachers.Where(x => x.UserName == keyValuePair.Key && x.Password == keyValuePair.Value).FirstOrDefault();
        }

        return teacher;
    }

    public static Student GetStudent(Dictionary<string, string> credentials)
    {
        var trainningDbContext = new TrainningDbContext();

        var student = default(Student);

        foreach (var keyValuePair in credentials)
        {
            student = trainningDbContext.Students.Where(x => x.UserName == keyValuePair.Key && x.Password == keyValuePair.Value).FirstOrDefault();
        }

        return student;
    }

    public static string ReadPassword()
    {
        string password = "";
        ConsoleKeyInfo info = Console.ReadKey(true);
        while (info.Key != ConsoleKey.Enter)
        {
            if (info.Key != ConsoleKey.Backspace)
            {
                Console.Write("*");
                password += info.KeyChar;
            }
            else if (info.Key == ConsoleKey.Backspace)
            {
                if (!string.IsNullOrEmpty(password))
                {
                    // remove one character from the list of password characters
                    password = password.Substring(0, password.Length - 1);
                    // get the location of the cursor
                    int pos = Console.CursorLeft;
                    // move the cursor to the left by one character
                    Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    // replace it with space
                    Console.Write(" ");
                    // move the cursor to the left by one character again
                    Console.SetCursorPosition(pos - 1, Console.CursorTop);
                }
            }
            info = Console.ReadKey(true);
        }

        // add a new line because user pressed enter at the end of their password
        Console.WriteLine();
        return password;
    }

    public static Dictionary<string, string> GetCredentials(string userType)
    {
        var dictionary = new Dictionary<string, string>();

        while (true)
        {
            PrintHorizontal();

            WriteColorLine($" Enter credentials for [{userType}] ", ConsoleColor.Green);

            Console.WriteLine();

            Console.Write(" Username: ");

            var userName = Console.ReadLine().Trim();

            Console.WriteLine();

            Console.Write(" Password: ");

            var password = ReadPassword();

            Helper.PrintHorizontal();

            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                dictionary.Add(userName, password);
                break;
            }
            else
            {
                Console.WriteLine(" Please enter user name and password again.");
            }
        }

        return dictionary;
    }
    public static (DateTime, DateTime) GetCourseDuration()
    {
        //course start time and end time
        while (true)
        {
                var startDate = default(DateTime);

                var endDate = default(DateTime);
            while (true)
            {
                Console.WriteLine();

                Console.Write(" Enter Course Start Date (YYYY,MM,DD) : ");

                var start = Console.ReadLine().Trim();

                if (!string.IsNullOrEmpty(start))
                {
                    try
                    {
                        startDate = DateTime.Parse(start);

                        break;
                    }
                    catch (Exception)
                    {

                        Helper.WriteColorLine(" [Invalid date format, Try again]", ConsoleColor.Red);
                    }
                }
                else
                {
                    Helper.WriteColorLine(" [Invalid date format, Try again]", ConsoleColor.Red);
                }
            }

            while (true)
            {
                Console.WriteLine();

                Console.Write(" Enter Course End Date (YYYY,MM,DD) : ");

                var end = Console.ReadLine().Trim();

                if (!string.IsNullOrEmpty(end))
                {
                    try
                    {
                        endDate = DateTime.Parse(end);

                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine();

                        Helper.WriteColorLine(" [Invalid date format, Try again]", ConsoleColor.Red);
                    }
                }
                else
                {
                    Console.WriteLine();

                    Helper.WriteColorLine(" [Invalid date format, Try again]", ConsoleColor.Red);
                }
            }

            if (startDate.ToString() != null && endDate.ToString() != null && startDate < endDate)
            {
                return (startDate, endDate);
            }
            else
            {
                Console.WriteLine();

                Helper.WriteColorLine(" [Invalid date, Try again]", ConsoleColor.Red);
            }
        }
    }

    public static DayOfWeek GetDayOfWeek()
    {
        while (true)
        {
            Console.WriteLine();

            Console.WriteLine(" Day of week: ");

            var weeklyDays = Data.GetWeeklyDays();

            for (int i = 0; i < weeklyDays.Count; i++)
            {
                var serialNo = i + 1;
                Console.WriteLine($" {serialNo}: {weeklyDays[i]}");
            }
            Console.WriteLine();

            Console.Write(" Select your day of week: ");

            var dayInput = Console.ReadLine().Trim();

            if (!string.IsNullOrEmpty(dayInput))
            {
                switch (dayInput)
                {
                    case "1":
                        return DayOfWeek.Monday;

                    case "2":
                        return DayOfWeek.Tuesday;

                    case "3":
                        return DayOfWeek.Wednesday;

                    case "4":
                        return DayOfWeek.Thursday;

                    case "5":
                        return DayOfWeek.Friday;

                    case "6":
                        return DayOfWeek.Saturday;

                    case "7":
                        return DayOfWeek.Sunday;
                }
            }
            else
            {
                WriteColorLine(" [Invalid input], Select between 1 to 7", ConsoleColor.Red);
            }
        }
    }

    public static (TimeSpan, TimeSpan) GetClassStartEndTime()
    {
        while (true)
        {
            var slots = Data.GetClassSlots();

            Console.WriteLine(" Available course slots: ");

            var serialNo = default(int);

            for (int i = 0; i < slots.Count; i++)
            {
                serialNo = i + 1;

                Console.WriteLine($"{serialNo}: {slots[i]}");
            }

            Console.Write(" Select your slot :");

            var selectedSlotNumber = Console.ReadLine().Trim();

            if (selectedSlotNumber != null && int.Parse(selectedSlotNumber) > 0 && int.Parse(selectedSlotNumber) < 4)
            {
                switch (selectedSlotNumber)
                {
                    case "1":
                        {
                            var classStartTime = new TimeSpan(09, 00, 00);

                            var classEndTime = new TimeSpan(11, 00, 00);

                            return (classStartTime, classEndTime);
                        }
                    case "2":
                        {
                            var classStartTime = new TimeSpan(15, 00, 00);

                            var classEndTime = new TimeSpan(17, 00, 00);

                            return (classStartTime, classEndTime);
                        }
                    case "3":
                        {
                            var classStartTime = new TimeSpan(21, 00, 00);

                            var classEndTime = new TimeSpan(23, 00, 00);

                            return (classStartTime, classEndTime);
                        }
                }
            }
            else
            {
                WriteColorLine(" [Invalid slot, try agian]", ConsoleColor.Red);
            }
        }
    }
}