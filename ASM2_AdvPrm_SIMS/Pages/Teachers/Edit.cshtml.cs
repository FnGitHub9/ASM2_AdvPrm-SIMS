using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ASM2_AdvPrm_SIMS.Models;
using ASM2_AdvPrm_SIMS.Service;

namespace ASM2_AdvPrm_SIMS.Pages.Teachers
{
    public class EditModel : PageModel
    {
        private readonly TeacherService _service;

        public EditModel(TeacherService service)
        {
            _service = service;
        }

        [BindProperty]
        public Teacher Teachers { get; set; }

        public async Task<IActionResult> OnGetAsync(int? itemid)
        {
            if (itemid == null)
            {
                return NotFound();
            }
            var teacher = _service.Teachers.FirstOrDefault(p => p.Id == itemid);
            if (teacher == null)
            {
                return NotFound();
            }
            Teachers = teacher;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _service.UpdateTeacher(Teachers.Id, Teachers);
            return RedirectToPage(nameof(Index));
        }
    }
}
