using ASM2_AdvPrm_SIMS.Context;
using ASM2_AdvPrm_SIMS.Models;

namespace ASM2_AdvPrm_SIMS.Service
{
    public class StudentService
    {
        private readonly StudentContext _studentContext;
        public IList<Student> Students { get; set; }

        public StudentService(StudentContext studentContext)
        {
            _studentContext = studentContext;
            Students = GetStudents();
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
            }
        }
        public void RemoveStudent(int id)
        {
            var teacher = _studentContext.Students.Find(t => t.Id == id);
            if (teacher != null)
            {
                _studentContext.DeleteStudent(id);
            }
        }
        public void UpdateStudent(int id, Student student)
        {
            if (_studentContext.Students != null)
            {
                _studentContext.UpdateStudent(id, student);
            }
        }
        public int CountStudentsInCsv()
        {
            return _studentContext.CountStudentsInCsv();
        }
    }
}
