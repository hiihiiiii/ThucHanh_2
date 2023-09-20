using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThucHanh_2.Models;

namespace ThucHanh_2.Controllers
{
    [Route("Admin/Student")]
    public class StudentController : Controller
    {
        private List<Student> listStudents = new List<Student>();
        public StudentController()
        {
            listStudents = new List<Student>()
            {
                new Student()
                {
                    Id=101, Name="Hai Nam",Branch=Branch.IT,
                    Gender=Gender.Male,IsRegular=true, Address="A1-2018",Email="nam@g.com"
                },
                new Student()
                {
                     Id=102, Name="Minh Tu",Branch=Branch.BE,
                    Gender=Gender.Female,IsRegular=true, Address="A1-2019",Email="tu@g.com"
                },
                new Student()
                {
                     Id=103, Name="Hoang Phong",Branch=Branch.CE,
                    Gender=Gender.Male,IsRegular=false, Address="A1-2020",Email="phong@g.com"
                },
                new Student()
                {
                    Id=104, Name="Xuan Mai",Branch=Branch.EE,
                    Gender=Gender.Female,IsRegular=false, Address="A1-2021",Email="mai@g.com"
                }
            };
        }

        [HttpGet("/Admin/Student/List")]
        public IActionResult Index()
        {
            return View(listStudents);
        }
        [HttpGet("/Admin/Student/Add")]

        public IActionResult Create()
        {

            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem {Text="IT", Value="1"},
                new SelectListItem {Text="BE", Value="2" },
                new SelectListItem {Text="CE", Value="3"},
                new SelectListItem {Text="EE", Value="4"}
            };
            return View();
        }
        [HttpPost("/Admin/Student/Add")]
        public IActionResult Create(Student s)
        {

            s.Id = listStudents.Last<Student>().Id + 1;
            listStudents.Add(s);
            return View("Index", listStudents);
        }

    }
}
