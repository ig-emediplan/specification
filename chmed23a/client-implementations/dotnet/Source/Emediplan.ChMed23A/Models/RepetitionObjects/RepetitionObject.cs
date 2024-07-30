using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Serialization.Constants;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.RepetitionObjects
{
    /// <summary>
    ///     Abstract base class specifying a repetition object.
    /// </summary>
    /// <seealso cref="ValidatableBase" />
    public abstract class RepetitionObject : ValidatableBase
    {
        #region Public Properties

        /// <summary>
        ///     The <see cref="PosologyObjectType" /> being handled by the <see cref="RepetitionObject" />.
        /// </summary>
        [JsonProperty(JsonPropertyNames.ObjectType)]
        public abstract RepetitionObjectType Type { get; }

        #endregion
    }
}