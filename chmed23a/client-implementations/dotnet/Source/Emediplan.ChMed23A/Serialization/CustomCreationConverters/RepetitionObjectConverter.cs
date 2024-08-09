using System;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Models.RepetitionObjects;
using Emediplan.ChMed23A.Serialization.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Emediplan.ChMed23A.Serialization.CustomCreationConverters
{
    /// <summary>
    ///     Instantiates the correct implementation fo the abstract <see cref="RepetitionObject" /> class according to the
    ///     specified <see cref="RepetitionObject.Type" />; if <see cref="RepetitionObject" /> is <c>null</c> (and thus
    ///     <see cref="RepetitionObjectType" /> cannot be determined), <c>null</c> will be returned.
    /// </summary>
    internal class RepetitionObjectConverter : CustomCreationConverter<RepetitionObject>
    {
        private RepetitionObjectType? _repetitionType;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jToken = JToken.ReadFrom(reader);
            // ReSharper disable once PossibleNullReferenceException
            _repetitionType = jToken[JsonPropertyNames.ObjectType]?.ToObject<RepetitionObjectType>();
            return base.ReadJson(jToken.CreateReader(), objectType, existingValue, serializer);
        }

        public override RepetitionObject Create(Type objectType)
        {
            switch (_repetitionType)
            {
                case null: return null;
                case RepetitionObjectType.Number: return new Number();
                case RepetitionObjectType.Duration: return new Duration();
                case RepetitionObjectType.NumberAndDuration: return new NumberAndDuration();
                default: throw new NotSupportedException($"{nameof(RepetitionObjectType)}={_repetitionType} is not supported");
            }
        }
    }
}