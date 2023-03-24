using System;
using RidingBikes.Common.Models.PersonModels;
using Xunit;

namespace RidingBikes.Common.Tests.Models;

public class PersonModelTests
{
    [Fact]
    public void CreateModelValidationWorks()
    {
        var createModel = new PersonCreateModel();
        var person = new PersonModel();

        var ex = Assert.ThrowsAny<Exception>(() => createModel.IsValid());

        ex = Assert.ThrowsAny<Exception>(() => person.Initialize(createModel));

        createModel.FirstName = "Hello";
        createModel.LastName = "World";
        createModel.Name = "42";
        createModel.Email = "buttz";
        createModel.IsValid();

        person.Initialize(createModel);
        Assert.NotNull(person);

        Assert.Equal("42", person.Name);

        var updateModel = new PersonUpdateModel();

        updateModel.IsValid();

        updateModel.FirstName = "Boooger";
        Assert.True(person.Update(updateModel));
        Assert.Equal("Boooger", person.FirstName);
    }
}