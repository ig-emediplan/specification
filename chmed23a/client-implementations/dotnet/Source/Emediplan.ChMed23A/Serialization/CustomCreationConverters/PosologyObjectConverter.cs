using System;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Serialization.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Single = Emediplan.ChMed23A.Models.PosologyDetailObjects.Single;

namespace Emediplan.ChMed23A.Serialization.CustomCreationConverters
{
    /// <summary>
    ///     Instantiates the correct implementation fo the abstract <see cref="PosologyDetailObject" /> class according to the
    ///     specified <see cref="PosologyDetailObject.ObjectType" />.
    /// </summary>
    internal class PosologyDetailObjectConverter : CustomCreationConverter<PosologyDetailObject>
    {
        private PosologyObjectType _posologyObjectType;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jToken = JToken.ReadFrom(reader);
            // ReSharper disable once PossibleNullReferenceException
            _posologyObjectType = jToken[JsonPropertyNames.ObjectType].ToObject<PosologyObjectType>();
            return base.ReadJson(jToken.CreateReader(), objectType, existingValue, serializer);
        }

        public override PosologyDetailObject Create(Type objectType)
        {
            switch (_posologyObjectType)
            {
                case PosologyObjectType.FreeText: return new FreeText();
                case PosologyObjectType.SingleApplication: return new Single();
                case PosologyObjectType.Cyclic: return new Cyclic();
                case PosologyObjectType.Daily: return new Daily();
                case PosologyObjectType.Sequence: return new Sequence();
                default: throw new NotSupportedException($"{nameof(PosologyObjectType)}={_posologyObjectType} is not supported");
            }
        }
    }
}