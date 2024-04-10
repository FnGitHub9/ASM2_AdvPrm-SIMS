using ASM2_AdvPrm_SIMS.Context;
using ASM2_AdvPrm_SIMS.Models;
using ASM2_AdvPrm_SIMS.Service;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace ASM2_AdvPrm_SIMS.Tests
{
    public class CourseServiceTests : IDisposable
    {
        private readonly string temporaryCsvFilePath;

        public CourseServiceTests()
        {
            // Create a temporary CSV file for testing
            temporaryCsvFilePath = Path.GetTempFileName();
            using (StreamWriter writer = new StreamWriter(temporaryCsvFilePath))
            {
                writer.WriteLine("ID,SubjectCode,Subject,Status");
                writer.WriteLine("1,MATH101,Mathematics,Active");
                writer.WriteLine("2,PHY101,Physics,Inactive");
            }
        }

        [Fact]
        public void AddCourse_ShouldAddCourseToList()
        {
            // Arrange
            var courseContext = new CourseContext(temporaryCsvFilePath);
            var courseService = new CourseService(courseContext);
            var initialCount = courseService.Courses.Count;
            var newCourse = new Course { Id = 3, SubjectCode = "CHEM101", Subject = "Chemistry", Status = "Active" };

            // Act
            courseService.AddCourse(newCourse);

            // Assert
            Assert.Equal(initialCount + 1, courseService.Courses.Count);
            Assert.Contains(newCourse, courseService.Courses);
        }

        [Fact]
        public void UpdateCourse_ShouldUpdateCourseInList()
        {
            // Arrange
            var courseContext = new CourseContext(temporaryCsvFilePath);
            var courseService = new CourseService(courseContext);
            var courseIdToUpdate = 1;
            var updatedCourse = new Course { Id = courseIdToUpdate, SubjectCode = "MATH102", Subject = "Advanced Mathematics", Status = "Inactive" };

            // Act
            courseService.UpdateCourse(courseIdToUpdate, updatedCourse);

            // Assert
            var updatedCourseFromList = courseService.Courses.FirstOrDefault(c => c.Id == courseIdToUpdate);
            Assert.NotNull(updatedCourseFromList);
            Assert.Equal(updatedCourse.SubjectCode, updatedCourseFromList.SubjectCode);
            Assert.Equal(updatedCourse.Subject, updatedCourseFromList.Subject);
            Assert.Equal(updatedCourse.Status, updatedCourseFromList.Status);
        }

        [Fact]
        public void DeleteCourse_ShouldRemoveCourseFromList()
        {
            // Arrange
            var courseContext = new CourseContext(temporaryCsvFilePath);
            var courseService = new CourseService(courseContext);
            var initialCount = courseService.Courses.Count;
            var courseIdToDelete = 1;

            // Act
            courseService.DeleteCourse(courseIdToDelete);

            // Assert
            Assert.Equal(initialCount - 1, courseService.Courses.Count);
            Assert.Null(courseService.Courses.FirstOrDefault(c => c.Id == courseIdToDelete));
        }

        // Dispose the temporary CSV file after all tests are executed
        public void Dispose()
        {
            File.Delete(temporaryCsvFilePath);
        }
    }
}
