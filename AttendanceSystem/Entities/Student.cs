namespace AttendanceSystem.Entities;

public class Student
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    List<StudentEnrollment> StudentEnrollments { get; set; }
}
