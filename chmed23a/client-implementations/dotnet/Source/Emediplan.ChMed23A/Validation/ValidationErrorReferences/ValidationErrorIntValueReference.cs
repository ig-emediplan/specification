namespace Emediplan.ChMed23A.Validation.ValidationErrorReferences
{
    public class ValidationErrorIntValueReference : ValidationErrorValueReference<int>
    {
        public override ValidationErrorReferenceType ReferenceType => ValidationErrorReferenceType.IntValue;

        public ValidationErrorIntValueReference(int value)
            : base(value) { }
    }
}