using System.ComponentModel.DataAnnotations;

namespace ASM2_AdvPrm_SIMS.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subject { get; set; }
        public string Status { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Teacher other = (Teacher)obj;
            return Id == other.Id
                && FirstName == other.FirstName
                && LastName == other.LastName
                && Subject == other.Subject
                && Status == other.Status;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FirstName, LastName, Subject, Status);
        }
    }
}