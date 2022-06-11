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
            if (student is null) return NotFound("There is no such student.");
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
                return BadRequest("This index number already exists in the database. Please enter a unique index number.");
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
                return NotFound("Student with given index number does not exist.");
            }
            else if (result == 2)
            {
                return BadRequest("Invalid data, index number in body and in URL are different.");
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
                return NotFound("Student with given index number does not exist");
            }
            else
                return BadRequest();
        }

    }
}
