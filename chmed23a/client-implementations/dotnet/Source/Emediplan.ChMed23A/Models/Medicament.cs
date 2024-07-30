using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Models.RepetitionObjects;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models
{
    /// <summary>
    ///     A medicament
    /// </summary>
    public class Medicament : ValidatableBase
    {
        #region Public Properties

        /// <summary>
        ///     The identifier.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Medicament.Id)]
        public string Id { get; set; }

        /// <summary>
        ///     The type of the identifier.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Medicament.IdType)]
        public MedicamentIdType? IdType { get; set; }

        /// <summary>
        ///     The posologies.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Medicament.Posologies)]
        public IList<Posology> Posologies { get; set; }

        /// <summary>
        ///     The taking reason.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Medicament.TakingReason)]
        public string TakingReason { get; set; }

        /// <summary>
        ///     Auto medication. <c>true</c> if it is an auto medication; otherwise <c>false</c>.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Medicament.IsAutoMedication)]
        public bool? IsAutoMedication { get; set; }

        /// <summary>
        ///     Prescribed by (the Gln or designation of doctor).
        /// </summary>
        [JsonProperty(JsonPropertyNames.Medicament.PrescribedBy)]
        public string PrescribedBy { get; set; }

        /// <summary>
        ///     Specifies the repetitions. (E.g. 4 times OR once per month)
        /// </summary>
        [JsonProperty(JsonPropertyNames.Medicament.Repetitions)]
        public RepetitionObject Repetitions { get; set; }

        /// <summary>
        ///     Specifies if a medicament is substitutable.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Medicament.IsNotSubstitutable)]
        public bool? IsNotSubstitutable { get; set; }

        /// <summary>
        ///     Sic erat scriptum (latin). Is intended to avoid misunderstandings between physician and pharmacist and indicates to
        ///     the pharmacist that the physician has deliberately chosen the prescription and wishes to prescribe the drug in
        ///     exactly this way and not otherwise
        /// </summary>
        [JsonProperty(JsonPropertyNames.Medicament.Sic)]
        public bool? Sic { get; set; }

        /// <summary>Number of packages to be delivered. Default : 1. </summary>
        [JsonProperty(JsonPropertyNames.Medicament.NumberOfPackages)]
        public double? NumberOfPackages { get; set; } = 1;

        /// <summary>
        ///     The list of private fields.
        /// </summary>
        [JsonProperty(JsonPropertyNames.Medicament.Extensions)]
        public IList<Extension> Extensions { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext) =>
            GetValidationErrors(Posologies, validationContext, nameof(Posologies), JsonPropertyNames.Medicament.Posologies)
                .Concat(GetValidationErrors(Repetitions, validationContext, nameof(Repetitions), JsonPropertyNames.Medicament.Repetitions))
                .Concat(GetValidationErrors(Extensions, validationContext, nameof(Extensions), JsonPropertyNames.Medicament.Extensions))
                .Concat(GetMyValidationErrors(validationContext));

        #endregion

        #region Private Methods

        private IEnumerable<ValidationError> GetMyValidationErrors(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                yield return new ValidationError("The id must be set",
                                                 nameof(Id),
                                                 JsonPropertyNames.Medicament.Id,
                                                 ValidationErrorReason.IsRequired);
            }

            if (IdType == null)
            {
                yield return new ValidationError("The id type must be set",
                                                 nameof(IdType),
                                                 JsonPropertyNames.Medicament.IdType,
                                                 ValidationErrorReason.IsRequired);
            }

            if (NumberOfPackages <= 0)
            {
                yield return new ValidationError("When specifying the number of packages, it must be greater than 0",
                                                 nameof(NumberOfPackages),
                                                 JsonPropertyNames.Medicament.NumberOfPackages,
                                                 ValidationErrorReason.MustBeGreaterThan,
                                                 new ValidationErrorDoubleValueReference(0));
            }

            if (validationContext.MedicationType == MedicationType.MedicationPlan)
            {
                if (IsAutoMedication == null)
                {
                    yield return new ValidationError("It must be indicated if it is an auto medication for a medication plan",
                                                     nameof(IsAutoMedication),
                                                     JsonPropertyNames.Medicament.IsAutoMedication,
                                                     ValidationErrorReason.IsRequired);
                }
            }
        }

        #endregion
    }
}