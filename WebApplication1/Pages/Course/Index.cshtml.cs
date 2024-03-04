using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.Reposatory;

namespace WebApplication1.Pages.Course
{
    
    public class IndexModel : PageModel
    {
        
        private readonly ICourseRepo _courseRepo;

        public IndexModel(ICourseRepo courseRepo)
        {
            _courseRepo = courseRepo;
        }

        public List<Models.Course> Courses { get; set; } = new ();

        public void OnGet()
        {
            Courses = _courseRepo.GetAll();
        }
    }
}
