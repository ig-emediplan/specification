namespace Emediplan.ChMed23A.Validation.ValidationErrorReferences
{
    public class ValidationErrorDoubleValueReference : ValidationErrorValueReference<double>
    {
        public override ValidationErrorReferenceType ReferenceType => ValidationErrorReferenceType.DoubleValue;

        public ValidationErrorDoubleValueReference(double value)
            : base(value) { }
    }
}