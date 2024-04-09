using ASM2_AdvPrm_SIMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace ASM2_AdvPrm_SIMS.Context
{
    public class StudentContext
    {
        private int nextStudentId = 1;

        public List<Student> Students { get; set; }
        private readonly string filePath;

        public StudentContext(string filePath)
        {
            this.filePath = filePath;
            Students = ReadDataFromCsvAndUpdateId(filePath);
        }
        public void AddStudent(Student student)
        {
            student.Id = nextStudentId++;
            Students.Add(student);
            WriteDataToCsv(filePath);
        }
        public void UpdateStudent(int studentID, Student updateStudent)
        {
            Student existingStudent = Students.FirstOrDefault(s => s.Id == studentID);

            if (existingStudent != null)
            {
                existingStudent.firstName = updateStudent.firstName;
                existingStudent.lastName = updateStudent.lastName;
                existingStudent.Status = updateStudent.Status;
                existingStudent.Birthdate = updateStudent.Birthdate;

                WriteDataToCsv(filePath);
            }
            else
            {
                Console.WriteLine($"Student with ID {studentID} not found.");
            }
        }
        public void DeleteStudent(int studentID)
        {
            Student studentToRemove = Students.FirstOrDefault( s => s.Id == studentID);
            
            if (studentToRemove != null)
            {
                Students.Remove(studentToRemove);
                WriteDataToCsv(filePath);
            }
            else
            {
                Console.WriteLine($"Student with ID {studentID} not found.");
            }
        }
        public int CountStudentsInCsv()
        {
            int studentCount = 0;
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        sr.ReadLine() ;
                        studentCount++;
                    }
                }
            }
            else
            {
                Console.WriteLine("Unable to read csv");
            }

            return studentCount;
        }
        public List<Student> ReadDataFromCsvAndUpdateId(string filePath)
        {
            Students = new List<Student>();
            nextStudentId = 1;

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // Skip the header line
                    reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] values = line.Split(',');

                        if (values.Length >= 5)
                        {
                            Student student = new Student();
                            {
                                student.Id = int.Parse(values[0]);
                                student.firstName = values[1];
                                student.lastName = values[2];
                                student.Status = values[3];
                                student.Birthdate = values[4];
                            };
                            Students.Add(student);
                            if (student.Id >= nextStudentId)
                            {
                                nextStudentId = student.Id + 1;
                            }
                        }
                    }
                }
            }
            return Students;
        }
        private void WriteDataToCsv(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Id,firstName,lastName,Status,Birthdate");

                foreach (var student in Students)
                {
                    writer.WriteLine($"{student.Id}, {student.firstName}, {student.lastName},{student.Status},{student.Birthdate}");
                }
            }
        }

    }
}
