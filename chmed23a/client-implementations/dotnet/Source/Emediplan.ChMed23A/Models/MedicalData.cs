using System;
using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Serialization.Converters;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models
{
    /// <summary>
    ///     Medical data of a patient.
    /// </summary>
    public class MedicalData : ValidatableBase
    {
        #region Public Properties

        /// <summary>
        ///     Only if risks Id 74,75,76 in risk group 3 First day of last menstruation.
        /// </summary>
        [JsonConverter(typeof(DateTimeJsonConverter), "yyyy-MM-dd")]
        [JsonProperty(JsonPropertyNames.MedicalData.LastMenstruationDate)]
        public DateTime? LastMenstruationDate { get; set; }

        /// <summary>
        ///     <c>True</c> if is a premature baby; <c>false</c> otherwise (only if age &lt;= 18 months).
        /// </summary>
        [JsonProperty(JsonPropertyNames.MedicalData.IsPrematureInfant)]
        public bool? IsPrematureInfant { get; set; }

        /// <summary>
        ///     Time of gestation in days (only if premature Baby == 1)
        /// </summary>
        [JsonConverter(typeof(TimeToGestationJsonConverter))]
        [JsonProperty(JsonPropertyNames.MedicalData.TimeToGestationDays)]
        public int? TimeToGestationDays { get; set; }

        /// <summary>
        ///     The risk categories.
        /// </summary>
        [JsonProperty(JsonPropertyNames.MedicalData.RiskCategories)]
        public IList<RiskCategory> RiskCategories { get; set; }

        /// <summary>
        ///     The weight in kilograms.
        /// </summary>
        [JsonProperty(JsonPropertyNames.MedicalData.WeightKg)]
        public double? WeightKg { get; set; }

        /// <summary>
        ///     The height in centimeters.
        /// </summary>
        [JsonProperty(JsonPropertyNames.MedicalData.HeightCm)]
        public double? HeightCm { get; set; }

        /// <summary>
        ///     The private fields.
        /// </summary>
        [JsonProperty(JsonPropertyNames.MedicalData.Extensions)]
        public IList<Extension> Extensions { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext) =>
            GetValidationErrors(Extensions, validationContext, nameof(Extensions), JsonPropertyNames.MedicalData.Extensions)
                .Concat(GetValidationErrors(RiskCategories, validationContext, nameof(RiskCategories), JsonPropertyNames.MedicalData.RiskCategories))
                .Concat(GetMyValidationErrors());

        #endregion

        #region Private Methods

        private IEnumerable<ValidationError> GetMyValidationErrors()
        {
            if (TimeToGestationDays < 0)
            {
                yield return new ValidationError("Time to gestation must be greater than 0, if set.",
                                                 nameof(TimeToGestationDays),
                                                 JsonPropertyNames.MedicalData.TimeToGestationDays,
                                                 ValidationErrorReason.MustBeGreaterThan,
                                                 new ValidationErrorIntValueReference(0));
            }

            if (WeightKg <= 0)
            {
                yield return new ValidationError("The weight must be greater than 0, if set.",
                                                 nameof(WeightKg),
                                                 JsonPropertyNames.MedicalData.WeightKg,
                                                 ValidationErrorReason.MustBeGreaterThan,
                                                 new ValidationErrorDoubleValueReference(0));
            }

            if (HeightCm <= 0)
            {
                yield return new ValidationError("The height must be greater than 0, if set.",
                                                 nameof(HeightCm),
                                                 JsonPropertyNames.MedicalData.HeightCm,
                                                 ValidationErrorReason.MustBeGreaterThan,
                                                 new ValidationErrorDoubleValueReference(0));
            }
        }

        #endregion
    }
}