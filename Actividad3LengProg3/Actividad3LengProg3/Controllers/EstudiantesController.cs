using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Actividad3LengProg3.Models;

namespace Actividad3LengProg3.Controllers
{
    public class EstudiantesController : Controller
    {
        private static List<EstudianteViewModel> _estudiantes = new List<EstudianteViewModel>();

        private List<SelectListItem> GetCarreras()
        {
            return new List<SelectListItem>
            {
                new SelectListItem("Educación", "Educación"),
                new SelectListItem("Enfermería", "Enfermería"),
                new SelectListItem("Ingeniería Agronómica", "Ingeniería Agronómica"),
                new SelectListItem("Ingeniería de Software", "Ingeniería de Software"),
                new SelectListItem("Ingeniería Eléctrica", "Ingeniería Eléctrica"),
                new SelectListItem("Ingeniería Industrial", "Ingeniería Industrial"),
                new SelectListItem("Administración de Empresas", "Administración de Empresas"),
                new SelectListItem("Contabilidad", "Contabilidad"),
                new SelectListItem("Otra", "Otra")
            };
        }

        private List<SelectListItem> GetRecintos()
        {
            return new List<SelectListItem>
            {
                new SelectListItem("Metropolitano", "Metropolitano"),
                new SelectListItem("Santo Domingo Oeste", "Santo Domingo Oeste"),
                new SelectListItem("La Romana", "La Romana"),
                new SelectListItem("Baní", "Baní"),
                new SelectListItem("Moca", "Moca"),
                new SelectListItem("Las Américas", "Las Américas"),
                new SelectListItem("Las Matas San Juan", "Las Matas San Juan")
            };
        }

        private List<SelectListItem> GetGeneros()
        {
            return new List<SelectListItem>
            {
                new SelectListItem("Masculino","Masculino"),
                new SelectListItem("Femenino","Femenino"),
                new SelectListItem("Otro","Otro")
            };
        }

        private List<SelectListItem> GetTurnos()
        {
            return new List<SelectListItem>
            {
                new SelectListItem("Mañana","Mañana"),
                new SelectListItem("Tarde","Tarde"),
                new SelectListItem("Noche","Noche")
            };
        }

        public IActionResult Index()
        {
            ViewBag.Carreras = GetCarreras();
            ViewBag.Recintos = GetRecintos();
            ViewBag.Generos = GetGeneros();
            ViewBag.Turnos = GetTurnos();

            return View(new EstudianteViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registrar(EstudianteViewModel estudiante)
        {
            ViewBag.Carreras = GetCarreras();
            ViewBag.Recintos = GetRecintos();
            ViewBag.Generos = GetGeneros();
            ViewBag.Turnos = GetTurnos();

            if (!ModelState.IsValid)
            {
                return View("Index", estudiante);
            }

            if (_estudiantes.Any(e => e.Matricula.Equals(estudiante.Matricula, StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError(nameof(estudiante.Matricula), "Ya existe un estudiante con esa matrícula.");
                return View("Index", estudiante);
            }

            _estudiantes.Add(estudiante);

            TempData["Mensaje"] = "Estudiante registrado correctamente.";
            return RedirectToAction(nameof(Lista));

        }

        public IActionResult Lista()
        {
            return View(_estudiantes);
        }


    }
}
