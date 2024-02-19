using Marten.Events.Aggregation;

namespace MartenConstructorIssue.Api;

public class SomeClassProjection : SingleStreamProjection<SomeClass>
{
    public SomeClassProjection()
    {
        DeleteEvent<AnEventForDeleting>();
    }
    
    public SomeClass Create(AnEventForCreating created)
    {
        return new SomeClass(created.Id, created.AnotherId, []);
    }
    
    public SomeClass Apply(AnotherEvent anotherEvent, SomeClass state)
    {
        state.SomeData = anotherEvent.SomeData;
        return state;
    }
}