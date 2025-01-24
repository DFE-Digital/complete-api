using Dfe.Complete.Domain.Entities;
using Dfe.Complete.Domain.Enums;
using Dfe.Complete.Domain.ValueObjects;

namespace Dfe.Complete.Application.Projects.Models
{
    public class ProjectDto
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

        public ProjectTeam? Team { get; set; }

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

        public  User? AssignedTo { get; set; }

        public  User? Caseworker { get; set; }

        public  ICollection<Contact> Contacts { get; set; } = new List<Contact>();

        public  ICollection<Note> Notes { get; set; } = new List<Note>();

        public  User? RegionalDeliveryOfficer { get; set; }

    }
}
