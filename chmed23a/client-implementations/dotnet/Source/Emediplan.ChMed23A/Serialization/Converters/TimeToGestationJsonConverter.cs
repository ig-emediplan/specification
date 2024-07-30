using System;
using System.Linq;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Serialization.Converters
{
    /// <summary>
    ///     Converter used to serialize/deserialize the time to gestation property. In the model it is specified in total
    ///     number of days. In the JSON file it is being serialized as {nb_weeks}-{nb_days}. E.g. the value 40 will be
    ///     serialized as 5-1.
    /// </summary>
    /// <seealso cref="JsonConverter" />
    internal class TimeToGestationJsonConverter : JsonConverter
    {
        #region Private Fields

        private const char Delimiter = '-';

        #endregion

        #region Public Methods

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var intValue = (int)value;
            var timeToGestationString = $"{intValue / 7}{Delimiter}{intValue % 7}";
            writer.WriteValue(timeToGestationString);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (!(reader.Value is string timeToGestationString))
                return null;

            var timeToGestationStringElements = timeToGestationString.Split(Delimiter)
                                                                     .Select(int.Parse)
                                                                     .ToList();

            if (timeToGestationStringElements.Count != 2)
                return null;

            return 7 * timeToGestationStringElements[0] + timeToGestationStringElements[1];
        }

        public override bool CanConvert(Type objectType) => objectType == typeof(int);

        #endregion
    }
}