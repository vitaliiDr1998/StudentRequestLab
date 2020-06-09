using BLL.Core;
using BLL.Core.DTO;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Authorize]
    public class LectorController : Controller
    {
        IStudentService StudentService;
        ILectorService LectorService;

        public LectorController(IStudentService studentService, ILectorService lectorService)
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
        public ActionResult Create(LectorDto lector)
        {
            try
            {
                this.LectorService.Add(lector);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Approve(string LectorId, string ConsultationId)
        {
            try
            {
                this.LectorService.MarkConsultationAsApproved(LectorId, ConsultationId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Visit(string LectorId, string ConsultationId)
        {
            try
            {
                this.LectorService.MarkConsultationAsVisitedByStudent(LectorId, ConsultationId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Missed(string LectorId, string ConsultationId)
        {
            try
            {
                this.LectorService.MarkConsultationAsMissedByStudent(LectorId, ConsultationId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}