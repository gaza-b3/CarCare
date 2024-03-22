using Client.Pages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    public class VehiculeService
    {
        private List<VehiculeForecast> Vehicules { get; set; } = new List<Vehicule>
        {
            new Vehicule { Id = 1, Marque = "Toyota", Modele = "Corolla", Immatriculation = "ABC-123", Annee = 2018, Kilometrage = 45000, Energie = "Essence" },
            new Vehicule { Id = 2, Marque = "Honda", Modele = "Civic", Immatriculation = "DEF-456", Annee = 2019, Kilometrage = 35000, Energie = "Essence" },
            new Vehicule { Id = 3, Marque = "Nissan", Modele = "Leaf", Immatriculation = "GHI-789", Annee = 2020, Kilometrage = 15000, Energie = "Électrique" }
        };

        public async Task<Vehicule> GetVehiculeById(int id)
        {
            return await Task.Run(() => Vehicules.FirstOrDefault(v => v.Id == id));
        }
    }
}