using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ASM2_AdvPrm_SIMS.Models;
using ASM2_AdvPrm_SIMS.Service;

namespace ASM2_AdvPrm_SIMS.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly StudentService _service;

        public EditModel(StudentService service)
        {
            _service = service;
        }

        [BindProperty]
        public Student Students { get; set; }

        public async Task<IActionResult> OnGetAsync(int? itemid)
        {
            if (itemid == null)
            {
                return NotFound();
            }
            var student = _service.Students.FirstOrDefault(p => p.Id == itemid);
            if (student == null)
            {
                return NotFound();
            }
            Students = student;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _service.UpdateStudent(Students.Id, Students);
            return RedirectToPage(nameof(Index));
        }
    }
}
