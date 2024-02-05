using AttendanceSystem.Entities;

namespace AttendanceSystem;

public class Attendance
{
    public int Id { get; set; }

    public Student Student { get; set; }

    public int StudentId { get; set; }

    public string StudentName { get; set; }

    public Course Course { get; set; }

    public int CourseId { get; set; }

    public string Status { get; set; }

    public DateTime AttendanceTime { get; set; }
}
