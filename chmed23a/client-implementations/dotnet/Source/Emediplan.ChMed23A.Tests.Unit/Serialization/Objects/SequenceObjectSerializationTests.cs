using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Models.SequenceObjects;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Serialization.Objects;

public class SequenceObjectSerializationTests : SerializationDeserializationTestsBase
{
    public SequenceObjectSerializationTests(ITestOutputHelper output)
        : base(output) { }

    [Fact]
    public void WhenDeserializingSerializedPosologySequence_ReturnsEqualObject()
    {
        // Arrange
        var posologySequence = new PosologySequence
                               {
                                   Duration = 21,
                                   DurationUnit = TimeUnit.Day,
                                   PosologyDetailObject = new Cyclic
                                                          {
                                                              CycleDuration = 1,
                                                              CycleDurationUnit = TimeUnit.Day,
                                                              TimedDosage = new DosageOnly
                                                                            {
                                                                                Dosage = new DosageSimple
                                                                                         {
                                                                                             Amount = 1
                                                                                         }
                                                                            }
                                                          }
                               };

        // Act and Assert
        TestSerialization(posologySequence, MedicationType.MedicationPlan);
    }

    [Fact]
    public void WhenDeserializingSerializedPause_ReturnsEqualObject()
    {
        // Arrange
        var pause = new Pause
                    {
                        Duration = 7,
                        DurationUnit = TimeUnit.Day
                    };

        // Act and Assert
        TestSerialization(pause, MedicationType.MedicationPlan);
    }
}