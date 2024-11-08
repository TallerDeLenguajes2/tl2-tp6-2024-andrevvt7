using Microsoft.Data.Sqlite;
using TiendaVistas.Models;
namespace TiendaVistas.Repositories;

public class ClienteRepository
{
    public string cadenaConexion = "Data Source=DB/Tienda.db";
    public List<Cliente> Clientes = new List<Cliente>();
    
    public List<Cliente> GetClientes()
    {
        var queryString = @"SELECT * FROM Clientes;";
        using (SqliteConnection conexion = new SqliteConnection(cadenaConexion))
        {
            conexion.Open();
            SqliteCommand comando = new SqliteCommand(queryString, conexion);
            using (SqliteDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.IdCliente = Convert.ToInt32(reader["ClienteId"]);
                    cliente.Nombre = reader["Nombre"].ToString();
                    cliente.Telefono = reader["Telefono"].ToString();
                    if (reader["Email"] != null)
                    {
                        cliente.Email = reader["Email"].ToString();
                    } else {
                        cliente.Email = "";
                    }

                    Clientes.Add(cliente);
                }
            }
            conexion.Close();
        }

        return Clientes;
    }

    public void CrearCliente(Cliente ClienteNuevo)
    {
        var queryString = $"INSERT INTO Clientes (Nombre,Email,Telefono) VALUES (@nombre,@email,@telefono);";
        using (SqliteConnection conexion = new SqliteConnection(cadenaConexion))
        {
            conexion.Open();
            SqliteCommand comando = new SqliteCommand(queryString, conexion);

            comando.Parameters.Add(new SqliteParameter("@nombre", ClienteNuevo.Nombre));
            comando.Parameters.Add(new SqliteParameter("@email", ClienteNuevo.Email));
            comando.Parameters.Add(new SqliteParameter("@telefono", ClienteNuevo.Telefono));

            comando.ExecuteNonQuery();
            conexion.Close();
        }

        Clientes.Add(ClienteNuevo);
    }

    public void ModificarCliente(int idCliente, Cliente ClienteNuevo)
    {
        var queryString = $"UPDATE Clientes SET Email=@email, Telefono=@telefono WHERE ClienteId=@idC;";
        using (SqliteConnection conexion = new SqliteConnection(cadenaConexion))
        {
            conexion.Open();
            SqliteCommand comando = new SqliteCommand(queryString, conexion);

            comando.Parameters.Add(new SqliteParameter("@email", ClienteNuevo.Email));
            comando.Parameters.Add(new SqliteParameter("@telefono", ClienteNuevo.Telefono));
            comando.Parameters.Add(new SqliteParameter("@idC", idCliente));

            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }

    public void EliminarCliente(int idCliente)
    {
        var consulta = "SELECT idPresupuesto FROM Presupuestos WHERE ClienteId=@idC;";
        var queryString = $"DELETE FROM PresupuestosDetalle WHERE idPresupuesto = @idP;";
        var queryString2 = $"DELETE FROM Presupuestos WHERE ClienteId=@idC;";
        var queryString3 = $"DELETE FROM Clientes WHERE ClienteId=@idC;";
        using (SqliteConnection conexion = new SqliteConnection(cadenaConexion))
        {
            conexion.Open();
            SqliteCommand comandoConsulta = new SqliteCommand(consulta, conexion);
            comandoConsulta.Parameters.Add(new SqliteParameter("@idC", idCliente));
            
            SqliteCommand comando2 = new SqliteCommand(queryString2, conexion);
            SqliteCommand comando3 = new SqliteCommand(queryString3, conexion);

            comando2.Parameters.Add(new SqliteParameter("@idC", idCliente));
            comando3.Parameters.Add(new SqliteParameter("@idC", idCliente));

            using (SqliteDataReader reader = comandoConsulta.ExecuteReader())
            {
                while (reader.Read())
                {
                    SqliteCommand comando = new SqliteCommand(queryString, conexion);
                    comando.Parameters.Add(new SqliteParameter("@idP", Convert.ToInt32(reader["idPresupuesto"])));
                    comando.ExecuteNonQuery();
                }
            }

            comando2.ExecuteNonQuery();
            comando3.ExecuteNonQuery();

            conexion.Close();
        }
    }
}