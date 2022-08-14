using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Core.Models;

namespace TestProject.Core.Services.ServicesInterface
{
    public interface ITeacher
    {
        List<Teacher> GetAll();
        Teacher GetById(int TeacherId);
        void Insert(Teacher teacher);
        void Update(Teacher teacher);
        void Delete(Teacher teacher);
        void Save();
    }
}
