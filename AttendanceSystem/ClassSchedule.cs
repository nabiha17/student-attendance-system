using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem;

public class ClassSchedule
{
    public int Id { get; set; }

    public DayOfWeek DayOfWeek { get; set; }


    public TimeSpan ClassStartTime { get; set; }


    public TimeSpan ClassEndTime { get; set; }

}

