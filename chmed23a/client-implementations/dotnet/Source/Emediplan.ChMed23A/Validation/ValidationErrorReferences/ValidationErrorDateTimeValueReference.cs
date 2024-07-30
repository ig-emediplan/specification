using System;

namespace Emediplan.ChMed23A.Validation.ValidationErrorReferences
{
    public class ValidationErrorDateTimeValueReference : ValidationErrorValueReference<DateTime>
    {
        public override ValidationErrorReferenceType ReferenceType => ValidationErrorReferenceType.DateTimeValue;

        public ValidationErrorDateTimeValueReference(DateTime value)
            : base(value) { }
    }
}