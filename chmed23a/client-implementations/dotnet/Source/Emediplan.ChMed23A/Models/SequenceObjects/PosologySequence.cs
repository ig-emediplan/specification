using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.SequenceObjects
{
    /// <summary>
    ///     Sequence object containing a posology.
    /// </summary>
    /// <seealso cref="SequenceObject" />
    public class PosologySequence : SequenceObject
    {
        #region Public Properties

        public override SequenceObjectType Type => SequenceObjectType.Posology;

        /// <summary>
        ///     The posology object.
        /// </summary>
        [JsonProperty(JsonPropertyNames.SequenceObject.PosologySequence.PosologyDetailObject)]
        public PosologyDetailObject PosologyDetailObject { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext) =>
            GetValidationErrors(PosologyDetailObject, validationContext, nameof(PosologyDetailObject), JsonPropertyNames.SequenceObject.PosologySequence.PosologyDetailObject)
                .Concat(GetMyValidationErrors())
                .Concat(base.GetValidationErrors(validationContext));

        #endregion

        #region Private Methods

        private IEnumerable<ValidationError> GetMyValidationErrors()
        {
            if (PosologyDetailObject == null)
            {
                yield return new ValidationError("The posology detail object must be set",
                                                 nameof(PosologyDetailObject),
                                                 JsonPropertyNames.SequenceObject.PosologySequence.PosologyDetailObject,
                                                 ValidationErrorReason.IsRequired);
            }
        }

        #endregion
    }
}