using System;
using RidingBikes.Common.Models;
using Xunit;

namespace RidingBikes.Common.Tests.Models;

public class GroupRideTests

{
    [Fact]
    public void CreateModelValidationWorks()
    {
        var createModel = new GroupRideCreateModel();
        var ride = new GroupRideModel();

        var ex = Assert.ThrowsAny<Exception>(() => createModel.IsValid());

        ex = Assert.ThrowsAny<Exception>(() => ride.Initialize(createModel));

        createModel.BikeRouteId = Guid.Empty;
        
        ex = Assert.ThrowsAny<Exception>(() => createModel.IsValid());

        createModel.BikeRouteId = Guid.NewGuid();
        createModel.DateTime = DateTime.Now;
        createModel.Location = "nowhere";
        createModel.RideType = RideType.B2A;
        createModel.IsValid();

        ride.Initialize(createModel);
        Assert.NotNull(ride);

        Assert.Equal("nowhere", ride.Location);

        var updateModel = new GroupRideUpdateModel();

        updateModel.IsValid();

        updateModel.Location = "somewhere";
        Assert.True(ride.Update(updateModel));
        Assert.Equal("somewhere", ride.Location);

    }
}
