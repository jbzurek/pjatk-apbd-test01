using System.Data.SqlClient;
using test01.Models;

namespace test01.Services.Prescriptions;

public class PrescriptionsService : IPrescriptionsService
{
    private readonly IConfiguration _configuration;

    public PrescriptionsService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<Prescription>> GetPrescriptionsByMedicament(int idMedicament)
    {
        var prescriptions = new List<Prescription>();

        await using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();

        await using var command = new SqlCommand();
        command.Connection = connection;

        command.CommandText = "SELECT Date, IdPatient, IdDoctor " +
                              "FROM Prescription " +
                              "WHERE IdPrescription IN " +
                              "(SELECT IdPrescription FROM Prescription_Medicament WHERE IdMedicament = @IdMedicament);";
        command.Parameters.AddWithValue("@IdMedicament", idMedicament);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var prescription = new Prescription()
            {
                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                IdPatient = reader.GetInt32(reader.GetOrdinal("IdPatient")),
                IdDoctor = reader.GetInt32(reader.GetOrdinal("IdDoctor"))
            };
            prescriptions.Add(prescription);
        }

        await reader.CloseAsync();
        command.Parameters.Clear();
        await connection.CloseAsync();

        return prescriptions;
    }
}