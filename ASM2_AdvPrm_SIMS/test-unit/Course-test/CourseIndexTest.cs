using ASM2_AdvPrm_SIMS.Context;
using ASM2_AdvPrm_SIMS.Models;
using ASM2_AdvPrm_SIMS.Pages;
using ASM2_AdvPrm_SIMS.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace ASM2_AdvPrm_SIMS.Tests
{
    public class CoursesModelTests : IDisposable
    {
        private string temporaryCsvFilePath;

        public CoursesModelTests()
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
        public void OnGet_PopulatesCourseList()
        {
            // Arrange
            var courseContext = new CourseContext(temporaryCsvFilePath);
            var courseService = new CourseService(courseContext);
            var pageModel = new CoursesModel(courseService);
            var expectedCourses = courseContext.Courses;

            // Act
            pageModel.OnGet();

            // Assert
            Assert.Equal(expectedCourses, pageModel.CourseList);
        }

        [Fact]
        public void OnPost_AddCourse_AddsToCourseServiceList()
        {
            // Arrange
            var courseContext = new CourseContext(temporaryCsvFilePath);
            var initialCount = courseContext.CountCourseInCSV();
            var newCourse = new Course { Id = 3, SubjectCode = "CHEM101", Subject = "Chemistry", Status = "Active" };
            var courseService = new CourseService(courseContext);
            var pageModel = new CoursesModel(courseService)
            {
                NewCourse = newCourse
            };

            // Act
            var result = pageModel.OnPost();

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(initialCount + 1, courseContext.CountCourseInCSV()); // Verify an increase in count
        }

        [Fact]
        public void OnPostDelete_RemovesCourse_RedirectsToGet()
        {
            // Arrange
            var courseContext = new CourseContext(temporaryCsvFilePath);
            var initialCount = courseContext.CountCourseInCSV();
            var courseIdToDelete = 1;
            var courseService = new CourseService(courseContext);
            var pageModel = new CoursesModel(courseService);

            // Act
            var result = pageModel.OnPostDelete(courseIdToDelete);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(initialCount - 1, courseContext.CountCourseInCSV()); // Verify a decrease in count
        }

        // Dispose the temporary CSV file after all tests are executed
        public void Dispose()
        {
            File.Delete(temporaryCsvFilePath);
        }
    }
}