using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Models.ApplicationObjects;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Models.SequenceObjects;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Xunit.Abstractions;

// ReSharper disable StringLiteralTypo

namespace Emediplan.ChMed23A.Examples.MedicationExamples;

public class MedicationExample3 : ExamplesBase
{
    #region Constructors

    public MedicationExample3(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Example

    [Fact]
    public void Example3()
    {
        var medication = new Medication
                         {
                             MedType = MedicationType.MedicationPlan,
                             CreationDate = new DateTimeOffset(2024, 02, 05, 21, 33, 46, TimeSpan.FromHours(+1)),
                             Author = MedicationAuthor.Patient,

                             Patient = new Patient
                                       {
                                           FirstName = "Dana",
                                           LastName = "Banana",
                                           BirthDate = new DateTime(1997, 07, 17),
                                           Gender = Gender.Female,
                                           Street = "Bodenseeweg 3",
                                           City = "Konstanz",
                                           Zip = "78462",
                                           Country = "DE",
                                           Language = "DE",
                                           Ids = new List<PatientId>
                                                 {
                                                     new()
                                                     {
                                                         Value = "57484",
                                                         SystemIdentifier = "9.99.999.9.99",
                                                         Type = PatientIdType.LocalPid
                                                     }
                                                 },
                                           Phones = new List<string> {"079 999 99 99"},
                                           Emails = new List<string> {"ana@email.de"},
                                           MedicalData = new MedicalData
                                                         {
                                                             WeightKg = 56,
                                                             HeightCm = 165,
                                                             RiskCategories = new List<RiskCategory>
                                                                              {
                                                                                  new() {Id = 1},
                                                                                  new() {Id = 2},
                                                                                  new()
                                                                                  {
                                                                                      Id = 3,
                                                                                      RiskIds = new List<int> {612}
                                                                                  },
                                                                                  new()
                                                                                  {
                                                                                      Id = 4,
                                                                                      RiskIds = new List<int> {580}
                                                                                  },
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

                             Medicaments = new List<Medicament>
                                           {
                                               new()
                                               {
                                                   Id = "7680298120012",
                                                   IdType = MedicamentIdType.Gtin,
                                                   TakingReason = "Thyroid gland",
                                                   IsAutoMedication = false,
                                                   PrescribedBy = "Dr. Hans Vogel, Konstanz",
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        InReserve = false,
                                                                        RelativeToMeal = RelativeToMeal.Before,
                                                                        Unit = "Stk",
                                                                        ApplicationInstructions = "30 minutes before breakfast.",
                                                                        PosologyDetailObject = new Sequence
                                                                                               {
                                                                                                   SequenceObjects = new List<SequenceObject>
                                                                                                                     {
                                                                                                                         new PosologySequence
                                                                                                                         {
                                                                                                                             Duration = 1,
                                                                                                                             DurationUnit = TimeUnit.Day,
                                                                                                                             PosologyDetailObject =
                                                                                                                                 new Models.PosologyDetailObjects.Single
                                                                                                                                 {
                                                                                                                                     TimedDosage = new DosageOnly
                                                                                                                                         {
                                                                                                                                             Dosage = new DosageSimple
                                                                                                                                                 {
                                                                                                                                                     Amount = 1
                                                                                                                                                 }
                                                                                                                                         }
                                                                                                                                 }
                                                                                                                         },
                                                                                                                         new PosologySequence
                                                                                                                         {
                                                                                                                             Duration = 1,
                                                                                                                             DurationUnit = TimeUnit.Day,
                                                                                                                             PosologyDetailObject =
                                                                                                                                 new Models.PosologyDetailObjects.Single
                                                                                                                                 {
                                                                                                                                     TimedDosage = new DosageOnly
                                                                                                                                         {
                                                                                                                                             Dosage = new DosageSimple
                                                                                                                                                 {
                                                                                                                                                     Amount = 2
                                                                                                                                                 }
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
                                                   Id = "7680662560024",
                                                   IdType = MedicamentIdType.Gtin,
                                                   TakingReason = "Sore thorat",
                                                   IsAutoMedication = false,
                                                   PrescribedBy = "Apotheke Blumenwiese, Romanshorn",
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        FromDate = new DateTime(2024, 01, 18),
                                                                        ToDate = new DateTime(2024, 01, 20),
                                                                        InReserve = false,
                                                                        Unit = "Stk",
                                                                        ApplicationInstructions = "Do not take the lozenge immediately before eating or drinking.",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDuration = 1,
                                                                                                   CycleDurationUnit = TimeUnit.Day,
                                                                                                   TimedDosage = new Interval
                                                                                                                 {
                                                                                                                     Dosage = new DosageSimple {Amount = 1},
                                                                                                                     MinIntervalDuration = 4,
                                                                                                                     MinIntervalDurationUnit = TimeUnit.Hour
                                                                                                                 },
                                                                                                   TimedDosagesPerCycle = 6
                                                                                               }
                                                                    }
                                                                }
                                               },

                                               new()
                                               {
                                                   Id = "7680563180079",
                                                   IdType = MedicamentIdType.Gtin,
                                                   TakingReason = "Fever/pain",
                                                   IsAutoMedication = false,
                                                   PrescribedBy = "Apotheke Blumenwiese, Romanshorn",
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        FromDate = new DateTime(2024, 01, 18),
                                                                        InReserve = true,
                                                                        Unit = "Stk",
                                                                        ApplicationInstructions = "Up to 4 pills daily with at least 6 hours between each dose.",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDuration = 1,
                                                                                                   CycleDurationUnit = TimeUnit.Day,
                                                                                                   TimedDosage = new Interval
                                                                                                                 {
                                                                                                                     Dosage = new DosageSimple {Amount = 1},
                                                                                                                     MinIntervalDuration = 6,
                                                                                                                     MinIntervalDurationUnit = TimeUnit.Hour
                                                                                                                 },
                                                                                                   TimedDosagesPerCycle = 4
                                                                                               }
                                                                    }
                                                                }
                                               },

                                               new()
                                               {
                                                   Id = "7680594920033",
                                                   IdType = MedicamentIdType.Gtin,
                                                   TakingReason = "Contraception",
                                                   IsAutoMedication = false,
                                                   PrescribedBy = "Dr. Hannelore Beispielhaft, Romanshorn",
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        Unit = "Stk",
                                                                        PosologyDetailObject = new Sequence
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
                                                                                                                                     TimedDosage = new Times
                                                                                                                                         {
                                                                                                                                             Applications =
                                                                                                                                                 new List<ApplicationAtTime>
                                                                                                                                                 {
                                                                                                                                                     new()
                                                                                                                                                     {
                                                                                                                                                         Dosage =
                                                                                                                                                             new DosageSimple
                                                                                                                                                             {Amount = 1},
                                                                                                                                                         TimeOfDay =
                                                                                                                                                             new TimeSpan(9,
                                                                                                                                                                      0,
                                                                                                                                                                      0)
                                                                                                                                                     }
                                                                                                                                                 }
                                                                                                                                         }
                                                                                                                                 }
                                                                                                                         },
                                                                                                                         new Pause
                                                                                                                         {
                                                                                                                             DurationUnit = TimeUnit.Day,
                                                                                                                             Duration = 7
                                                                                                                         }
                                                                                                                     }
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