using System.Data.SqlClient;
using test01.Models;

namespace test01.Repositories.Patients;

public class PatientsRepository : IPatientsRepository
{
    private readonly IConfiguration _configuration;

    public PatientsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Patient> DeletePatient(int id)
    {
        await using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();
        using var transaction = connection.BeginTransaction();


        await DeletePrescriptions(id, connection, transaction);

        var deletePatientCommand = new SqlCommand
        {
            Connection = connection,
            Transaction = transaction,
            CommandText = "DELETE FROM Patient WHERE IdPatient = @PatientId"
        };
        deletePatientCommand.Parameters.AddWithValue("@PatientId", id);
        await deletePatientCommand.ExecuteNonQueryAsync();

        transaction.Commit();

        return null;
    }

    private async Task DeletePrescriptions(int patientId, SqlConnection connection,
        SqlTransaction transaction)
    {
        var deletePrescriptionsCommand = new SqlCommand
        {
            Connection = connection,
            Transaction = transaction,
            CommandText = "DELETE FROM Prescription WHERE IdPatient = @PatientId"
        };
        deletePrescriptionsCommand.Parameters.AddWithValue("@PatientId", patientId);
        await deletePrescriptionsCommand.ExecuteNonQueryAsync();
    }
}