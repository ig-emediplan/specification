using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Validation;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation;

public class ExtensionsValidationTests : ValidationTestsBase
{
    #region Constructors

    public ExtensionsValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidExtension_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var extension = GetMinimalValidExtension();

        // Act
        var validationErrors = extension.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                        .ToList();

        OutputErrors(validationErrors);

        // Assert
        Assert.Empty(validationErrors);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingExtensionWithoutValue_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var extension = GetMinimalValidExtension();
        extension.Value = null;

        // Act
        var validationErrors = extension.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                        .ToList();

        OutputErrors(validationErrors);

        // Assert
        Assert.Empty(validationErrors);
    }

    #endregion

    #region Negative

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingExtensionWithoutName_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var extension = GetMinimalValidExtension();
        extension.Name = null;

        // Act
        var validationErrors = extension.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                        .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.EndsWith(nameof(Extension.Name), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingExtensionWithoutSchema_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var extension = GetMinimalValidExtension();
        extension.Schema = null;

        // Act
        var validationErrors = extension.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                        .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.EndsWith(nameof(Extension.Schema), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingExtensionWithInvalidNestedExtension_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var extension = GetMinimalValidExtension();

        extension.Extensions = new List<Extension>
                               {
                                   new()
                                   {
                                       Schema = "test",
                                       Name = null // invalid
                                   }
                               };

        // Act
        var validationErrors = extension.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                        .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.EndsWith($"{nameof(Extension.Extensions)}[0].{nameof(Extension.Name)}", validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    #endregion

    #endregion
}