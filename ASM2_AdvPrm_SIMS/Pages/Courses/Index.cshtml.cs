using ASM2_AdvPrm_SIMS.Models;
using ASM2_AdvPrm_SIMS.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASM2_AdvPrm_SIMS.Pages
{
    public class CoursesModel : PageModel
    {
        private readonly CourseService _service;
        public IList<Course> CourseList { get; set; } = default!;

        [BindProperty]
        public Course NewCourse { get; set; } = default!;

        public CoursesModel(CourseService service)
        {
            _service = service;
        }
        public void OnGet()
        {
            CourseList = _service.GetCourses();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || NewCourse == null)
            {
                return Page();
            }

            _service.AddCourse(NewCourse);

            return RedirectToAction("Get");
        }
        public IActionResult OnPostDelete(int id)
        {
            _service.DeleteCourse(id);
            return RedirectToAction("Get");
        }
    }
}
