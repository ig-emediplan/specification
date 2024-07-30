using System;
using System.Collections.Generic;
using Emediplan.ChMed23A.Models;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Serialization;

public class PatientSerializationTests : SerializationDeserializationTestsBase
{
    #region Constructors

    public PatientSerializationTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    [Fact]
    public void WhenDeserializingSerializedPatient_ReturnsEqualObject()
    {
        // Arrange
        var patient = new Patient
                      {
                          Language = "DE",
                          BirthDate = new DateTime(1980, 7, 12),
                          City = "Bern",
                          FirstName = "James",
                          LastName = "Bond",
                          Phones = new[] {"07912345678"},
                          Emails = new[] {"a@b.c"},
                          Gender = Gender.Other,
                          Street = "Am Bach",
                          Zip = "3000",
                          Ids = new List<PatientId>
                                {
                                    new()
                                    {
                                        Type = PatientIdType.InsuranceCardNumber,
                                        Value = "756.1234.5678.97"
                                    }
                                }
                      };

        // Act and Assert
        TestSerialization(patient, MedicationType.MedicationPlan);
    }

    #endregion
}