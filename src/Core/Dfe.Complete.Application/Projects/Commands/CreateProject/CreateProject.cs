using MediatR;
using Dfe.Complete.Domain.ValueObjects;
using Dfe.Complete.Domain.Enums;
using Dfe.Complete.Domain.Interfaces.Repositories;
using Dfe.Complete.Domain.Entities;
using Dfe.Complete.Infrastructure.Models;
using Dfe.Complete.Utils;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Dfe.Complete.Application.Projects.Commands.CreateProject
{
    public record CreateConversionProjectCommand(
        Urn Urn,
        DateOnly SignificantDate,
        bool IsSignificantDateProvisional,
        Ukprn IncomingTrustUkprn,
        Region? Region,
        bool IsDueTo2Ri,
        bool HasAcademyOrderBeenIssued,
        DateOnly AdvisoryBoardDate,
        string AdvisoryBoardConditions,
        string EstablishmentSharepointLink,
        string IncomingTrustSharepointLink,
        string GroupReferenceNumber,
        bool HandingOverToRegionalCaseworkService,
        string HandoverComments,
        User? RegionalDeliveryOfficer, 
        Team? Team) : IRequest<ProjectId>;

    public class CreateConversionProjectCommandHandler(
        ICompleteRepository<Project> projectRepository,
        ICompleteRepository<ConversionTasksData> conversionTaskRepository)
        : IRequestHandler<CreateConversionProjectCommand, ProjectId>
    {
        public async Task<ProjectId> Handle(CreateConversionProjectCommand request, CancellationToken cancellationToken)
        {
            var createdAt = DateTime.UtcNow;
            var conversionTaskId = Guid.NewGuid();
            var projectId = new ProjectId(Guid.NewGuid());

            var conversionTask = new ConversionTasksData(new TaskDataId(conversionTaskId), createdAt, createdAt);

            var groupId =
                await projectRepository.GetProjectGroupIdByIdentifierAsync(request.GroupReferenceNumber,
                    cancellationToken);

            string team;
            DateTime? assignedAt = null;
            User? assignedTo = null;
            Note? note = null;

            if (!string.IsNullOrEmpty(request.HandoverComments))
            {
                note = new Note
                {
                    Id = new NoteId(Guid.NewGuid()), CreatedAt = createdAt, Body = request.HandoverComments,
                    ProjectId = projectId, TaskIdentifier = "handover", UserId = request.RegionalDeliveryOfficer?.Id
                };
            }

            if (request.HandingOverToRegionalCaseworkService)
            {
                team = "regional_casework_services";
            }
            else
            {
                team = request.Team.ToDescription();
                assignedAt = DateTime.UtcNow;
                assignedTo = request.RegionalDeliveryOfficer;
            }

            var project = Project.CreateConversionProject(
                projectId,
                request.Urn,
                createdAt,
                createdAt,
                TaskType.Conversion,
                ProjectType.Conversion,
                conversionTaskId,
                request.SignificantDate,
                request.IsSignificantDateProvisional,
                request.IncomingTrustUkprn,
                request.Region,
                request.IsDueTo2Ri,
                request.HasAcademyOrderBeenIssued,
                request.AdvisoryBoardDate,
                request.AdvisoryBoardConditions,
                request.EstablishmentSharepointLink,
                request.IncomingTrustSharepointLink,
                groupId?.Value,
                team, 
                assignedAt, 
                assignedTo, 
                note, 
                request.RegionalDeliveryOfficer);
            
            await conversionTaskRepository.AddAsync(conversionTask, cancellationToken);
            await projectRepository.AddAsync(project, cancellationToken);

            return project.Id;
        }
    }
}