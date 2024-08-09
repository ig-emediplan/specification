using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation.TimedDosageObjects;

public class DaysOfMonthValidationTests : ValidationTestsBase
{
    #region Constructors

    public DaysOfMonthValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidDaysOfMonth_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var daysOfMonth = GetMinimalValidDaysOfMonth();

        // Act
        var validationErrors = daysOfMonth.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingDaysOfMonthWithoutDays_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var daysOfMonth = GetMinimalValidDaysOfMonth();
        daysOfMonth.Days = null;

        // Act
        var validationErrors = daysOfMonth.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                          .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(DaysOfMonth.Days), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(-10, MedicationType.MedicationPlan)]
    [InlineData(0, MedicationType.MedicationPlan)]
    [InlineData(32, MedicationType.MedicationPlan)]
    [InlineData(-10, MedicationType.Prescription)]
    [InlineData(0, MedicationType.Prescription)]
    [InlineData(32, MedicationType.Prescription)]
    public void WhenValidatingDaysOfMonthWithOutOfRangeDays_ReturnsValidationErrors(int day, MedicationType medicationType)
    {
        // Arrange
        var daysOfMonth = GetMinimalValidDaysOfMonth();
        daysOfMonth.Days = new[] {day};

        // Act
        var validationErrors = daysOfMonth.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                          .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(DaysOfMonth.Days), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeInRange, validationError.Reason);
        var validationErrorIntRangeReference = Assert.IsType<ValidationErrorIntRangeReference>(validationError.Reference);
        Assert.Equal(1, validationErrorIntRangeReference.ValueFrom);
        Assert.Equal(31, validationErrorIntRangeReference.ValueTo);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingDaysOfMonthWithEmptyDays_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var daysOfMonth = GetMinimalValidDaysOfMonth();
        daysOfMonth.Days = new List<int>();

        // Act
        var validationErrors = daysOfMonth.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                          .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(DaysOfMonth.Days), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingDaysOfMonthWithoutTimedDosage_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var daysOfMonth = GetMinimalValidDaysOfMonth();
        daysOfMonth.TimedDosage = null;

        // Act
        var validationErrors = daysOfMonth.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                          .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(DaysOfMonth.TimedDosage), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    #endregion

    #endregion
}