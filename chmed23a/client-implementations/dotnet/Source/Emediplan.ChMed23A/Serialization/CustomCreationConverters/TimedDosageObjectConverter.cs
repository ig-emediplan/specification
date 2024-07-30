using System;
using Emediplan.ChMed23A.Models.Enums.ObjectTypes;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Emediplan.ChMed23A.Serialization.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Emediplan.ChMed23A.Serialization.CustomCreationConverters
{
    /// <summary>
    ///     Instantiates the correct implementation fo the abstract <see cref="TimedDosageObject" /> class according to the
    ///     specified <see cref="TimedDosageObject.Type" />.
    /// </summary>
    internal class TimedDosageObjectConverter : CustomCreationConverter<TimedDosageObject>
    {
        private TimedDosageType _timeDosageType;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jToken = JToken.ReadFrom(reader);
            // ReSharper disable once PossibleNullReferenceException
            _timeDosageType = jToken[JsonPropertyNames.ObjectType].ToObject<TimedDosageType>();
            return base.ReadJson(jToken.CreateReader(), objectType, existingValue, serializer);
        }

        public override TimedDosageObject Create(Type objectType)
        {
            switch (_timeDosageType)
            {
                case TimedDosageType.DaySegments: return new DaySegments();
                case TimedDosageType.DaysOfMonth: return new DaysOfMonth();
                case TimedDosageType.DosageOnly: return new DosageOnly();
                case TimedDosageType.ApplicationTimes: return new Times();
                case TimedDosageType.WeekDays: return new WeekDays();
                case TimedDosageType.Interval: return new Interval();
                default: throw new NotSupportedException($"{nameof(TimedDosageType)}={_timeDosageType} is not supported");
            }
        }
    }
}