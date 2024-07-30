using System.Collections.Generic;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.Validators;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models
{
    public class HealthcarePerson : ValidatableBase
    {
        /// <summary>
        ///     GLN of a person or organization.
        /// </summary>
        [JsonProperty(JsonPropertyNames.HealthcarePerson.Gln)]
        public string Gln { get; set; }

        /// <summary>
        ///     The first name.
        /// </summary>
        [JsonProperty(JsonPropertyNames.HealthcarePerson.FirstName)]
        public string FirstName { get; set; }

        /// <summary>
        ///     The last name.
        /// </summary>
        [JsonProperty(JsonPropertyNames.HealthcarePerson.LastName)]
        public string LastName { get; set; }

        /// <summary>
        ///     The ZSR.
        /// </summary>
        [JsonProperty(JsonPropertyNames.HealthcarePerson.Zsr)]
        public string Zsr { get; set; }

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                yield return new ValidationError("The first name must be set.",
                                                 nameof(FirstName),
                                                 JsonPropertyNames.HealthcarePerson.FirstName,
                                                 ValidationErrorReason.IsRequired);
            }

            if (string.IsNullOrWhiteSpace(LastName))
            {
                yield return new ValidationError("The last name must be set.",
                                                 nameof(LastName),
                                                 JsonPropertyNames.HealthcarePerson.LastName,
                                                 ValidationErrorReason.IsRequired);
            }

            if (validationContext.MedicationType == MedicationType.Prescription)
            {
                if (string.IsNullOrWhiteSpace(Gln))
                {
                    yield return new ValidationError("The gln must be set.",
                                                     nameof(Gln),
                                                     JsonPropertyNames.HealthcarePerson.Gln,
                                                     ValidationErrorReason.IsRequired);
                }
                else if (!new GlnValidator().Validate(Gln))
                {
                    yield return new ValidationError("The format of the gln is not valid.",
                                                     nameof(Gln),
                                                     JsonPropertyNames.HealthcarePerson.Gln,
                                                     ValidationErrorReason.NotSupported);
                }
            }
        }
    }
}