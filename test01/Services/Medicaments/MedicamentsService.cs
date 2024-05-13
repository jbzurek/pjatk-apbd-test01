using test01.Models;
using test01.Repositories.Medicaments;

namespace test01.Services.Medicaments;

public class MedicamentsService : IMedicamentsService
{
    private readonly IMedicamentsRepository _medicamentsRepository;

    public MedicamentsService(IMedicamentsRepository medicamentsRepository)
    {
        _medicamentsRepository = medicamentsRepository;
    }

    public Task<Medicament> GetMedicamentAsync(int idMedicament)
    {
        return _medicamentsRepository.GetMedicamentAsync(idMedicament);
    }
}