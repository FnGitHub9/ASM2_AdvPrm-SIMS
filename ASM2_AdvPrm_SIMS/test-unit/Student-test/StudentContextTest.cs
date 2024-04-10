using ASM2_AdvPrm_SIMS.Context;
using ASM2_AdvPrm_SIMS.Models;
using System;
using System.IO;
using Xunit;

namespace ASM2_AdvPrm_SIMS.Tests
{
    public class StudentContextTests : IDisposable
    {
        private string temporaryCsvFilePath;

        public StudentContextTests()
        {
            // Create a temporary CSV file for testing
            temporaryCsvFilePath = Path.GetTempFileName();
            using (StreamWriter writer = new StreamWriter(temporaryCsvFilePath))
            {
                writer.WriteLine("Id,firstName,lastName,Status,Birthdate");
                writer.WriteLine("1,John,Doe,Active,2000-01-01");
                writer.WriteLine("2,Jane,Smith,Inactive,1999-12-31");
            }
        }

        [Fact]
        public void AddStudent_ShouldAddStudentToListAndWriteToCsv()
        {
            // Arrange
            var studentContext = new StudentContext(temporaryCsvFilePath);
            var initialCount = studentContext.Students.Count;
            var newStudent = new Student
            {
                firstName = "Alice",
                lastName = "Johnson",
                Status = "Active",
                Birthdate = "2001-02-28"
            };

            // Act
            studentContext.AddStudent(newStudent);

            // Assert
            Assert.Equal(initialCount + 1, studentContext.Students.Count);
            var addedStudent = studentContext.Students.Find(s => s.firstName == "Alice" && s.lastName == "Johnson");
            Assert.NotNull(addedStudent);

            // Verify if data is written to CSV
            var lines = File.ReadAllLines(temporaryCsvFilePath);
            Assert.Contains($"3,Alice,Johnson,Active,2001-02-28", lines);
        }

        [Fact]
        public void UpdateStudent_ShouldUpdateStudentAndWriteToCsv()
        {
            // Arrange
            var studentContext = new StudentContext(temporaryCsvFilePath);
            var studentIdToUpdate = 1;
            var updatedStudent = new Student
            {
                firstName = "Updated",
                lastName = "Student",
                Status = "Inactive",
                Birthdate = "2002-03-15"
            };

            // Act
            studentContext.UpdateStudent(studentIdToUpdate, updatedStudent);

            // Assert
            var updatedStudentFromList = studentContext.Students.Find(s => s.Id == studentIdToUpdate);
            Assert.NotNull(updatedStudentFromList);
            Assert.Equal("Updated", updatedStudentFromList.firstName);
            Assert.Equal("Student", updatedStudentFromList.lastName);
            Assert.Equal("Inactive", updatedStudentFromList.Status);
            Assert.Equal("2002-03-15", updatedStudentFromList.Birthdate);

            // Verify if data is written to CSV
            var lines = File.ReadAllLines(temporaryCsvFilePath);
            Assert.Contains($"1,Updated,Student,Inactive,2002-03-15", lines);
        }

        [Fact]
        public void DeleteStudent_ShouldRemoveStudentFromListAndWriteToCsv()
        {
            // Arrange
            var studentContext = new StudentContext(temporaryCsvFilePath);
            var initialCount = studentContext.Students.Count;
            var studentIdToDelete = 1;

            // Act
            studentContext.DeleteStudent(studentIdToDelete);

            // Assert
            Assert.Equal(initialCount - 1, studentContext.Students.Count);
            var deletedStudent = studentContext.Students.Find(s => s.Id == studentIdToDelete);
            Assert.Null(deletedStudent);

            // Verify if data is written to CSV
            var lines = File.ReadAllLines(temporaryCsvFilePath);
            Assert.DoesNotContain($"1,John,Doe,Active,2000-01-01", lines);
        }

        // Dispose the temporary CSV file after all tests are executed
        public void Dispose()
        {
            File.Delete(temporaryCsvFilePath);
        }
    }
}
