public class BadUserDesign
{
    public BadUserDesign(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }
}