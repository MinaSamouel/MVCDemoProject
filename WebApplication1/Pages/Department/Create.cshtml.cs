using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
using WebApplication1.Reposatory;

namespace WebApplication1.Pages.Department
{
    public class CreateModel : PageModel
    {
        private readonly IDepartmetRepo _departmentRepo;

        public CreateModel(IDepartmetRepo departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Department Department { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            var departmentValidator = new DepartmentValidator();
            var result = departmentValidator.Validate(Department);

            if (!ModelState.IsValid|| !result.IsValid)
            {
                ModelState.AddModelError("", result.ToString());
                return Page();
            }

            _departmentRepo.Add(Department);

            return RedirectToPage("/Department/Index");
        }
    }
}
