using System.Diagnostics.CodeAnalysis;
using Dfe.Complete.Domain.Common;
using Dfe.Complete.Domain.Interfaces.Repositories;
using Dfe.Complete.Infrastructure.Database;

namespace Dfe.Complete.Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
public class CompleteRepository<TAggregate>(CompleteContext dbContext) : Repository<TAggregate, CompleteContext>(dbContext), ICompleteRepository<TAggregate>
    where TAggregate : class, IAggregateRoot;