using System.Data.SqlClient;
using test01.Models;

namespace test01.Repositories.Medicaments;

public class MedicamentRepository : IMedicamentsRepository
{
    private readonly IConfiguration _configuration;

    public MedicamentRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Medicament> GetMedicamentAsync(int idMedicament)
    {
        await using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();

        await using var command = new SqlCommand();
        command.Connection = connection;

        command.CommandText = "SELECT IdMedicament, Name, Description, Type " +
                              "FROM Medicament " +
                              "WHERE IdMedicament = @IdMedicament; ";
        command.Parameters.AddWithValue("@IdMedicament", idMedicament);

        await using var reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
        {
            return null;
        }

        var medicament = new Medicament()
        {
            Name = reader["Name"].ToString(),
            Description = reader["Description"].ToString(),
            Type = reader["Type"].ToString()
        };

        await reader.CloseAsync();
        command.Parameters.Clear();
        await connection.CloseAsync();
        return medicament;
    }

    public async Task<List<Prescription>> GetPrescriptionsForMedicamentAsync(int idMedicament)
    {
        var prescriptions = new List<Prescription>();

        await using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();

        await using var command = new SqlCommand();
        command.Connection = connection;

        command.CommandText = "SELECT p.IdPrescription, p.Date, p.DueDate, p.IdPatient, p.IdDoctor " +
                              "FROM Prescription p " +
                              "INNER JOIN Prescription_Medicament pm ON p.IdPrescription = pm.IdPrescription " +
                              "WHERE pm.IdMedicament = @IdMedicament " +
                              "ORDER BY p.Date DESC;";

        command.Parameters.AddWithValue("@IdMedicament", idMedicament);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var prescription = new Prescription()
            {
                IdPrescription = (int)reader["IdPrescription"],
                Date = (DateTime)reader["Date"],
                DueDate = (DateTime)reader["DueDate"],
                IdPatient = (int)reader["IdPatient"],
                IdDoctor = (int)reader["IdDoctor"]
            };
            prescriptions.Add(prescription);
        }

        await reader.CloseAsync();
        command.Parameters.Clear();
        await connection.CloseAsync();

        return prescriptions;
    }
}