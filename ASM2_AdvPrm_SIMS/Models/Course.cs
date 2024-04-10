using System.ComponentModel.DataAnnotations;

namespace ASM2_AdvPrm_SIMS.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string SubjectCode { get; set; }
        public string Subject { get; set; }

        public string Status { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Course)obj;
            return Id == other.Id
                && Status == other.Status
                && Subject == other.Subject
                && SubjectCode == other.SubjectCode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Status, Subject, SubjectCode);
        }
    }
}
