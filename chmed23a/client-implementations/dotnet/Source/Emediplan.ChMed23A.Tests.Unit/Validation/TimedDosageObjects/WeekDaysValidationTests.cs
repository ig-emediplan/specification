using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Emediplan.ChMed23A.Validation;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation.TimedDosageObjects;

public class WeekDaysValidationTests : ValidationTestsBase
{
    #region Constructors

    public WeekDaysValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidWeekDays_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var weekDays = GetMinimalValidWeekDays();

        // Act
        var validationErrors = weekDays.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingWeekDaysWithoutDaysOfWeek_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var weekDays = GetMinimalValidWeekDays();
        weekDays.DaysOfWeek = null;

        // Act
        var validationErrors = weekDays.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(WeekDays.DaysOfWeek), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingWeekDaysWithEmptyDaysOfWeek_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var weekDays = GetMinimalValidWeekDays();
        weekDays.DaysOfWeek = new List<DayOfWeek>();

        // Act
        var validationErrors = weekDays.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(WeekDays.DaysOfWeek), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingWeekDaysWithNotUniqueDaysOfWeek_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var weekDays = GetMinimalValidWeekDays();
        weekDays.DaysOfWeek = new[] {DayOfWeek.Monday, DayOfWeek.Monday};

        // Act
        var validationErrors = weekDays.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(WeekDays.DaysOfWeek), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeUnique, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingWeekDaysWithInvalidDaysOfWeek_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var weekDays = GetMinimalValidWeekDays();

        weekDays.DaysOfWeek = new List<DayOfWeek>
                              {
                                  DayOfWeek.Friday,
                                  (DayOfWeek)234
                              };

        // Act
        var validationErrors = weekDays.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(WeekDays.DaysOfWeek), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.NotSupported, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingWeekDaysWithoutTimedDosage_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var weekDays = GetMinimalValidWeekDays();
        weekDays.TimedDosage = null;

        // Act
        var validationErrors = weekDays.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(WeekDays.TimedDosage), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingWeekDaysWithInvalidTimedDosage_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var weekDays = GetMinimalValidWeekDays();

        weekDays.TimedDosage = new DosageOnly
                               {
                                   Dosage = null // invalid
                               };

        // Act
        var validationErrors = weekDays.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal($"{nameof(WeekDays.TimedDosage)}.{nameof(DosageOnly.Dosage)}", validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    #endregion

    #endregion
}