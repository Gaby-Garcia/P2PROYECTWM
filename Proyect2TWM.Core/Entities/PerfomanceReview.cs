namespace Proyect2TWM.Core.Entities;

public class PerfomanceReview : EntityBase
{
    public int ID_Employee { get; set; }
    public DateTime EvaluationDate { get; set; }
    public decimal Clasification { get; set; }
    public string Comments { get; set; }
    public string Goals { get; set; }
}
