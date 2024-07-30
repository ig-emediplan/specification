using System;
using System.Collections.Generic;
using System.Linq;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Serialization.Constants;
using Emediplan.ChMed23A.Validation;
using Emediplan.ChMed23A.Validation.ValidationErrorReferences;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Models.PosologyDetailObjects
{
    /// <summary>
    ///     Specifies a daily posology with four possible intakes (morning, noon, evening, night).
    /// </summary>
    /// <seealso cref="PosologyDetailObject" />
    public class Daily : PosologyDetailObject
    {
        #region Private Fields

        private const int DosagesSize = 4;

        #endregion

        #region Public Properties

        public override PosologyObjectType ObjectType => PosologyObjectType.Daily;

        /// <summary>
        ///     Dosages. Must contain exactly four dosages for morning, midday, evening and night.
        /// </summary>
        [JsonProperty(JsonPropertyNames.PosologyDetailObject.Daily.Dosages)]
        public double[] Dosages { get; set; }

        #endregion

        #region Public Methods

        public override IEnumerable<ValidationError> GetValidationErrors(ValidationContext validationContext)
        {
            if (Dosages == null)
            {
                yield return new ValidationError($"The dosages must be set and contain {DosagesSize} elements.",
                                                 nameof(Dosages),
                                                 JsonPropertyNames.PosologyDetailObject.Daily.Dosages,
                                                 ValidationErrorReason.IsRequired);
            }
            else
            {
                if (Dosages.Length != DosagesSize)
                {
                    yield return new
                        ValidationError($"Dosages must contain exactly {DosagesSize} elements (for morning, midday, evening and night) but contained {Dosages.Length}.",
                                        nameof(Dosages),
                                        JsonPropertyNames.PosologyDetailObject.Daily.Dosages,
                                        ValidationErrorReason.ArrayLengthMustBe,
                                        new ValidationErrorIntValueReference(DosagesSize));
                }

                var negativeDosages = Dosages.Where(dosage => dosage < 0)
                                             .ToList();

                if (negativeDosages.Any())
                {
                    foreach (var negativeDosage in negativeDosages)
                    {
                        var dosageIndex = Array.IndexOf(Dosages, negativeDosage);

                        yield return new ValidationError("All dosages must be greater or equal to 0",
                                                         $"{nameof(Dosages)}[{dosageIndex}]",
                                                         $"{JsonPropertyNames.PosologyDetailObject.Daily.Dosages}[{dosageIndex}]",
                                                         ValidationErrorReason.MustBeGreaterThanOrEqual,
                                                         new ValidationErrorDoubleValueReference(0));
                    }
                }
            }
        }

        #endregion
    }
}