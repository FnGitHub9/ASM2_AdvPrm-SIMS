using ASM2_AdvPrm_SIMS.Context;
using ASM2_AdvPrm_SIMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace ASM2_AdvPrm_SIMS.Service
{
    public class TeacherService
    {
        private readonly TeacherContext _teacherContext;
        public IList<Teacher> Teachers { get; set; }

        public TeacherService(TeacherContext teacherContext)
        {
            _teacherContext = teacherContext;
            Teachers = GetTeachers();
        }

        public IList<Teacher> GetTeachers()
        {
            return _teacherContext.Teachers.ToList();
        }

        public void AddTeacher(Teacher teacher)
        {
            _teacherContext.AddTeacher(teacher);
            Teachers = GetTeachers(); // Refresh the list of teachers after addition
        }

        public void UpdateTeacher(int id, Teacher teacher)
        {
            _teacherContext.UpdateTeacher(id, teacher);
        }

        public void DeleteTeacher(int id)
        {
            var teacher = Teachers.FirstOrDefault(t => t.Id == id);
            if (teacher != null)
            {
                _teacherContext.DeleteTeacher(id);
                Teachers = GetTeachers(); // Refresh the list of teachers after deletion
            }
        }
    }
}