using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Reposatory;

namespace WebApplication1.Pages.Student
{
    public class EditModel : PageModel
    {
        private readonly IStudentRepo _studentRepo;
        private readonly IDepartmetRepo _departmentRepo;

        public EditModel(IStudentRepo studentRepo, IDepartmetRepo departmentRepo)
        {
            _studentRepo = studentRepo;
            _departmentRepo = departmentRepo;
        }

        [BindProperty]
        public Models.Student Student { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student =  _studentRepo.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            Student = student;
           ViewData["DepartmentId"] = new SelectList(_departmentRepo.GetAll(), "DepartmentId", "DepartmentName", Student.DepartmentId);

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost(Models.Student student)
        {
            var studentValidator = new StudentValidator();
            var result = studentValidator.Validate(student);

            if (!ModelState.IsValid || !result.IsValid)
            {
                ModelState.AddModelError("", result.ToString());
                ViewData["DepartmentId"] = new SelectList(_departmentRepo.GetAll(), "DepartmentId", "DepartmentName", Student.DepartmentId);
                return Page();
            }


            _studentRepo.Update(student);

            return RedirectToPage("./Index");
        }

    }
}
