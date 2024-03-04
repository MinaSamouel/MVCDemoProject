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
    public class IndexModel : PageModel
    {
        private readonly IStudentRepo _context;

        public IndexModel(IStudentRepo context)
        {
            _context = context;
        }

        public IList<Models.Student> Student { get;set; } = default!;

        public void OnGet()
        {
            Student = _context.GetAll();
        }
    }
}
