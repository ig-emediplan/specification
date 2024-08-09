using System.Collections.Generic;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Models.SequenceObjects;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Serialization.Objects;

public class PosologyDetailObjectSerializationTests : SerializationDeserializationTestsBase
{
    public PosologyDetailObjectSerializationTests(ITestOutputHelper output)
        : base(output) { }

    [Fact]
    public void WhenDeserializingSerializedDaily_ReturnsEqualObject()
    {
        // Arrange
        var daily = new Daily
                    {
                        Dosages = new[] {1.5, 0, 2, 0}
                    };

        // Act and Assert
        TestSerialization(daily, MedicationType.MedicationPlan);
    }

    [Fact]
    public void WhenDeserializingSerializedFreeText_ReturnsEqualObject()
    {
        // Arrange
        var freeText = new FreeText
                       {
                           Text = "Take one pill. Wait one hour. If symptoms persist, take a second pill and wait 30 minutes. If symptoms persist, contact doctor."
                       };

        // Act and Assert
        TestSerialization(freeText, MedicationType.MedicationPlan);
    }

    [Fact]
    public void WhenDeserializingSerializedSingle_ReturnsEqualObject()
    {
        // Arrange
        var single = new Single
                     {
                         TimedDosage = new DosageOnly
                                       {
                                           Dosage = new DosageSimple
                                                    {
                                                        Amount = 1
                                                    }
                                       }
                     };

        // Act and Assert
        TestSerialization(single, MedicationType.MedicationPlan);
    }

    [Fact]
    public void WhenDeserializingSerializedCyclic_ReturnsEqualObject()
    {
        // Arrange
        var cyclic = new Cyclic
                     {
                         CycleDuration = 5,
                         CycleDurationUnit = TimeUnit.Week,
                         TimedDosage = new DosageOnly
                                       {
                                           Dosage = new DosageSimple
                                                    {
                                                        Amount = 1
                                                    }
                                       },
                         TimedDosagesPerCycle = 2
                     };

        // Act and Assert
        TestSerialization(cyclic, MedicationType.MedicationPlan);
    }

    [Fact]
    public void WhenDeserializingSerializedSequence_ReturnsEqualObject()
    {
        // Arrange
        var sequence = new Sequence
                       {
                           SequenceObjects = new List<SequenceObject>
                                             {
                                                 new PosologySequence
                                                 {
                                                     DurationUnit = TimeUnit.Day,
                                                     Duration = 21,
                                                     PosologyDetailObject = new Cyclic
                                                                            {
                                                                                CycleDurationUnit = TimeUnit.Day,
                                                                                CycleDuration = 1,
                                                                                TimedDosage = new DosageOnly
                                                                                              {
                                                                                                  Dosage = new DosageSimple {Amount = 1}
                                                                                              }
                                                                            }
                                                 },
                                                 new Pause
                                                 {
                                                     DurationUnit = TimeUnit.Day,
                                                     Duration = 7
                                                 }
                                             }
                       };

        // Act and Assert
        TestSerialization(sequence, MedicationType.MedicationPlan);
    }
}