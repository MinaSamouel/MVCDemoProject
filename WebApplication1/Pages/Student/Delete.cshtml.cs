using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Reposatory;

namespace WebApplication1.Pages.Student
{
    public class DeleteModel : PageModel
    {
        private readonly IStudentRepo _studentRepo;
        private readonly IDepartmetRepo _departmetRepo;

        public DeleteModel(IStudentRepo studentRepo, IDepartmetRepo departmetRepo)
        {
            _studentRepo = studentRepo;
            _departmetRepo = departmetRepo;
        }

        [BindProperty]
        public Models.Student Student { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _studentRepo.GetById(id);

            if (student == null)
            {
                return NotFound();
            }
            
            Student = student;
            
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _studentRepo.GetById(id);

            if (student != null)
            {
                _studentRepo.Delete(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
