using ASM2_AdvPrm_SIMS.Context;
using ASM2_AdvPrm_SIMS.Models;
using ASM2_AdvPrm_SIMS.Pages;
using ASM2_AdvPrm_SIMS.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ASM2_AdvPrm_SIMS.Tests
{
    public class StudentsModelTests
    {
        [Fact]
        public void OnGet_ReturnsListOfStudents()
        {
            // Arrange: In-memory setup
            var expectedStudents = new List<Student>
            {
                new Student { Id = 1, firstName = "John", lastName = "Doe", Status = "Active", Birthdate = "1/1/2000" },
                new Student { Id = 2, firstName = "Jane", lastName = "Smith", Status = "Active", Birthdate = "2/2/2001" }
            };
            var studentContext = new StudentContext(expectedStudents);
            var studentService = new StudentService(studentContext);
            var pageModel = new StudentsModel(studentService);

            // Act
            pageModel.OnGet();

            // Assert
            Assert.Equal(expectedStudents, pageModel.StudentList);
        }
        [Fact]
        public void OnPost_AddStudent_AddsToStudentServiceList()
        {
            // Arrange
            var filePath = "CSV-Files/Student.csv"; // Replace with actual file path
            var studentContext = new StudentContext(filePath);
            var studentService = new StudentService(studentContext);
            var initialCount = studentService.Students.Count; // Get the count before adding

            var pageModel = new StudentsModel(studentService)
            {
                NewStudent = new Student { Id = 3, firstName = "Alice", lastName = "Johnson", Status = "Active", Birthdate = "3/3/2002" }
            };

            // Act
            var result = pageModel.OnPost();

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(initialCount + 1, studentService.Students.Count); // Verify an increase in count
        }
        [Fact]
        public void OnPostDelete_RemovesStudent_RedirectsToGet()
        {
            // Arrange: In-memory setup
            var inMemoryStudents = new List<Student>
            {
                new Student { Id = 1, firstName = "John", lastName = "Doe" },
                new Student { Id = 2, firstName = "Jane", lastName = "Smith" }
            };
            var studentContext = new StudentContext("CSV-Files/Student.csv"); // Provide a valid file path here
            studentContext.Students = inMemoryStudents; // Set the students list directly
            var studentService = new StudentService(studentContext);
            var pageModel = new StudentsModel(studentService);

            // Act
            var studentIdToDelete = 1;
            var result = pageModel.OnPostDelete(studentIdToDelete);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(1, inMemoryStudents.Count); // Check if a student was deleted
            Assert.Null(inMemoryStudents.FirstOrDefault(s => s.Id == studentIdToDelete)); // Ensure correct student was removed
        }
    }
}