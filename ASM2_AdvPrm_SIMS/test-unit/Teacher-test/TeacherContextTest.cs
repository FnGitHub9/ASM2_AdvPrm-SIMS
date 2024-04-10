using ASM2_AdvPrm_SIMS.Context;
using ASM2_AdvPrm_SIMS.Models;
using System;
using System.IO;
using Xunit;

namespace ASM2_AdvPrm_SIMS.Tests
{
    public class TeacherContextTests : IDisposable
    {
        private string temporaryCsvFilePath;

        public TeacherContextTests()
        {
            // Create a temporary CSV file for testing
            temporaryCsvFilePath = Path.GetTempFileName();
            using (StreamWriter writer = new StreamWriter(temporaryCsvFilePath))
            {
                writer.WriteLine("Id,FirstName,LastName,Subject,Status");
                writer.WriteLine("1,John,Doe,Math,Active");
                writer.WriteLine("2,Jane,Smith,Science,Inactive");
            }
        }

        [Fact]
        public void AddTeacher_ShouldAddTeacherToListAndWriteToCsv()
        {
            // Arrange
            var teacherContext = new TeacherContext(temporaryCsvFilePath);
            var initialCount = teacherContext.Teachers.Count;
            var newTeacher = new Teacher
            {
                FirstName = "Alice",
                LastName = "Johnson",
                Subject = "Physics",
                Status = "Active"
            };

            // Act
            teacherContext.AddTeacher(newTeacher);

            // Assert
            Assert.Equal(initialCount + 1, teacherContext.Teachers.Count);
            var addedTeacher = teacherContext.Teachers.Find(t => t.FirstName == "Alice" && t.LastName == "Johnson");
            Assert.NotNull(addedTeacher);

            // Verify if data is written to CSV
            var lines = File.ReadAllLines(temporaryCsvFilePath);
            Assert.Contains($"3,Alice,Johnson,Physics,Active", lines);
        }

        [Fact]
        public void UpdateTeacher_ShouldUpdateTeacherAndWriteToCsv()
        {
            // Arrange
            var teacherContext = new TeacherContext(temporaryCsvFilePath);
            var teacherIdToUpdate = 1;
            var updatedTeacher = new Teacher
            {
                FirstName = "Updated",
                LastName = "Teacher",
                Subject = "Chemistry",
                Status = "Inactive"
            };

            // Act
            teacherContext.UpdateTeacher(teacherIdToUpdate, updatedTeacher);

            // Assert
            var updatedTeacherFromList = teacherContext.Teachers.Find(t => t.Id == teacherIdToUpdate);
            Assert.NotNull(updatedTeacherFromList);
            Assert.Equal("Updated", updatedTeacherFromList.FirstName);
            Assert.Equal("Teacher", updatedTeacherFromList.LastName);
            Assert.Equal("Chemistry", updatedTeacherFromList.Subject);
            Assert.Equal("Inactive", updatedTeacherFromList.Status);

            // Verify if data is written to CSV
            var lines = File.ReadAllLines(temporaryCsvFilePath);
            Assert.Contains($"1,Updated,Teacher,Chemistry,Inactive", lines);
        }

        [Fact]
        public void DeleteTeacher_ShouldRemoveTeacherFromListAndWriteToCsv()
        {
            // Arrange
            var teacherContext = new TeacherContext(temporaryCsvFilePath);
            var initialCount = teacherContext.Teachers.Count;
            var teacherIdToDelete = 1;

            // Act
            teacherContext.DeleteTeacher(teacherIdToDelete);

            // Assert
            Assert.Equal(initialCount - 1, teacherContext.Teachers.Count);
            var deletedTeacher = teacherContext.Teachers.Find(t => t.Id == teacherIdToDelete);
            Assert.Null(deletedTeacher);

            // Verify if data is written to CSV
            var lines = File.ReadAllLines(temporaryCsvFilePath);
            Assert.DoesNotContain($"1,John,Doe,Math,Active", lines);
        }

        // Dispose the temporary CSV file after all tests are executed
        public void Dispose()
        {
            File.Delete(temporaryCsvFilePath);
        }
    }
}
