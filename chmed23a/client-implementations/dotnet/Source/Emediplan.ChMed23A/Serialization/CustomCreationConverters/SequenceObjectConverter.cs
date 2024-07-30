using System;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Models.SequenceObjects;
using Emediplan.ChMed23A.Serialization.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Emediplan.ChMed23A.Serialization.CustomCreationConverters
{
    /// <summary>
    ///     Instantiates the correct implementation fo the abstract <see cref="SequenceObject" /> class according to the
    ///     specified <see cref="SequenceObject.Type" />.
    /// </summary>
    internal class SequenceObjectConverter : CustomCreationConverter<SequenceObject>
    {
        private SequenceObjectType _sequenceType;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jToken = JToken.ReadFrom(reader);
            // ReSharper disable once PossibleNullReferenceException
            _sequenceType = jToken[JsonPropertyNames.ObjectType].ToObject<SequenceObjectType>();
            return base.ReadJson(jToken.CreateReader(), objectType, existingValue, serializer);
        }

        public override SequenceObject Create(Type objectType)
        {
            switch (_sequenceType)
            {
                case SequenceObjectType.Pause: return new Pause();
                case SequenceObjectType.Posology: return new PosologySequence();
                default: throw new NotSupportedException($"{nameof(SequenceObjectType)}={_sequenceType} is not supported");
            }
        }
    }
}