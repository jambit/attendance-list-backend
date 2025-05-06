using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ALB.Infrastructure.Persistence;

public class UuiDv7Generator : ValueGenerator<Guid>
{
    public override Guid Next(EntityEntry entry)
        => Guid.CreateVersion7();

    public override bool GeneratesTemporaryValues { get; }
}