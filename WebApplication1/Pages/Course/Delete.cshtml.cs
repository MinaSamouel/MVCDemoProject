using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Reposatory;

namespace WebApplication1.Pages.Course
{
    public class DeleteModel : PageModel
    {
        private readonly ICourseRepo _courseRepo;

        public DeleteModel(ICourseRepo courseRepo)
        {
            _courseRepo = courseRepo;
        }

        [BindProperty]
        public Models.Course Course { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = _courseRepo.GetById(id);

            if (course == null)
            {
                return NotFound();
            }

            Course = course;
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = _courseRepo.GetById(id);

            if (course != null)
            {
                _courseRepo.Delete(id);
            }


            return RedirectToPage("/Course/Index");
        }
    }
}
