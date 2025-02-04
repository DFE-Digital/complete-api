﻿using Dfe.Complete.Application.Common.Exceptions;
using Dfe.Complete.Application.Common.Models;
using Dfe.Complete.Domain.Entities;
using Dfe.Complete.Domain.Interfaces.Repositories;
using Dfe.Complete.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Dfe.Complete.Application.Projects.Commands.RemoveProject
{
    public record RemoveProjectCommand(
       Urn Urn) : IRequest;

    public class RemoveProjectCommandHandler(IHostEnvironment hostEnvironment,
                                             ICompleteRepository<Project> projectRepository,
                                             ICompleteRepository<TransferTasksData> transferTaskRepository,
                                             ICompleteRepository<ConversionTasksData> conversionTaskRepository)
        : IRequestHandler<RemoveProjectCommand>
    {
        public async Task Handle(RemoveProjectCommand request, CancellationToken cancellationToken)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                throw new NotDevEnvironmentException();
            }

            var project = await projectRepository.FindAsync(x => x.Urn == request.Urn);

            if(project.TasksDataType == Domain.Enums.TaskType.Conversion)
            {
                await conversionTaskRepository.RemoveAsync(conversionTaskRepository.Get(new TaskDataId(project.TasksDataId.Value)));
            }
            else
            {
                await transferTaskRepository.RemoveAsync(transferTaskRepository.Get(new TaskDataId(project.TasksDataId.Value)));
            }

            await projectRepository.RemoveAsync(project);
        }
    }
}
