using Microsoft.AspNetCore.Mvc;
using ProyectoWeb_Concesionaria.Models;

namespace ProyectoWeb_Concesionaria.Controllers
{
    // controlador principal, maneja todas las operaciones CRUD
    // reemplaza a los forms de windows forms
    public class CochesController : Controller
    {
        // pagina principal - menu de inicio (reemplaza a frminicio)
        public IActionResult Index()
        {
            return View();
        }

        // ALTA - mostrar formulario para registrar nuevo coche
        [HttpGet]
        public IActionResult Alta()
        {
            // creamos la lista de años igual que en windows forms
            int anioActual = DateTime.Now.Year;
            List<int> anios = new List<int>();
            for (int i = 0; i < 46; i++)
            {
                anios.Add(anioActual - i);
            }
            ViewBag.Anios = anios;
            
            return View();
        }

        // ALTA - procesar el formulario y guardar el coche
        [HttpPost]
        public IActionResult Alta(Coche coche)
        {
            try
            {
                // validaciones basicas igual que en windows forms
                if (string.IsNullOrWhiteSpace(coche.Placa) ||
                    string.IsNullOrWhiteSpace(coche.Marca) ||
                    string.IsNullOrWhiteSpace(coche.Modelo) ||
                    string.IsNullOrWhiteSpace(coche.Tipo))
                {
                    TempData["Error"] = "Por favor, complete todos los campos.";
                    return RedirectToAction("Alta");
                }

                // convertimos la placa a mayusculas igual que en windows forms
                coche.Placa = coche.Placa.Trim().ToUpper();
                coche.Marca = coche.Marca.Trim();
                coche.Modelo = coche.Modelo.Trim();

                // insertamos en la base de datos
                bool resultado = CocheDAO.InsertarCoche(coche);

                if (resultado)
                {
                    TempData["Exito"] = "Vehículo registrado correctamente en la base de datos.";
                    return RedirectToAction("Alta");
                }
                else
                {
                    TempData["Error"] = "No se pudo registrar el vehículo.";
                    return RedirectToAction("Alta");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Alta");
            }
        }

        // BAJA - mostrar formulario para eliminar coche
        [HttpGet]
        public IActionResult Baja()
        {
            return View();
        }

        // BAJA - procesar la eliminacion
        [HttpPost]
        public IActionResult Baja(string placa, string confirmar)
        {
            try
            {
                string placaEliminar = placa?.Trim().ToUpper() ?? "";

                // validacion basica igual que en windows forms
                if (string.IsNullOrWhiteSpace(placaEliminar))
                {
                    TempData["Error"] = "Por favor, ingrese una placa.";
                    return RedirectToAction("Baja");
                }

                // primero buscamos el coche para ver si existe
                Coche? coche = CocheDAO.ConsultarCochePorPlaca(placaEliminar);

                if (coche == null)
                {
                    TempData["Error"] = "Placa no encontrada en la base de datos.";
                    return RedirectToAction("Baja");
                }

                // si no ha confirmado, mostramos los datos
                if (confirmar != "SI")
                {
                    ViewBag.CocheAEliminar = coche;
                    return View();
                }

                // si confirmo, eliminamos
                bool resultado = CocheDAO.EliminarCoche(placaEliminar);

                if (resultado)
                {
                    TempData["Exito"] = "Vehículo eliminado correctamente de la base de datos.";
                }
                else
                {
                    TempData["Error"] = "No se pudo eliminar el vehículo.";
                }

                return RedirectToAction("Baja");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Baja");
            }
        }

        // CAMBIOS - mostrar formulario para editar coche
        [HttpGet]
        public IActionResult Cambios()
        {
            // creamos la lista de años
            int anioActual = DateTime.Now.Year;
            List<int> anios = new List<int>();
            for (int i = anioActual; i >= 1980; i--)
            {
                anios.Add(i);
            }
            ViewBag.Anios = anios;

            return View();
        }

        // CAMBIOS - buscar coche por placa
        [HttpPost]
        public IActionResult BuscarParaEditar(string placa)
        {
            try
            {
                string placaBuscada = placa?.Trim().ToUpper() ?? "";

                if (string.IsNullOrWhiteSpace(placaBuscada))
                {
                    TempData["Error"] = "Por favor, ingrese una placa.";
                    return RedirectToAction("Cambios");
                }

                // buscamos el coche en la base de datos
                Coche? coche = CocheDAO.ConsultarCochePorPlaca(placaBuscada);

                if (coche != null)
                {
                    // creamos la lista de años
                    int anioActual = DateTime.Now.Year;
                    List<int> anios = new List<int>();
                    for (int i = anioActual; i >= 1980; i--)
                    {
                        anios.Add(i);
                    }
                    ViewBag.Anios = anios;
                    ViewBag.CocheEncontrado = coche;
                    ViewBag.Mensaje = "Vehículo encontrado. Ahora puedes editar los datos.";
                }
                else
                {
                    TempData["Error"] = "Placa no encontrada en la base de datos.";
                    return RedirectToAction("Cambios");
                }

                return View("Cambios");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Cambios");
            }
        }

        // CAMBIOS - guardar los cambios
        [HttpPost]
        public IActionResult GuardarCambios(Coche coche)
        {
            try
            {
                // validaciones basicas
                if (string.IsNullOrWhiteSpace(coche.Placa) ||
                    string.IsNullOrWhiteSpace(coche.Marca) ||
                    string.IsNullOrWhiteSpace(coche.Modelo) ||
                    string.IsNullOrWhiteSpace(coche.Tipo))
                {
                    TempData["Error"] = "Por favor, completa todos los campos.";
                    return RedirectToAction("Cambios");
                }

                // actualizamos los datos
                coche.Marca = coche.Marca.Trim();
                coche.Modelo = coche.Modelo.Trim();

                bool resultado = CocheDAO.ActualizarCoche(coche);

                if (resultado)
                {
                    TempData["Exito"] = "Cambios guardados correctamente en la base de datos.";
                }
                else
                {
                    TempData["Error"] = "No se pudieron guardar los cambios.";
                }

                return RedirectToAction("Cambios");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Cambios");
            }
        }

        // CONSULTAR - mostrar formulario de consulta
        [HttpGet]
        public IActionResult Consultar()
        {
            return View();
        }

        // CONSULTAR - buscar coche por placa
        [HttpPost]
        public IActionResult Consultar(string placa)
        {
            try
            {
                string placaBuscada = placa?.Trim().ToUpper() ?? "";

                if (string.IsNullOrWhiteSpace(placaBuscada))
                {
                    TempData["Error"] = "Por favor, ingrese una placa.";
                    return RedirectToAction("Consultar");
                }

                // buscamos en la base de datos
                Coche? coche = CocheDAO.ConsultarCochePorPlaca(placaBuscada);

                if (coche != null)
                {
                    ViewBag.Coche = coche;
                    ViewBag.Mensaje = "Vehículo encontrado.";
                }
                else
                {
                    TempData["Error"] = "Placa no encontrada en la base de datos.";
                }

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }
        }

        // LISTAR TODOS - vista adicional pa ver todos los coches
        // esto no estaba en windows forms pero es util en web
        public IActionResult Listado()
        {
            try
            {
                var coches = CocheDAO.ConsultarTodosLosCoches();
                return View(coches);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(new List<Coche>());
            }
        }
    }
}
