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
    ///     A patient.
    /// </summary>
    public class Patient : ValidatableBase
    {
        #region Public Properties

        /// <summary>
        ///     The first name.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Patient.FirstName)]
        public string FirstName { get; set; }

        /// <summary>
        ///     The last name.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Patient.LastName)]
        public string LastName { get; set; }

        /// <summary>
        ///     Date of birth.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Patient.BirthDate)]
        [JsonConverter(typeof(DateTimeJsonConverter), "yyyy-MM-dd")]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        ///     The gender.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Patient.Gender)]
        public Gender? Gender { get; set; }

        /// <summary>
        ///     The street.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Patient.Street)]
        public string Street { get; set; }

        /// <summary>
        ///     The ZIP (postal) code.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Patient.Zip)]
        public string Zip { get; set; }

        /// <summary>
        ///     The city.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Patient.City)]
        public string City { get; set; }

        /// <summary>
        ///     The country (two Letter ISO3166 country code).
        /// </summary>
        [JsonProperty(JsonPropertyNames.Patient.Country)]
        public string Country { get; set; }

        /// <summary>
        ///     The language (ISO 639-1  language code).
        /// </summary>
        /// <example>de</example>
        [JsonProperty(JsonPropertyNames.Patient.Language)]
        public string Language { get; set; }

        /// <summary>
        ///     List of phone numbers.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Patient.Phones)]
        public IList<string> Phones { get; set; }

        /// <summary>
        ///     List of email addresses.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Patient.Emails)]
        public IList<string> Emails { get; set; }

        /// <summary>
        ///     The list of Patient Ids.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Patient.Ids)]
        public IList<PatientId> Ids { get; set; }

        /// <summary>
        ///     The private fields.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Patient.Extensions)]
        public IList<Extension> Extensions { get; set; }

        /// <summary>
        ///     Medical data containing information about the patients medical condition.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Patient.MedicalData)]
        public MedicalData MedicalData { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext) =>
            GetValidationErrors(Ids, validationContext, nameof(Ids), JsonPropertyNames.Patient.Ids)
                .Concat(GetValidationErrors(Extensions, validationContext, nameof(Extensions), JsonPropertyNames.Patient.Extensions))
                .Concat(GetValidationErrors(MedicalData, validationContext, nameof(MedicalData), JsonPropertyNames.Patient.MedicalData))
                .Concat(GetMyValidationErrors(validationContext));

        #endregion

        #region Private Methods

        private IEnumerable<ValidationError> GetMyValidationErrors(ValidationContext validationContext)
        {
            // Global validation
            if (string.IsNullOrEmpty(FirstName))
            {
                yield return new ValidationError("The first name must be set",
                                                 nameof(FirstName),
                                                 JsonPropertyNames.Patient.FirstName,
                                                 ValidationErrorReason.IsRequired);
            }

            if (string.IsNullOrEmpty(LastName))
            {
                yield return new ValidationError("The last name must be set",
                                                 nameof(LastName),
                                                 JsonPropertyNames.Patient.LastName,
                                                 ValidationErrorReason.IsRequired);
            }

            if (BirthDate == null)
            {
                yield return new ValidationError("The birth date must be set",
                                                 nameof(BirthDate),
                                                 JsonPropertyNames.Patient.BirthDate,
                                                 ValidationErrorReason.IsRequired);
            }

            if (Gender == null)
            {
                yield return new ValidationError("The gender must be set",
                                                 nameof(Gender),
                                                 JsonPropertyNames.Patient.Gender,
                                                 ValidationErrorReason.IsRequired);
            }

            if (Ids == null)
            {
                yield return new ValidationError("The ids must contain at least 1 element",
                                                 nameof(Ids),
                                                 JsonPropertyNames.Patient.Ids,
                                                 ValidationErrorReason.IsRequired);
            }
            else if (!Ids.Any())
            {
                yield return new ValidationError("The ids must contain at least 1 element",
                                                 nameof(Ids),
                                                 JsonPropertyNames.Patient.Ids,
                                                 ValidationErrorReason.ArrayLengthMustBe,
                                                 new ValidationErrorIntRangeReference(1, int.MaxValue));
            }

            if (!string.IsNullOrWhiteSpace(Country) && Country.Length != 2)
            {
                yield return new ValidationError("If set, the optional country code must have a length of two characters.",
                                                 nameof(Country),
                                                 JsonPropertyNames.Patient.Country,
                                                 ValidationErrorReason.MustHaveLength,
                                                 new ValidationErrorIntValueReference(2));
            }

            // Medication plan validation
            if (validationContext.MedicationType == MedicationType.MedicationPlan)
            {
                if (string.IsNullOrWhiteSpace(Language))
                {
                    yield return new ValidationError("The language must be set for a medication plan",
                                                     nameof(Language),
                                                     JsonPropertyNames.Patient.Language,
                                                     ValidationErrorReason.IsRequired);
                }
            }
        }

        #endregion
    }
}