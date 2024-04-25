using Proyect2TWM.Core.Entities;

namespace Proyect2TWM.Api.Dto;

public class EmploymentHistoryDto : DtoBase
{
    public int ID_Employee { get; set; }
    public string CompanyName { get; set; }
    public string CompanyPosition { get; set; }
    public string Description { get; set; }
    public DateTime QuitWork { get; set; }

    public EmploymentHistoryDto()
    {
        
    }

    public EmploymentHistoryDto(Employment_History employmentHistory)
    {
        id = employmentHistory.id;
        ID_Employee = employmentHistory.ID_Employee;
        CompanyName = employmentHistory.CompanyName;
        CompanyPosition = employmentHistory.CompanyPosition;
        Description = employmentHistory.Description;
        QuitWork = employmentHistory.QuitWork;
    }
}