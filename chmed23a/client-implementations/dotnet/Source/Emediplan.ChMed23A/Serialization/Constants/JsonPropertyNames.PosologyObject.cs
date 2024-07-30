namespace Emediplan.ChMed23A.Serialization.Constants
{
    public partial class JsonPropertyNames
    {
        public class PosologyDetailObject
        {
            public class SingleApplication
            {
                public const string TimedDosage = "tdo";
            }

            public class Sequence
            {
                public const string SequenceObjects = "sos";
            }

            public class FreeText
            {
                public const string Text = "text";
            }

            public class Daily
            {
                public const string Dosages = "ds";
            }

            public class Cyclic
            {
                public const string CycleDurationUnit = "cyDuU";
                public const string CycleDuration = "cyDu";
                public const string TimedDosage = "tdo";
                public const string TimedDosagesPerCycle = "tdpc";
            }
        }
    }
}