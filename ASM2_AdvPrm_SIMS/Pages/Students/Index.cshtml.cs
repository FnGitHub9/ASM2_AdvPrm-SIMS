using ASM2_AdvPrm_SIMS.Models;
using ASM2_AdvPrm_SIMS.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASM2_AdvPrm_SIMS.Pages
{
    public class StudentsModel : PageModel
    {
        private readonly StudentService _service;
        public IList<Student> StudentList { get; set; } = default!;

        [BindProperty]
        public Student NewStudent { get; set; } = default!;

        public StudentsModel(StudentService service)
        {
            _service = service;
        }
        public void OnGet()
        {
            StudentList = _service.GetStudents();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || NewStudent == null)
            {
                return Page();
            }

            _service.AddStudent(NewStudent);

            return RedirectToAction("Get");
        }
        public IActionResult OnPostDelete(int id)
        {
            _service.RemoveStudent(id);
            return RedirectToAction("Get");
        }
    }
}
