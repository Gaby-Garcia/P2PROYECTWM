using Proyect2TWM.Core.Entities;

namespace Proyect2TWM.Api.Dto;

public class PayrollsDto : DtoBase
{
    public int ID_Employee { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Salary { get; set; }
    public decimal Bonuses { get; set; }
    public decimal Deductions { get; set; }
    public int HoursWorked { get; set; }

    public PayrollsDto()
    {
        
    }

    public PayrollsDto(Pyrolls pyrolls)
    {
        id = pyrolls.id;
        ID_Employee = pyrolls.ID_Employee;
        PaymentDate = pyrolls.PaymentDate;
        Salary = pyrolls.Salary;
        Bonuses = pyrolls.Bonuses;
        Deductions = pyrolls.Deductions;
        HoursWorked = pyrolls.HoursWorked;
    }
}