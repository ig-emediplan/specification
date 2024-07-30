using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Models.ApplicationObjects;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Xunit.Abstractions;

// ReSharper disable StringLiteralTypo

namespace Emediplan.ChMed23A.Examples.MedicationExamples;

public class MedicationExample1 : ExamplesBase
{
    #region Constructors

    public MedicationExample1(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Example

    [Fact]
    public void Example1()
    {
        var medication = new Medication
                         {
                             MedType = MedicationType.MedicationPlan,
                             CreationDate = new DateTimeOffset(2024, 01, 09, 9, 14, 36, TimeSpan.FromHours(+1)),
                             Author = MedicationAuthor.HealthcarePerson,

                             Patient = new Patient
                                       {
                                           FirstName = "Alice",
                                           LastName = "Louloui",
                                           BirthDate = new DateTime(1945, 1, 19),
                                           Gender = Gender.Female,
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
                                                        "011 111 11 11",
                                                        "079 999 99 99"
                                                    },
                                           Emails = new List<string> {"alicelouloui1945@fake-e-mail.ch"},
                                           MedicalData = new MedicalData
                                                         {
                                                             WeightKg = 70,
                                                             HeightCm = 165,
                                                             RiskCategories = new List<RiskCategory>
                                                                              {
                                                                                  new()
                                                                                  {
                                                                                      Id = 1,
                                                                                      RiskIds = new List<int> {577}
                                                                                  },
                                                                                  new() {Id = 3},
                                                                                  new() {Id = 4},
                                                                                  new() {Id = 5},
                                                                                  new()
                                                                                  {
                                                                                      Id = 6,
                                                                                      RiskIds = new List<int> {503}
                                                                                  },
                                                                                  new()
                                                                                  {
                                                                                      Id = 7,
                                                                                      RiskIds = new List<int> {780}
                                                                                  }
                                                                              }
                                                         }
                                       },

                             HealthcarePerson = new HealthcarePerson
                                                {
                                                    Gln = "123123123123",
                                                    FirstName = "Hans",
                                                    LastName = "Muster"
                                                },

                             HealthcareOrganization = new HealthcareOrganization
                                                      {
                                                          Name = "Medical practice Dr. med. Hans Muster",
                                                          Street = "Bernstrasse 1",
                                                          City = "Bern",
                                                          Zip = "3000",
                                                          Country = "CH"
                                                      },

                             Medicaments = new List<Medicament>
                                           {
                                               new()
                                               {
                                                   Id = "1246564",
                                                   IdType = MedicamentIdType.ProductNr,
                                                   TakingReason = "Pancreas",
                                                   IsAutoMedication = false,
                                                   PrescribedBy = "123123123123",
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        InReserve = false,
                                                                        RelativeToMeal = RelativeToMeal.After,
                                                                        Unit = "Stk",
                                                                        PosologyDetailObject = new Daily
                                                                                               {
                                                                                                   Dosages = new double[] {1, 0, 1, 0}
                                                                                               }
                                                                    }
                                                                }
                                               },

                                               new()
                                               {
                                                   Id = "5292958",
                                                   IdType = MedicamentIdType.Pharmacode,
                                                   TakingReason = "Cholesterol-lowering drug",
                                                   IsAutoMedication = false,
                                                   PrescribedBy = "123123123123",
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        FromDate = new DateTime(2012, 05, 25),
                                                                        InReserve = false,
                                                                        Unit = "Stk",
                                                                        RouteOfAdministration = "20053000",
                                                                        MethodOfAdministration = "19",
                                                                        PosologyDetailObject = new Daily
                                                                                               {
                                                                                                   Dosages = new double[] {0, 0, 1, 0}
                                                                                               }
                                                                    }
                                                                }
                                               },

                                               new()
                                               {
                                                   Id = "7680334810013",
                                                   TakingReason = "Vitamins/minerals",
                                                   IdType = MedicamentIdType.Gtin,
                                                   IsAutoMedication = false,
                                                   PrescribedBy = "123123123123",
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        FromDate = new DateTime(2023, 09, 20),
                                                                        ToDate = new DateTime(2024, 04, 30),
                                                                        InReserve = false,
                                                                        Unit = "ml",
                                                                        ApplicationInstructions =
                                                                            "Dose using the dosing pipette, place on a spoon and then take undiluted. The pipette must not come into contact with the mouth, saliva or food.",
                                                                        RouteOfAdministration = "20053000",
                                                                        MethodOfAdministration = "20",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDuration = 1,
                                                                                                   CycleDurationUnit = TimeUnit.Week,
                                                                                                   TimedDosage = new DosageOnly
                                                                                                                 {
                                                                                                                     Dosage = new DosageSimple {Amount = 1.4}
                                                                                                                 }
                                                                                               }
                                                                    }
                                                                }
                                               },

                                               new()
                                               {
                                                   Id = "1512856",
                                                   IdType = MedicamentIdType.ProductNr,
                                                   TakingReason = "Vitamins/minerals",
                                                   IsAutoMedication = true,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
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
                                                                                                                                 TimeOfDay = new TimeSpan(9, 0, 0),
                                                                                                                                 Dosage = new DosageSimple {Amount = 1}
                                                                                                                             }
                                                                                                                         }
                                                                                                                 }
                                                                                               }
                                                                    }
                                                                }
                                               },

                                               new()
                                               {
                                                   Id = "7680473440263",
                                                   IdType = MedicamentIdType.Gtin,
                                                   TakingReason = "Rheumatism",
                                                   IsAutoMedication = false,
                                                   PrescribedBy = "123123123123",
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        InReserve = false,
                                                                        Unit = "Appl",
                                                                        RouteOfAdministration = "20003000",
                                                                        MethodOfAdministration = "5",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDuration = 1,
                                                                                                   CycleDurationUnit = TimeUnit.Day,
                                                                                                   TimedDosagesPerCycle = 3,
                                                                                                   TimedDosage = new DosageOnly
                                                                                                                 {
                                                                                                                     Dosage = new DosageSimple
                                                                                                                         {
                                                                                                                             Amount = 1
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