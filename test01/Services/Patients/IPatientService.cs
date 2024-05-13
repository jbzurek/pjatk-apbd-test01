using test01.Models;

namespace test01.Services.Patients;

public interface IPatientService
{
    Task<Patient> DeletePatient(int id);
}