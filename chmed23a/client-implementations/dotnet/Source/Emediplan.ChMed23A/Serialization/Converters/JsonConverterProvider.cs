using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Serialization.Converters
{
    internal static class JsonConverterProvider
    {
        /// <summary>
        ///     Gets all required JSON converters used by Newtonsoft.Json to serialize a ChMed23A JSON.
        /// </summary>
        /// <returns>Array containing all required converters to serialize a ChMed23A JSON.</returns>
        public static JsonConverter[] GetChMed23ASerializerJsonConverters() =>
            new JsonConverter[]
            {
                new DoubleJsonConverter()
            };
    }
}