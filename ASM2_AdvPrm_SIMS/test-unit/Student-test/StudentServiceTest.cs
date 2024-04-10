using ASM2_AdvPrm_SIMS.Context;
using ASM2_AdvPrm_SIMS.Models;
using ASM2_AdvPrm_SIMS.Service;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace ASM2_AdvPrm_SIMS.Tests
{
    public class StudentServiceTests : IDisposable
    {
        private readonly string temporaryCsvFilePath;

        public StudentServiceTests()
        {
            // Create a temporary CSV file for testing
            temporaryCsvFilePath = Path.GetTempFileName();
            using (StreamWriter writer = new StreamWriter(temporaryCsvFilePath))
            {
                writer.WriteLine("ID,firstName,lastName,Status,Birthdate");
                writer.WriteLine("1,John,Doe,Active,2000-01-01");
                writer.WriteLine("2,Jane,Smith,Inactive,1999-12-31");
            }
        }

        [Fact]
        public void AddStudent_ShouldAddStudentToList()
        {
            // Arrange
            var studentContext = new StudentContext(temporaryCsvFilePath);
            var studentService = new StudentService(studentContext);
            var initialCount = studentService.Students.Count;
            var newStudent = new Student { Id = 3, firstName = "Alice", lastName = "Johnson", Status = "Active" };

            // Act
            studentService.AddStudent(newStudent);

            // Assert
            Assert.Equal(initialCount + 1, studentService.Students.Count);
            Assert.Contains(newStudent, studentService.Students);
        }

        [Fact]
        public void UpdateStudent_ShouldUpdateStudentInList()
        {
            // Arrange
            var studentContext = new StudentContext(temporaryCsvFilePath);
            var studentService = new StudentService(studentContext);
            var studentIdToUpdate = 1;
            var updatedStudent = new Student { Id = studentIdToUpdate, firstName = "Updated", lastName = "Student", Status = "Inactive" };

            // Act
            studentService.UpdateStudent(studentIdToUpdate, updatedStudent);

            // Assert
            var updatedStudentFromList = studentService.Students.FirstOrDefault(s => s.Id == studentIdToUpdate);
            Assert.NotNull(updatedStudentFromList);
            Assert.Equal(updatedStudent.firstName, updatedStudentFromList.firstName);
            Assert.Equal(updatedStudent.lastName, updatedStudentFromList.lastName);
            Assert.Equal(updatedStudent.Status, updatedStudentFromList.Status);
        }

        [Fact]
        public void RemoveStudent_ShouldRemoveStudentFromList()
        {
            // Arrange
            var studentContext = new StudentContext(temporaryCsvFilePath);
            var studentService = new StudentService(studentContext);
            var initialCount = studentService.Students.Count;
            var studentIdToRemove = 1;

            // Act
            studentService.RemoveStudent(studentIdToRemove);

            // Assert
            Assert.Equal(initialCount - 1, studentService.Students.Count);
            Assert.Null(studentService.Students.FirstOrDefault(s => s.Id == studentIdToRemove));
        }

        // Dispose the temporary CSV file after all tests are executed
        public void Dispose()
        {
            File.Delete(temporaryCsvFilePath);
        }
    }
}