using test01.Models;
using test01.Repositories.Patients;

namespace test01.Services.Patients;

public class PatientsService : IPatientService
{
    private readonly IPatientsRepository _patientsRepository;

    public PatientsService(IPatientsRepository patientsRepository)
    {
        _patientsRepository = patientsRepository;
    }

    public Task<Patient> DeletePatient(int id)
    {
        return _patientsRepository.DeletePatient(id);
    }
}