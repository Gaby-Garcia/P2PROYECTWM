namespace Proyect2TWM.Core.Entities;

public class Employment_History : EntityBase
{
    public int ID_Employee { get; set; }
    public string CompanyName { get; set; }
    public string CompanyPosition { get; set; }
    public string Description { get; set; }
    public DateTime QuitWork { get; set; }

}