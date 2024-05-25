using Proyect2TWM.Api.Repositories.Interfecies;
using Proyect2TWM.Core.Entities;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Api.Services.Interfaces;

namespace Proyect2TWM.Api.Services;

public class PyrollsService: IPyrollsService
{
    private readonly IPayrollsRepository _payrollsRepository;

    public PyrollsService(IPayrollsRepository payrollsRepository)
    {
        _payrollsRepository = payrollsRepository;
    }

    public async Task<bool> PyrollsExist(int id)
    {
        var pyroll = await _payrollsRepository.GetByIdP(id);
        return (pyroll != null);
    }

    public async Task<PayrollsDto> SaveAsycnP(PayrollsDto pyrollsDto)
    {
        var pyrolls = new Pyrolls
        {
            ID_Employee = pyrollsDto.ID_Employee,
            PaymentDate = pyrollsDto.PaymentDate,
            Salary = pyrollsDto.Salary,
            Bonuses = pyrollsDto.Bonuses,
            Deductions = pyrollsDto.Deductions,
            HoursWorked = pyrollsDto.HoursWorked,
            CreatedBy = "Gaby-Garcia",
            CreatedDate = DateTime.Now,
            UpdatedBy = "Gaby-Garcia",
            UpdatedDate = DateTime.Now
        };
        pyrolls = await _payrollsRepository.SaveAsycnP(pyrolls);
        pyrollsDto.id = pyrolls.id;

        return pyrollsDto;
    }

    public async Task<PayrollsDto> UpdateAsyncP(PayrollsDto payrollsDto)
    {
        var pyroll = await _payrollsRepository.GetByIdP(payrollsDto.id);
        if (pyroll == null)
            throw new Exception("Pyroll Not Found");
        pyroll.ID_Employee = payrollsDto.ID_Employee;
        pyroll.PaymentDate = payrollsDto.PaymentDate;
        pyroll.Salary = payrollsDto.Salary;
        pyroll.Bonuses = payrollsDto.Bonuses;
        pyroll.Deductions = payrollsDto.Deductions;
        pyroll.HoursWorked = payrollsDto.HoursWorked;
        pyroll.UpdatedBy = "Gaby-Garcia";
        pyroll.UpdatedDate = DateTime.Now;
        
        await _payrollsRepository.UpdateAsyncP(pyroll);
        return payrollsDto;
    }

    public async Task<List<PayrollsDto>> GetAllAsyncP()
    {
        var pyroll = await _payrollsRepository.GetAllAsyncP();
        var pyrollDto = pyroll.Select(c => new PayrollsDto(c)).ToList();
        return pyrollDto;
    }

    public async Task<bool> DeleteAsyncP(int id)
    {
        return await _payrollsRepository.DeleteAsyncP(id);
    }

    public async Task<PayrollsDto> GetByIdP(int id)
    {
        var payroll = await _payrollsRepository.GetByIdP(id);
        if (payroll == null)
            throw new Exception("Pyroll not found");

        var pyrollDto = new PayrollsDto(payroll);
        return pyrollDto;
    }
    
    public async Task<bool> ExistByPaymentDate(string paymentDate, int id = 0)
    {
        var pyroll = await _payrollsRepository.GetByDatePayment(paymentDate, id);
        return pyroll != null;
    }
}