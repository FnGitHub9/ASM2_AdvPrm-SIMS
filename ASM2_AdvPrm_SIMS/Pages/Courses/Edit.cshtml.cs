using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ASM2_AdvPrm_SIMS.Models;
using ASM2_AdvPrm_SIMS.Service;

namespace ASM2_AdvPrm_SIMS.Pages.Courses
{
    public class EditModel : PageModel
    {
        private readonly CourseService _service;

        public EditModel(CourseService service)
        {
            _service = service;
        }

        [BindProperty]
        public Course Courses { get; set; }

        public async Task<IActionResult> OnGetAsync(int? itemid)
        {
            if (itemid == null)
            {
                return NotFound();
            }
            var pizza = _service.Courses.FirstOrDefault(p => p.Id == itemid);
            if (pizza == null)
            {
                return NotFound();
            }
            Courses = pizza;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _service.UpdateCourse(Courses.Id, Courses);
            return RedirectToPage(nameof(Index));
        }
    }
}
