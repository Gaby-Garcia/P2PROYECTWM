using Proyect2TWM.Core.Entities;

namespace Proyect2TWM.Api.Dto;

public class PerfomanceReviewsDto : DtoBase
{
    public int ID_Employee { get; set; }
    public DateTime EvaluationDate { get; set; }
    public decimal Clasification { get; set; }
    public string Comments { get; set; }
    public string Goals { get; set; }

    public PerfomanceReviewsDto()
    {
    }

    public PerfomanceReviewsDto(PerfomanceReview perfomanceReview)
    {
        id = perfomanceReview.id;
        ID_Employee = perfomanceReview.ID_Employee;
        EvaluationDate = perfomanceReview.EvaluationDate;
        Clasification = perfomanceReview.Clasification;
        Comments = perfomanceReview.Comments;
        Goals = perfomanceReview.Goals;
    }
}