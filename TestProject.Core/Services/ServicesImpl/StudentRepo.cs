using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TestProject.Core.Models;
using TestProject.Core.Services.ServicesInterface;

namespace TestProject.Core.Services.ServicesImpl
{
    public class StudentRepo : IStudent
    {
        private readonly ApplicationDbContext _DbContext;
        private readonly string _connectionString;
        public StudentRepo(ApplicationDbContext context, IConfiguration Configuration)
        {
            _DbContext = context;
            _connectionString = Configuration.GetConnectionString("TestProjectDbEntities");
        }
        public void Delete(Student student)
        {
            _DbContext.Students.Remove(student);
        }

        public List<Student> GetAll()
        {
            List<Student> studentlst = new List<Student>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetStudents", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter()
                {
                    ParameterName = "@StudentId",
                    SqlDbType = SqlDbType.Int,
                    Value = 0,
                    Direction = ParameterDirection.Input
                };
                cmd.Parameters.Add(param);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rd);
                con.Close();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    studentlst.Add(new Student
                    {
                        StudentId = (int)dt.Rows[i]["StudentId"],
                        FirstName = dt.Rows[i]["FirstName"].ToString(),
                        LastName = dt.Rows[i]["LastName"].ToString(),
                        Age = (int)dt.Rows[i]["Age"],
                        Sex = dt.Rows[i]["Sex"].ToString(),
                        TemporaryAddress = dt.Rows[i]["TemporaryAddress"].ToString(),
                        PermanentAddress = dt.Rows[i]["PermanentAddress"].ToString(),
                        FatherName = dt.Rows[i]["FatherName"].ToString(),
                        MotherName = dt.Rows[i]["MotherName"].ToString(),
                        ClassAttend = dt.Rows[i]["ClassAttend"].ToString()
                    });
                }
                return studentlst;
            }
        }

        public Student GetById(int StudentId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetStudents", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter()
                {
                    ParameterName = "@StudentId",
                    SqlDbType = SqlDbType.Int,
                    Value = StudentId,
                    Direction = ParameterDirection.Input
                };
                cmd.Parameters.Add(param);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rd);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    Student stu = new Student()
                    {
                        StudentId = (int)dt.Rows[0]["StudentId"],
                        FirstName = dt.Rows[0]["FirstName"].ToString(),
                        LastName = dt.Rows[0]["LastName"].ToString(),
                        Age = (int)dt.Rows[0]["Age"],
                        Sex = dt.Rows[0]["Sex"].ToString(),
                        TemporaryAddress = dt.Rows[0]["TemporaryAddress"].ToString(),
                        PermanentAddress = dt.Rows[0]["PermanentAddress"].ToString(),
                        FatherName = dt.Rows[0]["FatherName"].ToString(),
                        MotherName = dt.Rows[0]["MotherName"].ToString(),
                        ClassAttend = dt.Rows[0]["ClassAttend"].ToString()
                    };
                    return stu;
                }
                return null;
            }
            //return _DbContext.Students.Where(x => x.StudentId == StudentId).FirstOrDefault();
        }

        public void Insert(Student student)
        {
            _DbContext.Students.Add(student);
        }

        public void Save()
        {
            _DbContext.SaveChanges();
        }

        public void Update(Student student)
        {
            _DbContext.Students.Update(student);
        }
    }
}
