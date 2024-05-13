using test01.Models;

namespace test01.Services.Prescriptions;

public interface IPrescriptionsService
{
    Task<List<Prescription>> GetPrescriptionsByMedicament(int idMedicament);
}