using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Models.ApplicationObjects;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Xunit.Abstractions;
using Single = Emediplan.ChMed23A.Models.PosologyDetailObjects.Single;

// ReSharper disable StringLiteralTypo

namespace Emediplan.ChMed23A.Examples.PosologyExamples;

public class SinglePosologyExamples : ExamplesBase
{
    #region Constructors

    public SinglePosologyExamples(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    [Fact]
    public void Single()
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
                                                                        PosologyDetailObject = new Single
                                                                                               {
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
    public void Single_8H()
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
                                                                        FromDate = new DateTime(2021, 12, 01),
                                                                        PosologyDetailObject = new Single
                                                                                               {
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
    public void Single_Morning()
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
                                                                        PosologyDetailObject = new Single
                                                                                               {
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
                         };

        PrintSerializedJson(medication, medication.MedType.Value);
    }

    [Fact]
    public void Single_AtDate()
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
                                                                        FromDate = new DateTime(2021, 3, 1),
                                                                        PosologyDetailObject = new Single
                                                                                               {
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
    public void Single_2hBeforeDentist()
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
                                                                        ApplicationInstructions = "2h vor Zahneingriff",
                                                                        PosologyDetailObject = new Single
                                                                                               {
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
    public void Single_8H_1hBeforeExamination()
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
                                                                        ApplicationInstructions = "1h vor Untersuchung",
                                                                        PosologyDetailObject = new Single
                                                                                               {
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
    public void Single_MorningAtDate()
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
                                                                        FromDate = new DateTime(2020, 2, 27),
                                                                        PosologyDetailObject = new Single
                                                                                               {
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
                         };

        PrintSerializedJson(medication, medication.MedType.Value);
    }

    [Fact]
    public void Single_8HAtDate()
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
                                                                        FromDate = new DateTime(2020, 2, 27),
                                                                        PosologyDetailObject = new Single
                                                                                               {
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

    #endregion
}