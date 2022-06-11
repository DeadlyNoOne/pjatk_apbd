using CsvHelper;
using CsvHelper.Configuration;
using Cw3.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Services
{
    public class FileDbService : IFileDbService
    {
        CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            HasHeaderRecord = false,
            Comment = '#',
            AllowComments = true,
            Delimiter = ";",
        };

        public int AddStudent(Student student)
        {

            if(GetStudent(student.IndexNumber) != null) return 1;
            using(var stream = File.Open("students.csv", FileMode.Append))
            using (var csv = new CsvWriter(new StreamWriter(stream), csvConfig))
            {
                var students = new List<Student>
                {
                    student
                };
                csv.WriteRecords(students);
            }

            return 0;
        }

        private int AddStudentsAndRemoveOld(List<Student> students)
        {
            using (var stream = File.Open("students.csv", FileMode.Truncate))
            using (var csv = new CsvWriter(new StreamWriter(stream), csvConfig))
            {
                csv.WriteRecords(students);
            }
            return 0;
        }

        public int UpdateStudent(Student newStudent, string indexNumber)
        {
            if (newStudent.IndexNumber != indexNumber) return 2;

            var students = GetStudents().ToList();
            var sToRemove = students.Find(s => s.IndexNumber == indexNumber);
            if (sToRemove == null) return 1;
            students.Remove(sToRemove);
            students.Add(newStudent);

            AddStudentsAndRemoveOld(students);

            return 0;
        }

        public int DeleteStudent(string indexNumber)
        {

            var students = GetStudents().ToList();
            var sToRemove = students.Find(s => s.IndexNumber == indexNumber);
            if (sToRemove == null) return 1;
            students.Remove(sToRemove);


            AddStudentsAndRemoveOld(students);

            return 0;
        }
        public Student GetStudent(string indexNumber)
        {
            var resultList = new List<Student>();
            using (var csv = new CsvReader(new StreamReader("students.csv"), csvConfig))
            {
                while (csv.Read())
                {
                    var IndexNumber = csv.GetField(0);
                    var FirstName = csv.GetField(1);
                    var LastName = csv.GetField(2);
                    var NrRoku = Int32.Parse(csv.GetField(3));

                    resultList.Add(new Student
                    {
                        IndexNumber = IndexNumber,
                        FirstName = FirstName,
                        LastName = LastName,
                        NrRoku = NrRoku
                    });
                }
            }
            return resultList.Find(e => e.IndexNumber == indexNumber);
        }

        public IEnumerable<Student> GetStudents()
        {
            var resultList = new List<Student>();
            using (var csv = new CsvReader(new StreamReader("students.csv"), csvConfig))
            {
                while (csv.Read())
                {
                    var IndexNumber = csv.GetField(0);
                    var FirstName = csv.GetField(1);
                    var LastName = csv.GetField(2);
                    var NrRoku = Int32.Parse(csv.GetField(3));

                    resultList.Add(new Student
                    {
                        IndexNumber = IndexNumber,
                        FirstName = FirstName,
                        LastName = LastName,
                        NrRoku = NrRoku
                    });
                }
            }

            return resultList;
        }
    }
}
