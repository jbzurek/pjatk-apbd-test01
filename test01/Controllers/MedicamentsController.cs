using Microsoft.AspNetCore.Mvc;
using test01.Models;
using test01.Services.Medicaments;
using test01.Services.Patients;
using test01.Services.Prescriptions;

namespace test01.Controllers;

[ApiController]
[Route("api/medicaments")]
public class MedicamentsController : ControllerBase
{
    private readonly IMedicamentsService _medicamentsService;
    private readonly IPrescriptionsService _prescriptionsService;
    private readonly IPatientService _patientService;

    public MedicamentsController(IMedicamentsService medicamentsService, IPrescriptionsService prescriptionsService, IPatientService patientService)
    {
        _medicamentsService = medicamentsService;
        _prescriptionsService = prescriptionsService;
        _patientService = patientService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Medicament>> GetMedicamentAsync(int id)
    {
        var medicament = await _medicamentsService.GetMedicamentAsync(id);
        if (medicament == null)
        {
            return NotFound();
        }

        var prescriptions = await _prescriptionsService.GetPrescriptionsByMedicament(id);
        medicament.Prescriptions = prescriptions.OrderByDescending(p => p.Date).ToList();

        return Ok(medicament);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Patient>> DeletePatient(int id)
    {
        var deletedPatient = await _patientService.DeletePatient(id);
        if (deletedPatient == null)
        {
            return NoContent();
        }

        return NotFound();
    }
}