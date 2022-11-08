using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Cache;

using _06.ViewBagViewData.Models;

namespace _06.ViewBagViewData.Controllers
{
    public class StudentController : Controller
    {
        IList<Student> studentList = new List<Student>()
        {
                    new Student(){ StudentId=1, StudentName="Ümit", Age = 58 },
                    new Student(){ StudentId=2, StudentName="Nurgül", Age = 54 },
                    new Student(){ StudentId=3, StudentName="Doğa", Age = 22 },
                    new Student(){ StudentId=4, StudentName="Barış", Age = 50 },
                    new Student(){ StudentId=5, StudentName="Ayşegül", Age = 50 }
        };

        public IActionResult Index()
        {
            ViewBag.TotalStudents = studentList.Count; 
            
            ViewData["students"]=studentList;   



            return View();
        }
    }
}
