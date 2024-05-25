using Proyect2TWM.Api.Repositories.Interfecies;
using Proyect2TWM.Core.Entities;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Api.Services.Interfaces;

namespace Proyect2TWM.Api.Services;

public class EmploymentHistoryService : IEmploymentHistoryService
{
    private readonly IEmploymentHistoryRepository _employmentHistory;

    public EmploymentHistoryService(IEmploymentHistoryRepository employmentHistory)
    {
        _employmentHistory = employmentHistory;
    }

    public async Task<bool> EmploymentHistoryExist(int id)
    {
        var employmentHistory = await _employmentHistory.GetByIdEH(id);
        return (employmentHistory != null);
    }

    public async Task<EmploymentHistoryDto> SaveAsycnEH(EmploymentHistoryDto employmentHistoryDto)
    {
        var employeeHistory = new Employment_History
        {
            ID_Employee = employmentHistoryDto.ID_Employee,
            CompanyName = employmentHistoryDto.CompanyName,
            CompanyPosition = employmentHistoryDto.CompanyPosition,
            Description = employmentHistoryDto.Description,
            QuitWork = employmentHistoryDto.QuitWork,
            CreatedBy = "Gaby-Garcia",
            CreatedDate = DateTime.Now,
            UpdatedBy = "Gaby-Garcia",
            UpdatedDate = DateTime.Now
        };
        employeeHistory = await _employmentHistory.SaveAsycnEH(employeeHistory);
        employmentHistoryDto.id = employeeHistory.id;

        return employmentHistoryDto;
    }

    public async Task<EmploymentHistoryDto> UpdateAsyncEH(EmploymentHistoryDto employmentHistoryDto)
    {
        var employesHistory = await _employmentHistory.GetByIdEH(employmentHistoryDto.id);
        if (employesHistory == null)
            throw new Exception("Employment History Not Found");
        
        employesHistory.ID_Employee = employmentHistoryDto.ID_Employee;
        employesHistory.CompanyName = employmentHistoryDto.CompanyName;
        employesHistory.CompanyPosition = employmentHistoryDto.CompanyPosition;
        employesHistory.Description = employmentHistoryDto.Description;
        employesHistory.QuitWork = employmentHistoryDto.QuitWork;
        employesHistory.UpdatedBy = "Gaby-Garcia";
        employesHistory.UpdatedDate = DateTime.Now;
        
        await _employmentHistory.UpdateAsyncEH(employesHistory);
        return employmentHistoryDto;
    }

    public async Task<List<EmploymentHistoryDto>> GetAllAsyncEH()
    {
        var employmentHistory = await _employmentHistory.GetAllAsyncEH();
        var employmentHistoryDto = employmentHistory.Select(c => new EmploymentHistoryDto(c)).ToList();
        return employmentHistoryDto;
    }

    public async Task<bool> DeleteAsyncEH(int id)
    {
        return await _employmentHistory.DeleteAsyncEH(id);
    }

    public async Task<EmploymentHistoryDto> GetByIdEH(int id)
    {
        var employmentHistory = await _employmentHistory.GetByIdEH(id);
        if (employmentHistory == null)
            throw new Exception("Employment History not found");

        var employmentHistoryDto = new EmploymentHistoryDto(employmentHistory);
        return employmentHistoryDto;
    }
    
    public async Task<bool> ExistByCompanyName(string companyName, int id = 0)
    {
        var employment = await _employmentHistory.GetByCompanyName(companyName, id);
        return employment != null;
    }
}