using System;
using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Serialization;

public class PosologySerializationTests : SerializationDeserializationTestsBase
{
    public PosologySerializationTests(ITestOutputHelper output)
        : base(output) { }

    [Fact]
    public void WhenDeserializingSerializedPosology_ReturnsEqualObject()
    {
        // Arrange
        var posology = new Posology
                       {
                           FromDate = DateTime.Now.Date,
                           ToDate = DateTime.Now.AddDays(30).Date,
                           RelativeToMeal = RelativeToMeal.Before,
                           InReserve = false,
                           Unit = "mg",
                           PosologyDetailObject = new Daily
                                                  {
                                                      Dosages = new[] {1.0, 0, 0, 2}
                                                  }
                       };

        // Act and Assert
        TestSerialization(posology, MedicationType.MedicationPlan);
    }
}