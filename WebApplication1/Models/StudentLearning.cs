﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class StudentLearning
{
    public int CourseId { get; set; }

    public int StudentId { get; set; }

    public int? Grade { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
