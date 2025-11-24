using MySql.Data.MySqlClient;

namespace ProyectoWeb_Concesionaria.Models
{
    // el mismo DAO que en windows forms, funciona igual en web
    // lo unico que cambia es el namespace
    public class CocheDAO
    {
        // metodo para insertar un nuevo coche (ALTA)
        // exactamente igual que en windows forms
        public static bool InsertarCoche(Coche coche)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    string query = @"INSERT INTO coches (placa, marca, modelo, anio, tipo) 
                                   VALUES (@placa, @marca, @modelo, @anio, @tipo)";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@placa", coche.Placa);
                        cmd.Parameters.AddWithValue("@marca", coche.Marca);
                        cmd.Parameters.AddWithValue("@modelo", coche.Modelo);
                        cmd.Parameters.AddWithValue("@anio", coche.Anio);
                        cmd.Parameters.AddWithValue("@tipo", coche.Tipo);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) // placa duplicada
                {
                    throw new Exception("La placa ya existe en la base de datos.");
                }
                throw new Exception("Error al insertar: " + ex.Message);
            }
        }

        // metodo para consultar un coche por placa (CONSULTAR)
        public static Coche? ConsultarCochePorPlaca(string placa)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM coches WHERE placa = @placa";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@placa", placa);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Coche
                                {
                                    Id = reader.GetInt32("id"),
                                    Placa = reader.GetString("placa"),
                                    Marca = reader.GetString("marca"),
                                    Modelo = reader.GetString("modelo"),
                                    Anio = reader.GetInt32("anio"),
                                    Tipo = reader.GetString("tipo")
                                };
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar: " + ex.Message);
            }
        }

        // metodo para consultar todos los coches
        // este es nuevo, pa mostrar una lista completa en la web
        public static List<Coche> ConsultarTodosLosCoches()
        {
            List<Coche> coches = new List<Coche>();
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM coches ORDER BY id DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                coches.Add(new Coche
                                {
                                    Id = reader.GetInt32("id"),
                                    Placa = reader.GetString("placa"),
                                    Marca = reader.GetString("marca"),
                                    Modelo = reader.GetString("modelo"),
                                    Anio = reader.GetInt32("anio"),
                                    Tipo = reader.GetString("tipo")
                                });
                            }
                        }
                    }
                }
                return coches;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar todos los coches: " + ex.Message);
            }
        }

        // metodo para actualizar un coche (CAMBIOS)
        public static bool ActualizarCoche(Coche coche)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    string query = @"UPDATE coches 
                                   SET marca = @marca, modelo = @modelo, anio = @anio, tipo = @tipo 
                                   WHERE placa = @placa";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@placa", coche.Placa);
                        cmd.Parameters.AddWithValue("@marca", coche.Marca);
                        cmd.Parameters.AddWithValue("@modelo", coche.Modelo);
                        cmd.Parameters.AddWithValue("@anio", coche.Anio);
                        cmd.Parameters.AddWithValue("@tipo", coche.Tipo);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar: " + ex.Message);
            }
        }

        // metodo para eliminar un coche (BAJA)
        public static bool EliminarCoche(string placa)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    string query = "DELETE FROM coches WHERE placa = @placa";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@placa", placa);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar: " + ex.Message);
            }
        }
    }
}
