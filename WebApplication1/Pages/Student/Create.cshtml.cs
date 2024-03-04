using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
using WebApplication1.Reposatory;

namespace WebApplication1.Pages.Student
{
    public class CreateModel : PageModel
    {
        private readonly IStudentRepo _studentRepo;
        private readonly IDepartmetRepo _departmentRepo;


        public CreateModel( IStudentRepo studentRepo, IDepartmetRepo departmentRepo)
        {
            _studentRepo = studentRepo;
            _departmentRepo = departmentRepo;
        }

        public IActionResult OnGet()
        {
        ViewData["DepartmentId"] = new SelectList(_departmentRepo.GetAll(), "DepartmentId", "DepartmentName");
            return Page();
        }

        [BindProperty]
        public Models.Student Student { get; set; } = default!;

        public IActionResult OnPost()
        {
            var validator = new StudentValidator();
            var result = validator.Validate(Student);

            if (!ModelState.IsValid || !result.IsValid)
            {
                ModelState.AddModelError("", result.ToString());
                ViewData["DepartmentId"] = new SelectList(_departmentRepo.GetAll(), "DepartmentId", "DepartmentName");
                return Page();
            }

            _studentRepo.Add(Student);
            
            return RedirectToPage("./Index");
        }
    }
}
