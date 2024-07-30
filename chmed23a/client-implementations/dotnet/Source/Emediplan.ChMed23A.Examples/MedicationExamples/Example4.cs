using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Models.ApplicationObjects;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Models.RepetitionObjects;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Xunit.Abstractions;

// ReSharper disable StringLiteralTypo

namespace Emediplan.ChMed23A.Examples.MedicationExamples;

public class MedicationExample4 : ExamplesBase
{
    #region Constructors

    public MedicationExample4(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Example

    [Fact]
    public void Example4()
    {
        var medication = new Medication
                         {
                             MedType = MedicationType.Prescription,
                             CreationDate = new DateTimeOffset(2024, 02, 08, 10, 01, 23, TimeSpan.FromHours(+1)),
                             Author = MedicationAuthor.HealthcarePerson,

                             Patient = new Patient
                                       {
                                           FirstName = "Sebastian",
                                           LastName = "Example",
                                           BirthDate = new DateTime(1971, 05, 15),
                                           Gender = Gender.Male,
                                           Street = "Bernstrasse 1",
                                           City = "Bern",
                                           Zip = "3000",
                                           Country = "CH",
                                           Language = "DE",
                                           Ids = new List<PatientId>
                                                 {
                                                     new()
                                                     {
                                                         Value = "87588895877545621",
                                                         SystemIdentifier = "9.99.999.9.99",
                                                         Type = PatientIdType.LocalPid
                                                     }
                                                 },
                                           Phones = new List<string> {"079 999 99 99"},
                                           MedicalData = new MedicalData
                                                         {
                                                             WeightKg = 80,
                                                             HeightCm = 179,
                                                             RiskCategories = new List<RiskCategory>
                                                                              {
                                                                                  new() {Id = 1},
                                                                                  new() {Id = 2},
                                                                                  new() {Id = 4},
                                                                                  new()
                                                                                  {
                                                                                      Id = 5,
                                                                                      RiskIds = new List<int> {615}
                                                                                  },
                                                                                  new() {Id = 6},
                                                                                  new() {Id = 7}
                                                                              }
                                                         }
                                       },

                             HealthcarePerson = new HealthcarePerson
                                                {
                                                    Gln = "7601001362383",
                                                    FirstName = "Hans",
                                                    LastName = "Muster"
                                                },

                             HealthcareOrganization = new HealthcareOrganization
                                                      {
                                                          Gln = "7601001362383",
                                                          Name = "Arztpraxis Sonnenschein",
                                                          Street = "Bernstrasse 1",
                                                          Zip = "3000",
                                                          City = "Bern",
                                                          Country = "CH",
                                                          Zsr = "XX.1254"
                                                      },

                             Medicaments = new List<Medicament>
                                           {
                                               new()
                                               {
                                                   Id = "7680388400376",
                                                   IdType = MedicamentIdType.Gtin,
                                                   IsAutoMedication = false,
                                                   PrescribedBy = "123123123123",
                                                   Repetitions = new Duration
                                                                 {
                                                                     DurationValue = 6,
                                                                     Unit = TimeUnit.Month
                                                                 },
                                                   IsNotSubstitutable = false,
                                                   NumberOfPackages = 1,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        FromDate = new DateTime(2024, 02, 08),
                                                                        ToDate = new DateTime(2024, 02, 10),
                                                                        InReserve = false,
                                                                        Unit = "Stk",
                                                                        PosologyDetailObject = new Daily
                                                                                               {
                                                                                                   Dosages = new[] {0.25, 0, 0.25, 0}
                                                                                               }
                                                                    },

                                                                    new()
                                                                    {
                                                                        FromDate = new DateTime(2024, 02, 11),
                                                                        ToDate = new DateTime(2024, 02, 13),
                                                                        InReserve = false,
                                                                        Unit = "Stk",
                                                                        PosologyDetailObject = new Daily
                                                                                               {
                                                                                                   Dosages = new[] {0.5, 0, 0.5, 0}
                                                                                               }
                                                                    },

                                                                    new()
                                                                    {
                                                                        FromDate = new DateTime(2024, 02, 14),
                                                                        ToDate = new DateTime(2024, 02, 16),
                                                                        InReserve = false,
                                                                        Unit = "Stk",
                                                                        PosologyDetailObject = new Daily
                                                                                               {
                                                                                                   Dosages = new[] {0.75, 0, 0.75, 0}
                                                                                               }
                                                                    },

                                                                    new()
                                                                    {
                                                                        FromDate = new DateTime(2024, 02, 17),
                                                                        InReserve = false,
                                                                        Unit = "Stk",
                                                                        PosologyDetailObject = new Daily
                                                                                               {
                                                                                                   Dosages = new[] {1.0, 0, 1, 0}
                                                                                               }
                                                                    }
                                                                }
                                               },

                                               new()
                                               {
                                                   Id = "7680563180079",
                                                   IdType = MedicamentIdType.Gtin,
                                                   TakingReason = "Pain",
                                                   IsAutoMedication = false,
                                                   PrescribedBy = "123123123123",
                                                   IsNotSubstitutable = true,
                                                   NumberOfPackages = 2,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        FromDate = new DateTime(2024, 02, 08),
                                                                        InReserve = true,
                                                                        Unit = "Stk",
                                                                        ApplicationInstructions = "Maximum 4 pills a day",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDuration = 1,
                                                                                                   CycleDurationUnit = TimeUnit.Day,
                                                                                                   TimedDosage = new DaySegments
                                                                                                                 {
                                                                                                                     Applications = new List<ApplicationInSegment>
                                                                                                                         {
                                                                                                                             new()
                                                                                                                             {
                                                                                                                                 Segment = DaySegment.Morning,
                                                                                                                                 Dosage = new DosageRange
                                                                                                                                     {
                                                                                                                                         MinAmount = 1,
                                                                                                                                         MaxAmount = 2
                                                                                                                                     }
                                                                                                                             },
                                                                                                                             new()
                                                                                                                             {
                                                                                                                                 Segment = DaySegment.Evening,
                                                                                                                                 Dosage = new DosageRange
                                                                                                                                     {
                                                                                                                                         MinAmount = 1,
                                                                                                                                         MaxAmount = 2
                                                                                                                                     }
                                                                                                                             }
                                                                                                                         }
                                                                                                                 }
                                                                                               }
                                                                    }
                                                                }
                                               },

                                               new()
                                               {
                                                   Id = "7680552740055",
                                                   IdType = MedicamentIdType.Gtin,
                                                   TakingReason = "Cough",
                                                   PrescribedBy = "123123123123",
                                                   IsNotSubstitutable = false,
                                                   Repetitions = new Number {Value = 0},
                                                   NumberOfPackages = 1,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        FromDate = new DateTime(2024, 02, 08),
                                                                        ToDate = new DateTime(2024, 02, 28),
                                                                        InReserve = false,
                                                                        Unit = "gtt",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDuration = 1,
                                                                                                   CycleDurationUnit = TimeUnit.Day,
                                                                                                   TimedDosage = new Times
                                                                                                                 {
                                                                                                                     Applications = new List<ApplicationAtTime>
                                                                                                                         {
                                                                                                                             new()
                                                                                                                             {
                                                                                                                                 Dosage = new DosageSimple {Amount = 20},
                                                                                                                                 TimeOfDay = new TimeSpan(21, 0, 0)
                                                                                                                             }
                                                                                                                         }
                                                                                                                 }
                                                                                               }
                                                                    }
                                                                }
                                               },

                                               new()
                                               {
                                                   Id = "7680362030131",
                                                   IdType = MedicamentIdType.Gtin,
                                                   IsAutoMedication = false,
                                                   PrescribedBy = "123123123123",
                                                   Repetitions = new Number {Value = 2},
                                                   IsNotSubstitutable = false,
                                                   NumberOfPackages = 1,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        FromDate = new DateTime(2024, 02, 08),
                                                                        InReserve = true,
                                                                        Unit = "Stk",
                                                                        ApplicationInstructions = "Take half an hour before bedtime.",
                                                                        PosologyDetailObject = new Daily
                                                                                               {
                                                                                                   Dosages = new[] {0, 0, 0, 0.5}
                                                                                               }
                                                                    }
                                                                }
                                               }
                                           }
                         };

        PrintSerializedJson(medication, medication.MedType.Value);
    }

    #endregion
}