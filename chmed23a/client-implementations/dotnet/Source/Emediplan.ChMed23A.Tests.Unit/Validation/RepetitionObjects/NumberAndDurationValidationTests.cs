using System.Linq;
using Emediplan.ChMed23A.Models.RepetitionObjects;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation.RepetitionObjects;

public class NumberAndDurationValidationTests : ValidationTestsBase
{
    #region Constructors

    public NumberAndDurationValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(1, TimeUnit.Week, 7, MedicationType.MedicationPlan)]
    [InlineData(100, TimeUnit.Day, 50, MedicationType.MedicationPlan)]
    [InlineData(1, TimeUnit.Week, 7, MedicationType.Prescription)]
    [InlineData(100, TimeUnit.Day, 50, MedicationType.Prescription)]
    public void WhenValidatingValidNumberAndDuration_ReturnsNoValidationErrors(int durationValue, TimeUnit? durationUnit, int? value, MedicationType medicationType)
    {
        // Arrange
        var numberAndDuration = new NumberAndDuration
                                {
                                    Unit = durationUnit,
                                    DurationValue = durationValue,
                                    Value = value
                                };

        // Act
        var validationErrors = numberAndDuration.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                                .ToList();

        OutputErrors(validationErrors);

        // Assert
        Assert.Empty(validationErrors);
    }

    [Fact]
    public void WhenValidatingNumberAndDurationWithNullPropertiesForMedicationStatement_ReturnsNoValidationErrors()
    {
        // Arrange
        var numberAndDuration = new NumberAndDuration
                                {
                                    Unit = null,
                                    DurationValue = null,
                                    Value = null
                                };

        // Act
        var validationErrors = numberAndDuration.GetValidationErrors(new ValidationContext {MedicationType = MedicationType.MedicationPlan})
                                                .ToList();

        OutputErrors(validationErrors);

        // Assert
        Assert.Empty(validationErrors);
    }

    #endregion

    #region Negative

    [Theory]
    [InlineData(0, MedicationType.MedicationPlan)]
    [InlineData(-10, MedicationType.MedicationPlan)]
    [InlineData(0, MedicationType.Prescription)]
    [InlineData(-10, MedicationType.Prescription)]
    public void WhenValidatingNumberAndDurationWithInvalidDurationValue_ReturnsValidationError(int durationValue, MedicationType medicationType)
    {
        // Arrange
        var numberAndDuration = new NumberAndDuration
                                {
                                    Unit = TimeUnit.Day,
                                    DurationValue = durationValue,
                                    Value = 3
                                };

        // Act
        var validationErrors = numberAndDuration.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                                .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);

        Assert.False(string.IsNullOrEmpty(validationError.Message));

        Assert.Equal(nameof(NumberAndDuration.DurationValue), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThan, validationError.Reason);
        Assert.Equal(ValidationErrorReferenceType.IntValue, validationError.Reference.ReferenceType);
        var validationErrorIntValueReference = Assert.IsType<ValidationErrorIntValueReference>(validationError.Reference);
        Assert.Equal(0, validationErrorIntValueReference.Value);
    }

    [Theory]
    [InlineData(-10, MedicationType.MedicationPlan)]
    [InlineData(-10, MedicationType.Prescription)]
    public void WhenValidatingNumberAndDurationWithNegativeValue_ReturnsValidationError(int value, MedicationType medicationType)
    {
        // Arrange
        var numberAndDuration = new NumberAndDuration
                                {
                                    Unit = TimeUnit.Day,
                                    DurationValue = 3,
                                    Value = value
                                };

        // Act
        var validationErrors = numberAndDuration.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                                .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);

        Assert.False(string.IsNullOrEmpty(validationError.Message));

        Assert.Equal(nameof(NumberAndDuration.Value), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThanOrEqual, validationError.Reason);
        Assert.Equal(ValidationErrorReferenceType.IntValue, validationError.Reference.ReferenceType);
        var validationErrorIntValueReference = Assert.IsType<ValidationErrorIntValueReference>(validationError.Reference);
        Assert.Equal(0, validationErrorIntValueReference.Value);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingNumberAndDurationWithInvalidUnit_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var numberAndDuration = new NumberAndDuration
                                {
                                    Unit = (TimeUnit)1423,
                                    DurationValue = 3,
                                    Value = 3
                                };

        // Act
        var validationErrors = numberAndDuration.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                                .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);

        Assert.False(string.IsNullOrEmpty(validationError.Message));

        Assert.Equal(nameof(NumberAndDuration.Unit), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.NotSupported, validationError.Reason);
    }

    [Fact]
    public void WhenValidatingNumberAndDurationWithoutValueForPrescription_ReturnsValidationError()
    {
        // Arrange
        var numberAndDuration = new NumberAndDuration
                                {
                                    Unit = TimeUnit.Day,
                                    DurationValue = 3,
                                    Value = null
                                };

        // Act
        var validationErrors = numberAndDuration.GetValidationErrors(new ValidationContext {MedicationType = MedicationType.Prescription})
                                                .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);

        Assert.False(string.IsNullOrEmpty(validationError.Message));

        Assert.Equal(nameof(NumberAndDuration.Value), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Fact]
    public void WhenValidatingNumberAndDurationWithoutDurationValueForPrescription_ReturnsValidationError()
    {
        // Arrange
        var numberAndDuration = new NumberAndDuration
                                {
                                    Unit = TimeUnit.Day,
                                    DurationValue = null,
                                    Value = 3
                                };

        // Act
        var validationErrors = numberAndDuration.GetValidationErrors(new ValidationContext {MedicationType = MedicationType.Prescription})
                                                .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);

        Assert.False(string.IsNullOrEmpty(validationError.Message));

        Assert.Equal(nameof(NumberAndDuration.DurationValue), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Fact]
    public void WhenValidatingNumberAndDurationWithoutUnitForPrescription_ReturnsValidationError()
    {
        // Arrange
        var numberAndDuration = new NumberAndDuration
                                {
                                    Unit = null,
                                    DurationValue = 3,
                                    Value = 3
                                };

        // Act
        var validationErrors = numberAndDuration.GetValidationErrors(new ValidationContext {MedicationType = MedicationType.Prescription})
                                                .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);

        Assert.False(string.IsNullOrEmpty(validationError.Message));

        Assert.Equal(nameof(NumberAndDuration.Unit), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    #endregion

    #endregion
}