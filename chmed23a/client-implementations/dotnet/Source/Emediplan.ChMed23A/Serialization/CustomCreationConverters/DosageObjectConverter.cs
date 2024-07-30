using System;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Serialization.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Emediplan.ChMed23A.Serialization.CustomCreationConverters
{
    /// <summary>
    ///     Instantiates the correct implementation fo the abstract <see cref="DosageObject" /> class according to the
    ///     specified <see cref="DosageObject.Type" />.
    /// </summary>
    internal class DosageObjectConverter : CustomCreationConverter<DosageObject>
    {
        private DosageType _dosageType;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jToken = JToken.ReadFrom(reader);
            // ReSharper disable once PossibleNullReferenceException
            _dosageType = jToken[JsonPropertyNames.ObjectType].ToObject<DosageType>();
            return base.ReadJson(jToken.CreateReader(), objectType, existingValue, serializer);
        }

        public override DosageObject Create(Type objectType)
        {
            switch (_dosageType)
            {
                case DosageType.Simple: return new DosageSimple();
                case DosageType.FromTo: return new DosageFromTo();
                case DosageType.Range: return new DosageRange();
                default: throw new NotSupportedException($"{nameof(DosageType)}={_dosageType} is not supported");
            }
        }
    }
}