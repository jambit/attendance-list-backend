using ALB.Domain.Entities;

namespace ALB.Infrastructure.Persistence.Examples.Adapters;

public interface IExamplesAdapter
{
    public Task<Example> CreateExampleAsync(string name);
    public Task DeleteExampleAsync(Guid id);
}