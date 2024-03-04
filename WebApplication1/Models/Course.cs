using System;
using System.Collections.Generic;
using FluentValidation;

namespace WebApplication1.Models;

public class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public int TotalHours { get; set; }

    public virtual ICollection<StudentLearning> StudentLearnings { get; set; } = new List<StudentLearning>();

    public virtual ICollection<Department> Departs { get; set; } = new List<Department>();
}

public class CourseValidator : AbstractValidator<Course>
{
    public CourseValidator()
    {
        RuleFor(c => c.CourseName)
            .NotEmpty().WithMessage("Course Name is not allowed To Be Null")
            .Length(4, 20).WithMessage("The Course Name Length must be between 4 and 20 Character long");

        RuleFor(c => c.TotalHours)
            .NotEmpty().WithMessage("Course Hours is not allowed To Be Null")
            .InclusiveBetween(10, 100).WithMessage("Course Hour must between 10 and 100");

    }
}
