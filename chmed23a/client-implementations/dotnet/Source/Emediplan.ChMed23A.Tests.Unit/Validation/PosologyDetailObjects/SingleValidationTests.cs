using System.Linq;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Emediplan.ChMed23A.Validation;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation.PosologyDetailObjects;

public class SingleValidationTests : ValidationTestsBase
{
    #region Constructors

    public SingleValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidSingle_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var single = GetMinimalValidSingle();

        // Act
        var validationErrors = single.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingSingleWithoutTimedDosage_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var single = GetMinimalValidSingle();
        single.TimedDosage = null;

        // Act
        var validationErrors = single.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Single.TimedDosage), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingSingleWithInvalidTimedDosage_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var single = GetMinimalValidSingle();

        single.TimedDosage = new DosageOnly
                             {
                                 Dosage = null // invalid
                             };

        // Act
        var validationErrors = single.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal($"{nameof(Single.TimedDosage)}.{nameof(DosageOnly.Dosage)}", validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingSingleWithTimedDosageOfTypeDaysOfMonth_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var single = GetMinimalValidSingle();
        single.TimedDosage = GetMinimalValidDaysOfMonth();

        // Act
        var validationErrors = single.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Single.TimedDosage), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.NotSupported, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingSingleWithTimedDosageOfTypeInterval_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var single = GetMinimalValidSingle();
        single.TimedDosage = GetMinimalValidInterval();

        // Act
        var validationErrors = single.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Single.TimedDosage), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.NotSupported, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingSingleWithTimedDosageOfTypeWeekDays_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var single = GetMinimalValidSingle();
        single.TimedDosage = GetMinimalValidWeekDays();

        // Act
        var validationErrors = single.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Single.TimedDosage), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.NotSupported, validationError.Reason);
    }

    #endregion

    #endregion
}