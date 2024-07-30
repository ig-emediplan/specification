namespace Emediplan.ChMed23A.Serialization.Constants
{
    public partial class JsonPropertyNames
    {
        public class DosageObject
        {
            public class DosageSimple
            {
                public const string Amount = "a";
            }

            public class DosageRange
            {
                public const string MinAmount = "aMin";
                public const string MaxAmount = "aMax";
            }

            public class DosageFromTo
            {
                public const string AmountFrom = "aFrom";
                public const string AmountTo = "aTo";
                public const string DurationUnit = "duU";
                public const string Duration = "du";
            }
        }
    }
}