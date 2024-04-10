using ASM2_AdvPrm_SIMS.Context;
using ASM2_AdvPrm_SIMS.Models;
using ASM2_AdvPrm_SIMS.Pages;
using ASM2_AdvPrm_SIMS.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ASM2_AdvPrm_SIMS.Tests
{
    public class TeachersModelTests
    {
        private string temporaryCsvFilePath;

        public TeachersModelTests()
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
        public void OnGet_PopulatesTeacherList()
        {
            // Arrange
            var teacherContext = new TeacherContext(temporaryCsvFilePath);
            var teacherService = new TeacherService(teacherContext);
            var pageModel = new TeachersModel(teacherService);
            var expectedTeachers = teacherContext.Teachers;

            // Act
            pageModel.OnGet();

            // Assert
            Assert.Equal(expectedTeachers, pageModel.TeacherList);
        }

        [Fact]
        public void OnPost_AddTeacher_AddsToTeacherServiceList()
        {
            // Arrange
            var teacherContext = new TeacherContext(temporaryCsvFilePath);
            var initialCount = teacherContext.CountTeachersInCsv();
            var newTeacher = new Teacher { Id = 3, FirstName = "Alice", LastName = "Johnson", Subject = "Science", Status = "Active" };
            var teacherService = new TeacherService(teacherContext);
            var pageModel = new TeachersModel(teacherService)
            {
                NewTeacher = newTeacher
            };

            // Act
            var result = pageModel.OnPost();

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(initialCount + 1, teacherContext.CountTeachersInCsv()); // Verify an increase in count
        }

        [Fact]
        public void OnPostDelete_RemovesTeacher_RedirectsToGet()
        {
            // Arrange
            var teacherContext = new TeacherContext(temporaryCsvFilePath);
            var initialCount = teacherContext.CountTeachersInCsv();
            var teacherIdToDelete = 1;
            var teacherService = new TeacherService(teacherContext);
            var pageModel = new TeachersModel(teacherService);

            // Act
            var result = pageModel.OnPostDelete(teacherIdToDelete);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(initialCount - 1, teacherContext.CountTeachersInCsv()); // Verify a decrease in count
        }

        // Dispose the temporary CSV file after all tests are executed
        public void Dispose()
        {
            File.Delete(temporaryCsvFilePath);
        }
    }
}