namespace Emediplan.ChMed23A.Serialization.Constants
{
    public partial class JsonPropertyNames
    {
        public class TimedDosageObject
        {
            public class WeekDays
            {
                public const string DaysOfWeek = "wds";
                public const string TimedDosage = "tdo";
            }

            public class Times
            {
                public const string Applications = "ts";
            }

            public class Interval
            {
                public const string Dosage = "do";
                public const string MinIntervalDurationUnit = "miDuU";
                public const string MinIntervalDuration = "miDu";
            }

            public class DosageOnly
            {
                public const string Dosage = "do";
            }

            public class DaysOfMonth
            {
                public const string Days = "doms";
                public const string TimedDosage = "tdo";
            }

            public class DaySegments
            {
                public const string Applications = "ss";
            }
        }
    }
}