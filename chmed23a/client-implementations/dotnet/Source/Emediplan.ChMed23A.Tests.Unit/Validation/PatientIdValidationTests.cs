using System.Linq;
using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Validation;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation;

public class PatientIdValidationTests : ValidationTestsBase
{
    #region Constructors

    public PatientIdValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalValidPatientId_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var patientId = GetMinimalValidPatientId();

        // Act
        var validationErrors = patientId.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                        .ToList();

        OutputErrors(validationErrors);

        // Assert
        Assert.Empty(validationErrors);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPatientIdWithInsuranceCardNumberWithoutSystemIdentifier_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var patientId = new PatientId
                        {
                            Value = "MyValue",
                            Type = PatientIdType.InsuranceCardNumber
                        };

        // Act
        var validationErrors = patientId.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingPatientIdWithTypeLocalPidWithoutSystemIdentifier_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var patientId = new PatientId
                        {
                            Value = "MyValue",
                            Type = PatientIdType.LocalPid
                        };

        // Act
        var validationErrors = patientId.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                        .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
        Assert.Equal(nameof(PatientId.SystemIdentifier), validationError.PropertyPath);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPatientIdWithoutType_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var patientId = new PatientId
                        {
                            Value = "MyValue",
                            Type = null
                        };

        // Act
        var validationErrors = patientId.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                        .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
        Assert.Equal(nameof(PatientId.Type), validationError.PropertyPath);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan, null)]
    [InlineData(MedicationType.MedicationPlan, "")]
    [InlineData(MedicationType.Prescription, null)]
    [InlineData(MedicationType.Prescription, "")]
    public void WhenValidatingPatientIdWithoutValue_ReturnsValidationError(MedicationType medicationType, string? value)
    {
        // Arrange
        var patientId = new PatientId
                        {
                            Value = value,
                            SystemIdentifier = "sys-id",
                            Type = PatientIdType.LocalPid
                        };

        // Act
        var validationErrors = patientId.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                        .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
        Assert.Equal(nameof(PatientId.Value), validationError.PropertyPath);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPatientIdWithInvalidPatientIdType_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var patientId = new PatientId
                        {
                            Value = "MyValue",
                            Type = (PatientIdType)27
                        };

        // Act
        var validationErrors = patientId.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                        .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal(ValidationErrorReason.NotSupported, validationError.Reason);
        Assert.Equal(nameof(PatientId.Type), validationError.PropertyPath);
    }

    #endregion

    #endregion
}