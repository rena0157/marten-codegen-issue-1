namespace MartenConstructorIssue;

public class SomeBaseClass<T>
{
    public SomeBaseClass(Guid id, Guid anotherId, IReadOnlyCollection<string> aListOfThings)
    {
        Id = id;
        AnotherId = anotherId;
        AListOfThings = aListOfThings;
    }

    public Guid Id { get; set; }
    
    public Guid AnotherId { get; }
    
    public T? SomeData { get; set; }
    
    public IReadOnlyCollection<string> AListOfThings { get; }
}
