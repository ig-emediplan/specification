using System.Linq;
using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Validation;

public class MedicationValidationTests : ValidationTestsBase
{
    #region Constructors

    public MedicationValidationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region Positive

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMedicationWithMinimalPropertiesSet_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var medication = GetMinimalValidMedication(medicationType);

        // Act
        var validationErrors = medication.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                         .ToList();

        OutputErrors(validationErrors);

        // Assert
        Assert.Empty(validationErrors);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    public void WhenValidatingMedicationWithoutHealthcarePersonWhenAuthorIsPatient_ReturnsNoValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var medication = GetMinimalValidMedication(medicationType);
        medication.Author = MedicationAuthor.Patient;
        medication.HealthcarePerson = null;

        // Act
        var validationErrors = medication.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                         .ToList();

        OutputErrors(validationErrors);

        // Assert
        Assert.Empty(validationErrors);
    }

    [Fact]
    public void WhenValidatingMedicationWithoutMedicamentsForMedicationPlan_ReturnsNoValidationErrors()
    {
        // Arrange
        const MedicationType medicationType = MedicationType.MedicationPlan;
        var medication = GetMinimalValidMedication(medicationType);
        medication.Medicaments = null;

        // Act
        var validationErrors = medication.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                         .ToList();

        OutputErrors(validationErrors);

        // Assert
        Assert.Empty(validationErrors);
    }

    [Fact]
    public void WhenPatientIsMedicationPlanAuthor_ReturnsNoValidationErrors()
    {
        // Arrange
        var medicationType = MedicationType.MedicationPlan;
        var medication = GetMinimalValidMedication(medicationType);
        medication.Author = MedicationAuthor.Patient;

        // Act
        var validationErrors = medication.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
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
    public void WhenValidatingMedicationWithoutPatient_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var medication = GetMinimalValidMedication(medicationType);
        medication.Patient = null;

        // Act
        var validationErrors = medication.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                         .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal(nameof(Medication.Patient), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMedicationWithoutHealthcarePersonWhenAuthorIsHealthcarePerson_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var medication = GetMinimalValidMedication(medicationType);
        medication.Author = MedicationAuthor.HealthcarePerson;
        medication.HealthcarePerson = null;

        // Act
        var validationErrors = medication.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                         .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal(nameof(Medication.HealthcarePerson), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Fact]
    public void WhenValidatingMedicationWithoutMedicamentsForPrescription_ReturnsValidationErrors()
    {
        // Arrange
        const MedicationType medicationType = MedicationType.Prescription;
        var medication = GetMinimalValidMedication(medicationType);
        medication.Medicaments = null;

        // Act
        var validationErrors = medication.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                         .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal(nameof(Medication.Medicaments), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.ArrayLengthMustBe, validationError.Reason);
        var validationErrorIntValueReference = Assert.IsType<ValidationErrorIntRangeReference>(validationError.Reference);
        Assert.Equal(1, validationErrorIntValueReference.ValueFrom);
        Assert.Equal(int.MaxValue, validationErrorIntValueReference.ValueTo);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMedicationWithoutMedType_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var medication = GetMinimalValidMedication(medicationType);
        medication.MedType = null;

        // Act
        var validationErrors = medication.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                         .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal(nameof(Medication.MedType), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMedicationWithoutAuthor_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var medication = GetMinimalValidMedication(medicationType);
        medication.Author = null;

        // Act
        var validationErrors = medication.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                         .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal(nameof(Medication.Author), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMedicationWithInvalidAuthor_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var medication = GetMinimalValidMedication(medicationType);
        medication.Author = (MedicationAuthor)5634267;

        // Act
        var validationErrors = medication.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                         .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal(nameof(Medication.Author), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.NotSupported, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMedicationWithoutCreationDate_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var medication = GetMinimalValidMedication(medicationType);
        medication.CreationDate = null;

        // Act
        var validationErrors = medication.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                         .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal(nameof(Medication.CreationDate), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.IsRequired, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    public void WhenValidatingMedicationWithoutGlnForHealthcarePersonAndHealthcareOrganization_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var medication = GetMinimalValidMedication(medicationType);
        medication.HealthcarePerson.Gln = null;
        medication.HealthcareOrganization.Gln = null;

        // Act
        var validationErrors = medication.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                         .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal($"{nameof(HealthcarePerson)}.{nameof(HealthcarePerson.Gln)}", validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.NotSupported, validationError.Reason);
    }

    [Theory]
    [InlineData(MedicationType.MedicationPlan)]
    [InlineData(MedicationType.Prescription)]
    public void WhenValidatingMedicationWithZsrSetForHealthcarePersonAndHealthcareOrganization_ReturnsValidationErrors(MedicationType medicationType)
    {
        // Arrange
        var medication = GetMinimalValidMedication(medicationType);
        medication.HealthcarePerson.Zsr = "123";
        medication.HealthcareOrganization.Zsr = "456";

        // Act
        var validationErrors = medication.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                         .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal($"{nameof(HealthcarePerson)}.{nameof(HealthcarePerson.Zsr)}", validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.NotSupported, validationError.Reason);
    }

    [Fact]
    public void WhenPatientIsPrescriptionAuthor_ReturnsValidationErrors()
    {
        // Arrange
        var medicationType = MedicationType.Prescription;
        var medication = GetMinimalValidMedication(medicationType);
        medication.Author = MedicationAuthor.Patient;

        // Act
        var validationErrors = medication.GetValidationErrors(new ValidationContext {MedicationType = medicationType})
                                         .ToList();

        OutputErrors(validationErrors);

        // Assert
        var validationError = Assert.Single(validationErrors);
        Assert.Equal(nameof(Medication.Author), validationError.PropertyPath);
        Assert.Equal(ValidationErrorReason.NotSupported, validationError.Reason);
    }

    #endregion

    #endregion
}