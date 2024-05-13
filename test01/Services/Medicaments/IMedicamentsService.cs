using test01.Models;

namespace test01.Services.Medicaments;

public interface IMedicamentsService
{
    Task<Medicament> GetMedicamentAsync(int idMedicament);
}