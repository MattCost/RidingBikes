using System;
using RidingBikes.Common.Models;
using RidingBikes.Common.Models.BikeRouteModels;
using Xunit;

namespace RidingBikes.Common.Tests.Models;

public class BikeRouteTests
{
    [Fact]
    public void CreateModelValidationWorks()
    {
        var createModel = new BikeRouteCreateModel();
        var route = new BikeRouteModel();

        var ex = Assert.ThrowsAny<Exception>(() => createModel.IsValid());

        ex = Assert.ThrowsAny<Exception>(() => route.Initialize(createModel));

        createModel.Distance = 42;
        createModel.RideWithGPSUrl = "helloWorld";
        createModel.IsValid();

        route.Initialize(createModel);
        Assert.NotNull(route);

        Assert.Equal(42, route.Distance);
        Assert.Equal("helloWorld", route.RideWithGPSUrl);

        var updateModel = new BikeRouteUpdateModel();

        updateModel.IsValid();

        updateModel.Distance = 24;
        Assert.True(route.Update(updateModel));
        Assert.Equal(24, route.Distance);
    }
}