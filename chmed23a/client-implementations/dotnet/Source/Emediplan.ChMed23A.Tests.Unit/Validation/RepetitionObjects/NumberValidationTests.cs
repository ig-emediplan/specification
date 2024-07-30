using System.Linq;
using Emediplan.ChMed23A.Models.RepetitionObjects;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation.RepetitionObjects;

public class NumberValidationTests : ValidationTestsBase
{
    #region Constructors

    public NumberValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(0)]
    [InlineData(100)]
    public void WhenValidatingNumberWithPositiveValueForPrescription_ReturnsNoValidationErrors(int value)
    {
        // Arrange
        var number = new Number
                     {
                         Value = value
                     };

        // Act
        var validationErrors = number.GetValidationErrors(new ValidationContext {MedicationType = MedicationType.Prescription})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        Assert.Empty(validationErrors);
    }

    [Fact]
    public void WhenValidatingNumberWithoutValueForMedicationPlan_ReturnsNoValidationErrors()
    {
        // Arrange
        var number = new Number
                     {
                         Value = null
                     };

        // Act
        var validationErrors = number.GetValidationErrors(new ValidationContext {MedicationType = MedicationType.MedicationPlan})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        Assert.Empty(validationErrors);
    }

    #endregion

    #region Negative

    [Theory]
    [InlineData(-1, MedicationType.MedicationPlan)]
    [InlineData(-100, MedicationType.MedicationPlan)]
    [InlineData(-1, MedicationType.Prescription)]
    [InlineData(-100, MedicationType.Prescription)]
    public void WhenValidatingNumberWithNegativeValue_ReturnsValidationError(int value, MedicationType medicationType)
    {
        // Arrange
        var number = new Number
                     {
                         Value = value
                     };

        // Act
        var validationErrors = number.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);

        Assert.False(string.IsNullOrEmpty(validationError.Message));

        Assert.Equal(nameof(Number.Value), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThanOrEqual, validationError.Reason);
        Assert.Equal(ValidationErrorReferenceType.IntValue, validationError.Reference.ReferenceType);
        var validationErrorIntValueReference = Assert.IsType<ValidationErrorIntValueReference>(validationError.Reference);
        Assert.Equal(0, validationErrorIntValueReference.Value);
    }

    [Fact]
    public void WhenValidatingNumberWithoutValueForPrescription_ReturnsValidationError()
    {
        // Arrange
        var number = new Number
                     {
                         Value = null
                     };

        // Act
        var validationErrors = number.GetValidationErrors(new ValidationContext {MedicationType = MedicationType.Prescription})
                                     .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Number.Value), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    #endregion

    #endregion
}