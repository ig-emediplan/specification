using System.Linq;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation.PosologyDetailObjects;

public class CyclicValidationTests : ValidationTestsBase
{
    #region Constructors

    public CyclicValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidCyclic_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var cyclic = GetMinimalValidCyclic();

        // Act
        var validationErrors = cyclic.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingCyclicWithoutCycleDurationUnit_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var cyclic = GetMinimalValidCyclic();
        cyclic.CycleDurationUnit = null;

        // Act
        var validationErrors = cyclic.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Cyclic.CycleDurationUnit), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingCyclicWithInvalidCycleDurationUnit_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var cyclic = GetMinimalValidCyclic();
        cyclic.CycleDurationUnit = (TimeUnit)5432;

        // Act
        var validationErrors = cyclic.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Cyclic.CycleDurationUnit), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.NotSupported, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingCyclicWithoutCycleDuration_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var cyclic = GetMinimalValidCyclic();
        cyclic.CycleDuration = null;

        // Act
        var validationErrors = cyclic.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Cyclic.CycleDuration), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingCyclicWithInvalidCycleDuration_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var cyclic = GetMinimalValidCyclic();
        cyclic.CycleDuration = 0;

        // Act
        var validationErrors = cyclic.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Cyclic.CycleDuration), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThan, validationError.Reason);
        var validationErrorIntValueReference = Assert.IsType<ValidationErrorIntValueReference>(validationError.Reference);
        Assert.Equal(0, validationErrorIntValueReference.Value);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingCyclicWithoutTimedDosage_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var cyclic = GetMinimalValidCyclic();
        cyclic.TimedDosage = null;

        // Act
        var validationErrors = cyclic.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Cyclic.TimedDosage), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingCyclicWithInvalidTimedDosage_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var cyclic = GetMinimalValidCyclic();

        cyclic.TimedDosage = new DosageOnly
                             {
                                 Dosage = null // invalid
                             };

        // Act
        var validationErrors = cyclic.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal($"{nameof(Cyclic.TimedDosage)}.{nameof(DosageOnly.Dosage)}", validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingCyclicWithInvalidTimedDosagesPerCycle_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var cyclic = GetMinimalValidCyclic();
        cyclic.TimedDosagesPerCycle = 0;

        // Act
        var validationErrors = cyclic.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Cyclic.TimedDosagesPerCycle), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThan, validationError.Reason);
        var validationErrorIntValueReference = Assert.IsType<ValidationErrorIntValueReference>(validationError.Reference);
        Assert.Equal(0, validationErrorIntValueReference.Value);
    }

    #endregion

    #endregion
}