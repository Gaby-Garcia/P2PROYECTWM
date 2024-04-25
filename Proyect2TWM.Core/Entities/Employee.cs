namespace Proyect2TWM.Core.Entities;

public class Employee : EntityBase
{
    private static readonly HashSet<string> AllowedGenders = new HashSet<string> { "Hombre", "Mujer", "No binario" };

    private string _gender;
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Gender { 
        get => _gender;
        set
        {
            if (!AllowedGenders.Contains(value))
            {
                throw new ArgumentException("El género proporcionado no es válido.");
            }
            _gender = value;
        }
}
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public int ID_Department { get; set; }


}