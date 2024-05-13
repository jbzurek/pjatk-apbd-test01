using test01.Models;

namespace test01.Repositories.Medicaments;

public interface IMedicamentsRepository
{
    Task<Medicament> GetMedicamentAsync(int idMedicament);
    Task<List<Prescription>> GetPrescriptionsForMedicamentAsync(int idMedicament);
}