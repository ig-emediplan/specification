using System.Linq;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Models.SequenceObjects;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation.SequenceObjects;

public class PosologySequenceValidationTests : ValidationTestsBase
{
    #region Constructors

    public PosologySequenceValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidPosologySequence_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var posologySequence = GetMinimalValidPosologySequence();

        // Act
        var validationErrors = posologySequence.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingPosologySequenceWithoutDuration_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var posologySequence = GetMinimalValidPosologySequence();
        posologySequence.Duration = null;

        // Act
        var validationErrors = posologySequence.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                               .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(PosologySequence.Duration), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPosologySequenceWithInvalidDuration_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var posologySequence = GetMinimalValidPosologySequence();
        posologySequence.Duration = 0;

        // Act
        var validationErrors = posologySequence.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                               .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(PosologySequence.Duration), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThan, validationError.Reason);
        Assert.Equal(ValidationErrorReferenceType.IntValue, validationError.Reference.ReferenceType);
        var validationErrorDoubleValueReference = Assert.IsType<ValidationErrorIntValueReference>(validationError.Reference);
        Assert.Equal(0, validationErrorDoubleValueReference.Value);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPosologySequenceWithoutDurationUnit_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var posologySequence = GetMinimalValidPosologySequence();
        posologySequence.DurationUnit = null;

        // Act
        var validationErrors = posologySequence.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                               .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(PosologySequence.DurationUnit), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPosologySequenceWithInvalidDurationUnit_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var posologySequence = GetMinimalValidPosologySequence();
        posologySequence.DurationUnit = (TimeUnit)456;

        // Act
        var validationErrors = posologySequence.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                               .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(PosologySequence.DurationUnit), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.NotSupported, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPosologySequenceWithoutPosologyDetailObject_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var posologySequence = GetMinimalValidPosologySequence();
        posologySequence.PosologyDetailObject = null;

        // Act
        var validationErrors = posologySequence.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                               .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(PosologySequence.PosologyDetailObject), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPosologySequenceWithInvalidPosologyDetailObject_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var posologySequence = GetMinimalValidPosologySequence();

        posologySequence.PosologyDetailObject = new FreeText
                                                {
                                                    Text = null // invalid
                                                };

        // Act
        var validationErrors = posologySequence.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                               .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal($"{nameof(PosologySequence.PosologyDetailObject)}.{nameof(FreeText.Text)}", validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    #endregion

    #endregion
}