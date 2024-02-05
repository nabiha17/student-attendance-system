using AttendanceSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem;

public class ClassDetails
{
    public int Id { get; set; }

    public List<ClassSchedule> ClassSchedules { get; set; }

    public string TotalClass { get; set; }

    public DateTime CourseStartDate { get; set; }

    public DateTime CourseEndDate { get; set; }

    public Course Course { get; set; }

    public int CourseId { get; set; }
}
