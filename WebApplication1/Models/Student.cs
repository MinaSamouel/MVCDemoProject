using FluentValidation;
using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public class Student
{
    public int StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int? StudentAge { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<StudentLearning> StudentLearnings { get; set; } = new List<StudentLearning>();
}


public class StudentValidator : AbstractValidator<Student>
{
    public StudentValidator()
    {
        RuleFor(s => s.StudentName)
            .NotEmpty().WithMessage("Student name is required")
            .Length(3, 20).WithMessage("Student name must be between 3 and 20 characters long");

        RuleFor(s => s.Email)
            .NotEmpty().WithMessage("Student email is required")
            .EmailAddress().WithMessage("Student email is not valid");

        RuleFor(s => s.StudentAge)
            .NotEmpty().WithMessage("Student age is required")
            .InclusiveBetween(18, 60).WithMessage("Student age must be between 18 and 60");
    }
}
