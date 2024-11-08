using System.ComponentModel.DataAnnotations;

namespace TiendaVistas.Models;

public class Cliente{
    int idCliente;
    string? nombre;
    string? email;
    string? telefono;

    public int IdCliente { get => idCliente; set => idCliente = value; }
    public string? Nombre { get => nombre; set => nombre = value; }
    public string? Email { get => email; set => email = value; }
    public string? Telefono { get => telefono; set => telefono = value; }
}