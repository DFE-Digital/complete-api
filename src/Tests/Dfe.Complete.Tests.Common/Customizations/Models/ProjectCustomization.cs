using AutoFixture;
using Dfe.Complete.Domain.Entities;
using Dfe.Complete.Domain.Enums;
using Dfe.Complete.Domain.ValueObjects;

namespace Dfe.Complete.Tests.Common.Customizations.Models
{
    public class ProjectCustomization : ICustomization
    {
        public ProjectId Id { get; set; }

        public Urn Urn { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Ukprn? IncomingTrustUkprn { get; set; }

        public UserId? RegionalDeliveryOfficerId { get; set; }

        public UserId? CaseworkerId { get; set; }

        public DateTime? AssignedAt { get; set; }

        public DateOnly? AdvisoryBoardDate { get; set; }

        public string? AdvisoryBoardConditions { get; set; }

        public string? EstablishmentSharepointLink { get; set; }

        public DateTime? CompletedAt { get; set; }

        public string? IncomingTrustSharepointLink { get; set; }

        public ProjectType? Type { get; set; }

        public UserId? AssignedToId { get; set; }

        public DateOnly? SignificantDate { get; set; }

        public bool? SignificantDateProvisional { get; set; }

        public bool? DirectiveAcademyOrder { get; set; }

        public Region? Region { get; set; }

        public Urn? AcademyUrn { get; set; }

        public Guid? TasksDataId { get; set; }

        public TaskType? TasksDataType { get; set; }

        public Ukprn? OutgoingTrustUkprn { get; set; }

        public string? Team { get; set; }

        public bool? TwoRequiresImprovement { get; set; }

        public string? OutgoingTrustSharepointLink { get; set; }

        public bool? AllConditionsMet { get; set; }

        public ContactId? MainContactId { get; set; }

        public ContactId? EstablishmentMainContactId { get; set; }

        public ContactId? IncomingTrustMainContactId { get; set; }

        public ContactId? OutgoingTrustMainContactId { get; set; }

        public string? NewTrustReferenceNumber { get; set; }

        public string? NewTrustName { get; set; }

        public int State { get; set; }

        public int? PrepareId { get; set; }

        public ContactId? LocalAuthorityMainContactId { get; set; }

        public Guid? GroupId { get; set; }

        public void Customize(IFixture fixture)
        {
            fixture.Customize<Project>(composer => composer
                .With(x => x.IncomingTrustUkprn, IncomingTrustUkprn ?? fixture.Create<Ukprn>())
                .With(x => x.RegionalDeliveryOfficerId, RegionalDeliveryOfficerId ?? fixture.Create<UserId>())
                .With(x => x.CaseworkerId, CaseworkerId ?? fixture.Create<UserId>())
                .With(x => x.AdvisoryBoardConditions, AdvisoryBoardConditions ?? fixture.Create<string>())
                .With(x => x.EstablishmentSharepointLink, EstablishmentSharepointLink ?? fixture.Create<string>())
                .With(x => x.IncomingTrustSharepointLink, IncomingTrustSharepointLink ?? fixture.Create<string>())
                .With(x => x.Type, Type ?? fixture.Create<ProjectType>())
                .With(x => x.AssignedToId, AssignedToId ?? fixture.Create<UserId>())
                .With(x => x.SignificantDateProvisional, SignificantDateProvisional ?? fixture.Create<bool>())
                .With(x => x.DirectiveAcademyOrder, DirectiveAcademyOrder ?? fixture.Create<bool>())
                .With(x => x.Region, Region ?? fixture.Create<Region>())
                .With(x => x.AcademyUrn, AcademyUrn ?? fixture.Create<Urn>())
                .With(x => x.TasksDataId, TasksDataId ?? fixture.Create<Guid>())
                .With(x => x.TasksDataType, TasksDataType ?? fixture.Create<TaskType>())
                .With(x => x.OutgoingTrustUkprn, OutgoingTrustUkprn ?? fixture.Create<Ukprn>())
                .With(x => x.Team, Team ?? fixture.Create<string>())
                .With(x => x.TwoRequiresImprovement, TwoRequiresImprovement ?? fixture.Create<bool>())
                .With(x => x.OutgoingTrustSharepointLink, OutgoingTrustSharepointLink ?? fixture.Create<string>())
                .With(x => x.AllConditionsMet, AllConditionsMet ?? fixture.Create<bool>())
                .With(x => x.MainContactId, MainContactId ?? fixture.Create<ContactId>())
                .With(x => x.EstablishmentMainContactId, EstablishmentMainContactId ?? fixture.Create<ContactId>())
                .With(x => x.IncomingTrustMainContactId, IncomingTrustMainContactId ?? fixture.Create<ContactId>())
                .With(x => x.OutgoingTrustMainContactId, OutgoingTrustMainContactId ?? fixture.Create<ContactId>())
                .With(x => x.NewTrustReferenceNumber, NewTrustReferenceNumber ?? fixture.Create<string>())
                .With(x => x.NewTrustName, NewTrustName ?? fixture.Create<string>()));
        }
    }
}
