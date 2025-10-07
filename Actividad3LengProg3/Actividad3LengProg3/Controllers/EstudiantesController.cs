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

        public IActionResult Editar(string matricula)
        {
            if (string.IsNullOrEmpty(matricula))
                return RedirectToAction(nameof(Lista));

            var estudiante = _estudiantes.FirstOrDefault(e => e.Matricula.Equals(matricula, StringComparison.OrdinalIgnoreCase));
            if (estudiante == null)
            {
                TempData["Error"] = "Estudiante no encontrado.";
                return RedirectToAction(nameof(Lista));
            }

            ViewBag.Carreras = GetCarreras();
            ViewBag.Recintos = GetRecintos();
            ViewBag.Generos = GetGeneros();
            ViewBag.Turnos = GetTurnos();

            return View(estudiante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(EstudianteViewModel estudiante)
        {
            ViewBag.Carreras = GetCarreras();
            ViewBag.Recintos = GetRecintos();
            ViewBag.Generos = GetGeneros();
            ViewBag.Turnos = GetTurnos();

            if (!ModelState.IsValid)
            {
                return View(estudiante);
            }

            var existente = _estudiantes.FirstOrDefault(e => e.Matricula.Equals(estudiante.Matricula, StringComparison.OrdinalIgnoreCase));
            if (existente == null)
            {
                ModelState.AddModelError(string.Empty, "No se encontró el estudiante para actualizar.");
                return View(estudiante);
            }

            existente.NombreCompleto = estudiante.NombreCompleto;
            existente.Carrera = estudiante.Carrera;
            existente.Recinto = estudiante.Recinto;
            existente.CorreoInstitucional = estudiante.CorreoInstitucional;
            existente.Celular = estudiante.Celular;
            existente.Telefono = estudiante.Telefono;
            existente.Direccion = estudiante.Direccion;
            existente.FechaNacimiento = estudiante.FechaNacimiento;
            existente.Genero = estudiante.Genero;
            existente.Turno = estudiante.Turno;
            existente.EstaBecado = estudiante.EstaBecado;
            existente.PorcentajeBeca = estudiante.PorcentajeBeca;

            TempData["Mensaje"] = "Datos del estudiante actualizados correctamente.";
            return RedirectToAction(nameof(Lista));
        }

        public IActionResult Eliminar(string matricula)
        {
            if (string.IsNullOrEmpty(matricula))
            {
                TempData["Error"] = "Matrícula inválida.";
                return RedirectToAction(nameof(Lista));
            }

            var estudiante = _estudiantes.FirstOrDefault(e => e.Matricula.Equals(matricula, StringComparison.OrdinalIgnoreCase));
            if (estudiante != null)
            {
                _estudiantes.Remove(estudiante);
                TempData["Mensaje"] = "Estudiante eliminado.";
            }
            else
            {
                TempData["Error"] = "Estudiante no encontrado.";
            }

            return RedirectToAction(nameof(Lista));
        }
    }
}
