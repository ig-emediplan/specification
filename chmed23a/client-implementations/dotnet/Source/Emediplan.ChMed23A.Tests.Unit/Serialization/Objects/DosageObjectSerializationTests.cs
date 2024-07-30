using Emediplan.ChMed23A.Models.DosageObjects;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Serialization.Objects;

public class DosageObjectSerializationTests : SerializationDeserializationTestsBase
{
    public DosageObjectSerializationTests(ITestOutputHelper output)
        : base(output) { }

    [Fact]
    public void WhenDeserializingSerializedDosageSimple_ReturnsEqualObject()
    {
        // Arrange
        var dosageSimple = new DosageSimple
                           {
                               Amount = 5678
                           };

        // Act and Assert
        TestSerialization(dosageSimple, MedicationType.MedicationPlan);
    }

    [Fact]
    public void WhenDeserializingSerializedDosageFromTo_ReturnsEqualObject()
    {
        // Arrange
        var dosageFromTo = new DosageFromTo
                           {
                               AmountFrom = 5,
                               AmountTo = 10,
                               Duration = 45,
                               DurationUnit = TimeUnit.Minute
                           };

        // Act and Assert
        TestSerialization(dosageFromTo, MedicationType.MedicationPlan);
    }

    [Fact]
    public void WhenDeserializingSerializedDosageRange_ReturnsEqualObject()
    {
        // Arrange
        var dosageRange = new DosageRange
                          {
                              MinAmount = 1,
                              MaxAmount = 3,
                          };

        // Act and Assert
        TestSerialization(dosageRange, MedicationType.MedicationPlan);
    }
}