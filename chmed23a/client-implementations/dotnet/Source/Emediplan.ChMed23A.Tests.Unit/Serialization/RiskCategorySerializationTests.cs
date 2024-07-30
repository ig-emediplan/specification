using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Models.Constants;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Serialization;

public class RiskCategorySerializationTests : SerializationDeserializationTestsBase
{
    #region Constructors

    public RiskCategorySerializationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    [Fact]
    public void WhenDeserializingSerializedRiskCategory_ReturnsEqualObject()
    {
        // Arrange
        var riskCategory = new RiskCategory
                           {
                               Id = RiskCategoryCodeConstants.Allergies,
                               RiskIds = new[] {1, 17, 42}
                           };

        // Act and Assert
        TestSerialization(riskCategory, MedicationType.MedicationPlan);
    }

    #endregion
}