using DfE.CoreLibs.Testing.AutoFixture.Attributes;
using Dfe.Complete.Tests.Common.Customizations.Models;
using DfE.CoreLibs.Testing.AutoFixture.Customizations;
using Project = Dfe.Complete.Domain.Entities.Project;
using ProjectId = Dfe.Complete.Domain.ValueObjects.ProjectId;
using ProjectTeam = Dfe.Complete.Domain.Enums.ProjectTeam;
using ProjectType = Dfe.Complete.Domain.Enums.ProjectType;
using Region = Dfe.Complete.Domain.Enums.Region;
using TaskType = Dfe.Complete.Domain.Enums.TaskType;
using Ukprn = Dfe.Complete.Domain.ValueObjects.Ukprn;
using Urn = Dfe.Complete.Domain.ValueObjects.Urn;
using UserId = Dfe.Complete.Domain.ValueObjects.UserId;
using ProjectGroupId = Dfe.Complete.Domain.ValueObjects.ProjectGroupId;
using Dfe.Complete.Domain.ValueObjects;

namespace Dfe.Complete.Domain.Tests.Aggregates
{
    public class ProjectTests
    {
        [Theory]
        [CustomAutoData(typeof(ProjectCustomization), typeof(DateOnlyCustomization))]
        public void Constructor_ShouldThrowArgumentNullException_WhenProjectUrnIsNull(
            ProjectId id,
            DateTime createdAt,
            DateTime updatedAt,
            TaskType taskType,
            ProjectType projectType,
            Guid tasksDataId,
            DateOnly significantDate,
            bool isSignificantDateProvisional,
            Ukprn incomingTrustUkprn,
            Region region,
            bool isDueTo2RI,
            bool hasAcademyOrderBeenIssued,
            DateOnly advisoryBoardDate,
            string advisoryBoardConditions,
            string establishmentSharepointLink,
            string incomingTrustSharepointLink,
            ProjectGroupId groupId,
            ProjectTeam team,
            UserId regionalDeliveryOfficerId,
            UserId assignedToId,
            DateTime? assignedAt
        )
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new Project(
                            id,
                           null,
                           createdAt,
                           updatedAt,
                           taskType,
                           projectType,
                           tasksDataId,
                           significantDate,
                           isSignificantDateProvisional,
                           incomingTrustUkprn,
                           null,
                           region,
                           isDueTo2RI,
                           hasAcademyOrderBeenIssued,
                           advisoryBoardDate,
                           advisoryBoardConditions,
                           establishmentSharepointLink,
                           incomingTrustSharepointLink,
                           null,
                           groupId,
                           team,
                           regionalDeliveryOfficerId,
                           assignedToId,
                           assignedAt));




            Assert.Equal("urn", exception.ParamName);
        }

        [Theory]
        [CustomAutoData(typeof(ProjectCustomization), typeof(DateOnlyCustomization))]
        public void Constructor_ShouldThrowArgumentNullException_WhenProjectCreatedAtIsDefault(
            ProjectId id,
            Urn urn,
            DateTime updatedAt,
            TaskType taskType,
            ProjectType projectType,
            Guid tasksDataId,
            DateOnly significantDate,
            bool isSignificantDateProvisional,
            Ukprn incomingTrustUkprn,
            Region region,
            bool isDueTo2RI,
            bool hasAcademyOrderBeenIssued,
            DateOnly advisoryBoardDate,
            string advisoryBoardConditions,
            string establishmentSharepointLink,
            string incomingTrustSharepointLink,
            ProjectGroupId groupId,
            ProjectTeam team,
            UserId regionalDeliveryOfficerId,
            UserId assignedToId,
            DateTime? assignedAt
        )
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new Project(
                            id,
                           urn,
                           default,
                           updatedAt,
                           taskType,
                           projectType,
                           tasksDataId,
                           significantDate,
                           isSignificantDateProvisional,
                           incomingTrustUkprn,
                           null,
                           region,
                           isDueTo2RI,
                           hasAcademyOrderBeenIssued,
                           advisoryBoardDate,
                           advisoryBoardConditions,
                           establishmentSharepointLink,
                           incomingTrustSharepointLink,
                           null,
                           groupId,
                           team,
                           regionalDeliveryOfficerId,
                           assignedToId,
                           assignedAt));

            Assert.Equal("createdAt", exception.ParamName);
        }

        [Theory]
        [CustomAutoData(typeof(ProjectCustomization), typeof(DateOnlyCustomization))]
        public void Constructor_ShouldThrowArgumentNullException_WhenProjectUpdatedAtIsDefault(
            ProjectId id,
            Urn urn,
            DateTime createdAt,
            TaskType taskType,
            ProjectType projectType,
            Guid tasksDataId,
            DateOnly significantDate,
            bool isSignificantDateProvisional,
            Ukprn incomingTrustUkprn,
            Region region,
            bool isDueTo2RI,
            bool hasAcademyOrderBeenIssued,
            DateOnly advisoryBoardDate,
            string advisoryBoardConditions,
            string establishmentSharepointLink,
            string incomingTrustSharepointLink,
            ProjectGroupId groupId,
            ProjectTeam team,
            UserId regionalDeliveryOfficerId,
            UserId assignedToId,
            DateTime? assignedAt
        )
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new Project(
                            id,
                           urn,
                           createdAt,
                           default,
                           taskType,
                           projectType,
                           tasksDataId,
                           significantDate,
                           isSignificantDateProvisional,
                           incomingTrustUkprn,
                           null,
                           region,
                           isDueTo2RI,
                           hasAcademyOrderBeenIssued,
                           advisoryBoardDate,
                           advisoryBoardConditions,
                           establishmentSharepointLink,
                           incomingTrustSharepointLink,
                           null,
                           groupId,
                           team,
                           regionalDeliveryOfficerId,
                           assignedToId,
                           assignedAt));

            Assert.Equal("updatedAt", exception.ParamName);
        }


        [Theory]
        [CustomAutoData(typeof(ProjectCustomization), typeof(DateOnlyCustomization))]
        public void Constructor_ShouldCorrectlySetFields(
            ProjectId id,
            Urn urn,
            DateTime createdAt,
            DateTime updatedAt,
            TaskType taskType,
            ProjectType projectType,
            Guid tasksDataId,
            DateOnly significantDate,
            bool isSignificantDateProvisional,
            Ukprn incomingTrustUkprn,
            Region region,
            bool isDueTo2RI,
            bool hasAcademyOrderBeenIssued,
            DateOnly advisoryBoardDate,
            string advisoryBoardConditions,
            string establishmentSharepointLink,
            string incomingTrustSharepointLink,
            ProjectGroupId? groupId,
            ProjectTeam team,
            DateTime? assignedAt,
            UserId? assignedToId,
            UserId? regionalDeliveryOfficerId
        )
        {
            // Act & Assert
            var project = new Project(
                            id,
                           urn,
                           createdAt,
                           updatedAt,
                           taskType,
                           projectType,
                           tasksDataId,
                           significantDate,
                           isSignificantDateProvisional,
                           incomingTrustUkprn,
                           null,
                           region,
                           isDueTo2RI,
                           hasAcademyOrderBeenIssued,
                           advisoryBoardDate,
                           advisoryBoardConditions,
                           establishmentSharepointLink,
                           incomingTrustSharepointLink,
                           null,
                           groupId,
                           team,
                           regionalDeliveryOfficerId,
                           assignedToId,
                           assignedAt);

            Assert.Equal(urn, project.Urn);
        }

        [Theory]
        [CustomAutoData(typeof(ProjectCustomization), typeof(DateOnlyCustomization))]
        public void Factory_Create_ShouldCorrectlySetFields(
            ProjectId id,
            Urn urn,
            DateTime createdAt,
            DateTime updatedAt,
            TaskType taskType,
            ProjectType projectType,
            Guid tasksDataId,
            DateOnly significantDate,
            bool isSignificantDateProvisional,
            Ukprn incomingTrustUkprn,
            Region region,
            bool isDueTo2RI,
            bool hasAcademyOrderBeenIssued,
            DateOnly advisoryBoardDate,
            string advisoryBoardConditions,
            string establishmentSharepointLink,
            string incomingTrustSharepointLink,
            DateOnly provisionalConversionDate,
            bool handingOverToRegionalCaseworkService,
            ProjectGroupId? groupId,
            ProjectTeam team,
            DateTime? assignedAt,
            UserId? assignedToId,
            UserId? regionalDeliveryOfficer)
        {
            // Act & Assert
            var handoverComment = "handover comment";
            
            var project = Project.CreateConversionProject(
                id,
                urn,
                createdAt,
                updatedAt,
                taskType,
                projectType,
                tasksDataId,
                significantDate,
                isSignificantDateProvisional,
                incomingTrustUkprn,
                region,
                isDueTo2RI,
                hasAcademyOrderBeenIssued,
                advisoryBoardDate,
                advisoryBoardConditions,
                establishmentSharepointLink,
                incomingTrustSharepointLink,
                groupId,
                team,
                regionalDeliveryOfficer,
                assignedToId,
                assignedAt,
                handoverComment);

            Assert.Equal(urn, project.Urn);
            Assert.Equal(createdAt, project.CreatedAt);
            Assert.Equal(updatedAt, project.UpdatedAt);
            Assert.Equal(taskType, project.TasksDataType);
            Assert.Equal(projectType, project.Type);
            Assert.Equal(new TaskDataId(tasksDataId), project.TasksDataId);
            Assert.Equal(significantDate, project.SignificantDate);
            Assert.Equal(isSignificantDateProvisional, project.SignificantDateProvisional);
            Assert.Equal(incomingTrustUkprn, project.IncomingTrustUkprn);
            Assert.Equal(region, project.Region);
            Assert.Equal(isDueTo2RI, project.TwoRequiresImprovement);
            Assert.Equal(hasAcademyOrderBeenIssued, project.DirectiveAcademyOrder);
            Assert.Equal(advisoryBoardDate, project.AdvisoryBoardDate);
            Assert.Equal(advisoryBoardConditions, project.AdvisoryBoardConditions);
            Assert.Equal(establishmentSharepointLink, project.EstablishmentSharepointLink);
            Assert.Equal(incomingTrustSharepointLink, project.IncomingTrustSharepointLink);
            Assert.Equal(handoverComment, project.Notes.FirstOrDefault()?.Body);
        }
    }
}