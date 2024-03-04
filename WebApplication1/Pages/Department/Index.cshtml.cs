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
    public class IndexModel : PageModel
    {
        private readonly IDepartmetRepo _departmentRepo;

        public IndexModel(IDepartmetRepo departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }

        public IList<Models.Department> Department { get;set; } = default!;

        public void  OnGet()
        {
            Department = _departmentRepo.GetAll();
        }
    }
}
