using System;

namespace Emediplan.ChMed23A.Validation.ValidationErrorReferences
{
    public class ValidationErrorTimespanReference : ValidationErrorRangeReference<TimeSpan>
    {
        public override ValidationErrorReferenceType ReferenceType => ValidationErrorReferenceType.Timespan;

        public ValidationErrorTimespanReference(TimeSpan valueFrom, TimeSpan valueTo)
            : base(valueFrom, valueTo) { }
    }
}