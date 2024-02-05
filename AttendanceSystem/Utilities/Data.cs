using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.Utilities;

public class Data
{
    public static List<string> GetUsersList()
    {
        var users = new List<string> { "Admin", "Teacher", "Student" };

        return users;
    }

    public static List<string> GetAdminOperationsList()
    {
        var adminOperation = new List<string> { "Create Teacher", "Create Course", "Create Student", "Assign a teacher in a course", "Assign a student in a course", "Set class schedule for a course" };

        return adminOperation;
    }

    public static List<string> GetWeeklyDays()
    {
        var days = new List<string> { "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY", "SUNDAY" };

        return days;
    }

    public static List<string> GetClassSlots()
    {
        var slot1 = "9 am - 11 am";
        var slot2 = "3 pm - 5 pm";
        var slot3 = "9 pm - 11 pm";

        return new List<string> { slot1, slot2, slot3 };
    }

    public static List<string> GetBooleans()
    {
        return new List<string> { "yes", "no" };
    }
}
