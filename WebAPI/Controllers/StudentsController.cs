﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.Eventing.Reader;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private static List<Student> students = new List<Student>() {
            new Student { Indeks="PR1/2019", FirstName = "Milan", LastName="Milanovic", StudyYear=1 },
            new Student { Indeks="PR2/2019", FirstName = "Milos", LastName="Milosevic", StudyYear=2 },
            new Student { Indeks="PR3/2019", FirstName = "Marko", LastName="Markovic", StudyYear=3 },
            new Student { Indeks="PR4/2019", FirstName = "Milorad", LastName="Miloradovic", StudyYear=4 }};

        // COUNTER FOR INCOMING HTTP REQUESTS
        public static int requestCounter = 0;

        [HttpGet("count")]
        public ActionResult GetCount()
        {
            try
            {
                requestCounter++;
                return Ok(requestCounter);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
            }
        }


        //  CRUD OPERATIONS

        [HttpPost("upload")]
        public ActionResult Upload([FromBody] Student student)
        {
            try
            {
                requestCounter++;
                if (students.Any(x => x.Indeks == student.Indeks))
                    return BadRequest(new { Error = "Student with that indeks already exist!" });
                else
                {
                    students.Add(student);
                    return Ok(new { Message = "Successful upload!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
            }
        }

        [HttpGet("students")]
        public ActionResult GetAll()
        {
            try
            {
                requestCounter++;
                return Ok(new { Studenst = students });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
            }
        }

        [HttpPut("update")]
        public ActionResult Update([FromBody] Student student)
        {
            try
            {
                requestCounter++;
                if (students.Any(x => x.Indeks == student.Indeks))
                {
                    Student s = students.FirstOrDefault(x => x.Indeks == student.Indeks);
                    s.FirstName = student.FirstName;
                    s.LastName = student.LastName;
                    s.StudyYear = student.StudyYear;
                    return Ok(new { Message = "Successful update!" });
                }
                else
                    return BadRequest(new { Error = "Student with that indeks doesnt exist!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
            }
        }
        
        [HttpDelete("{indeks}")]
        public ActionResult DeleetByIndeks(string indeks)
        {
            try
            {
                requestCounter++;
                if (students.Any(x => x.Indeks == indeks))
                {
                    students.RemoveAll(x => x.Indeks == indeks);
                    return Ok(new { Message = $"Successfuly deleted student with indeks:{indeks}!" });
                }
                else
                    return BadRequest(new { Error = "Student with that indeks doesnt exist!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
            }
        }

    }
}
