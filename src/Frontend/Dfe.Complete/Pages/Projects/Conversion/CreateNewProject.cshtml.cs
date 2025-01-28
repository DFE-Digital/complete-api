using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dfe.Complete.Validators;
using Dfe.Complete.Services;
using System.ComponentModel.DataAnnotations;
using Dfe.Complete.Application.Projects.Commands.CreateProject;
using MediatR;
using Dfe.Complete.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;

namespace Dfe.Complete.Pages.Projects.Conversion
{
    [Authorize(policy: "CanCreateProjects")]
    public class CreateNewProjectModel(ISender sender, IErrorService errorService) : PageModel
    {
        [BindProperty]
        [Required]
        [Urn]
        [Display(Name = "Urn")]
        public string URN { get; set; }

        [BindProperty]
        [Ukprn]
        [Required]
        [Display(Name = "UKPRN")]
        public string UKPRN { get; set; }

        [BindProperty]
        [Display(Name = "Group Reference Number")]
        [GroupReferenceNumber]
        public string? GroupReferenceNumber { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Enter a date for the Advisory Board Date, like 1 4 2023")]
        [Display(Name = "Advisory Board Date")]
        public DateTime? AdvisoryBoardDate { get; set; }

        [BindProperty] 
        public string? AdvisoryBoardConditions { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Enter a date for the Provisional Conversion Date, like 1 4 2023")]
        [Display(Name = "Provisional Conversion Date")]
        public DateTime? ProvisionalConversionDate { get; set; }

        [BindProperty]
        [SharePointLink]
        [Required]
        [Display(Name = "School or academy SharePoint link")]
        public string SchoolSharePointLink { get; set; }

        [BindProperty]
        [SharePointLink]
        [Required]
        [Display(Name = "Incoming trust SharePoint link")]
        public string IncomingTrustSharePointLink { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "State if this project will be handed over to the Regional casework services team. Choose yes or no")]
        [Display(Name = "Is Handing To RCS")]
        public bool? IsHandingToRCS { get; set; }

        [BindProperty] 
        public string? HandoverComments { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Select directive academy order or academy order, whichever has been used for this conversion")]
        [Display(Name = "Directive Academy Order")]
        public bool? DirectiveAcademyOrder { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "State if the conversion is due to 2RI. Choose yes or no")]
        [Display(Name = "IsDueTo2RI")]
        public bool? IsDueTo2RI { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                errorService.AddErrors(ModelState);
                return Page();
            }

            var userAdId = User.Claims.SingleOrDefault(c => c.Type.Contains("objectidentifier"))?.Value;
            
            var createProjectCommand = new CreateConversionProjectCommand(
                Urn: new Urn(int.Parse(URN)),
                SignificantDate: ProvisionalConversionDate.HasValue ? DateOnly.FromDateTime(ProvisionalConversionDate.Value) : default,
                IsSignificantDateProvisional: true, // will be set to false in the stakeholder kick off task 
                IncomingTrustSharepointLink: IncomingTrustSharePointLink,
                EstablishmentSharepointLink: SchoolSharePointLink, 
                IsDueTo2Ri: IsDueTo2RI ?? false,
                AdvisoryBoardDate: AdvisoryBoardDate.HasValue ? DateOnly.FromDateTime(AdvisoryBoardDate.Value) : default,
                AdvisoryBoardConditions: AdvisoryBoardConditions ?? string.Empty,
                IncomingTrustUkprn: new Ukprn(int.Parse(UKPRN)),
                HasAcademyOrderBeenIssued: DirectiveAcademyOrder ?? default, 
                GroupReferenceNumber: GroupReferenceNumber ?? string.Empty,
                HandingOverToRegionalCaseworkService: IsHandingToRCS ?? default,
                HandoverComments: HandoverComments ?? string.Empty, 
                UserAdId: userAdId
            );

            var createResponse = await sender.Send(createProjectCommand, cancellationToken);

            var projectId = createResponse.Value;

            return Redirect($"/projects/conversion-projects/{projectId}/created");
        }
    }
}