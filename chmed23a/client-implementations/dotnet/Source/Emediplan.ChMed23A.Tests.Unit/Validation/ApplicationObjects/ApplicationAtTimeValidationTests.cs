using System;
using System.Linq;
using Emediplan.ChMed23A.Models.ApplicationObjects;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation.ApplicationObjects;

public class ApplicationAtTimeValidationTests : ValidationTestsBase
{
    #region Constructors

    public ApplicationAtTimeValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingValidApplicationAtTime_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var applicationAtTime = GetMinimalValidApplicationAtTime();

        // Act
        var validationErrors = applicationAtTime.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingApplicationAtTimeWithoutTimeOfDay_ReturnsValidationError(MedicationType medicationType)

    {
        // Arrange
        var applicationAtTime = GetMinimalValidApplicationAtTime();
        applicationAtTime.TimeOfDay = null;

        // Act
        var validationErrors = applicationAtTime.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                                .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);

        Assert.False(string.IsNullOrEmpty(validationError.Message));

        Assert.Equal(nameof(ApplicationAtTime.TimeOfDay), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingApplicationAtTimeWithoutDosage_ReturnsValidationError(MedicationType medicationType)

    {
        // Arrange
        var applicationAtTime = GetMinimalValidApplicationAtTime();
        applicationAtTime.Dosage = null;

        // Act
        var validationErrors = applicationAtTime.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                                .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);

        Assert.False(string.IsNullOrEmpty(validationError.Message));

        Assert.Equal(nameof(ApplicationAtTime.Dosage), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(-1, MedicationType.MedicationPlan)]
    [InlineData(25, MedicationType.MedicationPlan)]
    [InlineData(-1, MedicationType.Prescription)]
    [InlineData(25, MedicationType.Prescription)]
    public void WhenValidatingApplicationAtTimeWithInvalidTimeOfDay_ReturnsValidationError(int hourOfDay, MedicationType medicationType)
    {
        // Arrange
        var applicationAtTime = GetMinimalValidApplicationAtTime();
        applicationAtTime.TimeOfDay = new TimeSpan(hourOfDay, 0, 0);

        // Act
        var validationErrors = applicationAtTime.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                                .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(ApplicationAtTime.TimeOfDay), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeInRange, validationError.Reason);
        Assert.Equal(ValidationErrorReferenceType.Timespan, validationError.Reference.ReferenceType);
        var validationErrorTimespanReference = Assert.IsType<ValidationErrorTimespanReference>(validationError.Reference);
        Assert.Equal(TimeSpan.Zero, validationErrorTimespanReference.ValueFrom);
        Assert.Equal(new TimeSpan(24, 0, 0), validationErrorTimespanReference.ValueTo);
    }

    #endregion

    #endregion
}