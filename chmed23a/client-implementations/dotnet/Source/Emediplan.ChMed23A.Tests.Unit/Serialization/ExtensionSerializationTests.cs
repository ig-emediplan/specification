using Emediplan.ChMed23A.Models;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Serialization;

public class ExtensionSerializationTests : SerializationDeserializationTestsBase
{
    #region Constructors

    public ExtensionSerializationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    [Fact]
    public void WhenDeserializingSerializedExtension_ReturnsEqualObject()
    {
        // Arrange
        var extension = new Extension
                        {
                            Name = "P1",
                            Value = "V1",
                            Schema = "HCI",
                            Extensions = new[]
                                         {
                                             new Extension
                                             {
                                                 Schema = "HCI",
                                                 Name = "P1.1",
                                                 Value = "V1.1"
                                             }
                                         }
                        };

        // Act and Assert
        TestSerialization(extension, MedicationType.MedicationPlan);
    }

    #endregion
}