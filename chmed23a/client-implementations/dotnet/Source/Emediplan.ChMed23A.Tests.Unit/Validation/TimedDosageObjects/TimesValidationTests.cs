using System;
using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.ApplicationObjects;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Emediplan.ChMed23A.Validation;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation.TimedDosageObjects;

public class TimesValidationTests : ValidationTestsBase
{
    #region Constructors

    public TimesValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidTimes_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var times = GetMinimalValidTimes();

        // Act
        var validationErrors = times.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingTimesWithoutApplications_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var times = GetMinimalValidTimes();
        times.Applications = null;

        // Act
        var validationErrors = times.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                    .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Times.Applications), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingTimesWithEmptyApplications_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var times = GetMinimalValidTimes();
        times.Applications = new List<ApplicationAtTime>();

        // Act
        var validationErrors = times.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                    .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Times.Applications), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingTimesWithInvalidApplication_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var times = GetMinimalValidTimes();

        times.Applications = new List<ApplicationAtTime>
                             {
                                 new()
                                 {
                                     Dosage = null, // invalid
                                     TimeOfDay = new TimeSpan(8, 0, 0)
                                 }
                             };

        // Act
        var validationErrors = times.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                    .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal($"{nameof(Times.Applications)}[0].{nameof(ApplicationAtTime.Dosage)}", validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    #endregion

    #endregion
}