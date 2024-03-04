using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Reposatory;
using WebApplication1.Models;

namespace WebApplication1.Pages.Course
{
    public class CreateModel : PageModel
    {
        private readonly ICourseRepo _courseRepo;

        public CreateModel(ICourseRepo courseRepo)
        {
            _courseRepo = courseRepo;
        }

        [BindProperty]
        public Models.Course Course { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost(Models.Course course)
        {
            var courseValidate = new CourseValidator();
            var result = courseValidate.Validate(course);

            if (!result.IsValid)
            {
                ModelState.AddModelError("", result.ToString());
                return Page();
            }

            _courseRepo.Add(course);

            return RedirectToPage("Index");
        }
    }
}
