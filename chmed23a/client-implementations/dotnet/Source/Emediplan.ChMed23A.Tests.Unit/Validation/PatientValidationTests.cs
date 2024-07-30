using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation;

public class PatientValidationTests : ValidationTestsBase
{
    #region Constructors

    public PatientValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMinimalPatient_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var patient = GetMinimalValidPatient(medicationType);

        // Act
        var validationErrors = patient.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingPatientWithoutFirstName_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var patient = GetMinimalValidPatient(medicationType);
        patient.FirstName = null;

        // Act
        var validationErrors = patient.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                      .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Patient.FirstName), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPatientWithoutLastName_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var patient = GetMinimalValidPatient(medicationType);
        patient.LastName = null;

        // Act
        var validationErrors = patient.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                      .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Patient.LastName), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPatientWithoutBirthDate_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var patient = GetMinimalValidPatient(medicationType);
        patient.BirthDate = null;

        // Act
        var validationErrors = patient.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                      .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Patient.BirthDate), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPatientWithoutGender_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var patient = GetMinimalValidPatient(medicationType);
        patient.Gender = null;

        // Act
        var validationErrors = patient.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                      .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Patient.Gender), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPatientWithNullIds_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var patient = GetMinimalValidPatient(medicationType);
        patient.Ids = null;

        // Act
        var validationErrors = patient.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                      .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Patient.Ids), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPatientWithEmptyIds_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var patient = GetMinimalValidPatient(medicationType);
        patient.Ids = new List<PatientId>();

        // Act
        var validationErrors = patient.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                      .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Patient.Ids), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.ArrayLengthMustBe, validationError.Reason);
        Assert.Equal(ValidationErrorReferenceType.IntRange, validationError.Reference.ReferenceType);
        var validationErrorIntRangeReference = Assert.IsType<ValidationErrorIntRangeReference>(validationError.Reference);
        Assert.Equal(1, validationErrorIntRangeReference.ValueFrom);
        Assert.Equal(int.MaxValue, validationErrorIntRangeReference.ValueTo);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPatientWithInvalidIds_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var patient = GetMinimalValidPatient(medicationType);

        patient.Ids = new List<PatientId>
                      {
                          new()
                          {
                              Type = PatientIdType.LocalPid,
                              SystemIdentifier = null, // Invalid
                              Value = "42"
                          }
                      };

        // Act
        var validationErrors = patient.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                      .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal($"{nameof(Patient.Ids)}[0].{nameof(PatientId.SystemIdentifier)}", validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPatientWithInvalidExtensions_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var patient = GetMinimalValidPatient(medicationType);

        patient.Extensions = new List<Extension>
                             {
                                 new()
                                 {
                                     Schema = "test",
                                     Name = null // Invalid
                                 }
                             };

        // Act
        var validationErrors = patient.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                      .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal($"{nameof(Patient.Extensions)}[0].{nameof(Extension.Name)}", validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingPatientWithInvalidMedicalData_ReturnsValidationError(MedicationType medicationType)
    {
        // Arrange
        var patient = GetMinimalValidPatient(medicationType);

        patient.MedicalData = new MedicalData
                              {
                                  HeightCm = -50 // Invalid
                              };

        // Act
        var validationErrors = patient.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                      .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal($"{nameof(Patient.MedicalData)}.{nameof(MedicalData.HeightCm)}", validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustBeGreaterThan, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan, "c")]
    [InlineData(MedicationType.Prescription, "c")]
    [InlineData(MedicationType.MedicationPlan, "cha")]
    [InlineData(MedicationType.Prescription, "cha")]
    public void WhenValidatingPatientWithInvalidCounty_ReturnsValidationError(MedicationType medicationType, string country)
    {
        // Arrange
        var patient = GetMinimalValidPatient(medicationType);
        patient.Country = country;

        // Act
        var validationErrors = patient.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                      .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.False(string.IsNullOrEmpty(validationError.Message));
        Assert.Equal(nameof(Patient.Country), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.MustHaveLength, validationError.Reason);
        var intValueReference = Assert.IsType<ValidationErrorIntValueReference>(validationError.Reference);
        Assert.Equal(2, intValueReference.Value);
    }

    #endregion

    #endregion
}