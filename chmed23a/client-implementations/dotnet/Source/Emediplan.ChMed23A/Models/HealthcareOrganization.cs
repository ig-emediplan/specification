using System.Collections.Generic;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.Validators;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models
{
    public class HealthcareOrganization : ValidatableBase
    {
        /// <summary>
        ///     GLN of a person or organization.
        /// </summary>
        [JsonProperty(JsonPropertyNames.HealthcareOrganization.Gln)]
        public string Gln { get; set; }

        /// <summary>
        ///     The name of the organization.
        /// </summary>
        [JsonProperty(JsonPropertyNames.HealthcareOrganization.Name)]
        public string Name { get; set; }

        /// <summary>
        ///     The street.
        /// </summary>
        [JsonProperty(JsonPropertyNames.HealthcareOrganization.Street)]
        public string Street { get; set; }

        /// <summary>
        ///     The ZIP code.
        /// </summary>
        [JsonProperty(JsonPropertyNames.HealthcareOrganization.Zip)]
        public string Zip { get; set; }

        /// <summary>
        ///     The city.
        /// </summary>
        [JsonProperty(JsonPropertyNames.HealthcareOrganization.City)]
        public string City { get; set; }

        /// <summary>
        ///     The country represented by its two letter code.
        /// </summary>
        [JsonProperty(JsonPropertyNames.HealthcareOrganization.Country)]
        public string Country { get; set; }

        /// <summary>
        ///     The ZSR.
        /// </summary>
        [JsonProperty(JsonPropertyNames.HealthcareOrganization.Zsr)]
        public string Zsr { get; set; }

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                yield return new ValidationError("The name must be set.",
                                                 nameof(Name),
                                                 JsonPropertyNames.HealthcareOrganization.Name,
                                                 ValidationErrorReason.IsRequired);
            }

            if (string.IsNullOrWhiteSpace(Street))
            {
                yield return new ValidationError("The street must be set.",
                                                 nameof(Street),
                                                 JsonPropertyNames.HealthcareOrganization.Street,
                                                 ValidationErrorReason.IsRequired);
            }

            if (string.IsNullOrWhiteSpace(Zip))
            {
                yield return new ValidationError("The ZIP must be set.",
                                                 nameof(Zip),
                                                 JsonPropertyNames.HealthcareOrganization.Zip,
                                                 ValidationErrorReason.IsRequired);
            }

            if (string.IsNullOrWhiteSpace(City))
            {
                yield return new ValidationError("The city must be set.",
                                                 nameof(City),
                                                 JsonPropertyNames.HealthcareOrganization.City,
                                                 ValidationErrorReason.IsRequired);
            }

            if (!string.IsNullOrWhiteSpace(Gln) && !new GlnValidator().Validate(Gln))
            {
                yield return new ValidationError("The format of the gln is not valid.",
                                                 nameof(Gln),
                                                 JsonPropertyNames.HealthcarePerson.Gln,
                                                 ValidationErrorReason.NotSupported);
            }
        }
    }
}