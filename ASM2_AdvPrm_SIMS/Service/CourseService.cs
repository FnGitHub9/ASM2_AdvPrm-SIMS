using ASM2_AdvPrm_SIMS.Context;
using ASM2_AdvPrm_SIMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace ASM2_AdvPrm_SIMS.Service
{
    public class CourseService
    {
        private readonly CourseContext _courseContext;
        public IList<Course> Courses { get; set; }

        public CourseService(CourseContext courseContext)
        {
            _courseContext = courseContext;
            Courses = GetCourses();
        }

        public IList<Course> GetCourses()
        {
            return _courseContext.Courses.ToList();
        }

        public void AddCourse(Course course)
        {
            _courseContext.CreateCourse(course);
            Courses = GetCourses(); // Refresh courses after addition
        }

        public void UpdateCourse(int id, Course course)
        {
            _courseContext.UpdateCourse(id, course);
            Courses = GetCourses(); // Refresh courses after update
        }

        public void DeleteCourse(int id)
        {
            var course = _courseContext.Courses.Find(c => c.Id == id);
            if (course != null)
            {
                _courseContext.DeleteCourse(id);
                Courses = GetCourses(); // Refresh courses after deletion
            }
        }
        public int CountCourseInCSV()
        {
            return _courseContext.CountCourseInCSV();
        }
    }
}
