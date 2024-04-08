using ASM2_AdvPrm_SIMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ASM2_AdvPrm_SIMS.Context
{
    public class TeacherContext
    {
        private int nextTeacherId = 1;
        public List<Teacher> Teachers { get; set; }
        private readonly string filePath;

        public TeacherContext(string filePath)
        {
            this.filePath = filePath;
            Teachers = ReadDataFromCsvAndUpdateId(filePath);
        }

        public void AddTeacher(Teacher teacher)
        {
            teacher.Id = nextTeacherId++;
            Teachers.Add(teacher);
            WriteDataToCsv(filePath);
        }

        public void UpdateTeacher(int teacherID, Teacher updateTeacher)
        {
            Teacher existingTeacher = Teachers.FirstOrDefault(t => t.Id == teacherID);

            if (existingTeacher != null)
            {
                existingTeacher.FirstName = updateTeacher.FirstName;
                existingTeacher.LastName = updateTeacher.LastName;
                existingTeacher.Status = updateTeacher.Status;
                existingTeacher.Subject = updateTeacher.Subject;
                // Update other properties 

                WriteDataToCsv(filePath);
            }
        }

        private List<Teacher> ReadDataFromCsvAndUpdateId(string filePath)
        {
            List<Teacher> teachers = new List<Teacher>();

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

                        if (parts.Length == 5) // Assuming there are 5 fields: Id, FirstName, LastName, Subject, Status
                        {
                            int Id = int.Parse(parts[0]);
                            string FirstName = parts[1];
                            string LastName = parts[2];
                            string Subject = parts[3];
                            bool Status = bool.Parse(parts[4]);

                            teachers.Add(new Teacher
                            {
                                Id = Id,
                                FirstName = FirstName,
                                LastName = LastName,
                                Subject = Subject,
                                Status = Status
                            });

                            // Update nextTeacherId if necessary
                            if (Id >= nextTeacherId)
                            {
                                nextTeacherId = Id + 1;
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

            return teachers;
        }
        public void DeleteTeacher(int teacherID)
        {
            Teacher teacherToRemove = Teachers.FirstOrDefault(t => t.Id == teacherID);
            if (teacherToRemove != null)
            {
                Teachers.Remove(teacherToRemove);
                WriteDataToCsv(filePath);
            }
            else
            {
                Console.WriteLine($"Teacher with ID {teacherID} not found.");
            }
        }
        public int CountTeachersInCsv()
        {
            int teachersCount = 0;
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        sr.ReadLine() ;
                        teachersCount++;
                    }
                }
            }
            else
            {
                Console.WriteLine("Unable to read csv");
            }

            return teachersCount;
        }

        private void WriteDataToCsv(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write header line
                    writer.WriteLine("Id,FirstName,LastName,Subject,Status");

                    // Write data for each teacher
                    foreach (Teacher teacher in Teachers)
                    {
                        writer.WriteLine($"{teacher.Id},{teacher.FirstName},{teacher.LastName},{teacher.Subject},{teacher.Status}");
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
