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
    List<Producto> productos;
    ProductoRepository productoRepositorio = new ProductoRepository();

    public PresupuestoController(ILogger<PresupuestoController> logger)
    {
        __logger = logger;
        presupuestos = presupuestoRepositorio.GetPresupuestosCompleto();
        productos = productoRepositorio.GetProductos();
    }

    //_____________LISTAR PRESUPUESTOS___________________
    [HttpGet("Listar")]
    public IActionResult Listar()
    {
        return View(presupuestos);
    }

    //_____________OBTENER DETALLE DE UN PRESUPUESTO___________________
    [HttpGet("DetallePresupuesto")]
    public IActionResult DetallePresupuesto(int idPresupuesto)
    {
        Presupuesto? presupuesto = presupuestos.FirstOrDefault(p => p.IdPresupuesto == idPresupuesto);
        ViewBag.IdPresupuesto = idPresupuesto;

        return View(presupuesto.Detalle);
    }

    //_____________CREAR PRESUPUESTO___________________
    [HttpGet("Crear")]
    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost("CrearPresupuesto")]
    public IActionResult CrearPresupuesto()
    {
        Presupuesto presupuesto = new Presupuesto();
        presupuesto.NombreDestinatario = Request.Form["NombreDestinatario"];
        presupuestoRepositorio.CrearPresupuesto(presupuesto);

        ViewBag.Creado = true;

        return View("Crear");
    }

    //_____________MODIFICAR PRESUPUESTO___________________
    [HttpGet("Modificar")]
    public IActionResult Modificar(int idPresupuesto)
    {
        ViewBag.IdPresupuesto = idPresupuesto;
        return View();
    }

    [HttpPost("ModificarPresupuesto")] //Agregar DETALLE NUEVO
    public IActionResult ModificarPresupuesto()
    {
        Producto producto = productos.FirstOrDefault(p => p.IdProducto == int.Parse(Request.Form["IdProducto"]));
        int idPresupuesto = int.Parse(Request.Form["IdPresupuesto"]);
        PresupuestoDetalle detalleNuevo = new PresupuestoDetalle();
        detalleNuevo.Producto = producto;
        detalleNuevo.Cantidad = int.Parse(Request.Form["Cantidad"]);

        presupuestoRepositorio.AgregarDetalle(idPresupuesto, detalleNuevo);
        ViewBag.Modificado = true;

        return View("Modificar");
    }

    //_____________ELIMINAR PRESUPUESTO___________________
    [HttpGet("Eliminar")]
    public IActionResult Eliminar(int idPresupuesto)
    {
        presupuestoRepositorio.EliminarPresupuesto(idPresupuesto);

        ViewBag.Eliminado = $"Presupuesto {idPresupuesto} eliminado";

        return View();
    }
}