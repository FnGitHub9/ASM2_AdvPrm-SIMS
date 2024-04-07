using ASM2_AdvPrm_SIMS.Context;
using ASM2_AdvPrm_SIMS.Models;

namespace ASM2_AdvPrm_SIMS.Service
{
    public class StudentService
    {
        private readonly StudentContext _studentContext = default!;
        public IList<Student> Students { get; set; }

        public StudentService(StudentContext studentContext)
        {
            _studentContext = studentContext;
            Students = GetStudents();
        }
        public IList<Student> GetStudents()
        {
            if (_studentContext.Students != null)
            {
                return _studentContext.Students;
            }
            return new List<Student>();
        }
        public void AddStudent(Student student)
        {
            if (_studentContext.Students != null)
            {
                _studentContext.AddStudent(student);
            }
        }
        public void RemoveStudent(int id)
        {
            if (_studentContext.Students !=  null)
            {
                var student = _studentContext.Students.Find(s => s.Id == id);
                if (student != null)
                {
                    _studentContext.DeleteStudent(id);
                }
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
