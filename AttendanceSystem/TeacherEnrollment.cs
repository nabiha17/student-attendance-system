﻿using AttendanceSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem;

public class TeacherEnrollment
{
    public int TeacherId { get; set; }

    public Teacher Teacher { get; set; }

    public int CourseId { get; set; }

    public Course Course { get; set; }
}