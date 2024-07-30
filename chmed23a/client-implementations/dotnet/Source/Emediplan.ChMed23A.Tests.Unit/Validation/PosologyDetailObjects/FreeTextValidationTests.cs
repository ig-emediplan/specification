using System.Linq;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Validation;
using Xunit.Abstractions;

// ReSharper disable StringLiteralTypo

namespace Emediplan.ChMed23A.Tests.Unit.Validation.PosologyDetailObjects;

public class FreeTextValidationTests : ValidationTestsBase
{
    #region Constructors

    public FreeTextValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidFreeText_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var freeText = GetMinimalValidFreeText();

        // Act
        var validationErrors = freeText.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingFreeTextWithEmptyText_ReturnsValidationErrors(string text, MedicationType medicationType)
    {
        // Arrange
        var freeText = new FreeText
                       {
                           Text = text
                       };

        // Act
        var validationErrors = freeText.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                       .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(FreeText.Text), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    #endregion

    #endregion
}