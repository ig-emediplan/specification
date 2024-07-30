namespace Emediplan.ChMed23A.Validation
{
    // TODO PWA: Determine required types here
    public enum ValidationErrorReason
    {
        Unknown,
        IsRequired,
        ArrayLengthMustBe,
        MustBeGreaterThan,
        MustBeGreaterThanOrEqual,
        MustBeSmallerThan,
        MustBeInRange,
        NotSupported,
        MustBeUnique,
        MustHaveLength
    }
}