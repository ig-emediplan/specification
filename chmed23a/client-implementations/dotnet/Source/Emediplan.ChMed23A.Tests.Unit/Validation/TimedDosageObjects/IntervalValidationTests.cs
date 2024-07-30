using System.Linq;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation.TimedDosageObjects;

public class IntervalValidationTests : ValidationTestsBase
{
    #region Constructors

    public IntervalValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidInterval_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var interval = GetMinimalValidInterval();

        // Act
        var validationErrors = interval.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingIntervalWithoutDosage_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var interval = GetMinimalValidInterval();
        interval.Dosage = null;

        // Act
        var validationErrors = interval.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Interval.Dosage), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingIntervalWithInvalidDosage_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var interval = GetMinimalValidInterval();

        interval.Dosage = new DosageSimple
                          {
                              Amount = null // invalid
                          };

        // Act
        var validationErrors = interval.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal($"{nameof(Interval.Dosage)}.{nameof(DosageSimple.Amount)}", validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingIntervalWithoutMinIntervalDurationUnit_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var interval = GetMinimalValidInterval();
        interval.MinIntervalDurationUnit = null;

        // Act
        var validationErrors = interval.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Interval.MinIntervalDurationUnit), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingIntervalWithInvalidMinIntervalDurationUnit_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var interval = GetMinimalValidInterval();
        interval.MinIntervalDurationUnit = (TimeUnit)465;

        // Act
        var validationErrors = interval.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Interval.MinIntervalDurationUnit), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.NotSupported, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingIntervalWithoutMinIntervalDuration_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var interval = GetMinimalValidInterval();
        interval.MinIntervalDuration = null;

        // Act
        var validationErrors = interval.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Interval.MinIntervalDuration), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingIntervalWithInvalidMinIntervalDuration_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var interval = GetMinimalValidInterval();
        interval.MinIntervalDuration = 0;

        // Act
        var validationErrors = interval.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Interval.MinIntervalDuration), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThan, validationError.Reason);
        var validationErrorIntValueReference = Assert.IsType<ValidationErrorIntValueReference>(validationError.Reference);
        Assert.Equal(0, validationErrorIntValueReference.Value);
    }

    #endregion

    #endregion
}