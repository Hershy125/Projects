using Exercises.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exercises.Models.Data;
using Exercises.Models.ViewModels;

namespace Exercises.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = StudentRepository.GetAll();

            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new StudentVM();
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(StudentVM studentVM)
        {
            if(ModelState.IsValid)
            {
                studentVM.Student.Courses = new List<Course>();

                foreach (var id in studentVM.SelectedCourseIds)
                    studentVM.Student.Courses.Add(CourseRepository.Get(id));

                studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);

                StudentRepository.Add(studentVM.Student);

                return RedirectToAction("List");
            }
            else
            {
                studentVM.SetCourseItems(CourseRepository.GetAll());
                studentVM.SetMajorItems(MajorRepository.GetAll());

                return View(studentVM);
            }
            
        }

        [HttpGet]
        public ActionResult EditStudent(int id)
        {
            var viewModel = new StudentVM();
            viewModel.Student = StudentRepository.Get(id);
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            
            if (viewModel.Student.Courses != null)
            {
                foreach (var course in viewModel.Student.Courses)
                {
                    viewModel.SelectedCourseIds.Add(course.CourseId);
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditStudent(StudentVM studentVM)
        {
            if(ModelState.IsValid)
            {
                Student student = new Student();
                student.StudentId = studentVM.Student.StudentId;
                student.FirstName = studentVM.Student.FirstName;
                student.LastName = studentVM.Student.LastName;
                student.GPA = studentVM.Student.GPA;

                studentVM.Student.Courses = new List<Course>();

                foreach (var id in studentVM.SelectedCourseIds)
                    studentVM.Student.Courses.Add(CourseRepository.Get(id));

                student.Courses = studentVM.Student.Courses;

                studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);

                student.Major = studentVM.Student.Major;
                student.Address = studentVM.Student.Address;

                StudentRepository.Edit(student);

                return RedirectToAction("List");
            }
            else
            {
                studentVM.SetCourseItems(CourseRepository.GetAll());
                studentVM.SetMajorItems(MajorRepository.GetAll());

                return View(studentVM);
            }

            
        }

        [HttpGet]
        public ActionResult DeleteStudent(int id)
        {
            var student = StudentRepository.Get(id);
            return View(student);
        }

        [HttpPost]
        public ActionResult DeleteStudent(Student student)
        {
            StudentRepository.Delete(student.StudentId);
            return RedirectToAction("List");
        }
    }
}