using System.Linq;
using Emediplan.ChMed23A.Models.ApplicationObjects;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Validation;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation.ApplicationObjects;

public class ApplicationInSegmentValidationTests : ValidationTestsBase
{
    #region Constructors

    public ApplicationInSegmentValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingValidApplicationInSegment_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var applicationInSegment = GetMinimalValidApplicationInSegment();

        // Act
        var validationErrors = applicationInSegment.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingApplicationInSegmentWithoutSegment_ReturnsValidationError(MedicationType medicationType)

    {
        // Arrange
        var applicationInSegment = GetMinimalValidApplicationInSegment();
        applicationInSegment.Segment = null;

        // Act
        var validationErrors = applicationInSegment.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                                   .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(ApplicationInSegment.Segment), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingApplicationInSegmentWithoutDosage_ReturnsValidationError(MedicationType medicationType)

    {
        // Arrange
        var applicationInSegment = GetMinimalValidApplicationInSegment();
        applicationInSegment.Dosage = null;

        // Act
        var validationErrors = applicationInSegment.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                                   .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(ApplicationInSegment.Dosage), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingApplicationInSegmentWithInvalidSegment_ReturnsValidationError(MedicationType medicationType)

    {
        // Arrange
        var applicationInSegment = GetMinimalValidApplicationInSegment();
        applicationInSegment.Segment = (DaySegment)325;

        // Act
        var validationErrors = applicationInSegment.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                                   .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(ApplicationInSegment.Segment), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.NotSupported, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingApplicationInSegmentWithInvalidDosage_ReturnsValidationError(MedicationType medicationType)

    {
        // Arrange
        var applicationInSegment = GetMinimalValidApplicationInSegment();

        applicationInSegment.Dosage = new DosageSimple
                                      {
                                          Amount = null // Invalid
                                      };

        // Act
        var validationErrors = applicationInSegment.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                                   .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal($"{nameof(ApplicationInSegment.Dosage)}.{nameof(DosageSimple.Amount)}", validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    #endregion

    #endregion
}