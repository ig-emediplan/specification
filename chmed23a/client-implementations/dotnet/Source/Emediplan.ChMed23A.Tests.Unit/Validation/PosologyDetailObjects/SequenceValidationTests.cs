using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Models.SequenceObjects;
using Emediplan.ChMed23A.Validation;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation.PosologyDetailObjects;

public class SequenceValidationTests : ValidationTestsBase
{
    #region Constructors

    public SequenceValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidSequence_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var sequence = GetMinimalValidSequence();

        // Act
        var validationErrors = sequence.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingSequenceWithoutSequenceObjects_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var sequence = GetMinimalValidSequence();
        sequence.SequenceObjects = null;

        // Act
        var validationErrors = sequence.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Sequence.SequenceObjects), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingSequenceWithEmptySequenceObjects_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var sequence = GetMinimalValidSequence();
        sequence.SequenceObjects = new List<SequenceObject>();

        // Act
        var validationErrors = sequence.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Sequence.SequenceObjects), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingSequenceWithInvalidSequenceObjects_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var sequence = GetMinimalValidSequence();

        sequence.SequenceObjects = new List<SequenceObject>
                                   {
                                       new Pause
                                       {
                                           Duration = null, // invalid
                                           DurationUnit = TimeUnit.Hour
                                       }
                                   };

        // Act
        var validationErrors = sequence.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal($"{nameof(Sequence.SequenceObjects)}[0].{nameof(Pause.Duration)}", validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    #endregion

    #endregion
}