using ASM2_AdvPrm_SIMS.Context;
using ASM2_AdvPrm_SIMS.Models;
using System;
using System.IO;
using Xunit;

namespace ASM2_AdvPrm_SIMS.Tests
{
    public class CourseContextTests : IDisposable
    {
        private string temporaryCsvFilePath;

        public CourseContextTests()
        {
            // Create a temporary CSV file for testing
            temporaryCsvFilePath = Path.GetTempFileName();
            using (StreamWriter writer = new StreamWriter(temporaryCsvFilePath))
            {
                writer.WriteLine("ID,SubjectCode,Subject,Status");
                writer.WriteLine("1,MATH101,Mathematics,Active");
                writer.WriteLine("2,ENG101,English,Inactive");
            }
        }

        [Fact]
        public void CreateCourse_ShouldAddCourseToListAndWriteToCsv()
        {
            // Arrange
            var courseContext = new CourseContext(temporaryCsvFilePath);
            var initialCount = courseContext.Courses.Count;
            var newCourse = new Course
            {
                SubjectCode = "SCI101",
                Subject = "Science",
                Status = "Active"
            };

            // Act
            courseContext.CreateCourse(newCourse);

            // Assert
            Assert.Equal(initialCount + 1, courseContext.Courses.Count);
            var addedCourse = courseContext.Courses.Find(c => c.SubjectCode == "SCI101");
            Assert.NotNull(addedCourse);

            // Verify if data is written to CSV
            var lines = File.ReadAllLines(temporaryCsvFilePath);
            Assert.Contains($"3,SCI101,Science,Active", lines);
        }

        [Fact]
        public void UpdateCourse_ShouldUpdateCourseAndWriteToCsv()
        {
            // Arrange
            var courseContext = new CourseContext(temporaryCsvFilePath);
            var courseIdToUpdate = 1;
            var updatedCourse = new Course
            {
                SubjectCode = "MATH102",
                Subject = "Advanced Mathematics",
                Status = "Inactive"
            };

            // Act
            courseContext.UpdateCourse(courseIdToUpdate, updatedCourse);

            // Assert
            var updatedCourseFromList = courseContext.Courses.Find(c => c.Id == courseIdToUpdate);
            Assert.NotNull(updatedCourseFromList);
            Assert.Equal("MATH102", updatedCourseFromList.SubjectCode);
            Assert.Equal("Advanced Mathematics", updatedCourseFromList.Subject);
            Assert.Equal("Inactive", updatedCourseFromList.Status);

            // Verify if data is written to CSV
            var lines = File.ReadAllLines(temporaryCsvFilePath);
            Assert.Contains($"1,MATH102,Advanced Mathematics,Inactive", lines);
        }

        [Fact]
        public void DeleteCourse_ShouldRemoveCourseFromListAndWriteToCsv()
        {
            // Arrange
            var courseContext = new CourseContext(temporaryCsvFilePath);
            var initialCount = courseContext.Courses.Count;
            var courseIdToDelete = 1;

            // Act
            courseContext.DeleteCourse(courseIdToDelete);

            // Assert
            Assert.Equal(initialCount - 1, courseContext.Courses.Count);
            var deletedCourse = courseContext.Courses.Find(c => c.Id == courseIdToDelete);
            Assert.Null(deletedCourse);

            // Verify if data is written to CSV
            var lines = File.ReadAllLines(temporaryCsvFilePath);
            Assert.DoesNotContain($"1,MATH101,Mathematics,Active", lines);
        }

        // Dispose the temporary CSV file after all tests are executed
        public void Dispose()
        {
            File.Delete(temporaryCsvFilePath);
        }
    }
}
