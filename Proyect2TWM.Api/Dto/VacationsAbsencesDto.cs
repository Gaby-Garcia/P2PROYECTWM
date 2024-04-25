using Proyect2TWM.Core.Entities;

namespace Proyect2TWM.Api.Dto;

public class VacationsAbsencesDto : DtoBase
{
    public int ID_Employee { get; set; }
    public string Type { get; set; }
    public string Deducted { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public VacationsAbsencesDto()
    {
        
    }

    public VacationsAbsencesDto(VacationsAbsences vacationsAbsences)
    {
        id = vacationsAbsences.id;
        ID_Employee = vacationsAbsences.ID_Employee;
        Type = vacationsAbsences.Type;
        Deducted = vacationsAbsences.Deducted;
        StartDate = vacationsAbsences.StartDate;
        EndDate = vacationsAbsences.EndDate;
    }
}