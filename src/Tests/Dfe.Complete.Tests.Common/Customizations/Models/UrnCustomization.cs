﻿using AutoFixture;
using Dfe.Complete.Domain.ValueObjects;
using Dfe.Complete.Tests.Common.Extensions;

namespace Dfe.Complete.Tests.Common.Customizations.Models
{
    public class UrnCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register<Urn>(() => new Urn(fixture.CreateInt(10000, 99999)));
            //fixture.Customize<Urn>(composer => composer.FromFactory(() => fixture.CreateUrn()));
        }
    }
}
