using System.Linq;
using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Validation;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation;

public class HealthcarePersonValidationTests : ValidationTestsBase
{
    #region Constructors

    public HealthcarePersonValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidHealthcarePerson_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var healthcarePerson = GetMinimalValidHealthcarePerson();

        // Act
        var validationErrors = healthcarePerson.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                               .ToList();

        OutputErrors(validationErrors);

        // Assert
        Assert.Empty(validationErrors);
    }

    #endregion

    #region Negative

    [Theory]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingValidHealthcarePersonWithoutGln_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var healthcarePerson = GetMinimalValidHealthcarePerson();
        healthcarePerson.Gln = null;

        // Act
        var validationErrors = healthcarePerson.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                               .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
        Assert.Equal(nameof(HealthcarePerson.Gln), validationError.PropertyPath);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingValidHealthcarePersonWithoutFirstName_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var healthcarePerson = GetMinimalValidHealthcarePerson();
        healthcarePerson.FirstName = null;

        // Act
        var validationErrors = healthcarePerson.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                               .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
        Assert.Equal(nameof(HealthcarePerson.FirstName), validationError.PropertyPath);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingValidHealthcarePersonWithoutLastName_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var healthcarePerson = GetMinimalValidHealthcarePerson();
        healthcarePerson.LastName = null;

        // Act
        var validationErrors = healthcarePerson.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                               .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
        Assert.Equal(nameof(HealthcarePerson.LastName), validationError.PropertyPath);
    }

    #endregion

    #endregion
}