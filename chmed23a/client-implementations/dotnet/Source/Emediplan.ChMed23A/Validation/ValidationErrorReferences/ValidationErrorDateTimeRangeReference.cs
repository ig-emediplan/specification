using System;

namespace Emediplan.ChMed23A.Validation.ValidationErrorReferences
{
    public class ValidationErrorDateTimeRangeReference : ValidationErrorRangeReference<DateTime>
    {
        public override ValidationErrorReferenceType ReferenceType => ValidationErrorReferenceType.DateTimeRange;

        public ValidationErrorDateTimeRangeReference(DateTime valueFrom, DateTime valueTo)
            : base(valueFrom, valueTo) { }
    }
}