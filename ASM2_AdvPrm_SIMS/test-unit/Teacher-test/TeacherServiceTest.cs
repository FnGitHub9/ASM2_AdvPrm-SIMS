using ASM2_AdvPrm_SIMS.Context;
using ASM2_AdvPrm_SIMS.Models;
using ASM2_AdvPrm_SIMS.Service;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace ASM2_AdvPrm_SIMS.Tests
{
    public class TeacherServiceTests : IDisposable
    {
        private readonly string temporaryCsvFilePath;

        public TeacherServiceTests()
        {
            // Create a temporary CSV file for testing
            temporaryCsvFilePath = Path.GetTempFileName();
            using (StreamWriter writer = new StreamWriter(temporaryCsvFilePath))
            {
                writer.WriteLine("ID,FirstName,LastName,Subject,Status");
                writer.WriteLine("1,John,Doe,Mathematics,Active");
                writer.WriteLine("2,Jane,Smith,Physics,Inactive");
            }
        }

        [Fact]
        public void AddTeacher_ShouldAddTeacherToList()
        {
            // Arrange
            var teacherContext = new TeacherContext(temporaryCsvFilePath);
            var teacherService = new TeacherService(teacherContext);
            var initialCount = teacherService.Teachers.Count;
            var newTeacher = new Teacher { Id = 3, FirstName = "Alice", LastName = "Johnson", Subject = "Chemistry", Status = "Active" };

            // Act
            teacherService.AddTeacher(newTeacher);

            // Assert
            Assert.Equal(initialCount + 1, teacherService.Teachers.Count);
            Assert.Contains(newTeacher, teacherService.Teachers);
        }

        [Fact]
        public void UpdateTeacher_ShouldUpdateTeacherInList()
        {
            // Arrange
            var teacherContext = new TeacherContext(temporaryCsvFilePath);
            var teacherService = new TeacherService(teacherContext);
            var teacherIdToUpdate = 1;
            var updatedTeacher = new Teacher { Id = teacherIdToUpdate, FirstName = "Updated", LastName = "Teacher", Subject = "Mathematics", Status = "Inactive" };

            // Act
            teacherService.UpdateTeacher(teacherIdToUpdate, updatedTeacher);

            // Assert
            var updatedTeacherFromList = teacherService.Teachers.FirstOrDefault(t => t.Id == teacherIdToUpdate);
            Assert.NotNull(updatedTeacherFromList);
            Assert.Equal(updatedTeacher.FirstName, updatedTeacherFromList.FirstName);
            Assert.Equal(updatedTeacher.LastName, updatedTeacherFromList.LastName);
            Assert.Equal(updatedTeacher.Subject, updatedTeacherFromList.Subject);
            Assert.Equal(updatedTeacher.Status, updatedTeacherFromList.Status);
        }

        [Fact]
        public void DeleteTeacher_ShouldRemoveTeacherFromList()
        {
            // Arrange
            var teacherContext = new TeacherContext(temporaryCsvFilePath);
            var teacherService = new TeacherService(teacherContext);
            var initialCount = teacherService.Teachers.Count;
            var teacherIdToDelete = 1;

            // Act
            teacherService.DeleteTeacher(teacherIdToDelete);

            // Assert
            Assert.Equal(initialCount - 1, teacherService.Teachers.Count);
            Assert.Null(teacherService.Teachers.FirstOrDefault(t => t.Id == teacherIdToDelete));
        }

        // Dispose the temporary CSV file after all tests are executed
        public void Dispose()
        {
            File.Delete(temporaryCsvFilePath);
        }
    }
}
