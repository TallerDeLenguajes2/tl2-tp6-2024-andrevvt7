using Microsoft.AspNetCore.Mvc;
using TiendaVistas.Models;
using TiendaVistas.Repositories;
namespace TiendaVistas.Controllers;

[ApiController]
[Route("[controller]")]

public class ClienteController : Controller
{
    private readonly ILogger<ClienteController> _logger;
    List<Cliente> clientes;
    ClienteRepository clienteRepositorio = new ClienteRepository();

    public ClienteController(ILogger<ClienteController> logger)
    {
        _logger = logger;
        clientes = clienteRepositorio.GetClientes();
    }

    //_____________LISTAR CLIENTES___________________
    [HttpGet("Listar")]
    public IActionResult Listar()
    {
        return View(clientes);
    }

    //_____________CREAR CLIENTE___________________
    [HttpGet("Crear")]
    public IActionResult Crear()
    {
        return View();
    }
    [HttpPost("CrearCliente")]
    public IActionResult CrearCliente()
    {
        Cliente Cliente = new Cliente();
        Cliente.Nombre = Request.Form["Nombre"];
        Cliente.Email = Request.Form["Email"];
        Cliente.Telefono = Request.Form["Telefono"];
        clienteRepositorio.CrearCliente(Cliente);

        ViewBag.Creado = true;

        return View("Crear");
    }

    //_____________MODIFICAR CLIENTE___________________
    [HttpGet("Modificar")]
    public IActionResult Modificar(int idCliente)
    {
        ViewBag.IdCliente = idCliente;
        return View();
    }

    [HttpPost("ModificarCliente")]
    public IActionResult ModificarCliente()
    {
        int idCliente = int.Parse(Request.Form["IdCliente"]);
        Cliente Cliente = clientes.FirstOrDefault(p => p.IdCliente == idCliente);

        if (Request.Form["Emaiil"] != "")
        {
            Cliente.Email = Request.Form["Email"];
        }

        if (Request.Form["Telefono"] != "")
        {
            Cliente.Telefono = Request.Form["Telefono"];
        }

        clienteRepositorio.ModificarCliente(idCliente, Cliente);
        ViewBag.Modificado = true;

        return View("Modificar");
    }

    //_____________ELIMINAR CLIENTE___________________
    [HttpGet("Eliminar")]
    public IActionResult Eliminar(int idCliente)
    {
        clienteRepositorio.EliminarCliente(idCliente);

        ViewBag.Eliminado = $"Cliente {idCliente} eliminado";
        
        return View();
    }
}