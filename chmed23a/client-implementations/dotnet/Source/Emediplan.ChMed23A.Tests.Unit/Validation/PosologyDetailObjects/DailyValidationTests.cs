using System.Linq;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation.PosologyDetailObjects;

public class DailyValidationTests : ValidationTestsBase
{
    #region Constructors

    public DailyValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalDaily_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var daily = GetMinimalValidDaily();

        // Act
        var validationErrors = daily.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingDailyWithTooManyDosages_ReturnValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var daily = GetMinimalValidDaily();
        daily.Dosages = new[] {0, 1, 0, 1.0, 0};

        // Act
        var validationErrors = daily.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                    .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Daily.Dosages), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.ArrayLengthMustBe, validationError.Reason);
        var validationErrorIntValueReference = Assert.IsType<ValidationErrorIntValueReference>(validationError.Reference);
        Assert.Equal(4, validationErrorIntValueReference.Value);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingDailyWithTooFewDosages_ReturnValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var daily = GetMinimalValidDaily();
        daily.Dosages = new[] {1.0};

        // Act
        var validationErrors = daily.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                    .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Daily.Dosages), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.ArrayLengthMustBe, validationError.Reason);
        var validationErrorIntValueReference = Assert.IsType<ValidationErrorIntValueReference>(validationError.Reference);
        Assert.Equal(4, validationErrorIntValueReference.Value);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingDailyWithNegativeDosage_ReturnValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var daily = GetMinimalValidDaily();
        daily.Dosages = new[] {0, -1.0, 0, 0};

        // Act
        var validationErrors = daily.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                    .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal($"{nameof(Daily.Dosages)}[1]", validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThanOrEqual, validationError.Reason);
        var validationErrorIntValueReference = Assert.IsType<ValidationErrorDoubleValueReference>(validationError.Reference);
        Assert.Equal(0, validationErrorIntValueReference.Value);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingDailyWithNegativeDosages_ReturnValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var daily = GetMinimalValidDaily();
        daily.Dosages = new[] {0, -1.0, 0, -2.0};

        // Act
        var validationErrors = daily.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                    .ToList();

        OutputErrors(validationErrors);

        // Assert
        Assert.Equal(2, validationErrors.Count);

        var validationError1 = validationErrors[0];
        Assert.False(string.IsNullOrEmpty(validationError1.Message));
        Assert.Equal($"{nameof(Daily.Dosages)}[1]", validationError1.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThanOrEqual, validationError1.Reason);
        var validationError1IntValueReference = Assert.IsType<ValidationErrorDoubleValueReference>(validationError1.Reference);
        Assert.Equal(0, validationError1IntValueReference.Value);

        var validationError2 = validationErrors[1];
        Assert.False(string.IsNullOrEmpty(validationError2.Message));
        Assert.Equal($"{nameof(Daily.Dosages)}[3]", validationError2.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThanOrEqual, validationError2.Reason);
        var validationError2IntValueReference = Assert.IsType<ValidationErrorDoubleValueReference>(validationError2.Reference);
        Assert.Equal(0, validationError2IntValueReference.Value);
    }

    #endregion

    #endregion
}