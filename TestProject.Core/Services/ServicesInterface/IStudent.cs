using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Core.Models;

namespace TestProject.Core.Services.ServicesInterface
{
    public interface IStudent
    {
        List<Student> GetAll();
        Student GetById(int studentId);
        void Insert(Student student);
        void Update(Student student);
        void Delete(Student student);
        void Save();
    }
}
