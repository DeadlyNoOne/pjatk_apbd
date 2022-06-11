using Cw3.Models;
using Cw3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IFileDbService _fileDbService;
        public StudentsController(IFileDbService fileDbService)
        {
            _fileDbService = fileDbService;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _fileDbService.GetStudents();
            return Ok(students);
        }

        [HttpGet("{indexNumber}")]
        //[HttpGet]
        //[Route("{indexNumber}")]
        public IActionResult GetStudent(string indexNumber)
        {
            var student = _fileDbService.GetStudent(indexNumber);
            if (student is null) return NotFound("Nie ma takiego studenta");
            return Ok(student);
        }

        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            int result = _fileDbService.AddStudent(student);
            if (result == 0)
            {
                return Created("", student);
            }
            else
            {
                return BadRequest("Nr indexu już istnieje w bazie danych, podaj unikalny numer indeksu");
            }
        }

        [HttpPut("{indexNumber}")]
        public IActionResult UpdateStudent(Student student, string indexNumber)
        {
            int result = _fileDbService.UpdateStudent(student, indexNumber);
            if (result == 0)
            {
                return Ok(student);
            }
            else if (result == 1)
            {
                return NotFound("Student o podanym numerze nie istnieje");
            }
            else if (result == 2)
            {
                return BadRequest("Niespójność danych, numer indeksu inny w body i url");
            }
            else
                return BadRequest();
        }

        [HttpDelete("{indexNumber}")]
        public IActionResult DeleteStudent(string indexNumber)
        {
            int result = _fileDbService.DeleteStudent(indexNumber);
            if (result == 0)
            {
                return Ok(indexNumber);
            }
            else if (result == 1)
            {
                return NotFound("Student o podanym numerze nie istnieje");
            }
            else
                return BadRequest();
        }

    }
}
