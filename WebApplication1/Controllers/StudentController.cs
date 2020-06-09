using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Core;
using BLL.Core.DTO;
using BLL.Entities;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        IStudentService StudentService;
        ILectorService LectorService;


        public StudentController(IStudentService studentService, ILectorService lectorService)
        {
            StudentService = studentService;
            LectorService = lectorService;
        }

        
        public ActionResult Index()
        {

            var cons = StudentService.GetAllConsultations();
            var studs = StudentService.GetAll();
            var lecs = LectorService.GetAll();
            ViewBag.Students = studs;
            ViewBag.Lectors = lecs;
            ViewBag.Consultations = cons;
            return View();
        }

        [HttpPost]
        public ActionResult Create(StudentDto student)
        {
            try
            {
                this.StudentService.Add(student);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult CreateConsultation(ConsultationRequest consultationRequest)
        {
            try
            {

                this.StudentService.CreateConsultation(consultationRequest);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}