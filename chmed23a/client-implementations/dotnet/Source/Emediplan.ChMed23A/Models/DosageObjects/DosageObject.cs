using System.Collections.Generic;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.DosageObjects
{
    /// <summary>
    ///     Abstract base class for dosage objects.
    /// </summary>
    /// <seealso cref="ValidatableBase" />
    public abstract class DosageObject : ValidatableBase
    {
        #region Public Properties

        /// <summary>
        ///     The <see cref="DosageType" /> being handled by the <see cref="DosageObject" />.
        /// </summary>
        [JsonProperty(JsonPropertyNames.ObjectType)]
        public abstract DosageType Type { get; }

        #endregion

        #region Public Methods

        public abstract override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext);

        #endregion
    }
}