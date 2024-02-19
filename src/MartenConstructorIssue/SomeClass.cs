namespace MartenConstructorIssue;

public class SomeClass : SomeBaseClass<string>
{
    /// <inheritdoc />
    public SomeClass(Guid id, Guid anotherId, IReadOnlyCollection<string> aListOfThings) : base(id, anotherId, aListOfThings)
    {
    }
}