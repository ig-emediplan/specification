using System.Linq;
using Emediplan.ChMed23A.Models.RepetitionObjects;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation.RepetitionObjects;

public class DurationValidationTests : ValidationTestsBase
{
    #region Constructors

    public DurationValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(1, TimeUnit.Week, MedicationType.MedicationPlan)]
    [InlineData(100, TimeUnit.Day, MedicationType.MedicationPlan)]
    [InlineData(1, TimeUnit.Week, MedicationType.Prescription)]
    [InlineData(100, TimeUnit.Day, MedicationType.Prescription)]
    public void WhenValidatingValidDuration_ReturnsNoValidationErrors(int value, TimeUnit unit, MedicationType medicationType)
    {
        // Arrange
        var duration = new Duration
                       {
                           Unit = unit,
                           DurationValue = value
                       };

        // Act
        var validationErrors = duration.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        Assert.Empty(validationErrors);
    }

    [Fact]
    public void WhenValidatingDurationWithNullPropertiesForMedicationStatement_ReturnsNoValidationErrors()
    {
        // Arrange
        var duration = new Duration
                       {
                           Unit = null,
                           DurationValue = null
                       };

        // Act
        var validationErrors = duration.GetValidationErrors(new ValidationContext {MedicationType = MedicationType.MedicationPlan})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        Assert.Empty(validationErrors);
    }

    #endregion

    #region Negative

    [Theory]
    [InlineData(0, TimeUnit.Week, MedicationType.MedicationPlan)]
    [InlineData(-10, TimeUnit.Day, MedicationType.MedicationPlan)]
    [InlineData(0, TimeUnit.Week, MedicationType.Prescription)]
    [InlineData(-10, TimeUnit.Day, MedicationType.Prescription)]
    public void WhenValidatingDurationWithNegativeDurationValue_ReturnsValidationError(int value, TimeUnit unit, MedicationType medicationType)
    {
        // Arrange
        var duration = new Duration
                       {
                           Unit = unit,
                           DurationValue = value
                       };

        // Act
        var validationErrors = duration.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Duration.DurationValue), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThan, validationError.Reason);
        Assert.Equal(ValidationErrorReferenceType.IntValue, validationError.Reference.ReferenceType);
        var validationErrorIntValueReference = Assert.IsType<ValidationErrorIntValueReference>(validationError.Reference);
        Assert.Equal(0, validationErrorIntValueReference.Value);
    }

    [Fact]
    public void WhenValidatingDurationWithoutDurationValueForPrescription_ReturnsValidationError()
    {
        // Arrange
        var duration = new Duration
                       {
                           Unit = TimeUnit.Day,
                           DurationValue = null
                       };

        // Act
        var validationErrors = duration.GetValidationErrors(new ValidationContext {MedicationType = MedicationType.Prescription})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);

        Assert.False(string.IsNullOrEmpty(validationError.Message));

        Assert.Equal(nameof(Duration.DurationValue), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Fact]
    public void WhenValidatingDurationWithoutUnitForPrescription_ReturnsValidationError()
    {
        // Arrange
        var duration = new Duration
                       {
                           Unit = null,
                           DurationValue = 3
                       };

        // Act
        var validationErrors = duration.GetValidationErrors(new ValidationContext {MedicationType = MedicationType.Prescription})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);

        Assert.False(string.IsNullOrEmpty(validationError.Message));

        Assert.Equal(nameof(Duration.Unit), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    #endregion

    #endregion
}