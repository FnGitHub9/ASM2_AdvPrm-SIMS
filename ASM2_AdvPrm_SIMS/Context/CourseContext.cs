using ASM2_AdvPrm_SIMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ASM2_AdvPrm_SIMS.Context
{
    public class CourseContext
    {
        private int nextCourseId = 1;
        public List<Course> Courses { get; set; }
        private readonly string filePath;

        public CourseContext()
        {
            this.filePath = filePath;
            Courses = ReadDataFromCsvAndUpdateId(filePath);
        }

        public void CreateCourse(Course course)
        {
            course.Id = nextCourseId++;
            Courses.Add(course);
            WriteDataToCsv(filePath);
        }

        public void UpdateCourse(int CourseId, Course updatedCourse)
        {
            Course existingCourse = Courses.FirstOrDefault(c => c.Id == CourseId);

            if (existingCourse != null)
            {
                existingCourse.SubjectCode = updatedCourse.SubjectCode;
                existingCourse.Subject = updatedCourse.Subject;
                existingCourse.Status = updatedCourse.Status;

                WriteDataToCsv(filePath);
            }
        }

        public void DeleteCourse(int CourseId)
        {
            Course courseToRemove = Courses.FirstOrDefault(c => c.Id == CourseId);
            if (courseToRemove != null)
            {
                Courses.Remove(courseToRemove);
                WriteDataToCsv(filePath);
            }
            else
            {
                Console.WriteLine($"Course with ID {CourseId} not found.");
            }
        }

        private List<Course> ReadDataFromCsvAndUpdateId(string filePath)
        {
            List<Course> courses = new List<Course>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // Skip the header line
                    reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] parts = line.Split(',');

                        if (parts.Length == 4) // Assuming there are 4 fields: ID, SubjectCode, Subject, Status
                        {
                            int Id = int.Parse(parts[0]);
                            string SubjectCode = parts[1];
                            string Subject = parts[2];
                            bool Status = bool.Parse(parts[3]);

                            courses.Add(new Course
                            {
                                Id = Id,
                                SubjectCode = SubjectCode,
                                Subject = Subject,
                                Status = Status
                            });

                            // Update nextCourseId if necessary
                            if (Id >= nextCourseId)
                            {
                                nextCourseId = Id + 1;
                            }
                        }
                    }
                }
                Console.WriteLine("Data read from CSV file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading from CSV file: {ex.Message}");
            }

            return courses;
        }


        private void WriteDataToCsv(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write header line
                    writer.WriteLine("ID,SubjectCode,Subject,Status");

                    // Write data for each course
                    foreach (Course course in Courses)
                    {
                        writer.WriteLine($"{course.Id},{course.SubjectCode},{course.Subject},{course.Status}");
                    }
                }
                Console.WriteLine("Data written to CSV file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to CSV file: {ex.Message}");
            }
        }
    }
}
