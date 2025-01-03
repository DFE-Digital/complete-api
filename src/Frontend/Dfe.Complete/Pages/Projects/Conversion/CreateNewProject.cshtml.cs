using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;
using Dfe.Complete.Extensions;
using Dfe.Complete.Validators;
using Dfe.Complete.Services;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Dfe.Complete.Application.Projects.Commands.CreateProject;
using MediatR;
using Dfe.Complete.Domain.ValueObjects;

namespace Dfe.Complete.Pages.Projects.Conversion
{
    public class CreateNewProjectModel(ISender sender, IErrorService errorService) : PageModel
    {
        [BindProperty]
        [Required]
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
        public string GroupReferenceNumber { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Enter a date for the Advisory Board Date, like 1 4 2023")]
        [Display(Name = "Advisory Board Date")]
        public DateTime AdvisoryBoardDate { get; set; }

        [BindProperty] 
        public string AdvisoryBoardConditions { get; set; }

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
        public string HandoverComments { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Select directive academy order or academy order, whichever has been used for this conversion")]
        [Display(Name = "Directive Academy Order")]
        public bool? DirectiveAcademyOrder { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "State if the conversion is due to 2RI. Choose yes or no")]
        [Display(Name = "IsDueTo2RI")]
        public bool? IsDueTo2RI { get; set; }

        public async Task<IActionResult> OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            ManuallyValidateGroupReferenceNumber();

            //Validate
            await ValidateAllFields();

            if (!ModelState.IsValid)
            {
                errorService.AddErrors(ModelState);
                return Page();
            }

            var createProjectCommand = new CreateConversionProjectCommand(
                Urn: new Urn(int.Parse(URN)),
                SignificantDate: DateOnly.FromDateTime(DateTime.UtcNow),
                IsSignificantDateProvisional: true, // will be set to false in the stakeholder kick off task 
                IncomingTrustSharepointLink: IncomingTrustSharePointLink,
                EstablishmentSharepointLink: SchoolSharePointLink, //todo: is this correct?
                IsDueTo2Ri: IsDueTo2RI ?? false,
                AdvisoryBoardDate: AdvisoryBoardDate.HasValue ? DateOnly.FromDateTime(AdvisoryBoardDate.Value) : default,
                Region: Domain.Enums.Region.NorthWest,
                AdvisoryBoardConditions: AdvisoryBoardConditions,
                IncomingTrustUkprn: new Ukprn(int.Parse(UKPRN)),
                HasAcademyOrderBeenIssued: DirectiveAcademyOrder ?? default, 
                GroupReferenceNumber: GroupReferenceNumber,
                ProvisionalConversionDate: ProvisionalConversionDate.HasValue ? DateOnly.FromDateTime(ProvisionalConversionDate.Value) : default,
                HandoverComments: HandoverComments, 
                HandingOverToRegionalCaseworkService: IsHandingToRCS ?? default
            );

            var createResponse = await sender.Send(createProjectCommand);

            var projectId = createResponse.Value;

            return Redirect($"/projects/conversion-projects/{projectId}/created");
        }

        private void ManuallyValidateGroupReferenceNumber()
        {
            //This is a workaround for this field being required by default.
            ModelState.Remove(nameof(GroupReferenceNumber));
            new GroupReferenceNumberAttribute().Validate(GroupReferenceNumber, new ValidationContext(this));
        }

        public async Task ValidateAllFields()
        {
            await ValidateUrn();
        }

        private async Task ValidateUrn()
        {
            var fieldName = nameof(URN);
            var value = URN;

            if (string.IsNullOrEmpty(value))
            {
                ModelState.AddModelError($"{fieldName}", "Enter a school URN");
                return;
            }

            string pattern = "^[0-9]{6}$";

            var isAMatch = Regex.IsMatch(value, pattern);

            if (!isAMatch)
            {
                ModelState.AddModelError($"{fieldName}", "must be 6 digits");
                return;
            }

            var parsedUrn = value.ToInt();

            // var existingProject = await _getConversionProjectService.GetConversionProjectByUrn(parsedUrn);
            //
            // if (existingProject != null)
            // {
            //     ModelState.AddModelError($"{fieldName}", "project with this URN already exists");
            // }
        }

    }
}