using MediatR;
using Dfe.Complete.Domain.ValueObjects;
using Dfe.Complete.Domain.Enums;
using Dfe.Complete.Domain.Interfaces.Repositories;
using Dfe.Complete.Domain.Entities.Projects;

namespace Dfe.Complete.Application.Projects.Commands.CreateProject
{
    public record CreateProjectCommand(Urn urn,
            DateTime createdAt,
            DateTime updatedAt,
            TaskType taskType,
            ProjectType projectType,
            Guid tasksDataId,
            DateTime significantDate,
            bool isSignificantDateProvisional,
            Ukprn incomingTrustUkprn,
            Region region,
            bool isDueTo2RI,
            bool hasAcademyOrderBeenIssued,
            DateTime advisoryBoardDate,
            string advisoryBoardConditions,
            string establishmentSharepointLink,
            string incomingTrustSharepointLink) : IRequest<ProjectId>;


    public class CreateProjectCommandHandler(ICompleteRepository<Project> projectRepository)
        : IRequestHandler<CreateProjectCommand, ProjectId>
    {
        public async Task<ProjectId> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = Project.Create(request.urn,
                                     request.createdAt,
                                     request.updatedAt,
                                     request.taskType,
                                     request.projectType,
                                     request.tasksDataId,
                                     request.significantDate,
                                     request.isSignificantDateProvisional,
                                     request.incomingTrustUkprn,
                                     request.region,
                                     request.isDueTo2RI,
                                     request.hasAcademyOrderBeenIssued,
                                     request.advisoryBoardDate,
                                     request.advisoryBoardConditions,
                                     request.establishmentSharepointLink,
                                     request.incomingTrustSharepointLink);


            await projectRepository.AddAsync(project, cancellationToken);

            return project.Id!;
        }
    }
}