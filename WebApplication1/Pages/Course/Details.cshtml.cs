using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Reposatory;

namespace WebApplication1.Pages.Course
{
    public class DetailsModel : PageModel
    {
        private readonly ICourseRepo _courseRepo;

        public DetailsModel(ICourseRepo courseRepo)
        {
            _courseRepo = courseRepo;
        }
        public Models.Course Course { get; set; }
        public void OnGet(int? id)
        {
            Course = _courseRepo.GetById(id);

        }
    }
}
