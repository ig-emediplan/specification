using System;
using System.Linq;
using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation;

public class PosologyValidationTests : ValidationTestsBase
{
    #region Constructors

    public PosologyValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidPosology_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var posology = GetMinimalValidPosology(medicationType);

        // Act
        var validationErrors = posology.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingPosologyWithoutPosologyDetailObject_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var posology = GetMinimalValidPosology(medicationType);
        posology.PosologyDetailObject = null;

        // Act
        var validationErrors = posology.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Posology.PosologyDetailObject), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPosologyWithInvalidPosologyDetailObject_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var posology = GetMinimalValidPosology(medicationType);

        posology.PosologyDetailObject = new FreeText
                                        {
                                            Text = null // invalid
                                        };

        // Act
        var validationErrors = posology.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal($"{nameof(Posology.PosologyDetailObject)}.{nameof(FreeText.Text)}", validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPosologyWithInvalidRelativeToMeal_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var posology = GetMinimalValidPosology(medicationType);
        posology.RelativeToMeal = (RelativeToMeal)52;

        // Act
        var validationErrors = posology.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Posology.RelativeToMeal), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.NotSupported, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPosologyWithToDateBeforeFromDate_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var posology = GetMinimalValidPosology(medicationType);
        posology.FromDate = new DateTime(2023, 2, 1);
        posology.ToDate = new DateTime(2023, 1, 1);

        // Act
        var validationErrors = posology.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Posology.ToDate), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThanOrEqual, validationError.Reason);
        Assert.Equal(ValidationErrorReferenceType.DateTimeValue, validationError.Reference.ReferenceType);
        var validationErrorIntValueReference = Assert.IsType<ValidationErrorDateTimeValueReference>(validationError.Reference);
        Assert.Equal(posology.FromDate, validationErrorIntValueReference.Value);
    }

    #endregion

    #endregion
}