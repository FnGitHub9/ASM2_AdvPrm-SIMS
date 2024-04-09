using System.ComponentModel.DataAnnotations;

namespace ASM2_AdvPrm_SIMS.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Status {  get; set; }
        public string Birthdate { get; set; }

    }
}
