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
    [HttpPut("Modificar")]

    public IActionResult Modificar()
    {
        return View();
    }

    //_____________ELIMINAR PRODUCTO___________________
    [HttpDelete("Eliminar")]
    public IActionResult Eliminar()
    {
        return View();
    }

    //     [HttpGet("GetProductos")]
    //     public IActionResult GetProductos()
    //     {
    //         if (productos.Count() == 0)
    //         {
    //             return NotFound("No hay productos");
    //         }

    //         return Ok(productos);
    //     }

    //     [HttpPost("CrearProducto")]
    //     public IActionResult CrearProducto([FromBody] Producto producto)
    //     {
    //         productoRepositorio.CrearProducto(producto);

    //         return Ok($"Producto creado con ID {producto.IdProducto}");
    //     }

    //     [HttpPut("ModificarProducto/{id}")]
    //     public IActionResult ModificarProducto(int id, [FromBody]Producto producto)
    //     {
    //         if (productos.FirstOrDefault(p => p.IdProducto == id) == null)
    //         {
    //             return NotFound($"No se encontró el producto con ID {id}");
    //         }

    //         productoRepositorio.ModificarProducto(id, producto);

    //         return Ok($"Producto {id} modificado.");
    //     }

    //     [HttpDelete("EliminarProducto/{id}")]
    //     public IActionResult EliminarProducto(int id)
    //     {
    //         if (productos.FirstOrDefault(p => p.IdProducto == id) == null)
    //         {
    //             return NotFound($"El producto con ID {id} no existe");
    //         }

    //         productoRepositorio.EliminarProducto(id);

    //         return Ok($"Producto eliminado: ID {id}");
    //     }
}