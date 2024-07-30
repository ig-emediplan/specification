using System.Linq;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Emediplan.ChMed23A.Validation;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation.TimedDosageObjects;

public class DosageOnlyValidationTests : ValidationTestsBase
{
    #region Constructors

    public DosageOnlyValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidDosageOnly_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var dosageOnly = GetMinimalValidDosageOnly();

        // Act
        var validationErrors = dosageOnly.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingDosageOnlyWithoutDosage_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var dosageOnly = GetMinimalValidDosageOnly();
        dosageOnly.Dosage = null;

        // Act
        var validationErrors = dosageOnly.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                         .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(DosageOnly.Dosage), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingDosageOnlyWithInvalidDosage_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var dosageOnly = GetMinimalValidDosageOnly();

        dosageOnly.Dosage = new DosageSimple
                            {
                                Amount = -1 // invalid
                            };

        // Act
        var validationErrors = dosageOnly.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                         .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal($"{nameof(DosageOnly.Dosage)}.{nameof(DosageSimple.Amount)}", validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThan, validationError.Reason);
    }

    #endregion

    #endregion
}