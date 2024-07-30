using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Models.ApplicationObjects;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Models.SequenceObjects;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Xunit.Abstractions;
using DayOfWeek = Emediplan.ChMed23A.Models.Enums.DayOfWeek;

// ReSharper disable StringLiteralTypo

namespace Emediplan.ChMed23A.Examples.MedicationExamples;

public class MedicationExample2 : ExamplesBase
{
    #region Constructors

    public MedicationExample2(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Example

    [Fact]
    public void Example2()
    {
        var medication = new Medication
                         {
                             MedType = MedicationType.MedicationPlan,
                             CreationDate = new DateTimeOffset(2024, 01, 10, 15, 54, 06, TimeSpan.FromHours(+1)),
                             Author = MedicationAuthor.HealthcarePerson,
                             Remark = "Please measure your blood pressure daily.",

                             Patient = new Patient
                                       {
                                           FirstName = "Urs",
                                           LastName = "Mustermann",
                                           BirthDate = new DateTime(1968, 05, 15),
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
                                                         Type = PatientIdType.InsuranceCardNumber
                                                     }
                                                 },
                                           Phones = new List<string>
                                                    {
                                                        "079 999 99 99"
                                                    },
                                           MedicalData = new MedicalData
                                                         {
                                                             RiskCategories = new List<RiskCategory>
                                                                              {
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
                                                    FirstName = "Hans",
                                                    LastName = "Beispiel",
                                                },

                             HealthcareOrganization = new HealthcareOrganization
                                                      {
                                                          Gln = "7601001362383",
                                                          Name = "Arztpraxis Sonnenschein",
                                                          Street = "Bernstrasse 1",
                                                          City = "Bern",
                                                          Zip = "3000",
                                                          Country = "CH"
                                                      },

                             Medicaments = new List<Medicament>
                                           {
                                               new()
                                               {
                                                   Id = "7680549490031",
                                                   IdType = MedicamentIdType.Gtin,
                                                   TakingReason = "Thyroid gland",
                                                   IsAutoMedication = false,
                                                   PrescribedBy = "45745874584",
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        RelativeToMeal = RelativeToMeal.Before,
                                                                        Unit = "Stk",
                                                                        ApplicationInstructions = "Take on an empty stomach, at least 30 minutes before breakfast.",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDuration = 1,
                                                                                                   CycleDurationUnit = TimeUnit.Week,
                                                                                                   TimedDosage = new WeekDays
                                                                                                                 {
                                                                                                                     DaysOfWeek = new List<DayOfWeek>
                                                                                                                         {
                                                                                                                             DayOfWeek.Tuesday,
                                                                                                                             DayOfWeek.Thursday
                                                                                                                         },
                                                                                                                     TimedDosage = new Times
                                                                                                                         {
                                                                                                                             Applications = new List<ApplicationAtTime>
                                                                                                                                 {
                                                                                                                                     new()
                                                                                                                                     {
                                                                                                                                         TimeOfDay =
                                                                                                                                             TimeSpan.FromMinutes(7 * 60 + 30),
                                                                                                                                         Dosage = new DosageSimple {Amount = 1}
                                                                                                                                     }
                                                                                                                                 }
                                                                                                                         }
                                                                                                                 }
                                                                                               }
                                                                    },

                                                                    new()
                                                                    {
                                                                        RelativeToMeal = RelativeToMeal.Before,
                                                                        Unit = "Stk",
                                                                        ApplicationInstructions = "Take on an empty stomach, at least 30 minutes before breakfast.",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDuration = 1,
                                                                                                   CycleDurationUnit = TimeUnit.Week,
                                                                                                   TimedDosage = new WeekDays
                                                                                                                 {
                                                                                                                     DaysOfWeek = new List<DayOfWeek>
                                                                                                                         {
                                                                                                                             DayOfWeek.Saturday
                                                                                                                         },
                                                                                                                     TimedDosage = new Times
                                                                                                                         {
                                                                                                                             Applications = new List<ApplicationAtTime>
                                                                                                                                 {
                                                                                                                                     new()
                                                                                                                                     {
                                                                                                                                         TimeOfDay = TimeSpan.FromMinutes(9 * 60),
                                                                                                                                         Dosage = new DosageSimple {Amount = 1}
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
                                                   Id = "7680216930020",
                                                   IdType = MedicamentIdType.Gtin,
                                                   TakingReason = "Blood thinner",
                                                   IsAutoMedication = false,
                                                   PrescribedBy = "55255255255",
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        FromDate = new DateTime(2024, 01, 10),
                                                                        ToDate = new DateTime(2024, 02, 29),
                                                                        InReserve = false,
                                                                        RelativeToMeal = RelativeToMeal.Before,
                                                                        Unit = "Stk",
                                                                        ApplicationInstructions = "Next doctor’s appointment on 29.02.2024",
                                                                        PosologyDetailObject = new Sequence
                                                                                               {
                                                                                                   SequenceObjects = new List<SequenceObject>
                                                                                                                     {
                                                                                                                         new PosologySequence
                                                                                                                         {
                                                                                                                             Duration = 2,
                                                                                                                             DurationUnit = TimeUnit.Day,
                                                                                                                             PosologyDetailObject = new Daily
                                                                                                                                 {
                                                                                                                                     Dosages = new[] {0, 0, 0.5, 0}
                                                                                                                                 }
                                                                                                                         },
                                                                                                                         new Pause
                                                                                                                         {
                                                                                                                             Duration = 1,
                                                                                                                             DurationUnit = TimeUnit.Day
                                                                                                                         },
                                                                                                                         new PosologySequence
                                                                                                                         {
                                                                                                                             Duration = 3,
                                                                                                                             DurationUnit = TimeUnit.Day,
                                                                                                                             PosologyDetailObject = new Daily
                                                                                                                                 {
                                                                                                                                     Dosages = new[] {0, 0, 0.75, 0}
                                                                                                                                 }
                                                                                                                         },
                                                                                                                         new Pause
                                                                                                                         {
                                                                                                                             Duration = 1,
                                                                                                                             DurationUnit = TimeUnit.Day
                                                                                                                         },
                                                                                                                     }
                                                                                               }
                                                                    }
                                                                }
                                               },

                                               new()
                                               {
                                                   Id = "7680656220026",
                                                   IdType = MedicamentIdType.Gtin,
                                                   IsAutoMedication = false,
                                                   PrescribedBy = "45745874584",
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        FromDate = new DateTime(2023, 11, 12),
                                                                        InReserve = false,
                                                                        Unit = "Stk",
                                                                        ApplicationInstructions = "See leaflet",
                                                                        RouteOfAdministration = "20066000",
                                                                        MethodOfAdministration = "11",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDuration = 2,
                                                                                                   CycleDurationUnit = TimeUnit.Week,
                                                                                                   TimedDosage = new WeekDays
                                                                                                                 {
                                                                                                                     DaysOfWeek = new List<DayOfWeek> {DayOfWeek.Monday},
                                                                                                                     TimedDosage = new Times
                                                                                                                         {
                                                                                                                             Applications = new List<ApplicationAtTime>
                                                                                                                                 {
                                                                                                                                     new()
                                                                                                                                     {
                                                                                                                                         Dosage = new DosageSimple {Amount = 1},
                                                                                                                                         TimeOfDay = TimeSpan.FromMinutes(20 * 60)
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
                                                   Id = "7680562030092",
                                                   IdType = MedicamentIdType.Gtin,
                                                   TakingReason = "Infection",
                                                   IsAutoMedication = false,
                                                   PrescribedBy = "45745874584",
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        FromDate = new DateTime(2024, 02, 02),
                                                                        ToDate = new DateTime(2024, 02, 02),
                                                                        InReserve = false,
                                                                        Unit = "Stk",
                                                                        ApplicationInstructions =
                                                                            "Endocarditis prophylaxis. Take 1 pill 2 hours (09:00 a.m.) before the dentist's appointment (02.02.2024 11.00 a.m.).",
                                                                        PosologyDetailObject = new Models.PosologyDetailObjects.Single
                                                                                               {
                                                                                                   TimedDosage = new Times
                                                                                                                 {
                                                                                                                     Applications = new List<ApplicationAtTime>
                                                                                                                         {
                                                                                                                             new()
                                                                                                                             {
                                                                                                                                 Dosage = new DosageSimple {Amount = 1},
                                                                                                                                 TimeOfDay = TimeSpan.FromHours(9)
                                                                                                                             }
                                                                                                                         }
                                                                                                                 }
                                                                                               }
                                                                    },

                                                                    new()
                                                                    {
                                                                        FromDate = new DateTime(2024, 03, 06),
                                                                        ToDate = new DateTime(2024, 03, 11),
                                                                        InReserve = false,
                                                                        Unit = "Stk",
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
                                                                                                                                 Dosage = new DosageSimple {Amount = 1},
                                                                                                                                 TimeOfDay = TimeSpan.FromHours(8)
                                                                                                                             },
                                                                                                                             new()
                                                                                                                             {
                                                                                                                                 Dosage = new DosageSimple {Amount = 1},
                                                                                                                                 TimeOfDay = TimeSpan.FromHours(20)
                                                                                                                             }
                                                                                                                         }
                                                                                                                 }
                                                                                               }
                                                                    }
                                                                }
                                               },

                                               new()
                                               {
                                                   Id = "7680302190581",
                                                   IdType = MedicamentIdType.Gtin,
                                                   IsAutoMedication = false,
                                                   PrescribedBy = "45745874584",
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        InReserve = false,
                                                                        Unit = "Stk",
                                                                        ApplicationInstructions = "1 application once a month, next appointment on 10.02.2024",
                                                                        RouteOfAdministration = "20035000",
                                                                        MethodOfAdministration = "11",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDuration = 1,
                                                                                                   CycleDurationUnit = TimeUnit.Month,
                                                                                                   TimedDosage = new DosageOnly {Dosage = new DosageSimple {Amount = 1}}
                                                                                               }
                                                                    }
                                                                }
                                               },

                                               new()
                                               {
                                                   Id = "7680540300100",
                                                   IdType = MedicamentIdType.Gtin,
                                                   IsAutoMedication = false,
                                                   PrescribedBy = "45745874584",
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        FromDate = new DateTime(2022, 07, 06),
                                                                        InReserve = true,
                                                                        Unit = "Stk",
                                                                        PosologyDetailObject = new FreeText
                                                                                               {
                                                                                                   Text =
                                                                                                       "If palpitations occur, take \u00bd pill and wait 30 minutes. If palpitations persist, take another \u00bd pill and wait another 30 minutes. If it does not get better, contact a doctor."
                                                                                               }
                                                                    }
                                                                }
                                               },

                                               new()
                                               {
                                                   Id = "Imagikin",
                                                   IdType = MedicamentIdType.None,
                                                   IsAutoMedication = true,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        FromDate = new DateTime(2023, 11, 10),
                                                                        InReserve = false,
                                                                        Unit = "Tropfen",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDuration = 1,
                                                                                                   CycleDurationUnit = TimeUnit.Month,
                                                                                                   TimedDosage = new DaysOfMonth
                                                                                                                 {
                                                                                                                     Days = new List<int> {10},
                                                                                                                     TimedDosage = new DosageOnly
                                                                                                                         {
                                                                                                                             Dosage = new DosageRange
                                                                                                                                 {
                                                                                                                                     MinAmount = 10,
                                                                                                                                     MaxAmount = 20
                                                                                                                                 }
                                                                                                                         }
                                                                                                                 }
                                                                                               }
                                                                    },

                                                                    new()
                                                                    {
                                                                        FromDate = new DateTime(2024, 02, 02),
                                                                        ToDate = new DateTime(2024, 02, 18),
                                                                        InReserve = true,
                                                                        Unit = "Tropfen",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDuration = 1,
                                                                                                   CycleDurationUnit = TimeUnit.Week,
                                                                                                   TimedDosage = new WeekDays
                                                                                                                 {
                                                                                                                     DaysOfWeek = new List<DayOfWeek> {DayOfWeek.Monday},
                                                                                                                     TimedDosage = new Times
                                                                                                                         {
                                                                                                                             Applications = new List<ApplicationAtTime>
                                                                                                                                 {
                                                                                                                                     new()
                                                                                                                                     {
                                                                                                                                         TimeOfDay = TimeSpan.FromHours(18),
                                                                                                                                         Dosage = new DosageSimple {Amount = 2}
                                                                                                                                     }
                                                                                                                                 }
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