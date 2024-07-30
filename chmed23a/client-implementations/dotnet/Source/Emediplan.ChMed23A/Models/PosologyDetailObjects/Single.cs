using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.PosologyDetailObjects
{
    /// <summary>
    ///     Specifies a single application.
    /// </summary>
    /// <seealso cref="PosologyDetailObject" />
    public class Single : PosologyDetailObject
    {
        #region Public Properties

        public override PosologyObjectType ObjectType => PosologyObjectType.SingleApplication;

        /// <summary>
        ///     The timed dosage specifying when the single taking has to occur.
        /// </summary>
        [JsonProperty(JsonPropertyNames.PosologyDetailObject.SingleApplication.TimedDosage)]
        public TimedDosageObject TimedDosage { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext) =>
            GetValidationErrors(TimedDosage, validationContext, nameof(TimedDosage), JsonPropertyNames.PosologyDetailObject.SingleApplication.TimedDosage)
                .Concat(GetMyValidationErrors());

        #endregion

        #region Private Methods

        private IEnumerable<ValidationError> GetMyValidationErrors()
        {
            switch (TimedDosage)
            {
                case Times _:
                case DosageOnly _:
                case DaySegments _:
                    // These are the supported timed dosages
                    // TODO PWA: Determine if we want to validate that Times and DaySegments must contain exactly one entry
                    break;

                case null:
                    yield return new ValidationError("The timed dosage must be set",
                                                     nameof(TimedDosage),
                                                     JsonPropertyNames.PosologyDetailObject.SingleApplication.TimedDosage,
                                                     ValidationErrorReason.IsRequired);

                    break;

                default:
                    yield return new ValidationError($"Timed dosage of type {TimedDosage.GetType().Name} is not supported.",
                                                     nameof(TimedDosage),
                                                     JsonPropertyNames.PosologyDetailObject.SingleApplication.TimedDosage,
                                                     ValidationErrorReason.NotSupported);

                    break;
            }
        }

        #endregion
    }
}