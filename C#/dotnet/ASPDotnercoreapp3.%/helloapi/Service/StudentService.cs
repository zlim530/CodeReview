using helloapi.Context;
using helloapi.Model;
using System.Linq;

namespace helloapi.Service
{
    public class StudentService : IStudentService
    {
        private readonly SchoolContext _schoolContext;

        public StudentService(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }

        public void Create(Student student)
        {
            _schoolContext.Students.Add(student);
            _schoolContext.SaveChanges();
        }

        public string GetFirstName()
        {
            return _schoolContext.Students.First().Name;
        }
    }
}