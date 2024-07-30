using Emediplan.ChMed23A.Models;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Serialization;

public class PatientIdSerializationTests : SerializationDeserializationTestsBase
{
    #region Constructors

    public PatientIdSerializationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    [Fact]
    public void WhenDeserializingSerializedPatientId_ReturnsEqualObject()
    {
        // Arrange
        var patientId = new PatientId
                        {
                            SystemIdentifier = "Id1",
                            Value = "V1",
                            Type = PatientIdType.LocalPid
                        };

        // Act and Assert
        TestSerialization(patientId, MedicationType.MedicationPlan);
    }

    #endregion
}