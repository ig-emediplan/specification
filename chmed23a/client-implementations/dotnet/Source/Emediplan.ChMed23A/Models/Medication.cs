using System;
using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Serialization.Converters;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models
{
    /// <summary>
    ///     A medication.
    /// </summary>
    public class Medication : ValidatableBase
    {
        #region Public Properties

        /// <summary>
        ///     The patient.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Medication.Patient)]
        public Patient Patient { get; set; }

        /// <summary>
        ///     The list of medicaments.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Medication.Medicaments)]
        public IList<Medicament> Medicaments { get; set; }

        /// <summary>
        ///     The type of the medication.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Medication.MedType)]
        public MedicationType? MedType { get; set; }

        /// <summary>
        ///     The id of the medication object.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Medication.Id)]
        public string Id { get; set; }

        /// <summary>
        ///     The author (Gln if available, otherwise name).
        /// </summary>
        [JsonProperty(JsonPropertyNames.Medication.Author)]
        public MedicationAuthor? Author { get; set; }

        /// <summary>
        ///     Date of creation.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Medication.CreationDate)]
        [JsonConverter(typeof(DateTimeJsonConverter), "O")]
        public DateTimeOffset? CreationDate { get; set; }

        /// <summary>
        ///     The remark.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Medication.Remark)]
        public string Remark { get; set; }

        /// <summary>
        ///     The GLN or email of the receiver.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Medication.ReceiverGln)]
        public string ReceiverGln { get; set; }

        /// <summary>
        ///     The health care person having created the medication.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Medication.HealthcarePerson)]
        public HealthcarePerson HealthcarePerson { get; set; }

        /// <summary>
        ///     The health care organization having created the medication.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Medication.HealthcareOrganization)]
        public HealthcareOrganization HealthcareOrganization { get; set; }

        /// <summary>
        ///     The list of Private fields.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Medication.Extensions)]
        public IList<Extension> Extensions { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext) =>
            GetValidationErrors(Medicaments, validationContext, nameof(Medicaments), JsonPropertyNames.Medication.Medicaments)
                .Concat(GetValidationErrors(Extensions, validationContext, nameof(Extensions), JsonPropertyNames.Medication.Extensions))
                .Concat(GetValidationErrors(Patient, validationContext, nameof(Patient), JsonPropertyNames.Medication.Patient))
                .Concat(GetValidationErrors(HealthcarePerson, validationContext, nameof(HealthcarePerson), JsonPropertyNames.Medication.HealthcarePerson))
                .Concat(GetValidationErrors(HealthcareOrganization, validationContext, nameof(HealthcareOrganization), JsonPropertyNames.Medication.HealthcareOrganization))
                .Concat(GetMyValidationErrors(validationContext));

        #endregion

        #region Private Methods

        private IEnumerable<ValidationError> GetMyValidationErrors(ValidationContext validationContext)
        {
            // Global validation
            if (Patient == null)
            {
                yield return new ValidationError("The patient must be set",
                                                 nameof(Patient),
                                                 JsonPropertyNames.Medication.Patient,
                                                 ValidationErrorReason.IsRequired);
            }

            if (Author == MedicationAuthor.HealthcarePerson && HealthcarePerson == null)
            {
                yield return new ValidationError("The healthcare person must be set when the author is a health care person",
                                                 nameof(HealthcarePerson),
                                                 JsonPropertyNames.Medication.HealthcarePerson,
                                                 ValidationErrorReason.IsRequired);
            }

            if (MedType == null)
            {
                yield return new ValidationError("The medication type must be set",
                                                 nameof(MedType),
                                                 JsonPropertyNames.Medication.MedType,
                                                 ValidationErrorReason.IsRequired);
            }

            if (Author == null)
            {
                yield return new ValidationError("The author must be set",
                                                 nameof(Author),
                                                 JsonPropertyNames.Medication.Author,
                                                 ValidationErrorReason.IsRequired);
            }
            else if (!Enum.IsDefined(typeof(MedicationAuthor), Author))
            {
                var enumValues = (MedicationAuthor[])Enum.GetValues(typeof(MedicationAuthor));
                var enumValuesString = string.Join(", ", enumValues.Select(val => $"{(int)val} ({val})"));

                yield return new ValidationError($"The type '{Author}' is not supported. Possible values: {enumValuesString}",
                                                 nameof(Author),
                                                 JsonPropertyNames.Medication.Author,
                                                 ValidationErrorReason.NotSupported);
            }

            if (CreationDate == null)
            {
                yield return new ValidationError("The creation date must be set",
                                                 nameof(CreationDate),
                                                 JsonPropertyNames.Medication.CreationDate,
                                                 ValidationErrorReason.IsRequired);
            }

            if (!string.IsNullOrWhiteSpace(HealthcarePerson?.Zsr)
             && !string.IsNullOrWhiteSpace(HealthcareOrganization?.Zsr))
            {
                yield return new ValidationError($"The ZSR can only be set once for either the {nameof(HealthcarePerson)} OR {nameof(HealthcareOrganization)}",
                                                 $"{nameof(HealthcarePerson)}.{nameof(HealthcarePerson.Zsr)}",
                                                 $"{JsonPropertyNames.Medication.HealthcarePerson}.{JsonPropertyNames.HealthcarePerson.Zsr}",
                                                 ValidationErrorReason.NotSupported);
            }

            // Medication plan specific validations
            if (validationContext.MedicationType == MedicationType.MedicationPlan)
            {
                if (Author != MedicationAuthor.Patient
                 && string.IsNullOrWhiteSpace(HealthcarePerson?.Gln)
                 && string.IsNullOrWhiteSpace(HealthcareOrganization?.Gln))
                {
                    yield return new ValidationError("For a medication plan, where the patient isn't the author, "
                                                   + $"the GLN must be set on either the {nameof(HealthcarePerson)} OR {nameof(HealthcareOrganization)}",
                                                     $"{nameof(HealthcarePerson)}.{nameof(HealthcarePerson.Gln)}",
                                                     $"{JsonPropertyNames.Medication.HealthcarePerson}.{JsonPropertyNames.HealthcarePerson.Gln}",
                                                     ValidationErrorReason.NotSupported);
                }
            }

            // Prescription specific validations
            if (validationContext.MedicationType == MedicationType.Prescription)
            {
                if (Medicaments == null || !Medicaments.Any())
                {
                    yield return new ValidationError("At least one medicament must be set for a prescription",
                                                     nameof(Medicaments),
                                                     JsonPropertyNames.Medication.Medicaments,
                                                     ValidationErrorReason.ArrayLengthMustBe,
                                                     new ValidationErrorIntRangeReference(1, int.MaxValue));
                }

                if (Author == MedicationAuthor.Patient)
                {
                    yield return new ValidationError("The patient can't be the author of a prescription",
                                                     nameof(Author),
                                                     JsonPropertyNames.Medication.Author,
                                                     ValidationErrorReason.NotSupported);
                }
            }
        }

        #endregion
    }
}