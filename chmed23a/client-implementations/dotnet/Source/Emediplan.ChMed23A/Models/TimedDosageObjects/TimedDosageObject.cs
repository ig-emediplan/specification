using System.Collections.Generic;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.TimedDosageObjects
{
    /// <summary>
    ///     Abstract base object specifying a timed dosage.
    /// </summary>
    /// <seealso cref="ValidatableBase" />
    public abstract class TimedDosageObject : ValidatableBase
    {
        #region Public Properties

        /// <summary>
        ///     The <see cref="TimedDosageType" /> being handled by the <see cref="TimedDosageObject" />.
        /// </summary>
        [JsonProperty(JsonPropertyNames.ObjectType)]
        public abstract TimedDosageType Type { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Validates the <see cref="PosologyDetailObject" />. If the returned list is not empty, validation errors have
        ///     occurred.
        /// </summary>
        /// <param name="validationContext"></param>
        public abstract override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext);

        #endregion
    }
}