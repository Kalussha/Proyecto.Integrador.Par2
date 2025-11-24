namespace ProyectoWeb_Concesionaria.Models
{
    // la misma clase coche que en windows forms, no cambia nada
    public class Coche
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public string Tipo { get; set; }

        // constructor vacio pa inicializar los strings
        public Coche()
        {
            Placa = string.Empty;
            Marca = string.Empty;
            Modelo = string.Empty;
            Tipo = string.Empty;
        }
    }
}
