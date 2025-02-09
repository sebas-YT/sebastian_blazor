namespace GestionVehiculos.Models
{
    public class Vehiculo
    {
        public int Id { get; set; } // ID único para cada vehículo
        public string Nombre { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
    }
}
