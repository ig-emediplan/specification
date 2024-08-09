using System.Linq;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation.DosageObjects;

public class DosageFromToValidationTests : ValidationTestsBase
{
    #region Constructors

    public DosageFromToValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidDosageFromTo_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var dosage = GetMinimalValidDosageFromTo();

        // Act
        var validationErrors = dosage.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingDosageFromToWithoutAmountFrom_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var dosage = GetMinimalValidDosageFromTo();
        dosage.AmountFrom = null;

        // Act
        var validationErrors = dosage.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(DosageFromTo.AmountFrom), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingDosageFromToWithAmountFromOutOfRange_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var dosage = GetMinimalValidDosageFromTo();
        dosage.AmountFrom = -1;

        // Act
        var validationErrors = dosage.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(DosageFromTo.AmountFrom), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThan, validationError.Reason);
        Assert.Equal(ValidationErrorReferenceType.DoubleValue, validationError.Reference.ReferenceType);
        var validationErrorDoubleValueReference = Assert.IsType<ValidationErrorDoubleValueReference>(validationError.Reference);
        Assert.Equal(0, validationErrorDoubleValueReference.Value);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingDosageFromToWithoutAmountTo_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var dosage = GetMinimalValidDosageFromTo();
        dosage.AmountTo = null;

        // Act
        var validationErrors = dosage.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(DosageFromTo.AmountTo), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(1, 1, MedicationType.MedicationPlan)]
    [InlineData(50, 10, MedicationType.MedicationPlan)]
    [InlineData(1, 1, MedicationType.Prescription)]
    [InlineData(50, 10, MedicationType.Prescription)]
    public void WhenValidatingDosageFromToWithoutAmountFromGreaterOrEqualAmountTo_ReturnsValidationErrors(double amountFrom, double amountTo, MedicationType medicationType)
    {
        // Arrange
        var dosage = GetMinimalValidDosageFromTo();
        dosage.AmountFrom = amountFrom;
        dosage.AmountTo = amountTo;

        // Act
        var validationErrors = dosage.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(DosageFromTo.AmountTo), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThan, validationError.Reason);
        Assert.Equal(ValidationErrorReferenceType.DoubleValue, validationError.Reference.ReferenceType);
        var validationErrorDoubleValueReference = Assert.IsType<ValidationErrorDoubleValueReference>(validationError.Reference);
        Assert.Equal(amountFrom, validationErrorDoubleValueReference.Value);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingDosageFromToWithoutDuration_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var dosage = GetMinimalValidDosageFromTo();
        dosage.Duration = null;

        // Act
        var validationErrors = dosage.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(DosageFromTo.Duration), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingDosageFromToWithInvalidDuration_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var dosage = GetMinimalValidDosageFromTo();
        dosage.Duration = 0;

        // Act
        var validationErrors = dosage.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(DosageFromTo.Duration), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThan, validationError.Reason);
        Assert.Equal(ValidationErrorReferenceType.IntValue, validationError.Reference.ReferenceType);
        var validationErrorIntValueReference = Assert.IsType<ValidationErrorIntValueReference>(validationError.Reference);
        Assert.Equal(0, validationErrorIntValueReference.Value);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingDosageFromToWithoutDurationUnit_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var dosage = GetMinimalValidDosageFromTo();
        dosage.DurationUnit = null;

        // Act
        var validationErrors = dosage.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(DosageFromTo.DurationUnit), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingDosageFromToWithInvalidDurationUnit_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var dosage = GetMinimalValidDosageFromTo();
        dosage.DurationUnit = (TimeUnit)342;

        // Act
        var validationErrors = dosage.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(DosageFromTo.DurationUnit), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.NotSupported, validationError.Reason);
    }

    #endregion

    #endregion
}