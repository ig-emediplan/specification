namespace Emediplan.ChMed23A.Validation.ValidationErrorReferences
{
    public class ValidationErrorIntRangeReference : ValidationErrorRangeReference<int>
    {
        public override ValidationErrorReferenceType ReferenceType => ValidationErrorReferenceType.IntRange;

        public ValidationErrorIntRangeReference(int valueFrom, int valueTo)
            : base(valueFrom, valueTo) { }
    }
}