using System.ComponentModel.DataAnnotations;

namespace ASM2_AdvPrm_SIMS.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string firstName { get; set; }
        public string lastName { get; set; }

        public string Password { get; set; }

        public int StudentNo { get; set; }

        public bool Status {  get; set; }
        public DateTime Birthdate { get; set; }

    }
}
