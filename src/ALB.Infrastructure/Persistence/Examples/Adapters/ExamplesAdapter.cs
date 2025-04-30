using ALB.Domain.Entities;

namespace ALB.Infrastructure.Persistence.Examples.Adapters;

public class ExamplesAdapter(ExamplesDbContext context): IExamplesAdapter
{
    public Task<Example> CreateExampleAsync(string name)
    {
        // Here you would use the DbContext
        throw new NotImplementedException();
    }

    public Task DeleteExampleAsync(Guid id)
    {
        // Here you would use the DbContext
        throw new NotImplementedException();
    }
}