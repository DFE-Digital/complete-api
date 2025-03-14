﻿using Dfe.Complete.Application.Projects.Models;
using Dfe.Complete.Constants;
using Dfe.Complete.Domain.Enums;
using Dfe.Complete.Pages.Projects.List;
using Dfe.Complete.Tests.Common.Customizations.Models;
using DfE.CoreLibs.Testing.AutoFixture.Attributes;

namespace Dfe.Complete.Tests.Pages.Projects.List;

public class AllProjectsModelTests
{
    [Theory]
    [CustomAutoData(typeof(ListAllProjectResultModelCustomization))]
    public void GetProjectSummaryUrl_ShouldReturnConversionTaskListUrl_WhenProjectTypeIsConversion(IFixture fixture)
    {
        // Arrange
        var project = fixture.Customize(new ListAllProjectResultModelCustomization
        {
            ProjectType = ProjectType.Conversion
        }).Create<ListAllProjectsResultModel>();

        string expectedUrl = string.Format(RouteConstants.ConversionProjectTaskList, project.ProjectId);

        // Act
        var result = AllProjectsModel.GetProjectSummaryUrl(project);

        // Assert
        Assert.Equal(expectedUrl, result);
    }

    [Theory]
    [CustomAutoData(typeof(ListAllProjectResultModelCustomization))]
    public void GetProjectSummaryUrl_ShouldReturnTransferTaskListUrl_WhenProjectTypeIsTransfer(IFixture fixture)
    {
        // Arrange
        var project = fixture.Customize(new ListAllProjectResultModelCustomization
        {
            ProjectType = ProjectType.Transfer
        }).Create<ListAllProjectsResultModel>();

        string expectedUrl = string.Format(RouteConstants.TransferProjectTaskList, project.ProjectId);

        // Act
        var result = AllProjectsModel.GetProjectSummaryUrl(project);

        // Assert
        Assert.Equal(expectedUrl, result);
    }
}