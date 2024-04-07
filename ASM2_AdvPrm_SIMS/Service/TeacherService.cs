using ASM2_AdvPrm_SIMS.Context;
using ASM2_AdvPrm_SIMS.Models;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace ASM2_AdvPrm_SIMS.Service
{
    public class TeacherService
    {
        private readonly TeacherContext _teacherContext = default!;
        public IList<Teacher> Teachers { get; set; }
        public TeacherService(TeacherContext teacherContext, IList<Teacher> teachers)
        {
            _teacherContext = teacherContext;
            Teachers = GetTeachers();
        }

        public IList<Teacher> GetTeachers()
        {
            if (_teacherContext != null)
            {
                return _teacherContext.Teachers.ToList();
            }
            return new List<Teacher>();
        }
        public void AddTeacher(Teacher teacher)
        {
            if (_teacherContext.Teachers != null)
            {
                _teacherContext.AddTeacher(teacher);
            }
        }
        public void UpdateTeacher(int id, Teacher teacher)
        {
            if (_teacherContext.Teachers != null)
            {
                _teacherContext.UpdateTeacher(id, teacher);
            }
        }
        public void DeleteTeacher(int id)
        {
            if (_teacherContext.Teachers != null)
            {
                var teacher = _teacherContext.Teachers.Find(t => t.Id == id);
                if (teacher != null)
                {
                    _teacherContext.DeleteTeacher(id);
                }
            }
        }
    }
}
