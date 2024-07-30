using System;
using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models
{
    /// <summary>
    ///     A patient identifier.
    /// </summary>
    public class PatientId : ValidatableBase
    {
        #region Public Properties

        /// <summary>
        ///     The type of the patient Id.
        /// </summary>
        [JsonProperty(JsonPropertyNames.PatientId.Type)]
        public PatientIdType? Type { get; set; }

        /// <summary>
        ///     The system (e.g., OID, URL etc.) allowing to identify the Patient (system identifier). To be used only with Type
        ///     <see cref="PatientIdType.LocalPid" />.
        /// </summary>
        [JsonProperty(JsonPropertyNames.PatientId.SystemIdentifier)]
        public string SystemIdentifier { get; set; }

        /// <summary>
        ///     The value of the Id.
        /// </summary>
        [JsonProperty(JsonPropertyNames.PatientId.Value)]
        public string Value { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext)
        {
            if (Type == null)
            {
                yield return new ValidationError("The type must be set",
                                                 nameof(Type),
                                                 JsonPropertyNames.PatientId.Type,
                                                 ValidationErrorReason.IsRequired);
            }
            else if (!Enum.IsDefined(typeof(PatientIdType), (int)Type))
            {
                var patientIdEnumValues = (PatientIdType[])Enum.GetValues(typeof(PatientIdType));
                var patientIdEnumValuesString = string.Join(", ", patientIdEnumValues.Select(pId => $"{(int)pId} ({pId})"));

                yield return new ValidationError($"The type '{Type}' is not supported. Possible values: {patientIdEnumValuesString}",
                                                 nameof(Type),
                                                 JsonPropertyNames.PatientId.Type,
                                                 ValidationErrorReason.NotSupported);
            }

            if (Type == PatientIdType.LocalPid && string.IsNullOrWhiteSpace(SystemIdentifier))
            {
                yield return new ValidationError("The system identifier must be set",
                                                 nameof(SystemIdentifier),
                                                 JsonPropertyNames.PatientId.SystemIdentifier,
                                                 ValidationErrorReason.IsRequired);
            }

            if (string.IsNullOrWhiteSpace(Value))
            {
                yield return new ValidationError("The value must be set",
                                                 nameof(Value),
                                                 JsonPropertyNames.PatientId.Value,
                                                 ValidationErrorReason.IsRequired);
            }
        }

        #endregion
    }
}