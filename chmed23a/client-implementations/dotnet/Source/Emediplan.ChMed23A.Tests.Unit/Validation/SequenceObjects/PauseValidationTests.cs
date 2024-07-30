using System.Linq;
using Emediplan.ChMed23A.Models.SequenceObjects;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation.SequenceObjects;

public class PauseValidationTests : ValidationTestsBase
{
    #region Constructors

    public PauseValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidPause_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var pause = GetMinimalValidPause();

        // Act
        var validationErrors = pause.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingPauseWithoutDuration_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var pause = GetMinimalValidPause();
        pause.Duration = null;

        // Act
        var validationErrors = pause.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                    .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Pause.Duration), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPauseWithInvalidDuration_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var pause = GetMinimalValidPause();
        pause.Duration = 0;

        // Act
        var validationErrors = pause.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                    .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Pause.Duration), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThan, validationError.Reason);
        Assert.Equal(ValidationErrorReferenceType.IntValue, validationError.Reference.ReferenceType);
        var validationErrorDoubleValueReference = Assert.IsType<ValidationErrorIntValueReference>(validationError.Reference);
        Assert.Equal(0, validationErrorDoubleValueReference.Value);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPauseWithoutDurationUnit_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var pause = GetMinimalValidPause();
        pause.DurationUnit = null;

        // Act
        var validationErrors = pause.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                    .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Pause.DurationUnit), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPauseWithInvalidDurationUnit_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var pause = GetMinimalValidPause();
        pause.DurationUnit = (TimeUnit)456;

        // Act
        var validationErrors = pause.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                    .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Pause.DurationUnit), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.NotSupported, validationError.Reason);
    }

    #endregion

    #endregion
}