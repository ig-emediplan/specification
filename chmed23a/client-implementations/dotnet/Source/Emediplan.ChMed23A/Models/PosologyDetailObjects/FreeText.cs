using System.Collections.Generic;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.PosologyDetailObjects
{
    /// <summary>
    ///     Specifies an unstructured posology consisting of free text.
    /// </summary>
    /// <seealso cref="PosologyDetailObject" />
    public class FreeText : PosologyDetailObject
    {
        #region Public Properties

        public override PosologyObjectType ObjectType => PosologyObjectType.FreeText;

        /// <summary>
        ///     The posology text.
        /// </summary>
        [JsonProperty(JsonPropertyNames.PosologyDetailObject.FreeText.Text)]
        public string Text { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                yield return new ValidationError("The text must be set",
                                                 nameof(Text),
                                                 JsonPropertyNames.PosologyDetailObject.FreeText.Text,
                                                 ValidationErrorReason.IsRequired);
            }
        }

        #endregion
    }
}