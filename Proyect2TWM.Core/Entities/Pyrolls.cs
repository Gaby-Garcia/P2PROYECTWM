namespace Proyect2TWM.Core.Entities;

public class Pyrolls : EntityBase
{
    public int ID_Employee { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Salary { get; set; }
    public decimal Bonuses { get; set; }
    public decimal Deductions { get; set; }
    public int HoursWorked { get; set; }
}

