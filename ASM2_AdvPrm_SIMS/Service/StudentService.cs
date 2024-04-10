using ASM2_AdvPrm_SIMS.Context;
using ASM2_AdvPrm_SIMS.Models;

namespace ASM2_AdvPrm_SIMS.Service
{
    public class StudentService
    {
        private readonly StudentContext _studentContext;
        public virtual IList<Student> Students { get; set; } = new List<Student>(); // Initialize to an empty list

        public StudentService(StudentContext studentContext)
        {
            _studentContext = studentContext;
            Students = GetStudents();
            // Remove initialization of Students here
        }
        public IList<Student> GetStudents()
        {
            return _studentContext.Students.ToList();
        }
        public void AddStudent(Student student)
        {
            if (_studentContext.Students != null)
            {
                _studentContext.AddStudent(student);
                Students.Add(student); // Add the new student to the Students collection
            }
        }

        public void RemoveStudent(int id)
        {
            var student = _studentContext.Students.Find(s => s.Id == id);
            if (student != null)
            {
                _studentContext.DeleteStudent(id);
                Students.Remove(student); // Remove the student from the local Students collection
            }
        }
        public void UpdateStudent(int id, Student student)
        {
            if (_studentContext.Students != null)
            {
                _studentContext.UpdateStudent(id, student);
            }
        }
    }
}
