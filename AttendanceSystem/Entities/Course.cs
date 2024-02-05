namespace AttendanceSystem.Entities;

public class Course
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Fees { get; set; }

    public List<ClassDetails> ClassDetails { get; set; }

    public List<TeacherEnrollment> TeacherEnrollments { get; set; }

    public List<StudentEnrollment> StudentEnrollments { get; set; }

    public List<Attendance> Attendances { get; set; }
}
