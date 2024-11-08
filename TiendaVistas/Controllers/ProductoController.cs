using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Diagnostics;
using TiendaVistas.Models;
using TiendaVistas.Repositories;
using System.Threading.Tasks;
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
    [HttpGet("Crear")]
    public IActionResult Crear()
    {
        return View();
    }
    [HttpPost("CrearProducto")]
    public IActionResult CrearProducto()
    {
        Producto producto = new Producto();
        producto.Descripcion = Request.Form["Descripcion"];
        producto.Precio = int.Parse(Request.Form["Precio"]);
        productoRepositorio.CrearProducto(producto);

        ViewBag.Creado = true;

        return View("Crear");
    }

    //_____________MODIFICAR PRODUCTO___________________
    [HttpGet("Modificar")]
    public IActionResult Modificar(int idProducto)
    {
        ViewBag.IdProducto = idProducto;
        return View();
    }

    [HttpPost("ModificarProducto")]
    public IActionResult ModificarProducto()
    {
        int idProducto = int.Parse(Request.Form["IdProducto"]);
        Producto producto = productos.FirstOrDefault(p => p.IdProducto == idProducto);

        if (Request.Form["Precio"] != "")
        {
            producto.Precio = Convert.ToInt32(Request.Form["Precio"]);
        }

        if (Request.Form["Descripcion"] != "")
        {
            producto.Descripcion = Request.Form["Descripcion"];
        }

        productoRepositorio.ModificarProducto(idProducto, producto);
        ViewBag.Modificado = true;

        return View("Modificar");
    }

    //_____________ELIMINAR PRODUCTO___________________
    [HttpGet("Eliminar")]
    public IActionResult Eliminar(int idProducto)
    {
        productoRepositorio.EliminarProducto(idProducto);

        ViewBag.Eliminado = $"Producto {idProducto} eliminado";
        
        return View();
    }
}