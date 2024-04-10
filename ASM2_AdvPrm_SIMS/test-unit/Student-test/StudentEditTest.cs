using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASM2_AdvPrm_SIMS.Context;
using ASM2_AdvPrm_SIMS.Models;
using ASM2_AdvPrm_SIMS.Pages.Students;
using ASM2_AdvPrm_SIMS.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Xunit;

namespace ASM2_AdvPrm_SIMS.Tests
{
    public class StudentEditTests
    {
        [Fact]
        public async Task OnGetAsync_ValidId_ReturnsPageWithStudent()
        {
            // Arrange
            int validId = 1;
            var expectedStudent = new Student { Id = validId, firstName = "John", lastName = "Doe", Status = "Active", Birthdate = "1/1/2000" };
            var filePath = "path/to/valid/Student.csv"; // Replace with a valid file path
            var studentContext = new StudentContext(filePath);
            studentContext.Students.Add(expectedStudent); // Add the expected student to the context
            var studentService = new StudentService(studentContext);
            var editModel = new EditModel(studentService);

            // Act
            IActionResult result = await editModel.OnGetAsync(validId);

            // Assert
            var pageResult = Assert.IsType<PageResult>(result);
            var actualStudent = Assert.IsAssignableFrom<Student>(editModel.Students);
            Assert.Equal(expectedStudent.Id, actualStudent.Id);
            Assert.Equal(expectedStudent.firstName.Trim(), actualStudent.firstName.Trim());
            Assert.Equal(expectedStudent.lastName.Trim(), actualStudent.lastName.Trim());
            Assert.Equal(expectedStudent.Status, actualStudent.Status);
            Assert.Equal(expectedStudent.Birthdate, actualStudent.Birthdate);
        }

        [Fact]
        public async Task OnPostAsyncValidModelReturnsRedirectToIndex()
        {
            // Arrange
            var student = new Student { Id = 1, firstName = "John", lastName = "Doe", Status = "Active", Birthdate = "1/1/2000" };
            var filePath = "path/to/valid/Student.csv"; // Replace with a valid file path
            var studentContext = new StudentContext(filePath);
            var studentService = new StudentService(studentContext);
            studentService.AddStudent(student); // Add the student to the service
            var editModel = new EditModel(studentService)
            {
                Students = student
            };

            // Act
            IActionResult result = await editModel.OnPostAsync();

            // Assert
            var redirectToPageResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("Index", redirectToPageResult.PageName);
        }
    }
}
