using System.ComponentModel.DataAnnotations;

namespace ASM2_AdvPrm_SIMS.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string SubjectCode { get; set; }
        public string Subject { get; set; }

        public bool Status { get; set; }
    }
}
