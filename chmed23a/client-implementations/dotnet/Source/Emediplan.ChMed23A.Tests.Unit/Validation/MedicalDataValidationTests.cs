using System;
using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation;

public class MedicalDataValidationTests : ValidationTestsBase
{
    #region Constructors

    public MedicalDataValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidMedicalData_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var medicalData = GetMinimalValidMedicalData();

        // Act
        var validationErrors = medicalData.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                          .ToList();

        OutputErrors(validationErrors);

        // Assert
        Assert.Empty(validationErrors);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingFullMedicalData_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var medicalData = new MedicalData
                          {
                              IsPrematureInfant = false,
                              TimeToGestationDays = 42,
                              LastMenstruationDate = DateTime.Now.AddDays(-20),
                              WeightKg = 65,
                              Extensions = new[] {GetMinimalValidExtension()},
                              RiskCategories = new[] {GetMinimalValidRiskCategory()}
                          };

        // Act
        var validationErrors = medicalData.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingMedicalDataWithNegativeTimeToGestationDays_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var medicalData = GetMinimalValidMedicalData();
        medicalData.TimeToGestationDays = -1;

        // Act
        var validationErrors = medicalData.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                          .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(MedicalData.TimeToGestationDays), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThan, validationError.Reason);
        Assert.Equal(ValidationErrorReferenceType.IntValue, validationError.Reference.ReferenceType);
        var validationErrorIntValueReference = Assert.IsType<ValidationErrorIntValueReference>(validationError.Reference);
        Assert.Equal(0, validationErrorIntValueReference.Value);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMedicalDataWithNegativeWeightKg_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var medicalData = GetMinimalValidMedicalData();
        medicalData.WeightKg = -1;

        // Act
        var validationErrors = medicalData.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                          .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(MedicalData.WeightKg), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThan, validationError.Reason);
        Assert.Equal(ValidationErrorReferenceType.DoubleValue, validationError.Reference.ReferenceType);
        var validationErrorIntValueReference = Assert.IsType<ValidationErrorDoubleValueReference>(validationError.Reference);
        Assert.Equal(0, validationErrorIntValueReference.Value);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMedicalDataWithNegativeHeightCm_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var medicalData = GetMinimalValidMedicalData();
        medicalData.HeightCm = -1;

        // Act
        var validationErrors = medicalData.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                          .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(MedicalData.HeightCm), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThan, validationError.Reason);
        Assert.Equal(ValidationErrorReferenceType.DoubleValue, validationError.Reference.ReferenceType);
        var validationErrorIntValueReference = Assert.IsType<ValidationErrorDoubleValueReference>(validationError.Reference);
        Assert.Equal(0, validationErrorIntValueReference.Value);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMedicalDataWithInvalidExtension_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var medicalData = GetMinimalValidMedicalData();

        medicalData.Extensions = new List<Extension>
                                 {
                                     new()
                                     {
                                         Schema = "test",
                                         Name = null // Invalid
                                     }
                                 };

        // Act
        var validationErrors = medicalData.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                          .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal($"{nameof(MedicalData.Extensions)}[0].{nameof(Extension.Name)}", validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMedicalDataWithInvalidRiskCategory_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var medicalData = GetMinimalValidMedicalData();

        medicalData.RiskCategories = new List<RiskCategory>
                                     {
                                         new()
                                         {
                                             Id = null // Invalid
                                         }
                                     };

        // Act
        var validationErrors = medicalData.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                          .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal($"{nameof(MedicalData.RiskCategories)}[0].{nameof(RiskCategory.Id)}", validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    #endregion

    #endregion
}