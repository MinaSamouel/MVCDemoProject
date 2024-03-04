using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Reposatory;

namespace WebApplication1.Pages.Course
{
    public class EditModel : PageModel
    {
        private readonly ICourseRepo _courseRepo;

        public EditModel(ICourseRepo courseRepo)
        {
            _courseRepo = courseRepo;
        }

        [BindProperty]
        public Models.Course Course{ get; set; }
        public IActionResult OnGet(int? id)
        {
            Course = _courseRepo.GetById(id);
            return Page();
        }

        public IActionResult OnPost(Models.Course course)
        {
            var courseValidator = new CourseValidator();
            var result = courseValidator.Validate(course);

            if (!result.IsValid)
            {
                ModelState.AddModelError("", result.ToString());
                return Page();
            }

            _courseRepo.Update(course);

            return RedirectToPage("Index");
        }
    }
}
