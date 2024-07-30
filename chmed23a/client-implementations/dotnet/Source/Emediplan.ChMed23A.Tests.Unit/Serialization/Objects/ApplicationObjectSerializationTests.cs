using System;
using Emediplan.ChMed23A.Models.ApplicationObjects;
using Emediplan.ChMed23A.Models.DosageObjects;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Serialization.Objects;

public class ApplicationObjectSerializationTests : SerializationDeserializationTestsBase
{
    public ApplicationObjectSerializationTests(ITestOutputHelper output)
        : base(output) { }

    [Fact]
    public void WhenDeserializingSerializedApplicationAtTime_ReturnsEqualObject()
    {
        // Arrange
        var applicationAtTime = new ApplicationAtTime
                                {
                                    TimeOfDay = new TimeSpan(12, 0, 0),
                                    Dosage = new DosageSimple
                                             {
                                                 Amount = 1
                                             }
                                };

        // Act and Assert
        TestSerialization(applicationAtTime, MedicationType.MedicationPlan);
    }

    [Fact]
    public void WhenDeserializingSerializedApplicationInSegment_ReturnsEqualObject()
    {
        // Arrange
        var applicationInSegment = new ApplicationInSegment
                                   {
                                       Segment = DaySegment.Evening,
                                       Dosage = new DosageSimple
                                                {
                                                    Amount = 1
                                                }
                                   };

        // Act and Assert
        TestSerialization(applicationInSegment, MedicationType.MedicationPlan);
    }
}