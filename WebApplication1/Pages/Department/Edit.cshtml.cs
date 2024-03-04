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

namespace WebApplication1.Pages.Department
{
    public class EditModel : PageModel
    {
        private readonly IDepartmetRepo _departmentRepo;

        public EditModel(IDepartmetRepo departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }

        [BindProperty]
        public Models.Department Department { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department =  _departmentRepo.GetById(id);

            if (department == null)
            {
                return NotFound();
            }

            Department = department;
            return Page();
        }

        public IActionResult OnPost(Models.Department department)
        {
            var departmentValidator = new DepartmentValidator();
            var result = departmentValidator.Validate(department);

            if (!ModelState.IsValid || !result.IsValid)
            {
                ModelState.AddModelError("", result.ToString());
                return Page();
            }

            _departmentRepo.Update(department);
            return RedirectToPage("/Department/Index");
        }

    }
}
