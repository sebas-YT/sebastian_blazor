using System.Text.Json;
using GestionVehiculos.Models;

namespace GestionVehiculos.Services
{
    public class VehiculoService
    {
        private readonly string _filePath = "vehiculos.json";
        private int _nextId = 1;
        public List<Vehiculo> Vehiculos { get; private set; } = new();

        public VehiculoService()
        {
            CargarDesdeDisco();
        }

        private void GuardarEnDisco()
        {
            var json = JsonSerializer.Serialize(Vehiculos);
            File.WriteAllText(_filePath, json);
        }

        private void CargarDesdeDisco()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                Vehiculos = JsonSerializer.Deserialize<List<Vehiculo>>(json) ?? new List<Vehiculo>();

                // Asegurar que _nextId sea mayor que cualquier ID existente
                if (Vehiculos.Any())
                    _nextId = Vehiculos.Max(v => v.Id) + 1;
            }
        }

        public void AgregarVehiculo(VehiculoDto nuevoVehiculo)
        {
            var vehiculo = new Vehiculo
            {
                Id = _nextId++,
                Nombre = nuevoVehiculo.Nombre,
                Modelo = nuevoVehiculo.Modelo
            };

            Vehiculos.Add(vehiculo);
            GuardarEnDisco(); // Guardar cambios en el disco
        }

        public void EliminarVehiculo(int id)
        {
            var vehiculo = Vehiculos.FirstOrDefault(v => v.Id == id);
            if (vehiculo != null)
            {
                Vehiculos.Remove(vehiculo);
                GuardarEnDisco(); // Guardar cambios en el disco
            }
        }
    }
}

