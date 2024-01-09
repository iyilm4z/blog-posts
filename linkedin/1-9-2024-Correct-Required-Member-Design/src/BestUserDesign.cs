public class BestUserDesign
{
    public BestUserDesign([NotNull] string name, [NotNull] string surname)
    {
        // Throw exception if required arguments are not valid

        Name = string.IsNullOrEmpty(name)
            ? throw new ArgumentException($"{name} can not be null or empty!", name)
            : name;

        Surname = string.IsNullOrEmpty(surname)
            ? throw new ArgumentException($"{surname} can not be null or empty!", surname)
            : surname;
    }

    [Required]
    [NotNull] // Help client: Client don't need to check whether this property is null or empty, because it can't be.
    public string Name { get; private set; } // Make setter private

    [Required]
    [NotNull] // Help client: Client don't need to check whether this property is null or empty, because it can't be.
    public string Surname { get; private set; } // Make setter private
}