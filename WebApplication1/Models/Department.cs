using FluentValidation;
using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}

public class DepartmentValidator : AbstractValidator<Department>
{
    public DepartmentValidator()
    {
        RuleFor(c => c.DepartmentId)
            .NotEmpty().WithMessage("Course hours is required\n")
            .Must(BeMultipleOf100).WithMessage("Department Number  must be a multiple of 100 like 100,200,300,....\n");

        RuleFor(x => x.DepartmentName)
            .NotEmpty().WithMessage("\nDepartment name is required\n")
            .Length(8, 50).WithMessage("\nDepartment name Should be between 8 and 50 letters\n");
    }
    private bool BeMultipleOf100(int hours)
    {
        // Your custom validation logic here
        return hours % 100 == 0;
    }
}

