using MySql.Data.MySqlClient;

namespace ProyectoWeb_Concesionaria.Models
{
    // misma clase de conexion que en windows forms pero adaptada pa web
    public class DatabaseConnection
    {
        private static string connectionString = "Server=localhost;Database=concesionaria;Uid=root;Pwd=8875421390";

        // metodo pa obtener la conexion
        public static MySqlConnection GetConnection()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                return connection;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectar con la base de datos: " + ex.Message);
            }
        }

        // metodo para probar la conexion
        public static bool TestConnection()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
