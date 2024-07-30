using System.Collections.Generic;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.PosologyDetailObjects
{
    /// <summary>
    ///     Abstract base class specifying a posology object.
    /// </summary>
    /// <seealso cref="ValidatableBase" />
    public abstract class PosologyDetailObject : ValidatableBase
    {
        #region Public Properties

        /// <summary>
        ///     The <see cref="PosologyObjectType" /> being handled by the <see cref="PosologyDetailObject" />.
        /// </summary>
        [JsonProperty(JsonPropertyNames.ObjectType)]
        public abstract PosologyObjectType ObjectType { get; }

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