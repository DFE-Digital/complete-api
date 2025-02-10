using Dfe.Complete.Application.Projects.Models;
using Dfe.Complete.Domain.Enums;

namespace Dfe.Complete.Application.Projects.Interfaces;

public interface IListAllProjectLocalAuthoritiesQueryService
{
    IQueryable<ListAllProjectLocalAuthoritiesQueryModel> ListAllProjectLocalAuthorities(ProjectState? projectStatus, ProjectType? type);
}