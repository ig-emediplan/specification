using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Models.ApplicationObjects;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Models.SequenceObjects;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Examples.PosologyExamples;

public class PauseInPosologyExamples : ExamplesBase
{
    #region Constructors

    public PauseInPosologyExamples(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    [Fact]
    public void Daily_21d_7dBreak()
    {
        var medication = new Medication
                         {
                             Id = Guid.NewGuid().ToString(),
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
                                                         Value = "abcd1234"
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
                                                                                                                                     TimedDosage = new DosageOnly
                                                                                                                                         {
                                                                                                                                             Dosage = new DosageSimple {Amount = 1}
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

    [Fact]
    public void Daily_2d8H_2dBreak()
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
                                                                        PosologyDetailObject = new Sequence
                                                                                               {
                                                                                                   SequenceObjects = new List<SequenceObject>
                                                                                                                     {
                                                                                                                         new PosologySequence
                                                                                                                         {
                                                                                                                             DurationUnit = TimeUnit.Day,
                                                                                                                             Duration = 2,
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
                                                                                                                                                         Dosage = new DosageSimple
                                                                                                                                                             {
                                                                                                                                                                 Amount = 1
                                                                                                                                                             },
                                                                                                                                                         TimeOfDay =
                                                                                                                                                             new TimeSpan(8, 0, 0)
                                                                                                                                                     }
                                                                                                                                                 }
                                                                                                                                         }
                                                                                                                                 }
                                                                                                                         },
                                                                                                                         new Pause
                                                                                                                         {
                                                                                                                             DurationUnit = TimeUnit.Day,
                                                                                                                             Duration = 2
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
    public void Daily_2dMorning_2dBreak()
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
                                                                        PosologyDetailObject = new Sequence
                                                                                               {
                                                                                                   SequenceObjects = new List<SequenceObject>
                                                                                                                     {
                                                                                                                         new PosologySequence
                                                                                                                         {
                                                                                                                             DurationUnit = TimeUnit.Day,
                                                                                                                             Duration = 2,
                                                                                                                             PosologyDetailObject = new Cyclic
                                                                                                                                 {
                                                                                                                                     CycleDurationUnit = TimeUnit.Day,
                                                                                                                                     CycleDuration = 1,
                                                                                                                                     TimedDosage = new DaySegments
                                                                                                                                         {
                                                                                                                                             Applications =
                                                                                                                                                 new List<ApplicationInSegment>
                                                                                                                                                 {
                                                                                                                                                     new()
                                                                                                                                                     {
                                                                                                                                                         Segment = DaySegment
                                                                                                                                                             .Morning,
                                                                                                                                                         Dosage = new DosageSimple
                                                                                                                                                             {
                                                                                                                                                                 Amount = 1
                                                                                                                                                             }
                                                                                                                                                     }
                                                                                                                                                 }
                                                                                                                                         }
                                                                                                                                 }
                                                                                                                         },
                                                                                                                         new Pause
                                                                                                                         {
                                                                                                                             DurationUnit = TimeUnit.Day,
                                                                                                                             Duration = 2
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
    public void Week1Morning_Week2MorningAndEvening()
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
                                                                        PosologyDetailObject = new Sequence
                                                                                               {
                                                                                                   SequenceObjects = new List<SequenceObject>
                                                                                                                     {
                                                                                                                         new PosologySequence
                                                                                                                         {
                                                                                                                             DurationUnit = TimeUnit.Week,
                                                                                                                             Duration = 1,
                                                                                                                             PosologyDetailObject = new Cyclic
                                                                                                                                 {
                                                                                                                                     CycleDurationUnit = TimeUnit.Day,
                                                                                                                                     CycleDuration = 1,
                                                                                                                                     TimedDosage = new DaySegments
                                                                                                                                         {
                                                                                                                                             Applications =
                                                                                                                                                 new List<ApplicationInSegment>
                                                                                                                                                 {
                                                                                                                                                     new()
                                                                                                                                                     {
                                                                                                                                                         Segment = DaySegment
                                                                                                                                                             .Morning,
                                                                                                                                                         Dosage = new DosageSimple
                                                                                                                                                             {
                                                                                                                                                                 Amount = 1
                                                                                                                                                             }
                                                                                                                                                     }
                                                                                                                                                 }
                                                                                                                                         }
                                                                                                                                 }
                                                                                                                         },
                                                                                                                         new PosologySequence
                                                                                                                         {
                                                                                                                             DurationUnit = TimeUnit.Week,
                                                                                                                             Duration = 1,
                                                                                                                             PosologyDetailObject = new Cyclic
                                                                                                                                 {
                                                                                                                                     CycleDurationUnit = TimeUnit.Day,
                                                                                                                                     CycleDuration = 1,
                                                                                                                                     TimedDosage = new DaySegments
                                                                                                                                         {
                                                                                                                                             Applications =
                                                                                                                                                 new List<ApplicationInSegment>
                                                                                                                                                 {
                                                                                                                                                     new()
                                                                                                                                                     {
                                                                                                                                                         Segment = DaySegment
                                                                                                                                                             .Morning,
                                                                                                                                                         Dosage = new DosageSimple
                                                                                                                                                             {
                                                                                                                                                                 Amount = 1
                                                                                                                                                             }
                                                                                                                                                     },
                                                                                                                                                     new()
                                                                                                                                                     {
                                                                                                                                                         Segment = DaySegment
                                                                                                                                                             .Evening,
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