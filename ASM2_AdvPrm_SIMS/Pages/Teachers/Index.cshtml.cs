using ASM2_AdvPrm_SIMS.Models;
using ASM2_AdvPrm_SIMS.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASM2_AdvPrm_SIMS.Pages
{
    public class TeachersModel : PageModel
    {
        private readonly TeacherService _service;
        public IList<Teacher> TeacherList { get; set; } = default!;

        [BindProperty]
        public Teacher NewTeacher { get; set; } = default!;

        public TeachersModel(TeacherService service)
        {
            _service = service;
        }
        public void OnGet()
        {
            TeacherList = _service.GetTeachers();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || NewTeacher == null)
            {
                return Page();
            }

            _service.AddTeacher(NewTeacher);

            return RedirectToAction("Get");
        }
        public IActionResult OnPostDelete(int id)
        {
            _service.DeleteTeacher(id);
            return RedirectToAction("Get");
        }
    }
}
