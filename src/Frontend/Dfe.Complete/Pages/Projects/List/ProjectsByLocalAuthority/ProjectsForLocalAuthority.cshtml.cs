using Dfe.Complete.Application.Projects.Queries.ListAllProjectsForLocalAuthority;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Complete.Pages.Projects.List.ProjectsByLocalAuthority;

public class ProjectsForLocalAuthority(ISender sender) : AllProjectsModel(ByLocalAuthorityNavigation)
{
    [BindProperty(SupportsGet = true)] public string LocalAuthorityCode { get; set; }

    public string LocalAuthorityName { get; set; }

    public async Task OnGet()
    {
        var query = new ListAllProjectsForLocalAuthorityQuery(LocalAuthorityCode = LocalAuthorityCode)
            { Count = PageSize, Page = PageNumber - 1 };

        var result = await sender.Send(query);
    }
}