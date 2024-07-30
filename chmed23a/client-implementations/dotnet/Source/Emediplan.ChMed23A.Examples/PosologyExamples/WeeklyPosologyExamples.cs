using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Models.ApplicationObjects;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Xunit.Abstractions;
using DayOfWeek = Emediplan.ChMed23A.Models.Enums.DayOfWeek;

namespace Emediplan.ChMed23A.Examples.PosologyExamples;

public class WeeklyPosologyExamples : ExamplesBase
{
    #region Constructors

    public WeeklyPosologyExamples(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    [Fact]
    public void Weekly_MondayWednesdayFriday()
    {
        var medication = new Medication
                         {
                             Id = "MyId",
                             MedType = MedicationType.MedicationPlan,
                             CreationDate = DateTimeOffset.Now,
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
                             Author = MedicationAuthor.Patient,
                             Medicaments = new List<Medicament>
                                           {
                                               new()
                                               {
                                                   Id = "Med1",
                                                   IdType = MedicamentIdType.None,
                                                   IsAutoMedication = false,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        Unit = "TABL",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDurationUnit = TimeUnit.Week,
                                                                                                   CycleDuration = 1,
                                                                                                   TimedDosage = new WeekDays
                                                                                                                 {
                                                                                                                     DaysOfWeek = new[]
                                                                                                                         {
                                                                                                                             DayOfWeek.Monday,
                                                                                                                             DayOfWeek.Wednesday,
                                                                                                                             DayOfWeek.Friday
                                                                                                                         },
                                                                                                                     TimedDosage = new DosageOnly
                                                                                                                         {
                                                                                                                             Dosage = new DosageSimple {Amount = 1}
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

    [Fact]
    public void Weekly_1x()
    {
        var medication = new Medication
                         {
                             Id = "MyId",
                             MedType = MedicationType.MedicationPlan,
                             CreationDate = DateTimeOffset.Now,
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
                             Author = MedicationAuthor.Patient,
                             Medicaments = new List<Medicament>
                                           {
                                               new()
                                               {
                                                   Id = "Med1",
                                                   IdType = MedicamentIdType.None,
                                                   IsAutoMedication = false,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        Unit = "TABL",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDurationUnit = TimeUnit.Week,
                                                                                                   CycleDuration = 1,
                                                                                                   TimedDosage = new DosageOnly
                                                                                                                 {
                                                                                                                     Dosage = new DosageSimple {Amount = 1}
                                                                                                                 }
                                                                                               }
                                                                    }
                                                                }
                                               }
                                           }
                         };

        PrintSerializedJson(medication, medication.MedType.Value);
    }

    [Fact]
    public void Weekly_2x()
    {
        var medication = new Medication
                         {
                             Id = "MyId",
                             MedType = MedicationType.MedicationPlan,
                             CreationDate = DateTimeOffset.Now,
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
                             Author = MedicationAuthor.Patient,
                             Medicaments = new List<Medicament>
                                           {
                                               new()
                                               {
                                                   Id = "Med1",
                                                   IdType = MedicamentIdType.None,
                                                   IsAutoMedication = false,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        Unit = "TABL",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDurationUnit = TimeUnit.Week,
                                                                                                   CycleDuration = 1,
                                                                                                   TimedDosagesPerCycle = 2,
                                                                                                   TimedDosage = new DosageOnly
                                                                                                                 {
                                                                                                                     Dosage = new DosageSimple {Amount = 1}
                                                                                                                 }
                                                                                               }
                                                                    }
                                                                }
                                               }
                                           }
                         };

        PrintSerializedJson(medication, medication.MedType.Value);
    }

    [Fact]
    public void Weekly_MondayWednesdayFriday_8H()
    {
        var medication = new Medication
                         {
                             Id = "MyId",
                             MedType = MedicationType.MedicationPlan,
                             CreationDate = DateTimeOffset.Now,
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
                             Author = MedicationAuthor.Patient,
                             Medicaments = new List<Medicament>
                                           {
                                               new()
                                               {
                                                   Id = "Med1",
                                                   IdType = MedicamentIdType.None,
                                                   IsAutoMedication = false,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        Unit = "TABL",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDurationUnit = TimeUnit.Week,
                                                                                                   CycleDuration = 1,
                                                                                                   TimedDosage = new WeekDays
                                                                                                                 {
                                                                                                                     DaysOfWeek = new[]
                                                                                                                         {
                                                                                                                             DayOfWeek.Monday,
                                                                                                                             DayOfWeek.Wednesday,
                                                                                                                             DayOfWeek.Friday
                                                                                                                         },
                                                                                                                     TimedDosage = new Times
                                                                                                                         {
                                                                                                                             Applications = new List<ApplicationAtTime>
                                                                                                                                 {
                                                                                                                                     new()
                                                                                                                                     {
                                                                                                                                         TimeOfDay = new TimeSpan(8, 0, 0),
                                                                                                                                         Dosage = new DosageSimple {Amount = 1}
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

    [Fact]
    public void Weekly_2x_10H()
    {
        var medication = new Medication
                         {
                             Id = "MyId",
                             MedType = MedicationType.MedicationPlan,
                             CreationDate = DateTimeOffset.Now,
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
                             Author = MedicationAuthor.Patient,
                             Medicaments = new List<Medicament>
                                           {
                                               new()
                                               {
                                                   Id = "Med1", IdType = MedicamentIdType.None, IsAutoMedication = false,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        Unit = "TABL",
                                                                        PosologyDetailObject =
                                                                            new Cyclic
                                                                            {
                                                                                CycleDurationUnit = TimeUnit.Week, CycleDuration = 1, TimedDosagesPerCycle = 2,
                                                                                TimedDosage = new Times
                                                                                              {
                                                                                                  Applications = new[]
                                                                                                                 {
                                                                                                                     new ApplicationAtTime
                                                                                                                     {
                                                                                                                         TimeOfDay = new TimeSpan(8, 0, 0),
                                                                                                                         Dosage = new DosageSimple {Amount = 1}
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

    [Fact]
    public void Weekly_MondayWednesdayFriday_Morning()
    {
        var medication = new Medication
                         {
                             Id = "MyId",
                             MedType = MedicationType.MedicationPlan,
                             CreationDate = DateTimeOffset.Now,
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
                             Author = MedicationAuthor.Patient,
                             Medicaments = new List<Medicament>
                                           {
                                               new()
                                               {
                                                   Id = "Med1",
                                                   IdType = MedicamentIdType.None,
                                                   IsAutoMedication = false,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        Unit = "TABL",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDurationUnit = TimeUnit.Week,
                                                                                                   CycleDuration = 1,
                                                                                                   TimedDosage = new WeekDays
                                                                                                                 {
                                                                                                                     DaysOfWeek = new[]
                                                                                                                         {
                                                                                                                             DayOfWeek.Monday,
                                                                                                                             DayOfWeek.Wednesday,
                                                                                                                             DayOfWeek.Friday
                                                                                                                         },
                                                                                                                     TimedDosage = new DaySegments
                                                                                                                         {
                                                                                                                             Applications = new List<ApplicationInSegment>
                                                                                                                                 {
                                                                                                                                     new()
                                                                                                                                     {
                                                                                                                                         Segment = DaySegment.Morning,
                                                                                                                                         Dosage = new DosageSimple {Amount = 1}
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

    [Fact]
    public void Weekly_MondayMorning_FridayEvening()
    {
        var medication = new Medication
                         {
                             Id = "MyId",
                             MedType = MedicationType.MedicationPlan,
                             CreationDate = DateTimeOffset.Now,
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
                             Author = MedicationAuthor.Patient,
                             Medicaments = new List<Medicament>
                                           {
                                               new()
                                               {
                                                   Id = "Med1",
                                                   IdType = MedicamentIdType.None,
                                                   IsAutoMedication = false,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        Unit = "TABL",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDurationUnit = TimeUnit.Week,
                                                                                                   CycleDuration = 1,
                                                                                                   TimedDosage = new WeekDays
                                                                                                                 {
                                                                                                                     DaysOfWeek = new[] {DayOfWeek.Monday},
                                                                                                                     TimedDosage = new DaySegments
                                                                                                                         {
                                                                                                                             Applications = new List<ApplicationInSegment>
                                                                                                                                 {
                                                                                                                                     new()
                                                                                                                                     {
                                                                                                                                         Segment = DaySegment.Morning,
                                                                                                                                         Dosage = new DosageSimple {Amount = 1}
                                                                                                                                     }
                                                                                                                                 }
                                                                                                                         }
                                                                                                                 }
                                                                                               }
                                                                    },
                                                                    new()
                                                                    {
                                                                        Unit = "TABL",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDurationUnit = TimeUnit.Week,
                                                                                                   CycleDuration = 1,
                                                                                                   TimedDosage = new WeekDays
                                                                                                                 {
                                                                                                                     DaysOfWeek = new[] {DayOfWeek.Friday},
                                                                                                                     TimedDosage = new DaySegments
                                                                                                                         {
                                                                                                                             Applications = new List<ApplicationInSegment>
                                                                                                                                 {
                                                                                                                                     new()
                                                                                                                                     {
                                                                                                                                         Segment = DaySegment.Evening,
                                                                                                                                         Dosage = new DosageSimple {Amount = 1}
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

    [Fact]
    public void Weekly_MondayFriday_2ml()
    {
        var medication = new Medication
                         {
                             Id = "MyId",
                             MedType = MedicationType.MedicationPlan,
                             CreationDate = DateTimeOffset.Now,
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
                             Author = MedicationAuthor.Patient,
                             Medicaments = new List<Medicament>
                                           {
                                               new()
                                               {
                                                   Id = "Med1",
                                                   IdType = MedicamentIdType.None,
                                                   IsAutoMedication = false,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        Unit = "ml",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDurationUnit = TimeUnit.Week,
                                                                                                   CycleDuration = 1,
                                                                                                   TimedDosage = new WeekDays
                                                                                                                 {
                                                                                                                     DaysOfWeek = new[] {DayOfWeek.Monday},
                                                                                                                     TimedDosage = new DosageOnly
                                                                                                                         {
                                                                                                                             Dosage = new DosageSimple {Amount = 2}
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

    [Fact]
    public void Weekly_Plan_1010100()
    {
        var medication = new Medication
                         {
                             Id = "MyId",
                             MedType = MedicationType.MedicationPlan,
                             CreationDate = DateTimeOffset.Now,
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
                             Author = MedicationAuthor.Patient,
                             Medicaments = new List<Medicament>
                                           {
                                               new()
                                               {
                                                   Id = "Med1", IdType = MedicamentIdType.None,
                                                   IsAutoMedication = false,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        Unit = "TABL",
                                                                        PosologyDetailObject =
                                                                            new Cyclic
                                                                            {
                                                                                CycleDurationUnit = TimeUnit.Week, CycleDuration = 1,
                                                                                TimedDosage = new WeekDays
                                                                                              {
                                                                                                  DaysOfWeek = new[]
                                                                                                               {
                                                                                                                   DayOfWeek.Monday, DayOfWeek.Wednesday,
                                                                                                                   DayOfWeek.Friday
                                                                                                               },
                                                                                                  TimedDosage =
                                                                                                      new DosageOnly
                                                                                                      {Dosage = new DosageSimple {Amount = 1}}
                                                                                              }
                                                                            }
                                                                    }
                                                                }
                                               }
                                           }
                         };

        PrintSerializedJson(medication, medication.MedType.Value);
    }

    [Fact]
    public void Weekly_2x_HalfPill()
    {
        var medication = new Medication
                         {
                             Id = "MyId",
                             MedType = MedicationType.MedicationPlan,
                             CreationDate = DateTimeOffset.Now,
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
                             Author = MedicationAuthor.Patient,
                             Medicaments = new List<Medicament>
                                           {
                                               new()
                                               {
                                                   Id = "Med1",
                                                   IdType = MedicamentIdType.None,
                                                   IsAutoMedication = false,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        Unit = "Tabl",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDurationUnit = TimeUnit.Week,
                                                                                                   CycleDuration = 1,
                                                                                                   TimedDosagesPerCycle = 2,
                                                                                                   TimedDosage = new DosageOnly
                                                                                                                 {
                                                                                                                     Dosage = new DosageSimple {Amount = 0.5}
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