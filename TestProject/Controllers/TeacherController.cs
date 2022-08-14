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
    public class TeacherController : ControllerBase
    {
        private readonly ITeacher _teacher;
        public TeacherController(ITeacher teacher)
        {
            _teacher = teacher;
        }

        [HttpGet]
        [Route("GetAllTeachers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllTeachers()
        {
            try
            {
                List<Teacher> teacherlst = _teacher.GetAll();
                if (teacherlst.Count != 0)
                {
                    List<TeacherVM> TVMlst = new List<TeacherVM>();
                    foreach (var item in teacherlst)
                    {
                        TeacherVM TeacherVm = new TeacherVM()
                        {
                            TeacherId = item.TeacherId,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Sex = item.Sex,
                            Address = item.Address,
                            PhoneNumber = item.PhoneNumber,
                            Salary = item.Salary,
                            SubjectTaught = item.SubjectTaught,
                            IsPermanent = item.IsPermanent
                        };
                        TVMlst.Add(TeacherVm);
                    }
                    return StatusCode(StatusCodes.Status200OK, TVMlst);
                }
                return StatusCode(StatusCodes.Status404NotFound, new ResponseVM { Status = "NotFound", Message = "No Teachers Data is Found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseVM { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetTeacher/{TeacherId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTeacher(int? TeacherId)
        {
            if (TeacherId != null)
            {
                try
                {
                    Teacher teacher = _teacher.GetById(TeacherId.Value);
                    if (teacher != null)
                    {
                        TeacherVM TeacherVm = new TeacherVM()
                        {
                            TeacherId = teacher.TeacherId,
                            FirstName = teacher.FirstName,
                            LastName = teacher.LastName,
                            Sex = teacher.Sex,
                            Address = teacher.Address,
                            PhoneNumber = teacher.PhoneNumber,
                            Salary = teacher.Salary,
                            SubjectTaught = teacher.SubjectTaught,
                            IsPermanent = teacher.IsPermanent
                        };
                        return StatusCode(StatusCodes.Status200OK, TeacherVm);
                    }
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseVM { Status = "NotFound", Message = "Teacher with Id " + TeacherId + " is Not Found." });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseVM { Status = "Error", Message = ex.Message });
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseVM { Status = "BadRequest", Message = "TeacherId Cannnot be null or Empty." });
        }

        [HttpPost]
        [Route("AddNewTeacher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddNewTeacher([FromBody] TeacherVM TVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Teacher teacher = new Teacher()
                    {
                        TeacherId = TVM.TeacherId,
                        FirstName = TVM.FirstName,
                        LastName = TVM.LastName,
                        Sex = TVM.Sex,
                        Address = TVM.Address,
                        PhoneNumber = TVM.PhoneNumber,
                        Salary = TVM.Salary,
                        SubjectTaught = TVM.SubjectTaught,
                        IsPermanent = TVM.IsPermanent
                    };
                    _teacher.Insert(teacher);
                    _teacher.Save();
                    TVM.TeacherId = teacher.TeacherId;
                    return StatusCode(StatusCodes.Status200OK, TVM);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseVM { Status = "NotFound", Message = ex.Message });
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseVM { Status = "BadRequest", Message = "Validation Error!!" });
        }
        [HttpPatch]
        [Route("EditTeacher/{TeacherId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult EditTeacher(int? TeacherId, [FromBody] TeacherVM TVM)
        {
            if (TeacherId != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Teacher Storedteacher = _teacher.GetById(TVM.TeacherId);
                        if (Storedteacher != null)
                        {
                            if (Storedteacher.TeacherId == TeacherId)
                            {
                                Storedteacher.FirstName = TVM.FirstName;
                                Storedteacher.LastName = TVM.LastName;
                                Storedteacher.Sex = TVM.Sex;
                                Storedteacher.Address = TVM.Address;
                                Storedteacher.PhoneNumber = TVM.PhoneNumber;
                                Storedteacher.Salary = TVM.Salary;
                                Storedteacher.SubjectTaught = TVM.SubjectTaught;
                                Storedteacher.IsPermanent = TVM.IsPermanent;
                            }
                            _teacher.Update(Storedteacher);
                            _teacher.Save();
                            return StatusCode(StatusCodes.Status200OK, Storedteacher);
                        }
                        return StatusCode(StatusCodes.Status404NotFound, new ResponseVM { Status = "NotFound", Message = "Teacher with Id " + TeacherId + " is Not Found." });
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new ResponseVM { Status = "NotFound", Message = ex.Message });
                    }
                }
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseVM { Status = "BadRequest", Message = "Validation Error!!" });
            }
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseVM { Status = "BadRequest", Message = "TeacherId Cannnot be null or Empty." });
        }

        [HttpDelete]
        [Route("DeleteTeacher/{TeacherId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteStudent(int? TeacherId)
        {
            if (TeacherId != null)
            {
                try
                {
                    Teacher teacher = _teacher.GetById(TeacherId.Value);
                    if (teacher != null)
                    {
                        _teacher.Delete(teacher);
                        _teacher.Save();
                        return StatusCode(StatusCodes.Status200OK, new ResponseVM { Status = "Success", Message = "Teacher with the Id " + TeacherId + " is Deleted." });
                    }
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseVM { Status = "NotFound", Message = "Teacher with Id " + TeacherId + " is Not Found." });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseVM { Status = "NotFound", Message = ex.Message });
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseVM { Status = "BadRequest", Message = "TeacherId Cannnot be null or Empty." });

        }
    }
}
