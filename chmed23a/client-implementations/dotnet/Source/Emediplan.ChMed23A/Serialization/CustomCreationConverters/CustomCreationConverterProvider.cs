using Newtonsoft.Json;

namespace Emediplan.ChMed23A.Serialization.CustomCreationConverters
{
    internal static class CustomCreationConverterProvider
    {
        /// <summary>
        ///     Gets all required custom converters used by Newtonsoft.Json to deserialize a ChMed23A JSON.
        /// </summary>
        /// <returns>Array containing all required converters to deserialize a ChMed23A JSON.</returns>
        public static JsonConverter[] GetChMed23ACustomCreationConverters() =>
            new JsonConverter[]
            {
                new DosageObjectConverter(),
                new PosologyDetailObjectConverter(),
                new SequenceObjectConverter(),
                new TimedDosageObjectConverter(),
                new RepetitionObjectConverter()
            };
    }
}