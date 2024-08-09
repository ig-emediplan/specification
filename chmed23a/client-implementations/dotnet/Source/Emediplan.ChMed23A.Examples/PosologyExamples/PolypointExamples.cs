using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Models.ApplicationObjects;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Xunit.Abstractions;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace Emediplan.ChMed23A.Examples.PosologyExamples;

public class PolypointExamples : ExamplesBase
{
    #region Constructors

    public PolypointExamples(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    #region UC1: Marcoumarverordnung für 7 Tage, am Tag 8. Morgens Quick + Überprüfung --> ab Tag 8 abends will der Arzt ein neues Schema für 7 Tage verordnen mit ggf. anderer Dosierung

    [Fact]
    public void UC1_DailyGrouped_Serialization()
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
                                                   Id = "7157",
                                                   IdType = MedicamentIdType.ProductNr,
                                                   IsAutoMedication = false,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        Unit = "STK",
                                                                        FromDate = new DateTime(2021, 11, 4),
                                                                        ToDate = new DateTime(2021, 11, 5),
                                                                        PosologyDetailObject = new Daily
                                                                                               {
                                                                                                   Dosages = new[] {0.0, 0, 2, 0}
                                                                                               }
                                                                    },
                                                                    new()
                                                                    {
                                                                        Unit = "STK",
                                                                        FromDate = new DateTime(2021, 11, 6),
                                                                        ToDate = new DateTime(2021, 11, 6),
                                                                        PosologyDetailObject = new Daily
                                                                                               {
                                                                                                   Dosages = new[] {0.0, 0, 1.5, 0}
                                                                                               }
                                                                    },
                                                                    new()
                                                                    {
                                                                        Unit = "STK",
                                                                        FromDate = new DateTime(2021, 11, 7),
                                                                        ToDate = new DateTime(2021, 11, 8),
                                                                        PosologyDetailObject = new Daily
                                                                                               {
                                                                                                   Dosages = new[] {0.0, 0, 1, 0}
                                                                                               }
                                                                    },
                                                                    new()
                                                                    {
                                                                        Unit = "STK",
                                                                        FromDate = new DateTime(2021, 11, 9),
                                                                        ToDate = new DateTime(2021, 11, 10),
                                                                        PosologyDetailObject = new Daily
                                                                                               {
                                                                                                   Dosages = new[] {0, 0, 0.5, 0}
                                                                                               }
                                                                    },
                                                                    new()
                                                                    {
                                                                        Unit = "STK",
                                                                        FromDate = new DateTime(2021, 11, 11),
                                                                        ToDate = new DateTime(2021, 11, 11),
                                                                        PosologyDetailObject = new FreeText
                                                                                               {
                                                                                                   Text = "Quick + Dosierung überprüfen + neues Schema verordnen"
                                                                                               }
                                                                    },
                                                                }
                                               }
                                           }
                         };

        PrintSerializedJson(medication, medication.MedType.Value);
    }

    [Fact]
    public void UC1_DailySingle_Serialization()
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
                                                   Id = "7157",
                                                   IdType = MedicamentIdType.ProductNr,
                                                   IsAutoMedication = false,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        Unit = "STK",
                                                                        FromDate = new DateTime(2021, 11, 4),
                                                                        ToDate = new DateTime(2021, 11, 4),
                                                                        PosologyDetailObject = new Daily
                                                                                               {
                                                                                                   Dosages = new[] {0.0, 0, 2, 0}
                                                                                               }
                                                                    },
                                                                    new()
                                                                    {
                                                                        Unit = "STK",
                                                                        FromDate = new DateTime(2021, 11, 5),
                                                                        ToDate = new DateTime(2021, 11, 5),
                                                                        PosologyDetailObject = new Daily
                                                                                               {
                                                                                                   Dosages = new[] {0.0, 0, 2, 0}
                                                                                               }
                                                                    },
                                                                    new()
                                                                    {
                                                                        Unit = "STK",
                                                                        FromDate = new DateTime(2021, 11, 6),
                                                                        ToDate = new DateTime(2021, 11, 6),
                                                                        PosologyDetailObject = new Daily
                                                                                               {
                                                                                                   Dosages = new[] {0.0, 0, 1.5, 0}
                                                                                               }
                                                                    },
                                                                    new()
                                                                    {
                                                                        Unit = "STK",
                                                                        FromDate = new DateTime(2021, 11, 7),
                                                                        ToDate = new DateTime(2021, 11, 7),
                                                                        PosologyDetailObject = new Daily
                                                                                               {
                                                                                                   Dosages = new[] {0.0, 0, 1, 0}
                                                                                               }
                                                                    },
                                                                    new()
                                                                    {
                                                                        Unit = "STK",
                                                                        FromDate = new DateTime(2021, 11, 8),
                                                                        ToDate = new DateTime(2021, 11, 8),
                                                                        PosologyDetailObject = new Daily
                                                                                               {
                                                                                                   Dosages = new[] {0.0, 0, 1, 0}
                                                                                               }
                                                                    },
                                                                    new()
                                                                    {
                                                                        Unit = "STK",
                                                                        FromDate = new DateTime(2021, 11, 9),
                                                                        ToDate = new DateTime(2021, 11, 9),
                                                                        PosologyDetailObject = new Daily
                                                                                               {
                                                                                                   Dosages = new[] {0, 0, 0.5, 0}
                                                                                               }
                                                                    },
                                                                    new()
                                                                    {
                                                                        Unit = "STK",
                                                                        FromDate = new DateTime(2021, 11, 10),
                                                                        ToDate = new DateTime(2021, 11, 10),
                                                                        PosologyDetailObject = new Daily
                                                                                               {
                                                                                                   Dosages = new[] {0, 0, 0.5, 0}
                                                                                               }
                                                                    },
                                                                    new()
                                                                    {
                                                                        Unit = "STK",
                                                                        FromDate = new DateTime(2021, 11, 11),
                                                                        ToDate = new DateTime(2021, 11, 11),
                                                                        PosologyDetailObject = new FreeText
                                                                                               {
                                                                                                   Text = "Quick + Dosierung überprüfen + neues Schema verordnen"
                                                                                               }
                                                                    },
                                                                }
                                               }
                                           }
                         };

        PrintSerializedJson(medication, medication.MedType.Value);
    }

    #endregion

    #region UC2: Prednison Ausschleichschema

    [Fact]
    public void UC2_Serialization()
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
                                                   Id = "1088302",
                                                   IdType = MedicamentIdType.ProductNr,
                                                   IsAutoMedication = false,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        Unit = "STK",
                                                                        FromDate = new DateTime(2021, 11, 4),
                                                                        ToDate = new DateTime(2021, 11, 5),
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDurationUnit = TimeUnit.Day,
                                                                                                   CycleDuration = 1,
                                                                                                   TimedDosage = new Times
                                                                                                                 {
                                                                                                                     Applications = new[]
                                                                                                                         {
                                                                                                                             new ApplicationAtTime
                                                                                                                             {
                                                                                                                                 TimeOfDay = new TimeSpan(8, 0, 0),
                                                                                                                                 Dosage = new DosageSimple
                                                                                                                                     {
                                                                                                                                         Amount = 2
                                                                                                                                     }
                                                                                                                             }
                                                                                                                         }
                                                                                                                 }
                                                                                               }
                                                                    },
                                                                    new()
                                                                    {
                                                                        Unit = "STK",
                                                                        FromDate = new DateTime(2021, 11, 6),
                                                                        ToDate = new DateTime(2021, 11, 7),
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDurationUnit = TimeUnit.Day,
                                                                                                   CycleDuration = 1,
                                                                                                   TimedDosage = new Times
                                                                                                                 {
                                                                                                                     Applications = new[]
                                                                                                                         {
                                                                                                                             new ApplicationAtTime
                                                                                                                             {
                                                                                                                                 TimeOfDay = new TimeSpan(8, 0, 0),
                                                                                                                                 Dosage = new DosageSimple
                                                                                                                                     {
                                                                                                                                         Amount = 1
                                                                                                                                     }
                                                                                                                             }
                                                                                                                         }
                                                                                                                 }
                                                                                               }
                                                                    },
                                                                    new()
                                                                    {
                                                                        Unit = "STK",
                                                                        FromDate = new DateTime(2021, 11, 8),
                                                                        ToDate = new DateTime(2021, 11, 9),
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDurationUnit = TimeUnit.Day,
                                                                                                   CycleDuration = 1,
                                                                                                   TimedDosage = new Times
                                                                                                                 {
                                                                                                                     Applications = new[]
                                                                                                                         {
                                                                                                                             new ApplicationAtTime
                                                                                                                             {
                                                                                                                                 TimeOfDay = new TimeSpan(8, 0, 0),
                                                                                                                                 Dosage = new DosageSimple
                                                                                                                                     {
                                                                                                                                         Amount = 0.5
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
                                                   Id = "1088300",
                                                   IdType = MedicamentIdType.ProductNr,
                                                   IsAutoMedication = false,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        Unit = "STK",
                                                                        FromDate = new DateTime(2021, 11, 10),
                                                                        ToDate = new DateTime(2021, 11, 11),
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDurationUnit = TimeUnit.Day,
                                                                                                   CycleDuration = 1,
                                                                                                   TimedDosage = new Times
                                                                                                                 {
                                                                                                                     Applications = new[]
                                                                                                                         {
                                                                                                                             new ApplicationAtTime
                                                                                                                             {
                                                                                                                                 TimeOfDay = new TimeSpan(8, 0, 0),
                                                                                                                                 Dosage = new DosageSimple
                                                                                                                                     {
                                                                                                                                         Amount = 1
                                                                                                                                     }
                                                                                                                             }
                                                                                                                         }
                                                                                                                 }
                                                                                               }
                                                                    },
                                                                    new()
                                                                    {
                                                                        Unit = "STK",
                                                                        FromDate = new DateTime(2021, 11, 12),
                                                                        ToDate = new DateTime(2021, 11, 13),
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDurationUnit = TimeUnit.Day,
                                                                                                   CycleDuration = 1,
                                                                                                   TimedDosage = new Times
                                                                                                                 {
                                                                                                                     Applications = new[]
                                                                                                                         {
                                                                                                                             new ApplicationAtTime
                                                                                                                             {
                                                                                                                                 TimeOfDay = new TimeSpan(8, 0, 0),
                                                                                                                                 Dosage = new DosageSimple
                                                                                                                                     {
                                                                                                                                         Amount = 0.5
                                                                                                                                     }
                                                                                                                             }
                                                                                                                         }
                                                                                                                 }
                                                                                               }
                                                                    },
                                                                    new()
                                                                    {
                                                                        Unit = "STK",
                                                                        FromDate = new DateTime(2021, 11, 14),
                                                                        ToDate = new DateTime(2021, 11, 15),
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDurationUnit = TimeUnit.Day,
                                                                                                   CycleDuration = 1,
                                                                                                   TimedDosage = new Times
                                                                                                                 {
                                                                                                                     Applications = new[]
                                                                                                                         {
                                                                                                                             new ApplicationAtTime
                                                                                                                             {
                                                                                                                                 TimeOfDay = new TimeSpan(8, 0, 0),
                                                                                                                                 Dosage = new DosageSimple
                                                                                                                                     {
                                                                                                                                         Amount = 0.25
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

    #region UC3.1 : Madopar Darstellung von häufigen Abgaben

    [Fact]
    public void UC3_1_Serialization()
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
                                                   Id = "1088302",
                                                   IdType = MedicamentIdType.ProductNr,
                                                   IsAutoMedication = false,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        Unit = "STK",
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDurationUnit = TimeUnit.Day,
                                                                                                   CycleDuration = 1,
                                                                                                   TimedDosage = new Times
                                                                                                                 {
                                                                                                                     Applications = new[]
                                                                                                                         {
                                                                                                                             new ApplicationAtTime
                                                                                                                             {
                                                                                                                                 TimeOfDay = new TimeSpan(8, 0, 0),
                                                                                                                                 Dosage = new DosageSimple
                                                                                                                                     {
                                                                                                                                         Amount = 1
                                                                                                                                     }
                                                                                                                             },
                                                                                                                             new ApplicationAtTime
                                                                                                                             {
                                                                                                                                 TimeOfDay = new TimeSpan(10, 30, 0),
                                                                                                                                 Dosage = new DosageSimple
                                                                                                                                     {
                                                                                                                                         Amount = 2
                                                                                                                                     }
                                                                                                                             },
                                                                                                                             new ApplicationAtTime
                                                                                                                             {
                                                                                                                                 TimeOfDay = new TimeSpan(14, 0, 0),
                                                                                                                                 Dosage = new DosageSimple
                                                                                                                                     {
                                                                                                                                         Amount = 1
                                                                                                                                     }
                                                                                                                             },
                                                                                                                             new ApplicationAtTime
                                                                                                                             {
                                                                                                                                 TimeOfDay = new TimeSpan(16, 30, 0),
                                                                                                                                 Dosage = new DosageSimple
                                                                                                                                     {
                                                                                                                                         Amount = 3
                                                                                                                                     }
                                                                                                                             },
                                                                                                                             new ApplicationAtTime
                                                                                                                             {
                                                                                                                                 TimeOfDay = new TimeSpan(20, 0, 0),
                                                                                                                                 Dosage = new DosageSimple
                                                                                                                                     {
                                                                                                                                         Amount = 1
                                                                                                                                     }
                                                                                                                             },
                                                                                                                             new ApplicationAtTime
                                                                                                                             {
                                                                                                                                 TimeOfDay = new TimeSpan(22, 0, 0),
                                                                                                                                 Dosage = new DosageSimple
                                                                                                                                     {
                                                                                                                                         Amount = 3
                                                                                                                                     }
                                                                                                                             },
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

    #region UC3.2 : Duodopaverordnung (Austrittsverordnung für Pat. oder nachsorgende Institution)

    [Fact]
    public void UC3_2_Serialization()
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
                                                   Id = "Duodopa-Pumpe: Levodopa 20 mg/Carbidopa 4,63 mg pro 1 ml Duodopa-Gel",
                                                   IdType = MedicamentIdType.None,
                                                   IsAutoMedication = false,
                                                   Posologies = new List<Posology>()
                                               }
                                           }
                         };

        PrintSerializedJson(medication, medication.MedType.Value);
    }

    #endregion

    #endregion
}