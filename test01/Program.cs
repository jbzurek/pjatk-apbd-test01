using test01.Repositories;
using test01.Repositories.Medicaments;
using test01.Repositories.Patients;
using test01.Services;
using test01.Services.Medicaments;
using test01.Services.Patients;
using test01.Services.Prescriptions;

namespace test01;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        
        builder.Services.AddScoped<IMedicamentsRepository, MedicamentRepository>();
        builder.Services.AddScoped<IMedicamentsService, MedicamentsService>();
        
        builder.Services.AddScoped<IPrescriptionsService, PrescriptionsService>();
        
        builder.Services.AddScoped<IPatientsRepository, PatientsRepository>();
        builder.Services.AddScoped<IPatientService, PatientsService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}