using test01.Models;

namespace test01.Repositories.Patients;

public interface IPatientsRepository
{
    Task<Patient> DeletePatient(int id);
}