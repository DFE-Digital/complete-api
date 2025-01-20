﻿using Dfe.Complete.Application.Projects.Interfaces;
using Dfe.Complete.Application.Projects.Model;
using Dfe.Complete.Domain.Enums;
using Dfe.Complete.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Dfe.Complete.Infrastructure.QueryServices;

internal class ListAllProjectsQueryService(CompleteContext context) : IListAllProjectsQueryService
{
    public IQueryable<ListAllProjectsQueryModel> ListAllProjects(ProjectState? projectStatus, ProjectType? type, bool? includeFormAMat)
    {
        var query = context.Projects
            .Where(project => projectStatus == null || project.State == projectStatus)
            .Where(project => type == null || type == project.Type)
            .Where(project => includeFormAMat == null ||
                              includeFormAMat == (project.IncomingTrustUkprn == null))
            .Include(p => p.AssignedTo)
            .Join(context.GiasEstablishments, project => project.Urn, establishment => establishment.Urn,
                (project, establishment) => new ListAllProjectsQueryModel(project, establishment));

        return query;
    }
}