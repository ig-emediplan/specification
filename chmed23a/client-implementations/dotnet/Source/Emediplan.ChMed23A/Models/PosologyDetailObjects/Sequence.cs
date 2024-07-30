using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Models.SequenceObjects;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.PosologyDetailObjects
{
    /// <summary>
    ///     Allows to specify a sequence of posologies like e.g. take for 2 days, then make a pause  of 1 day, then start over.
    /// </summary>
    /// <seealso cref="PosologyDetailObject" />
    public class Sequence : PosologyDetailObject
    {
        #region Public Properties

        public override PosologyObjectType ObjectType => PosologyObjectType.Sequence;

        /// <summary>
        ///     A list of sequence objects.
        /// </summary>
        [JsonProperty(JsonPropertyNames.PosologyDetailObject.Sequence.SequenceObjects)]
        public IList<SequenceObject> SequenceObjects { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext) =>
            GetMyValidationErrors()
                .Concat(GetValidationErrors(SequenceObjects, validationContext, nameof(SequenceObjects), JsonPropertyNames.PosologyDetailObject.Sequence.SequenceObjects));

        #endregion

        #region Private Methods

        private IEnumerable<ValidationError> GetMyValidationErrors()
        {
            if (SequenceObjects == null || !SequenceObjects.Any())
            {
                yield return new ValidationError("The sequence objects must be set.",
                                                 nameof(SequenceObjects),
                                                 JsonPropertyNames.PosologyDetailObject.Sequence.SequenceObjects,
                                                 ValidationErrorReason.IsRequired);
            }
        }

        #endregion
    }
}