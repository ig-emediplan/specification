using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Models.ApplicationObjects;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Examples.PosologyExamples;

public class MixedPosologyExamples : ExamplesBase
{
    #region Constructors

    public MixedPosologyExamples(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    [Fact]
    public void Every2Weeks_8H()
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
                                                                                                   CycleDuration = 2,
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
    public void EveryTwoWeeks_Evening()
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
                                                                                                   CycleDuration = 2,
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
                         };

        PrintSerializedJson(medication, medication.MedType.Value);
    }

    #endregion
}