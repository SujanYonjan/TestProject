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
    public class TeacherRepo : ITeacher
    {
        private readonly ApplicationDbContext _DbContext;
        private readonly string _connectionString;
        public TeacherRepo(ApplicationDbContext context, IConfiguration Configuration)
        {
            _DbContext = context;
            _connectionString = Configuration.GetConnectionString("TestProjectDbEntities");
        }
        public void Delete(Teacher teacher)
        {
            _DbContext.Remove(teacher);
        }

        public List<Teacher> GetAll()
        {
            List<Teacher> teacherlst = new List<Teacher>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetTeachers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter()
                {
                    ParameterName = "@TeacherId",
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
                    teacherlst.Add(new Teacher
                    {
                        TeacherId = (int)dt.Rows[i]["TeacherId"],
                        FirstName = dt.Rows[i]["FirstName"].ToString(),
                        LastName = dt.Rows[i]["LastName"].ToString(),
                        Sex = dt.Rows[i]["Sex"].ToString(),
                        Address = dt.Rows[i]["Address"].ToString(),
                        PhoneNumber = dt.Rows[i]["PhoneNumber"].ToString(),
                        Salary = (double)dt.Rows[i]["Salary"],
                        SubjectTaught = dt.Rows[i]["SubjectTaught"].ToString(),
                        IsPermanent = (bool)dt.Rows[i]["IsPermanent"]
                    });
                }
                return teacherlst;
            }
        }

        public Teacher GetById(int TeacherId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetTeachers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter()
                {
                    ParameterName = "@TeacherId",
                    SqlDbType = SqlDbType.Int,
                    Value = TeacherId,
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
                    Teacher teacher = new Teacher()
                    {
                        TeacherId = (int)dt.Rows[0]["TeacherId"],
                        FirstName = dt.Rows[0]["FirstName"].ToString(),
                        LastName = dt.Rows[0]["LastName"].ToString(),
                        Sex = dt.Rows[0]["Sex"].ToString(),
                        Address = dt.Rows[0]["Address"].ToString(),
                        PhoneNumber = dt.Rows[0]["PhoneNumber"].ToString(),
                        Salary = (double)dt.Rows[0]["Salary"],
                        SubjectTaught = dt.Rows[0]["SubjectTaught"].ToString(),
                        IsPermanent = (bool)dt.Rows[0]["IsPermanent"]
                    };
                    return teacher;
                }
                return null;
            }
        }

        public void Insert(Teacher teacher)
        {
            _DbContext.Teachers.Add(teacher);
        }

        public void Save()
        {
            _DbContext.SaveChanges();
        }

        public void Update(Teacher teacher)
        {
            _DbContext.Teachers.Update(teacher);
        }
    }
}
