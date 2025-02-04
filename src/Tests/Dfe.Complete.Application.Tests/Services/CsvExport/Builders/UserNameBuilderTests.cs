﻿using AutoFixture;
using AutoFixture.Xunit2;
using Dfe.Complete.Application.Services.CsvExport.Builders;
using Dfe.Complete.Domain.Entities;
using Dfe.Complete.Domain.Enums;
using Dfe.Complete.Tests.Common.Customizations.Models;
using DfE.CoreLibs.Testing.AutoFixture.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.Complete.Application.Tests.Services.CsvExport.Builders
{
    public class UserNameBuilderTests
    {
        [Fact]
        public void ReturnsBlankIfNull()
        {            
            var builder = new UserNameBuilder<User>(x => x);

            var result = builder.Build(null);

            Assert.Equal(string.Empty, result);
        }

        [Theory]
        [CustomAutoData(typeof(UserCustomization))]
        public void ReturnsName(User user)
        {
            var builder = new UserNameBuilder<User>(x => x);

            var result = builder.Build(user);

            Assert.Equal($"{user.FirstName} {user.LastName}", result);
        }
    }
}
