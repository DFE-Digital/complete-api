using AutoFixture.Xunit2;
using DfE.CoreLibs.Testing.AutoFixture.Attributes;
using Dfe.Complete.Domain.Interfaces.Repositories;
using NSubstitute;
using Dfe.Complete.Application.Projects.Commands.CreateProject;
using Dfe.Complete.Domain.Entities;
using Dfe.Complete.Domain.Enums;
using Dfe.Complete.Domain.ValueObjects;
using Dfe.Complete.Tests.Common.Customizations.Behaviours;
using Dfe.Complete.Tests.Common.Customizations.Models;
using Dfe.Complete.Utils;
using DfE.CoreLibs.Testing.AutoFixture.Customizations;
using Dfe.Complete.Application.Projects.Models;
using MediatR;
using Dfe.Complete.Application.Common.Models;
using Moq;
using Dfe.Complete.Application.Projects.Queries.GetUser;
using Dfe.Complete.Application.Projects.Queries.GetProject;

namespace Dfe.Complete.Application.Tests.CommandHandlers.Project;

public class CreateConversionProjectCommandHandlerTests
{
    [Theory]
    [CustomAutoData(typeof(DateOnlyCustomization),
        typeof(IgnoreVirtualMembersCustomisation))]
    public async Task Handle_ShouldCreateAndReturnProjectId_WhenCommandIsValid(
        [Frozen] ICompleteRepository<Domain.Entities.Project> mockProjectRepository,
        [Frozen] ICompleteRepository<ConversionTasksData> mockConversionTaskRepository,
        [Frozen] Mock<ISender> mockSender,
        CreateConversionProjectCommand command
    )
    {
        // Arrange
        var handler = new CreateConversionProjectCommandHandler(mockProjectRepository, mockConversionTaskRepository, mockSender.Object);

        const ProjectTeam team = ProjectTeam.WestMidlands;
        var userDto = new UserDto
        {
            Id = new UserId(Guid.NewGuid()),
            Team = team.ToDescription()
        };

        var createdAt = DateTime.UtcNow;
        var conversionTaskId = Guid.NewGuid();
        var conversionTask = new ConversionTasksData(new TaskDataId(conversionTaskId), createdAt, createdAt);

        var groupId = new ProjectGroupId(Guid.NewGuid());

        mockSender
                .Setup(sender => sender.Send(It.IsAny<GetUserByAdIdQuery>(), default))
                .ReturnsAsync(Result<UserDto?>.Success(userDto));

        mockSender.Setup(s => s.Send(It.IsAny<GetProjectGroupByGroupReferenceNumberQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(Result<ProjectGroupDto>.Success(new ProjectGroupDto { Id = groupId }));


        Domain.Entities.Project capturedProject = null!;
        
        mockProjectRepository.AddAsync(Arg.Do<Domain.Entities.Project>(proj => capturedProject = proj), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(capturedProject));

        mockProjectRepository.AddAsync(capturedProject, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(capturedProject));
        
        mockConversionTaskRepository.AddAsync(Arg.Any<ConversionTasksData>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(conversionTask));

        // Act
        var projectId = await handler.Handle(command, default);

        Assert.NotNull(projectId);
        Assert.IsType<ProjectId>(projectId);

        await mockProjectRepository.Received(1).AddAsync(capturedProject);
        await mockConversionTaskRepository.Received(1).AddAsync(Arg.Any<ConversionTasksData>());

        Assert.Equal(command.Urn, capturedProject.Urn);
        Assert.Equal(command.SignificantDate, capturedProject.SignificantDate);
        Assert.Equal(command.IsSignificantDateProvisional, capturedProject.SignificantDateProvisional);
        Assert.Equal(command.IncomingTrustUkprn, capturedProject.IncomingTrustUkprn);
        Assert.Equal(command.IsDueTo2Ri, capturedProject.TwoRequiresImprovement);
        Assert.Equal(command.HasAcademyOrderBeenIssued, capturedProject.DirectiveAcademyOrder);
        Assert.Equal(command.EstablishmentSharepointLink, capturedProject.EstablishmentSharepointLink);
        Assert.Equal(command.IncomingTrustSharepointLink, capturedProject.IncomingTrustSharepointLink);
        Assert.Equal(groupId, capturedProject.GroupId);

        var capturedNote = capturedProject.Notes.FirstOrDefault();
        Assert.Equal(command.HandoverComments, capturedNote?.Body);
        Assert.Equal("handover", capturedNote?.TaskIdentifier);
    }
    
    [Theory]
    [CustomAutoData(typeof(DateOnlyCustomization), typeof(ProjectCustomization), typeof(IgnoreVirtualMembersCustomisation))]
    public async Task Handle_ShouldSetTeamToRcs_WhenHandoverToRcsTrue(
        [Frozen] ICompleteRepository<Domain.Entities.Project> mockProjectRepository,
        [Frozen] ICompleteRepository<ConversionTasksData> mockConversionTaskRepository,
        [Frozen] Mock<ISender> mockSender,
        CreateConversionProjectCommand command
    )
    {
        // Arrange
        var handler = new CreateConversionProjectCommandHandler(mockProjectRepository, mockConversionTaskRepository, mockSender.Object);

        command = command with { HandingOverToRegionalCaseworkService = true };

        const ProjectTeam team = ProjectTeam.WestMidlands;
        var userDto = new UserDto
        {
            Id = new UserId(Guid.NewGuid()),
            Team = team.ToDescription()
        };

        var createdAt = DateTime.UtcNow;
        var conversionTaskId = Guid.NewGuid();
        var conversionTask = new ConversionTasksData(new TaskDataId(conversionTaskId), createdAt, createdAt);
        var groupId = new ProjectGroupId(Guid.NewGuid());


        mockSender.Setup(s => s.Send(It.IsAny<GetUserByAdIdQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(Result<UserDto>.Success(userDto));

        mockSender.Setup(s => s.Send(It.IsAny<GetProjectGroupByGroupReferenceNumberQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(Result<ProjectGroupDto>.Success(new ProjectGroupDto { Id = groupId }));

        Domain.Entities.Project capturedProject = null!;
        
        mockProjectRepository.AddAsync(Arg.Do<Domain.Entities.Project>(proj => capturedProject = proj), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(capturedProject));
        
        mockConversionTaskRepository.AddAsync(Arg.Any<ConversionTasksData>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(conversionTask));

        // Act
        var projectId = await handler.Handle(command, default);

        // Assert
        Assert.NotNull(projectId);
        Assert.Equal(ProjectTeam.RegionalCaseWorkerServices, capturedProject.Team);
        Assert.Null(capturedProject.AssignedAt);
        Assert.Null(capturedProject.AssignedToId);
    }
    
     
    [Theory]
    [CustomAutoData(typeof(DateOnlyCustomization), typeof(ProjectCustomization), typeof(IgnoreVirtualMembersCustomisation))]
    public async Task Handle_ShouldSetTeam_AssignedAt_AssignedTo_WhenNOTHandingOverToRcs(
        [Frozen] ICompleteRepository<Domain.Entities.Project> mockProjectRepository,
        [Frozen] ICompleteRepository<ConversionTasksData> mockConversionTaskRepository,
        [Frozen] Mock<ISender> mockSender,
        CreateConversionProjectCommand command
    )
    {
        // Arrange
        var handler = new CreateConversionProjectCommandHandler(mockProjectRepository, mockConversionTaskRepository, mockSender.Object);

        command = command with { HandingOverToRegionalCaseworkService = false };

        const ProjectTeam team = ProjectTeam.WestMidlands;
        var userDto = new UserDto
        {
            Id = new UserId(Guid.NewGuid()),
            Team = team.ToDescription()
        };

        var createdAt = DateTime.UtcNow;
        var conversionTaskId = Guid.NewGuid();
        var conversionTask = new ConversionTasksData(new TaskDataId(conversionTaskId), createdAt, createdAt);

        var groupId = new ProjectGroupId(Guid.NewGuid());

        mockSender.Setup(s => s.Send(It.IsAny<GetUserByAdIdQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(Result<UserDto>.Success(userDto));

        mockSender.Setup(s => s.Send(It.IsAny<GetProjectGroupByGroupReferenceNumberQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(Result<ProjectGroupDto>.Success(new ProjectGroupDto { Id = groupId }));

        Domain.Entities.Project capturedProject = null!;
        
        mockProjectRepository.AddAsync(Arg.Do<Domain.Entities.Project>(proj => capturedProject = proj), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(capturedProject));

        mockProjectRepository.AddAsync(capturedProject, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(capturedProject));
        
        mockConversionTaskRepository.AddAsync(Arg.Any<ConversionTasksData>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(conversionTask));

        // Act
        var projectId = await handler.Handle(command, default);

        // Assert
        Assert.NotNull(projectId);
        Assert.Equal(team, capturedProject.Team);
        Assert.NotNull(capturedProject.AssignedAt);
        Assert.NotNull(capturedProject.AssignedToId);
    }

    [Theory]
    [CustomAutoData(typeof(DateOnlyCustomization), typeof(ProjectCustomization), typeof(IgnoreVirtualMembersCustomisation))]
    public async Task Handle_ShouldThrowExceptionWhenUserRequestFails(
    [Frozen] ICompleteRepository<Domain.Entities.Project> mockProjectRepository,
    [Frozen] ICompleteRepository<ConversionTasksData> mockConversionTaskRepository,
    [Frozen] Mock<ISender> mockSender,
    CreateConversionProjectCommand command)
    {
        // Arrange
        var handler = new CreateConversionProjectCommandHandler(mockProjectRepository, mockConversionTaskRepository, mockSender.Object);
        var expectedErrorMessage = "User retrieval failed: DB ERROR";


        mockSender.Setup(s => s.Send(It.IsAny<GetUserByAdIdQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(Result<UserDto>.Failure("DB ERROR"));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, default));


        await mockProjectRepository.Received(0).AddAsync(Arg.Any<Domain.Entities.Project>());
        await mockConversionTaskRepository.Received(0).AddAsync(Arg.Any<ConversionTasksData>());

        Assert.Equal(exception.Message, expectedErrorMessage);
    }

    [Theory]
    [CustomAutoData(typeof(DateOnlyCustomization), typeof(ProjectCustomization), typeof(IgnoreVirtualMembersCustomisation))]
    public async Task Handle_ShouldThrowExceptionWhenProjectGroupRequestFails(
       [Frozen] ICompleteRepository<Domain.Entities.Project> mockProjectRepository,
       [Frozen] ICompleteRepository<ConversionTasksData> mockConversionTaskRepository,
       [Frozen] Mock<ISender> mockSender,
       CreateConversionProjectCommand command,
       UserDto userDto)
        {
            // Arrange
            var handler = new CreateConversionProjectCommandHandler(mockProjectRepository, mockConversionTaskRepository, mockSender.Object);

            userDto.Team = "regional_casework_services";


            var expectedErrorMessage = "Project Group retrieval failed: DB ERROR";

            mockSender.Setup(s => s.Send(It.IsAny<GetUserByAdIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(Result<UserDto?>.Success(userDto));

            mockSender.Setup(s => s.Send(It.IsAny<GetProjectGroupByGroupReferenceNumberQuery>(), It.IsAny<CancellationToken>()))
                            .ReturnsAsync(Result<ProjectGroupDto?>.Failure("DB ERROR"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, default));

            await mockProjectRepository.Received(0).AddAsync(Arg.Any<Domain.Entities.Project>());
            await mockConversionTaskRepository.Received(0).AddAsync(Arg.Any<ConversionTasksData>());

            Assert.Equal(exception.Message, expectedErrorMessage);
        }


    //[Theory]
    //[CustomAutoData(typeof(DateOnlyCustomization), typeof(ProjectCustomization), typeof(IgnoreVirtualMembersCustomisation))]
    //public async Task Handle_ShouldThrowExceptionWhenProjectGroupReposirotyErrors(
    //   [Frozen] ICompleteRepository<Domain.Entities.Project> mockProjectRepository,
    //   [Frozen] ICompleteRepository<ConversionTasksData> mockConversionTaskRepository,
    //   [Frozen] Mock<ISender> mockSender,
    //   CreateConversionProjectCommand command,
    //   UserDto userDto)
    //    {
    //        // Arrange
    //        var handler = new CreateConversionProjectCommandHandler(mockProjectRepository, mockConversionTaskRepository, mockSender.Object);

    //        userDto.Team = "regional_casework_services";


    //        var expectedErrorMessage = "Error";

    //        mockSender.Setup(s => s.Send(It.IsAny<GetUserByAdIdQuery>(), It.IsAny<CancellationToken>()))
    //                    .ReturnsAsync(Result<UserDto?>.Success(userDto));

    //        mockSender.Setup(s => s.Send(It.IsAny<GetProjectGroupByGroupReferenceNumberQuery>(), It.IsAny<CancellationToken>()))
    //                        .ThrowsAsync(new Exception(expectedErrorMessage));

    //        // Act & Assert
    //        var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, default));

    //        await mockProjectRepository.Received(0).AddAsync(Arg.Any<Domain.Entities.Project>());
    //        await mockConversionTaskRepository.Received(0).AddAsync(Arg.Any<ConversionTasksData>());

    //        Assert.Equal(exception.Message, expectedErrorMessage);
    //    }
}