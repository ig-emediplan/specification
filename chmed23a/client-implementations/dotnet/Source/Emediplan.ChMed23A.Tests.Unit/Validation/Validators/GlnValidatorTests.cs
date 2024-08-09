using Emediplan.ChMed23A.Validation.Validators;

namespace Emediplan.ChMed23A.Tests.Unit.Validation.Validators;

public class GlnValidatorTests
{
    [Theory]
    [InlineData("1234567890128", true)] // Valid GLN
    [InlineData("7601001362383", true)] // HCI GLN is valid
    [InlineData("1234567890123", false)] // Invalid GLN (wrong check digit)
    [InlineData("12345abc67890", false)] // Invalid GLN (contains non-digit characters)
    [InlineData("12345678901234", false)] // Invalid GLN (too long)
    [InlineData("123456789012", false)] // Invalid GLN (too short)
    [InlineData(null, false)] // Invalid GLN (null)
    [InlineData("", false)] // Invalid GLN (empty string)
    public void WhenValidatingGln_ReturnsExceptedValidationResult(string? gln, bool isValidExpected)
    {
        // Arrange
        var validator = new GlnValidator();

        // Act
        var isValidComputed = validator.Validate(gln);

        // Assert
        Assert.Equal(isValidExpected, isValidComputed);
    }
}