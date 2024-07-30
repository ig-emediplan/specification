using System;
using Xunit.Abstractions;
using MedicalData = Emediplan.ChMed23A.Models.MedicalData;

namespace Emediplan.ChMed23A.Tests.Unit.Serialization;

public class MedicalDataSerializationTests : SerializationDeserializationTestsBase
{
    #region Constructors

    public MedicalDataSerializationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    [Fact]
    public void WhenDeserializingSerializedMedicalData_ReturnsEqualObject()
    {
        // Arrange
        var medicalData = new MedicalData
                          {
                              HeightCm = 185,
                              WeightKg = 80,
                              TimeToGestationDays = 42,
                              IsPrematureInfant = false,
                              LastMenstruationDate = DateTime.Now.Date.AddDays(-7)
                          };

        // Act and Assert
        TestSerialization(medicalData, MedicationType.MedicationPlan);
    }

    #endregion
}