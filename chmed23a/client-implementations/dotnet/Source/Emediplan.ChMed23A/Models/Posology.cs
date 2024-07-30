using System;
using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Serialization.Converters;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models
{
    /// <summary>
    ///     A posology describing how a medicament has to be taken.
    /// </summary>
    public class Posology : ValidatableBase
    {
        #region Public Properties

        /// <summary>
        ///     Date when the posology starts.
        /// </summary>
        [JsonConverter(typeof(DateTimeJsonConverter), "yyyy-MM-dd")]
        [JsonProperty(JsonPropertyNames.Posology.FromDate)]
        public DateTime? FromDate { get; set; }

        /// <summary>
        ///     Date when the posology ends. The to date has to be considered as inclusive.
        /// </summary>
        [JsonConverter(typeof(DateTimeJsonConverter), "yyyy-MM-dd")]
        [JsonProperty(JsonPropertyNames.Posology.ToDate)]
        public DateTime? ToDate { get; set; }

        /// <summary>
        ///     The posology object containing the properties for the specified posology type.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Posology.PosologyDetailObject)]
        public PosologyDetailObject PosologyDetailObject { get; set; }

        /// <summary>
        ///     Specifies if the medicament has to be taken before, during or after a meal.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Posology.RelativeToMeal)]
        public RelativeToMeal? RelativeToMeal { get; set; }

        /// <summary>
        ///     Specifies if the posology is a reserve having to be taken when needed.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Posology.InReserve)]
        public bool? InReserve { get; set; }

        /// <summary>
        ///     The route of administration. Possible values: https://fhir.ch/ig/ch-emed/ValueSet-edqm-routeofadministration.html
        ///     OR https://index.hcisolutions.ch/index/current/get.aspx?schema=CODE&keytype=CDTYP&key=61&xsl=table.xslt.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Posology.RouteOfAdministration)]
        public string RouteOfAdministration { get; set; }

        /// <summary>
        ///     The route of administration. Possible values: https://index.hcisolutions.ch/index/current/get.aspx?schema=CODE
        ///     &keytype=CDTYP&key=62&xsl=table.xslt.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Posology.MethodOfAdministration)]
        public string MethodOfAdministration { get; set; }

        /// <summary>
        ///     Gets or sets Unit. The quantity unit / Mandatory if pos is defined Possible values : codeType
        ///     9 in IndexProducts.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Posology.Unit)]
        public string Unit { get; set; }

        /// <summary>
        ///     The application instructions.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Posology.ApplicationInstructions)]
        public string ApplicationInstructions { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext) =>
            GetValidationErrors(PosologyDetailObject, validationContext, nameof(PosologyDetailObject), JsonPropertyNames.Posology.PosologyDetailObject)
                .Concat(GetMyValidationErrors(validationContext));

        #endregion

        #region Private Methods

        private IEnumerable<ValidationError> GetMyValidationErrors(ValidationContext validationContext)
        {
            if (PosologyDetailObject == null)
            {
                yield return new ValidationError("The posology object must be set",
                                                 nameof(PosologyDetailObject),
                                                 JsonPropertyNames.Posology.PosologyDetailObject,
                                                 ValidationErrorReason.IsRequired);
            }

            if (FromDate > ToDate)
            {
                yield return new ValidationError("The to date must be greater than the from date.",
                                                 nameof(ToDate),
                                                 JsonPropertyNames.Posology.ToDate,
                                                 ValidationErrorReason.MustBeGreaterThanOrEqual,
                                                 new ValidationErrorDateTimeValueReference(FromDate.Value));
            }

            if (RelativeToMeal != null && !Enum.IsDefined(typeof(RelativeToMeal), RelativeToMeal))
            {
                var enumValues = (RelativeToMeal[])Enum.GetValues(typeof(RelativeToMeal));
                var enumValuesString = string.Join(", ", enumValues.Select(val => $"{(int)val} ({val})"));

                yield return new ValidationError($"The type '{RelativeToMeal}' is not supported. Possible values: {enumValuesString}",
                                                 nameof(RelativeToMeal),
                                                 JsonPropertyNames.Posology.RelativeToMeal,
                                                 ValidationErrorReason.NotSupported);
            }

            if (validationContext.MedicationType == MedicationType.MedicationPlan)
            {
                if (string.IsNullOrEmpty(Unit))
                {
                    yield return new ValidationError("The unit must be set for a medication plan",
                                                     nameof(Unit),
                                                     JsonPropertyNames.Posology.Unit,
                                                     ValidationErrorReason.IsRequired);
                }
            }
        }

        #endregion
    }
}