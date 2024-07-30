using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Validation;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation;

public class RiskCategoryValidationTests : ValidationTestsBase
{
    #region Constructors

    public RiskCategoryValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidRiskCategory_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var riskCategory = GetMinimalValidRiskCategory();

        // Act
        var validationErrors = riskCategory.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                           .ToList();

        OutputErrors(validationErrors);

        // Assert
        Assert.Empty(validationErrors);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingRiskCategoryWithEmptyRiskIds_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var riskCategory = GetMinimalValidRiskCategory();
        riskCategory.RiskIds = new List<int>();

        // Act
        var validationErrors = riskCategory.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                           .ToList();

        OutputErrors(validationErrors);

        // Assert
        Assert.Empty(validationErrors);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingRiskCategoryWithRiskIds_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var riskCategory = GetMinimalValidRiskCategory();
        riskCategory.RiskIds = new[] {1, 5, 42};

        // Act
        var validationErrors = riskCategory.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingRiskCategoryWithoutId_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var riskCategory = GetMinimalValidRiskCategory();
        riskCategory.Id = null;

        // Act
        var validationErrors = riskCategory.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                           .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
        Assert.Equal(nameof(RiskCategory.Id), validationError.PropertyPath);
    }

    #endregion

    #endregion
}