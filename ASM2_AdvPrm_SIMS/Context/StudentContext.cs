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
                //continue from here 
            }
        }
    }
}
