namespace test01.Models;

public class Medicament
{
    public List<Prescription> Prescriptions { get; set; }
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
}