using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models
{
    /// <summary>
    ///     An extension which has a name and can have a value and additional, nested extensions.
    /// </summary>
    public class Extension : ValidatableBase
    {
        #region Public Propertis

        /// <summary>
        ///     The name of the private field. (required)
        /// </summary>
        [JsonProperty(JsonPropertyNames.Extension.Name)]
        public string Name { get; set; }

        /// <summary>
        ///     The value of the private field. (optional)
        /// </summary>
        [JsonProperty(JsonPropertyNames.Extension.Value)]
        public string Value { get; set; }

        /// <summary>
        ///     The schema this extension belongs to.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Extension.Schema)]
        public string Schema { get; set; }

        /// <summary>
        ///     A list of Private sub-fields.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Extension.Extensions)]
        public IList<Extension> Extensions { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext) =>
            GetValidationErrors(Extensions, validationContext, nameof(Extensions), JsonPropertyNames.Extension.Extensions)
                .Concat(GetMyValidationErrors());

        #endregion

        #region Private Methods

        private IEnumerable<ValidationError> GetMyValidationErrors()
        {
            if (string.IsNullOrEmpty(Name))
            {
                yield return new ValidationError("The name must be set",
                                                 nameof(Name),
                                                 JsonPropertyNames.Extension.Name,
                                                 ValidationErrorReason.IsRequired);
            }

            if (string.IsNullOrEmpty(Schema))
            {
                yield return new ValidationError("The schema must be set",
                                                 nameof(Schema),
                                                 JsonPropertyNames.Extension.Schema,
                                                 ValidationErrorReason.IsRequired);
            }
        }

        #endregion
    }
}