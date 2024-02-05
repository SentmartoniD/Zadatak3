using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private static List<Student> students = new List<Student>() {
            new Student { Indeks="PR1/2019", FirstName = "Milan", LastName="Milanovic", studyYear=3 },
            new Student { Indeks="PR2/2019", FirstName = "Milos", LastName="Milosevic", studyYear=3 },
            new Student { Indeks="PR3/2019", FirstName = "Marko", LastName="Markovic", studyYear=3 },
            new Student { Indeks="PR4/2019", FirstName = "Milorad", LastName="Miloradovic", studyYear=3 }};


        //  CRUD OPERATIONS

        [HttpPost("upload")]
        public ActionResult Upload([FromBody] Student student)
        {
            try {
                students.Add(student);
                return Ok(new { Message = "Successful upload!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
            }
        }

        [HttpGet("all")]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(new { Studenst = students });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
            }
        }

    }
}
