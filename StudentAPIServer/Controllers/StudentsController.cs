using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StudentAPIBusinessLayer;
using StudentAPIDataAccessLayer;
using StudentAPIServer.DataSimulator;
using StudentAPIServer.Model;

namespace StudentAPIServer.Controllers
{
    [Route("api/Students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet("", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<StudentDTO>> GetAllStudents()
        {
            var StudentList = clsStudent.GetAllStudents();
            if (StudentList.Count == 0)
            {
                return NotFound("No Students Found");
            }
            return Ok(StudentList);
        }

        [HttpGet("Passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<StudentDTO>> GetPassedStudents()
        {
            var StudentList = clsStudent.GetPassedStudents();
            if (StudentList.Count == 0)
            {
                return NotFound("No Passed Students Found");
            }
            return Ok(StudentList);
        }

        [HttpGet("AverageGrade", Name = "GetAverageGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<double> GetAverageGrade()
        {
            double average = clsStudent.GetAverageGrade();
            return Ok(average);
        }

        [HttpGet("{ID}", Name = "GetStudentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDTO> GetStudentByID(int ID)
        {
            if (ID < 0)
            {
                return BadRequest("Invalid ID");
            }
            var student = clsStudent.Find(ID);
            if (student == null)
            {
                return NotFound("Student Not Found");
            }
            return Ok(student);
        }

        private bool _IsValidStudentDTO(StudentDTO student)
        {
            if (student.Id < 0 || string.IsNullOrEmpty(student.Name) || student.Grade < 0 || student.Grade > 100)
            {
                return false;
            }
            return true;
        }
        [HttpPost("", Name = "AddStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDTO> AddStudent(StudentDTO newStudentDTO)
        {
            if (!_IsValidStudentDTO(newStudentDTO))
            {
                return BadRequest("Invalid Student");
            }
            var student = new clsStudent(newStudentDTO, clsStudent.enMode.AddNew);
            if (!student.Save())
            {
                return Problem($"The Student With ID:{newStudentDTO.Id} Has Not Been Added");
            }
            return CreatedAtRoute("GetStudentByID", new { ID = student.SDTO.Id }, student.SDTO);
        }

        [HttpDelete("{ID}", Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteStudent(int ID)
        {
            if (ID < 0)
            {
                return BadRequest("Invalid ID");
            }
            var student = clsStudent.Find(ID);
            if (student == null)
            {
                return NotFound("Student Not Found");
            }
            if (clsStudent.DeleteStudent(ID))
            {
                return Ok($"The Student With ID:{ID} Has Been Deleted");

            }
            else
            {
                return Problem($"The Student With ID:{ID} Has Not Been Deleted");
            }
        }
        [HttpPut("{ID}", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> UpdateStudent(int ID, StudentDTO updateData)
        {
            if (!_IsValidStudentDTO(updateData))
            {
                return BadRequest("Invalid Student");
            }
            clsStudent? student = clsStudent.Find(ID);
            if (student == null)
            {
                return NotFound("Student Not Found");
            }
            student.Name = updateData.Name;
            student.Age = updateData.Age;
            student.Grade = updateData.Grade;
            if(!student.Save())
            {
                return Problem($"The Student With ID:{ID} Has Not Been Updated");
            }
            return Ok(student.SDTO);
        }
    }
}
