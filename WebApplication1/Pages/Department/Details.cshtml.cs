using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Reposatory;

namespace WebApplication1.Pages.Department
{
    public class DetailsModel : PageModel
    {
        private readonly IDepartmetRepo _departmetRepo;

        public DetailsModel(WebApplication1.Models.ItiDemoContext context, IDepartmetRepo departmetRepo)
        {
            _departmetRepo = departmetRepo;
        }

        public Models.Department Department { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _departmetRepo.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            
            Department = department;
            
            return Page();
        }
    }
}
