using System.Threading.Tasks;
using ASM2_AdvPrm_SIMS.Context;
using ASM2_AdvPrm_SIMS.Models;
using ASM2_AdvPrm_SIMS.Pages.Courses;
using ASM2_AdvPrm_SIMS.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Xunit;

namespace ASM2_AdvPrm_SIMS.Tests
{
    public class CoruseEditTest
    {
        [Fact]
        public async Task OnGetAsync_ValidId_ReturnsPageWithCourse()
        {
            // Arrange
            int validId = 1;
            var expectedCourse = new Course { Id = validId, Subject = "Mathematics", SubjectCode = "MATH101", Status = "Active" };
            var filePath = "path/to/valid/Course.csv"; // Replace with a valid file path
            var courseContext = new CourseContext(filePath);
            courseContext.Courses.Add(expectedCourse); // Add the expected course to the context
            var courseService = new CourseService(courseContext);
            var editModel = new EditModel(courseService);

            // Act
            IActionResult result = await editModel.OnGetAsync(validId);

            // Assert
            var pageResult = Assert.IsType<PageResult>(result);
            Assert.Equal(expectedCourse, editModel.Courses);
        }

        [Fact]
        public async Task OnPostAsync_ValidModel_ReturnsRedirectToIndex()
        {
            // Arrange
            var course = new Course { Id = 1, Subject = "Mathematics", SubjectCode = "MATH101", Status = "Active" };
            var filePath = "path/to/valid/Course.csv"; // Replace with a valid file path
            var courseContext = new CourseContext(filePath);
            var courseService = new CourseService(courseContext);
            courseService.AddCourse(course); // Add the course to the service
            var editModel = new EditModel(courseService)
            {
                Courses = course
            };

            // Act
            IActionResult result = await editModel.OnPostAsync();

            // Assert
            var redirectToPageResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("Index", redirectToPageResult.PageName);
        }
    }
}