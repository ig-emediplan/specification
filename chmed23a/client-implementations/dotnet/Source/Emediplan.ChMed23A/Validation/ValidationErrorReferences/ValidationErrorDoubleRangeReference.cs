namespace Emediplan.ChMed23A.Validation.ValidationErrorReferences
{
    public class ValidationErrorDoubleRangeReference : ValidationErrorRangeReference<double>
    {
        public override ValidationErrorReferenceType ReferenceType => ValidationErrorReferenceType.DoubleRange;

        public ValidationErrorDoubleRangeReference(double valueFrom, double valueTo)
            : base(valueFrom, valueTo) { }
    }
}