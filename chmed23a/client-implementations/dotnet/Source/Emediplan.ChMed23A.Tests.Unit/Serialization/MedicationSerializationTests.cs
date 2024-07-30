using System;
using System.Collections.Generic;
using Emediplan.ChMed23A.Models;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Serialization;

public class MedicationSerializationTests : SerializationDeserializationTestsBase
{
    #region Constructors

    public MedicationSerializationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    [Fact]
    public void WhenDeserializingSerializedMedication_ReturnsEqualObject()
    {
        // Arrange
        var medication = new Medication
                         {
                             CreationDate = DateTimeOffset.Now,
                             Author = MedicationAuthor.Patient,
                             Id = "Id1",
                             MedType = MedicationType.MedicationPlan,
                             Remark = "Remark",
                             ReceiverGln = "12345",
                             
                             Patient = new Patient
                                       {
                                           FirstName = "Dora",
                                           LastName = "Graber",
                                           BirthDate = new DateTime(1954, 12, 1),
                                           Gender = Gender.Female,
                                           Language = "de",
                                           Ids = new List<PatientId>
                                                 {
                                                     new()
                                                     {
                                                         Type = PatientIdType.InsuranceCardNumber,
                                                         Value = "756.1234.5678.97"
                                                     }
                                                 }
                                       },
                             Medicaments = new List<Medicament>
                                           {
                                               new()
                                               {
                                                   Id = "MyMed",
                                                   IdType = MedicamentIdType.None,
                                                   IsAutoMedication = false
                                               }
                                           }
                         };

        // Act and Assert
        TestSerialization(medication, MedicationType.MedicationPlan);
    }

    #endregion
}