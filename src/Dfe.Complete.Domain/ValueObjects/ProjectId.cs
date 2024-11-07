using Dfe.Complete.Domain.Common;

namespace Dfe.Complete.Domain.ValueObjects
{
    public record ProjectId(Guid Value) : IStronglyTypedId;

}
