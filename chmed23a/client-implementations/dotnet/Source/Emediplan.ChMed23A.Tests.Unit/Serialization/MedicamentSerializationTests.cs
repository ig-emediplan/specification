using Emediplan.ChMed23A.Models;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Serialization;

public class MedicamentSerializationTests : SerializationDeserializationTestsBase
{
    #region Constructors

    public MedicamentSerializationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    [Fact]
    public void WhenDeserializingSerializedMedication_ReturnsEqualObject()
    {
        // Arrange
        var medicament = new Medicament
                         {
                             Id = "Id1",
                             IdType = MedicamentIdType.None,
                             IsAutoMedication = false,
                             IsNotSubstitutable = true,
                             NumberOfPackages = 3,
                             PrescribedBy = "PrescribedBy",
                             TakingReason = "Reason"
                         };

        // Act and Assert
        TestSerialization(medicament, MedicationType.MedicationPlan);
    }

    #endregion
}