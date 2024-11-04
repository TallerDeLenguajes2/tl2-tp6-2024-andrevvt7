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
● En el controlador de Productos : Listar, Crear, Modificar y Eliminar Productos.
*/
public class ProductoController : Controller
{
    private readonly ILogger<ProductoController> _logger;
    List<Producto> productos;
    ProductoRepository productoRepositorio = new ProductoRepository();

    public ProductoController(ILogger<ProductoController> logger)
    {
        _logger = logger;
        productos = productoRepositorio.GetProductos();
    }

    //_____________LISTAR PRODUCTOS___________________
    [HttpGet("Listar")]
    public IActionResult Listar()
    {
        return View(productos);
    }

    //_____________CREAR PRODUCTO___________________
    [HttpPost("Crear")]
    public IActionResult Crear()
    {
        return View();
    }

    //_____________MODIFICAR PRODUCTO___________________
    // [HttpPut("Modificar")]

    // public IActionResult Modificar(int IdProducto)
    // {
    //     return View();
    // }

    //_____________ELIMINAR PRODUCTO___________________
    // [HttpDelete("Eliminar")]
    // public IActionResult Eliminar(int idProducto)
    // {
    //     return View();
    // }
}