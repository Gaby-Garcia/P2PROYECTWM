namespace Proyect2TWM.Core.Entities;

public class VacationsAbsences : EntityBase
{
    public int ID_Employee { get; set; }
    public string Type { get; set; }
    public string Deducted { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
