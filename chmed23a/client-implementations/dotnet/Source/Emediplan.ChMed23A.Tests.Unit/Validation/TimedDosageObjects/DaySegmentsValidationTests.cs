using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.ApplicationObjects;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Emediplan.ChMed23A.Validation;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation.TimedDosageObjects;

public class DaySegmentsValidationTests : ValidationTestsBase
{
    #region Constructors

    public DaySegmentsValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidDaySegments_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var daySegments = GetMinimalValidDaySegments();

        // Act
        var validationErrors = daySegments.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingDaySegmentsWithoutApplications_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var daySegments = GetMinimalValidDaySegments();
        daySegments.Applications = null;

        // Act
        var validationErrors = daySegments.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                          .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(DaySegments.Applications), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingDaySegmentsWithEmptyApplications_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var daySegments = GetMinimalValidDaySegments();
        daySegments.Applications = new List<ApplicationInSegment>();

        // Act
        var validationErrors = daySegments.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                          .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(DaySegments.Applications), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingDaySegmentsWithInvalidApplication_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var daySegments = GetMinimalValidDaySegments();

        daySegments.Applications = new List<ApplicationInSegment>
                                   {
                                       new()
                                       {
                                           Segment = null, // invalid
                                           Dosage = GetMinimalValidDosageSimple()
                                       }
                                   };

        // Act
        var validationErrors = daySegments.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                          .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal($"{nameof(DaySegments.Applications)}[0].{nameof(ApplicationInSegment.Segment)}", validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    #endregion

    #endregion
}