using System;
using System.Globalization;
using System.Linq;
using Emediplan.ChMed23A.Exceptions;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Serialization.Converters;
using Emediplan.ChMed23A.Serialization.CustomCreationConverters;
using Emediplan.ChMed23A.Validation;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Serialization
{
    public class ChMed23ASerializer : IChMed23ASerializer
    {
        public string Serialize<T>(T obj, bool prettyPrint = false, bool validate = false, MedicationType? medicationType = null)
            where T : class, IValidatable
        {
            ThrowValidationExceptionIfNotValid(validate, obj, medicationType);

            return JsonConvert.SerializeObject(obj,
                                               prettyPrint ? Formatting.Indented : Formatting.None,
                                               new JsonSerializerSettings
                                               {
                                                   NullValueHandling = NullValueHandling.Ignore,
                                                   DefaultValueHandling = DefaultValueHandling.Include,
                                                   Culture = CultureInfo.InvariantCulture,
                                                   Converters = JsonConverterProvider.GetChMed23ASerializerJsonConverters()
                                               });
        }

        public T Deserialize<T>(string jsonString, bool validate = false, MedicationType? medicationType = null)
            where T : class, IValidatable
        {
            var obj = JsonConvert.DeserializeObject<T>(jsonString, CustomCreationConverterProvider.GetChMed23ACustomCreationConverters());
            ThrowValidationExceptionIfNotValid(validate, obj, medicationType);
            return obj;
        }

        #region Private Methods

        private static void ThrowValidationExceptionIfNotValid<T>(bool validate, T validatable, MedicationType? medicationType)
            where T : class, IValidatable
        {
            if (!validate)
                return;

            if (medicationType == null)
                throw new NotSupportedException($"If validation has been enabled, {nameof(medicationType)} must be provided.");

            var validationErrors = validatable.GetValidationErrors(new ValidationContext {MedicationType = medicationType.Value})
                                              .ToList();

            if (validationErrors.Any())
                throw new ChMed23AValidationException(validationErrors);
        }

        #endregion
    }
}