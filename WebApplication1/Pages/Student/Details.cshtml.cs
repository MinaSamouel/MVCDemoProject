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
    public class DetailsModel : PageModel
    {
        private readonly IStudentRepo _studentRepo;

        public DetailsModel(WebApplication1.Models.ItiDemoContext context, IStudentRepo studentRepo)
        {
            _studentRepo = studentRepo;
        }

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
    }
}
