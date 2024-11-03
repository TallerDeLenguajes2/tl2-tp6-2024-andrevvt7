using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Diagnostics;
using TiendaVistas.Models;
using TiendaVistas.Repositories;
namespace TiendaVistas.Controllers;
// namespace tienda;

[ApiController]
[Route("[controller]")]

/*
Construya un controlador para Productos y otro para Presupuestos e implemente las
siguientes Vistas:
● En el controlador de Presupuestos: Listar, Crear, Modificar y Eliminar Presupuestos.
    ○ Tiene que poder cargar productos a un presupuesto específico
    ○ Tiene que poder ver un presupuesto con el listado de productos correspondientes.
*/

public class PresupuestoController : Controller
{
    private readonly ILogger<PresupuestoController> __logger;
    List<Presupuesto> presupuestos;
    PresupuestoRepository presupuestoRepositorio = new PresupuestoRepository();

    public PresupuestoController(ILogger<PresupuestoController> logger)
    {
        __logger = logger;
        presupuestos = presupuestoRepositorio.GetPresupuestosCompleto();
    }

//_____________LISTAR PRESUPUESTOS___________________
    [HttpGet("Listar")]
    public IActionResult Listar()
    {
        return View(presupuestos);
    }

//_____________OBTENER DETALLE DE UN PRESUPUESTO___________________
    [HttpGet("DetallePresupuesto")]
    public IActionResult DetallePresupuesto(int IdPresupuesto)
    {
        Presupuesto? presupuesto = presupuestos.FirstOrDefault(p => p.IdPresupuesto == IdPresupuesto);
        ViewBag.IdPresupuesto = IdPresupuesto;

        return View(presupuesto.Detalle);
    }

//_____________CREAR PRESUPUESTO___________________
    [HttpPost("Crear")]
    public IActionResult Crear()
    {
        return View();
    }

//_____________MODIFICAR PRESUPUESTO___________________
    [HttpPut("Modificar")]
    public IActionResult Modificar()
    {
        return View();
    }

//_____________ELIMINAR PRESUPUESTO___________________
    [HttpDelete("Eliminar")]
    public IActionResult Eliminar()
    {
        return View();
    }
}