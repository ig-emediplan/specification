using System.Linq;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation.DosageObjects;

public class DosageRangeValidationTests : ValidationTestsBase
{
    #region Constructors

    public DosageRangeValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidDosageRange_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var dosage = GetMinimalValidDosageRange();

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
    public void WhenValidatingDosageRangeWithoutMinAmount_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var dosage = GetMinimalValidDosageRange();
        dosage.MinAmount = null;

        // Act
        var validationErrors = dosage.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(DosageRange.MinAmount), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingDosageRangeWithNegativeMinAmount_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var dosage = GetMinimalValidDosageRange();
        dosage.MinAmount = -0.5;

        // Act
        var validationErrors = dosage.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(DosageRange.MinAmount), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThanOrEqual, validationError.Reason);
        Assert.Equal(ValidationErrorReferenceType.DoubleValue, validationError.Reference.ReferenceType);
        var validationErrorDoubleValueReference = Assert.IsType<ValidationErrorDoubleValueReference>(validationError.Reference);
        Assert.Equal(0, validationErrorDoubleValueReference.Value);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingDosageRangeWithoutMaxAmount_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var dosage = GetMinimalValidDosageRange();
        dosage.MaxAmount = null;

        // Act
        var validationErrors = dosage.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(DosageRange.MaxAmount), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(0, 0, MedicationType.MedicationPlan)]
    [InlineData(10, 5.5, MedicationType.MedicationPlan)]
    [InlineData(0, 0, MedicationType.Prescription)]
    [InlineData(10, 5.5, MedicationType.Prescription)]
    public void WhenValidatingDosageRangeWithMaxAmountGreaterThanOrEqualMinAmount_ReturnsValidationErrors(double minAmount, double maxAmount, MedicationType medicationType)
    {
        // Arrange
        var dosage = GetMinimalValidDosageRange();
        dosage.MinAmount = minAmount;
        dosage.MaxAmount = maxAmount;

        // Act
        var validationErrors = dosage.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(DosageRange.MaxAmount), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThan, validationError.Reason);
        Assert.Equal(ValidationErrorReferenceType.DoubleValue, validationError.Reference.ReferenceType);
        var validationErrorDoubleValueReference = Assert.IsType<ValidationErrorDoubleValueReference>(validationError.Reference);
        Assert.Equal(minAmount, validationErrorDoubleValueReference.Value);
    }

    #endregion

    #endregion
}