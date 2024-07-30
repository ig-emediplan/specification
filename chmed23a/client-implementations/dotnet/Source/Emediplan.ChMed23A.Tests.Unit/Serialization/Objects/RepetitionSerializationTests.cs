using Emediplan.ChMed23A.Models.RepetitionObjects;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Serialization.Objects;

public class RepetitionSerializationTests : SerializationDeserializationTestsBase
{
    #region Constructors

    public RepetitionSerializationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    [Fact]
    public void WhenDeserializingSerializedDuration_ReturnsEqualObject()
    {
        // Arrange
        var duration = new Duration
                       {
                           Unit = TimeUnit.Month,
                           DurationValue = 3
                       };

        // Act and Assert
        TestSerialization(duration, MedicationType.MedicationPlan);
    }

    [Fact]
    public void WhenDeserializingSerializedNumber_ReturnsEqualObject()
    {
        // Arrange
        var number = new Number
                     {
                         Value = 3
                     };

        // Act and Assert
        TestSerialization(number, MedicationType.MedicationPlan);
    }

    [Fact]
    public void WhenDeserializingSerializedNumberAndDuration_ReturnsEqualObject()
    {
        // Arrange
        var numberAndDuration = new NumberAndDuration
                                {
                                    Value = 3,
                                    DurationValue = 3,
                                    Unit = TimeUnit.Day
                                };

        // Act and Assert
        TestSerialization(numberAndDuration, MedicationType.MedicationPlan);
    }

    #endregion
}