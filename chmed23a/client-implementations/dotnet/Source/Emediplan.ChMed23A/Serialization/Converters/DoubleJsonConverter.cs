using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Serialization.Converters
{
    /// <summary>
    ///     Serializes doubles as concise (e.g. '2' instead of '2.0') as possible and deserializes them.
    /// </summary>
    internal class DoubleJsonConverter : JsonConverter<double>
    {
        public override double ReadJson(JsonReader reader, Type objectType, double existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, double value, JsonSerializer serializer)
        {
            writer.WriteRawValue(value.ToString(CultureInfo.InvariantCulture));
        }
    }
}