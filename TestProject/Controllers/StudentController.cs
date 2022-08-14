using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Core.Models;
using TestProject.Core.Models.ViewModel;
using TestProject.Core.Services.ServicesInterface;

namespace TestProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent _student;
        public StudentController(IStudent student)
        {
            _student = student;
        }

        [HttpGet]
        [Route("GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllStudents()
        {
            try
            {
                List<Student> stulst = _student.GetAll();
                if (stulst.Count != 0)
                {
                    List<StudentVM> SVMlst = new List<StudentVM>();
                    foreach (var item in stulst)
                    {
                        StudentVM studentVm = new StudentVM()
                        {
                            StudentId = item.StudentId,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Age = item.Age,
                            Sex = item.Sex,
                            TemporaryAddress = item.TemporaryAddress,
                            PermanentAddress = item.PermanentAddress,
                            FatherName = item.FatherName,
                            MotherName = item.MotherName,
                            ClassAttend = item.ClassAttend
                        };
                        SVMlst.Add(studentVm);
                    }
                    return StatusCode(StatusCodes.Status200OK, SVMlst);
                }
                return StatusCode(StatusCodes.Status404NotFound, new ResponseVM { Status = "NotFound", Message = "No StudentData" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseVM { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetStudent/{StudentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetStudent(int? StudentId)
        {
            if (StudentId != null)
            {
                try
                {
                    Student stu = _student.GetById(StudentId.Value);
                    if (stu != null)
                    {
                        StudentVM SVM = new StudentVM()
                        {
                            StudentId = stu.StudentId,
                            FirstName = stu.FirstName,
                            LastName = stu.LastName,
                            Age = stu.Age,
                            Sex = stu.Sex,
                            TemporaryAddress = stu.TemporaryAddress,
                            PermanentAddress = stu.PermanentAddress,
                            FatherName = stu.FatherName,
                            MotherName = stu.MotherName,
                            ClassAttend = stu.ClassAttend
                        };
                        return StatusCode(StatusCodes.Status200OK, SVM);
                    }
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseVM { Status = "NotFound", Message = "Student with Id " + StudentId + " is Not Found." });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseVM { Status = "Error", Message = ex.Message });
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseVM { Status = "BadRequest", Message = "StudentId Cannnot be null or Empty." });
        }

        [HttpPost]
        [Route("AddNewStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddNewStudent([FromBody] StudentVM SVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Student stu = new Student()
                    {
                        FirstName = SVM.FirstName,
                        LastName = SVM.LastName,
                        Age = SVM.Age,
                        Sex = SVM.Sex,
                        TemporaryAddress = SVM.TemporaryAddress,
                        PermanentAddress = SVM.PermanentAddress,
                        FatherName = SVM.FatherName,
                        MotherName = SVM.MotherName,
                        ClassAttend = SVM.ClassAttend
                    };
                    _student.Insert(stu);
                    _student.Save();
                    SVM.StudentId = stu.StudentId;
                    return StatusCode(StatusCodes.Status200OK, SVM);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseVM { Status = "NotFound", Message = ex.Message });
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseVM { Status = "BadRequest", Message = "Validation Error!!" });
        }
        [HttpPatch]
        [Route("EditStudent/{StudentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult EditStudent(int? StudentId, [FromBody] StudentVM SVM)
        {
            if (StudentId != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Student storedstu = _student.GetById(SVM.StudentId);
                        if (storedstu != null)
                        {
                            if (storedstu.StudentId == StudentId)
                            {
                                storedstu.FirstName = SVM.FirstName;
                                storedstu.LastName = SVM.LastName;
                                storedstu.Age = SVM.Age;
                                storedstu.Sex = SVM.Sex;
                                storedstu.TemporaryAddress = SVM.TemporaryAddress;
                                storedstu.PermanentAddress = SVM.PermanentAddress;
                                storedstu.FatherName = SVM.FatherName;
                                storedstu.MotherName = SVM.MotherName;
                                storedstu.ClassAttend = SVM.ClassAttend;
                            }
                            _student.Update(storedstu);
                            _student.Save();
                            return StatusCode(StatusCodes.Status200OK, storedstu);
                        }
                        return StatusCode(StatusCodes.Status404NotFound, new ResponseVM { Status = "NotFound", Message = "Student with Id " + StudentId + " is Not Found." });
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new ResponseVM { Status = "NotFound", Message = ex.Message });
                    }
                }
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseVM { Status = "BadRequest", Message = "Validation Error!!" });
            }
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseVM { Status = "BadRequest", Message = "StudentId Cannnot be null or Empty." });
        }

        [HttpDelete]
        [Route("DeleteStudent/{StudentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteStudent(int? StudentId)
        {
            if (StudentId != null)
            {
                try
                {
                    Student stu = _student.GetById(StudentId.Value);
                    if (stu != null)
                    {
                        _student.Delete(stu);
                        _student.Save();
                        return StatusCode(StatusCodes.Status200OK, new ResponseVM { Status = "Success", Message = "Student with the Id " + StudentId + " is Deleted." });
                    }
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseVM { Status = "NotFound", Message = "Student with Id " + StudentId + " is Not Found." });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseVM { Status = "NotFound", Message = ex.Message });
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseVM { Status = "BadRequest", Message = "StudentId Cannnot be null or Empty." });

        }
    }
}
