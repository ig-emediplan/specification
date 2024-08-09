using System.Linq;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation.DosageObjects;

public class DosageSimpleValidationTests : ValidationTestsBase
{
    #region Constructors

    public DosageSimpleValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingDosageSimpleWithValidAmount_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var dosage = GetMinimalValidDosageSimple();

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
    [InlineData(0, MedicationType.MedicationPlan)]
    [InlineData(-100, MedicationType.MedicationPlan)]
    [InlineData(0, MedicationType.Prescription)]
    [InlineData(-100, MedicationType.Prescription)]
    public void WhenValidatingValidDosageSimpleWithInvalidAmount_ReturnsValidationError(double amount, MedicationType medicationType)
    {
        // Arrange
        var dosage = GetMinimalValidDosageSimple();
        dosage.Amount = amount;

        // Act
        var validationErrors = dosage.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(DosageSimple.Amount), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThan, validationError.Reason);
        Assert.Equal(ValidationErrorReferenceType.DoubleValue, validationError.Reference.ReferenceType);
        var validationErrorDoubleValueReference = Assert.IsType<ValidationErrorDoubleValueReference>(validationError.Reference);
        Assert.Equal(0, validationErrorDoubleValueReference.Value);
    }

    #endregion

    #endregion
}