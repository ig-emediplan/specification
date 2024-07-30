using System.Linq;
using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation;

public class MedicamentValidationTests : ValidationTestsBase
{
    #region Constructors

    public MedicamentValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidMedicament_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var medicament = GetMinimalValidMedicament();

        // Act
        var validationErrors = medicament.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                         .ToList();

        OutputErrors(validationErrors);

        // Assert
        Assert.Empty(validationErrors);
    }

    #endregion

    #region Negative

    [Theory]
    [InlineData(null, MedicationType.MedicationPlan)]
    [InlineData("", MedicationType.MedicationPlan)]
    [InlineData("     ", MedicationType.MedicationPlan)]
    [InlineData(null, MedicationType.Prescription)]
    [InlineData("", MedicationType.Prescription)]
    [InlineData("     ", MedicationType.Prescription)]
    public void WhenValidatingMedicamentWithoutId_ReturnsValidationErrors(string id, MedicationType medicationType)
    {
        // Arrange
        var medicament = GetMinimalValidMedicament();
        medicament.Id = id;

        // Act
        var validationErrors = medicament.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                         .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Medicament.Id), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMedicamentWithInvalidNumberOfPackages_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var medicament = GetMinimalValidMedicament();
        medicament.NumberOfPackages = 0;

        // Act
        var validationErrors = medicament.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                         .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Medicament.NumberOfPackages), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThan, validationError.Reason);
        var validationErrorDoubleValueReference = Assert.IsType<ValidationErrorDoubleValueReference>(validationError.Reference);
        Assert.Equal(0, validationErrorDoubleValueReference.Value);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMedicamentWithoutIdType_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var medicament = GetMinimalValidMedicament();
        medicament.IdType = null;

        // Act
        var validationErrors = medicament.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                         .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Medicament.IdType), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Fact]
    public void WhenValidatingMedicamentWithoutIsAutoMedicationForMedicationPlan_ReturnsValidationErrors()
    {
        // Arrange
        const MedicationType medicationType = MedicationType.MedicationPlan;
        var medicament = GetMinimalValidMedicament();
        medicament.IsAutoMedication = null;

        // Act
        var validationErrors = medicament.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                         .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Medicament.IsAutoMedication), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    #endregion

    #endregion
}