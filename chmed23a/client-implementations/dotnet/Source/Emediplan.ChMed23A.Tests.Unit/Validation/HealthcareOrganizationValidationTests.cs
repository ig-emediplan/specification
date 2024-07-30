using System.Linq;
using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Validation;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation;

public class HealthcareOrganizationValidationTests : ValidationTestsBase
{
    #region Constructors

    public HealthcareOrganizationValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidHealthcareOrganization_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var healthcareOrganization = GetMinimalValidHealthcareOrganization();

        // Act
        var validationErrors = healthcareOrganization.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingValidHealthcareOrganizationWithoutName_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var healthcareOrganization = GetMinimalValidHealthcareOrganization();
        healthcareOrganization.Name = null;

        // Act
        var validationErrors = healthcareOrganization.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
        Assert.Equal(nameof(HealthcareOrganization.Name), validationError.PropertyPath);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingValidHealthcareOrganizationWithoutStreet_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var healthcareOrganization = GetMinimalValidHealthcareOrganization();
        healthcareOrganization.Street = null;

        // Act
        var validationErrors = healthcareOrganization.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
        Assert.Equal(nameof(HealthcareOrganization.Street), validationError.PropertyPath);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingValidHealthcareOrganizationWithoutZip_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var healthcareOrganization = GetMinimalValidHealthcareOrganization();
        healthcareOrganization.Zip = null;

        // Act
        var validationErrors = healthcareOrganization.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
        Assert.Equal(nameof(HealthcareOrganization.Zip), validationError.PropertyPath);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingValidHealthcareOrganizationWithoutCity_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var healthcareOrganization = GetMinimalValidHealthcareOrganization();
        healthcareOrganization.City = null;

        // Act
        var validationErrors = healthcareOrganization.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
        Assert.Equal(nameof(HealthcareOrganization.City), validationError.PropertyPath);
    }

    #endregion

    #endregion
}