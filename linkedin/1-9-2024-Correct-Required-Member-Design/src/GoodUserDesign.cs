public class GoodUserDesign
{
    public GoodUserDesign(string name, string surname)
    {
        // Throw exceptions if required arguments are not valid

        Name = string.IsNullOrEmpty(name)
            ? throw new ArgumentException($"{name} can not be null or empty!", name)
            : name;

        Surname = string.IsNullOrEmpty(surname)
            ? throw new ArgumentException($"{surname} can not be null or empty!", surname)
            : surname;
    }

    [Required]
    public string Name { get; private set; } // Make setter private

    [Required]
    public string Surname { get; private set; } // Make setter private
}