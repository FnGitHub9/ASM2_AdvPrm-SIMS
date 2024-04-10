using System;
using System.IO;
using System.Threading.Tasks;
using ASM2_AdvPrm_SIMS.Context;
using ASM2_AdvPrm_SIMS.Models;
using ASM2_AdvPrm_SIMS.Pages.Teachers;
using ASM2_AdvPrm_SIMS.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Xunit;

namespace ASM2_AdvPrm_SIMS.Tests
{
    public class EditlTestsTeacher
    {
        [Fact]
        public async Task OnGetAsync_ValidId_ReturnsPageWithTeacher()
        {
            // Arrange
            int validId = 1;
            var expectedTeacher = new Teacher { Id = validId, FirstName = "John", LastName = "Doe", Subject = "Math", Status = "Active" };
            var filePath = "path/to/valid/Teacher.csv"; // Replace with a valid file path
            var teacherContext = new TeacherContext(filePath);
            teacherContext.Teachers.Add(expectedTeacher); // Add the expected teacher to the context
            var teacherService = new TeacherService(teacherContext);
            var editModel = new EditModel(teacherService);

            // Act
            IActionResult result = await editModel.OnGetAsync(validId);

            // Assert
            var pageResult = Assert.IsType<PageResult>(result);
            Assert.Equal(expectedTeacher, editModel.Teachers);
        }

        [Fact]
        public async Task OnPostAsync_ValidModel_ReturnsRedirectToIndex()
        {
            // Arrange
            var teacher = new Teacher { Id = 1, FirstName = "John", LastName = "Doe", Subject = "Math", Status = "Active" };
            var filePath = "path/to/valid/Teacher.csv"; // Replace with a valid file path
            var teacherContext = new TeacherContext(filePath);
            var teacherService = new TeacherService(teacherContext);
            teacherService.AddTeacher(teacher); // Add the teacher to the service
            var editModel = new EditModel(teacherService)
            {
                Teachers = teacher
            };

            // Act
            IActionResult result = await editModel.OnPostAsync();

            // Assert
            var redirectToPageResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("Index", redirectToPageResult.PageName);
        }
    }
}